using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace SVTServerMonitor
{
    public partial class MainForm : Form
    {
        delegate void MonotorMessageHandler(string message);
        MonotorMessageHandler SVTLog;
        MonotorMessageHandler MonitorLog;

        public MainForm()
        {
            InitializeComponent();
            this.SVTLog += new MonotorMessageHandler(WriteSVTLog);
            this.MonitorLog += new MonotorMessageHandler(WriteMonitorLog);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.MonitorNotifyIcon.Visible = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.MonitorNotifyIcon.Visible = true;
            this.MonitorNotifyIcon.BalloonTipTitle = "SVT Server Monitor";
            this.MonitorNotifyIcon.BalloonTipText = "SVT Server Monitor is still running!";
            this.MonitorNotifyIcon.ShowBalloonTip(1000);
        }

        private void MonitorNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void ShowMonitorStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MonitorNotifyIcon.Visible = false;
            System.Environment.Exit(1);
        }

        TcpClient client;
        NetworkStream stream;

        private void Connect(string server, int port)
        {
            try
            {
                this.MonitorLog("Creating connection to monitor interface at: " + server + ":" + port.ToString());
                client = new TcpClient(server, port);
                stream = client.GetStream();

                if (client.Connected)
                {
                    this.MonitorLog("Connected to monitor interface.");
                    this.ConnectButton.Enabled = false;
                    this.DisconnectButton.Enabled = true;
                }

                // Buffer to store the response bytes.
                while (client.Connected)
                {
                    if (this.ConnectBackgroundWorker.CancellationPending)
                    {
                        this.MonitorLog("Connection thread cancelled.");
                        Disconnect();
                        break;
                    }
                    if (client.Available > 0)
                    {
                        Byte[] data = new Byte[256];
                        string responseData = string.Empty;
                        Int32 bytes = stream.Read(data, 0, data.Length);
                        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        //this.MonitorLog("Received data: " + responseData);
                        this.SVTLog(responseData);
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                this.MonitorLog(string.Format("ArgumentNullException: {0}", e));
            }
            catch (SocketException e)
            {
                this.MonitorLog(string.Format("SocketException: {0}", e));
            }
        }

        private void Disconnect()
        {
            this.MonitorLog("Closing connection...");
            stream.Close();
            client.Close();
            this.MonitorLog("Connection closed.");
            this.ConnectButton.Enabled = true;
            this.DisconnectButton.Enabled = false;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (!this.ConnectBackgroundWorker.IsBusy)
            {
                this.MonitorLog("Preparing to establish connection...");
                this.ConnectBackgroundWorker.RunWorkerAsync();
            }
            else
            {
                this.MonitorLog("Connection is already existing!");
            }
        }

        private void ConnectBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Connect(this.AddressTextBox.Text, int.Parse(this.PortTextBox.Text) );
        }

        private void ConnectBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.MonitorLog("Connection thread stopped.");
        }

        private void WriteSVTLog(string message)
        {
            string[] tokens = message.Split(' ');
            ListViewItem item = this.listView1.Items.Add(DateTime.Now.ToShortDateString());
            item.SubItems.Add(DateTime.Now.ToShortTimeString());
            for ( int i = 0; i < tokens.Length; i++ )
            {
                item.SubItems.Add(tokens[i]);
            }
            item.EnsureVisible();
        }

        private void WriteMonitorLog(string message)
        {
            ListViewItem item = this.listView2.Items.Add(message);
            item.EnsureVisible();
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            this.MonitorLog("Preparing to cancel connection...");
            this.ConnectBackgroundWorker.CancelAsync();
        }

    }
}
