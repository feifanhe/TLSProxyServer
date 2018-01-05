package main

import (
	"C"
	"crypto/tls"
	"crypto/x509"
	"fmt"
	"github.com/getlantern/netx"
	"github.com/oxtoacart/bpool"
	"io/ioutil"
	"log"
	"net"
	"os"
	"path/filepath"
	"strings"
	"time"
)

var (
	buffers         = bpool.NewBytePool(25000, 32768)
	serverStopped   = true
	tlsConfig       tls.Config
	serverListener  *net.Listener = nil
	execName        string
	execPath        string
	logFileName     string
	logFile         *os.File
	monitorAddr     string
	monitorListener *net.Listener = nil
	monitorConn     *net.Conn     = nil
	cmdChannel      = make(chan int)
	rtnChannel      = make(chan int)
	paramChannel    = make(chan string, 2)
)

func InitLogFile() {
	execName, _ = os.Executable()
	execPath = filepath.Dir(execName)
	logFileName = execPath + "\\SVTServer.log"
	f, err := os.OpenFile(logFileName, os.O_RDWR|os.O_CREATE|os.O_APPEND, 0666)
	if err != nil {
		log.Printf("Fail to open file: %v", err)
	}
	logFile = f
	log.SetOutput(logFile)
}

func CloseLogFile() {
	logFile.Close()
}

//export RunInterface
func RunInterface() {
	InitLogFile()
	log.Printf("Srarting SVTServer Interface...")
	go func() {
		log.Printf("SVTServer Interface started.")
		for {
			log.Printf("Waiting for command.")
			cmd := <-cmdChannel
			param := <-paramChannel
			log.Printf("Command: %v, Parameter: %v", cmd, param)

			if cmd == -1 {
				// Stop interface
				break
			}
			switch cmd {
			case 0:
				StartServer(param)
				rtnChannel <- 0
			case 1:
				StopServer()
				rtnChannel <- 0
			case 2:
				SetKeys(param)
				rtnChannel <- 0
			case 3:
				CreateMonitorInterface(param)
				rtnChannel <- 0
			default:
				log.Printf("Unknown command")
				rtnChannel <- 0
			}
		}

		log.Printf("SVTServer Interface stopped.")
		CloseLogFile()
		rtnChannel <- 0
	}()
}

//export Command
func Command(cmd C.int, param *C.char) {
	p := C.GoString(param)
	c := int(cmd)
	log.Printf("Cmd: %v, Param: %v", c, p)
	paramChannel <- p
	cmdChannel <- c
	// wait for command executed
	<-rtnChannel
}

func SetKeys(filenames string) {
	log.Printf("Set keys from files: %v", filenames)
	params := strings.Split(filenames, ";")
	if len(params) != 3 {
		return
	}

	certFile := params[0]
	if filepath.Dir(certFile) == "." {
		certFile = execPath + "\\" + certFile
	}
	pkFile := params[1]
	if filepath.Dir(pkFile) == "." {
		pkFile = execPath + "\\" + pkFile
	}
	caFile := params[2]
	if filepath.Dir(caFile) == "." {
		caFile = execPath + "\\" + caFile
	}
	log.Printf("Set keys: %v, %v, %v", certFile, pkFile, caFile)

	// load key pair
	cert, err := tls.LoadX509KeyPair(certFile, pkFile)
	if err != nil {
		log.Printf("Loading key pair error: %v", err)
		return
	}

	// read CA
	caCert, err := ioutil.ReadFile(caFile)
	if err != nil {
		log.Printf("Loading CA error: %v", err)
		return
	}

	// create CA cert pool
	caCertPool := x509.NewCertPool()
	caCertPool.AppendCertsFromPEM(caCert)

	tlsConfig = tls.Config{
		Certificates:       []tls.Certificate{cert},
		ClientAuth:         tls.RequireAndVerifyClientCert,
		ClientCAs:          caCertPool,
		InsecureSkipVerify: true,
	}
}

func StartServer(localAddr string) {
	// check server status
	if !serverStopped {
		log.Printf("Server already starts")
		return
	}
	log.Printf("Start SVTServer")

	// create listener
	listener, err := tls.Listen("tcp", localAddr, &tlsConfig)
	if err != nil {
		log.Printf("Unable to listen: %v", err)
		return
	}
	serverListener = &listener
	serverStopped = false
	log.Printf("Listening for incoming connections at: %v", listener.Addr())

	go func() {
		for !serverStopped {
			// wait for connections
			in, err := listener.Accept()
			if err != nil {
				log.Printf("Listener failed to accept: %v", err)
				continue
			}

			// transport data
			go func() {
				defer in.Close()

				// read target address from connection
				remote, err := GetStringFromConn(&in)
				if err != nil {
					log.Printf("Fail to read remote address")
					return
				}
				log.Printf("Read remote address: %v", remote)

				// create connection to target server
				out, err := net.DialTimeout("tcp", remote, 30*time.Second)
				if err != nil {
					log.Printf("Unable to dial forwarding address: %v", err)
					return
				}
				defer out.Close()

				// copy data
				CopyData(&in, &out)
			}()
		}
	}()
}

func GetStringFromConn(conn *net.Conn) (string, error) {
	buffer := buffers.Get()
	defer buffers.Put(buffer)
	n, err := (*conn).Read(buffer)
	if err != nil {
		return "", err
	}
	return string(buffer[:n]), nil
}

func CopyData(pIn, pOut *net.Conn) {
	in := *pIn
	out := *pOut
	log.Printf("Copying from %v to %v", in.RemoteAddr(), out.RemoteAddr())
	SendMessageToMonitor(fmt.Sprintf("%v %v ", in.RemoteAddr(), out.RemoteAddr()))
	bufIn := buffers.Get()
	bufOut := buffers.Get()
	defer buffers.Put(bufOut)
	defer buffers.Put(bufIn)
	netx.BidiCopy(out, in, bufOut, bufIn)
}

func StopServer() {
	serverStopped = true
	CloseMonitorInterface()
	if serverListener != nil {
		(*serverListener).Close()
	}
}

func CreateMonitorInterface(addr string) {
	log.Printf("Create monitor interface listener at: %v", addr)
	monitorAddr = addr

	listener, err := net.Listen("tcp", monitorAddr)
	if err != nil {
		log.Printf("Failed to create monitor interface listener: %v", err)
		return
	}
	monitorListener = &listener
	log.Printf("Monitor interface is listening for incoming connection at: %v", listener.Addr())

	WaitMonitorConnection()
}

func WaitMonitorConnection() {
	go func() {
		for {
			log.Printf("Monitor interface is waiting for incoming connection")
			in, err := (*monitorListener).Accept()
			log.Printf("Monitor interface accept connection: %v", in.RemoteAddr())
			if err != nil {
				log.Printf("Monitor interface listener failed to accept: %v", err)
				continue
			}
			CloseMonitorConn()
			monitorConn = &in
			log.Printf("Monitor interface connection established from: %v", in.RemoteAddr())
		}
	}()
}

func CloseMonitorInterface() {
	CloseMonitorConn()
	if monitorListener != nil {
		(*monitorListener).Close()
	}
}

func CloseMonitorConn() {
	if monitorConn != nil {
		(*monitorConn).Close()
	}
}

func SendMessageToMonitor(message string) {
	if monitorConn != nil {
		(*monitorConn).Write([]byte(message))
	}
}

func main() {
}
