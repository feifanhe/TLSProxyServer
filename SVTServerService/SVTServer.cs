using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SVTServerService
{
    class SVTServer
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void runInterfaceHandler();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void commandHandler(int cmd, [MarshalAs(UnmanagedType.LPStr)] string param);

        private IntPtr pSVTServerDll;
        private runInterfaceHandler runInterface;
        private commandHandler command;
        
        private bool isGood = false;
        public bool IsGood
        {
            get
            {
                return this.isGood;
            }
        }

        public void StartServer(string ServerAddress)
        {
            Log.Write("String SVT Server");
            this.command(0, ServerAddress);
        }

        public void StopServer()
        {
            Log.Write("Stopping SVT Server");
            this.command(1, string.Empty);
        }

        public void SetKeys(string Keys)
        {
            Log.Write("Set SVT Server certificate");
            this.command(2, Keys);
        }

        public void CreateMonitorInterface(string ListenerAddress)
        {
            Log.Write("Create SVT Server Monitor listener");
            this.command(3, ListenerAddress);
        }

        public void RunInterface()
        {
            Log.Write("Starting SVT Server interface");
            this.runInterface();
        }

        public void StopInterface()
        {
            Log.Write("Stopping SVT Server interface");
            this.command(-1, string.Empty);
        }

        public void LoadSVTServerDll()
        {
            this.pSVTServerDll = LoadLibrary(@"SVTServer.dll");
            if (pSVTServerDll == IntPtr.Zero)
            {
                Log.Write("Fail to load library: {0}", GetLastError());
                return;
            }

            IntPtr pRunInterface = GetProcAddress(pSVTServerDll, "runInterface");
            if (pRunInterface == IntPtr.Zero)
            {
                Log.Write("Fail to find DLL function: runInterface: {0}", GetLastError());
                return;
            }
            this.runInterface = (runInterfaceHandler)Marshal.GetDelegateForFunctionPointer(
                pRunInterface,
                typeof(runInterfaceHandler));

            IntPtr pCommand = GetProcAddress(pSVTServerDll, "command");
            if (pCommand == IntPtr.Zero)
            {
                Log.Write("Fail to find DLL function: command: {0}", GetLastError());
                return;
            }
            this.command = (commandHandler)Marshal.GetDelegateForFunctionPointer(
                pCommand,
                typeof(commandHandler));

            Log.Write("SVT Server DLL loaded successfully");
            this.isGood = true;
        }

        public void FreeSVTServerDLL()
        {
            Log.Write("Free SVT Server DLL");
            FreeLibrary(pSVTServerDll);
        }

    }
}
