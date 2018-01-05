using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace SVTServerService
{
    public partial class SVTServerService : ServiceBase
    {
        private SVTServer SVTServer;
        private IniReader IniReader;

        public SVTServerService()
        {
            InitializeComponent();
            this.SVTServer = new SVTServer();
            this.IniReader = new IniReader(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "SVTServer.ini");
        }

        protected override void OnStart(string[] args)
        {
            Log.Write("Load SVT Server DLL");
            SVTServer.LoadSVTServerDll();
            if (SVTServer.IsGood)
            {
                // start SVT server
                Log.Write("Start SVT Server");
                SVTServer.RunInterface();
                Log.Write("SVT Server interface started");

                string KeysParam = this.GetKeysParam();
                Log.Write("SVT Server set keys: {0}", KeysParam);
                SVTServer.SetKeys(KeysParam);

                string ServerAddress = this.GetServerAddress();
                SVTServer.StartServer(ServerAddress);
                Log.Write("SVT Server started at {0}", ServerAddress);

                string MonitorAddress = this.GetMonitorAddress();
                SVTServer.CreateMonitorInterface(MonitorAddress);
                Log.Write("SVT Server minitor started at {0}", MonitorAddress);
            }
        }

        protected override void OnStop()
        {
            if (SVTServer.IsGood)
            {
                SVTServer.StopServer();
                SVTServer.StopInterface();
                SVTServer.FreeSVTServerDLL();
            }
        }

        private string GetKeysParam()
        {
            string certFile = this.IniReader.GetValue("SVTServer", "certFile");
            string pkFile = this.IniReader.GetValue("SVTServer", "pkFile");
            string caFile = this.IniReader.GetValue("SVTServer", "caFile");
            return string.Format("{0};{1};{2}", certFile, pkFile, caFile );
        }

        private string GetServerAddress()
        {
            return this.IniReader.GetValue("SVTServer", "serverAddress");
        }

        private string GetMonitorAddress()
        {
            return this.IniReader.GetValue("SVTServer", "monitorAddress");
        }
    }
}
