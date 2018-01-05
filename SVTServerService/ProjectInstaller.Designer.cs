namespace SVTServerService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SVTServerServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SVTServerServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // SVTServerServiceProcessInstaller
            // 
            this.SVTServerServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.SVTServerServiceProcessInstaller.Password = null;
            this.SVTServerServiceProcessInstaller.Username = null;
            // 
            // SVTServerServiceInstaller
            // 
            this.SVTServerServiceInstaller.Description = "SVTServer";
            this.SVTServerServiceInstaller.DisplayName = "SVTServer";
            this.SVTServerServiceInstaller.ServiceName = "SVTServer";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SVTServerServiceProcessInstaller,
            this.SVTServerServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SVTServerServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SVTServerServiceInstaller;
    }
}