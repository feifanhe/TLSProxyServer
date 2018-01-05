namespace SVTServerMonitor
{
    partial class MainForm
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MonitorNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.NotifyIconContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.DateColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TimeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClientAddressColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TargetAddressColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressTextBox = new System.Windows.Forms.TextBox();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ConnectBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.LogColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.InterfaceLabel = new System.Windows.Forms.Label();
            this.ShowMonitorStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.NotifyIconContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MonitorNotifyIcon
            // 
            this.MonitorNotifyIcon.ContextMenuStrip = this.NotifyIconContextMenuStrip;
            this.MonitorNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("MonitorNotifyIcon.Icon")));
            this.MonitorNotifyIcon.Text = "SVT Server Monitor";
            this.MonitorNotifyIcon.Visible = true;
            this.MonitorNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MonitorNotifyIcon_MouseDoubleClick);
            // 
            // NotifyIconContextMenuStrip
            // 
            this.NotifyIconContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowMonitorStripMenuItem,
            this.ToolStripSeparator1,
            this.ExitToolStripMenuItem});
            this.NotifyIconContextMenuStrip.Name = "NotifyIconContextMenuStrip";
            this.NotifyIconContextMenuStrip.Size = new System.Drawing.Size(155, 54);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DateColumnHeader,
            this.TimeColumnHeader,
            this.ClientAddressColumnHeader,
            this.TargetAddressColumnHeader});
            this.listView1.Location = new System.Drawing.Point(12, 40);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(600, 360);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // DateColumnHeader
            // 
            this.DateColumnHeader.Text = "Date";
            this.DateColumnHeader.Width = 100;
            // 
            // TimeColumnHeader
            // 
            this.TimeColumnHeader.Text = "Time";
            this.TimeColumnHeader.Width = 100;
            // 
            // ClientAddressColumnHeader
            // 
            this.ClientAddressColumnHeader.Text = "Client Address";
            this.ClientAddressColumnHeader.Width = 180;
            // 
            // TargetAddressColumnHeader
            // 
            this.TargetAddressColumnHeader.Text = "Target Address";
            this.TargetAddressColumnHeader.Width = 180;
            // 
            // AddressTextBox
            // 
            this.AddressTextBox.Location = new System.Drawing.Point(107, 12);
            this.AddressTextBox.Name = "AddressTextBox";
            this.AddressTextBox.Size = new System.Drawing.Size(180, 22);
            this.AddressTextBox.TabIndex = 2;
            this.AddressTextBox.Text = "127.0.0.1";
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(293, 12);
            this.PortTextBox.MaxLength = 5;
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(60, 22);
            this.PortTextBox.TabIndex = 3;
            this.PortTextBox.Text = "8801";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(359, 11);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 4;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ConnectBackgroundWorker
            // 
            this.ConnectBackgroundWorker.WorkerSupportsCancellation = true;
            this.ConnectBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ConnectBackgroundWorker_DoWork);
            this.ConnectBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ConnectBackgroundWorker_RunWorkerCompleted);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Enabled = false;
            this.DisconnectButton.Location = new System.Drawing.Point(440, 11);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(75, 23);
            this.DisconnectButton.TabIndex = 5;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LogColumnHeader});
            this.listView2.Location = new System.Drawing.Point(12, 406);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(600, 143);
            this.listView2.TabIndex = 6;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // LogColumnHeader
            // 
            this.LogColumnHeader.Text = "Log";
            this.LogColumnHeader.Width = 600;
            // 
            // InterfaceLabel
            // 
            this.InterfaceLabel.AutoSize = true;
            this.InterfaceLabel.Location = new System.Drawing.Point(12, 15);
            this.InterfaceLabel.Name = "InterfaceLabel";
            this.InterfaceLabel.Size = new System.Drawing.Size(89, 12);
            this.InterfaceLabel.TabIndex = 7;
            this.InterfaceLabel.Text = "Interface Address:";
            // 
            // ShowMonitorStripMenuItem
            // 
            this.ShowMonitorStripMenuItem.Name = "ShowMonitorStripMenuItem";
            this.ShowMonitorStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.ShowMonitorStripMenuItem.Text = "Show Monitor";
            this.ShowMonitorStripMenuItem.Click += new System.EventHandler(this.ShowMonitorStripMenuItem_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(151, 6);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 561);
            this.Controls.Add(this.InterfaceLabel);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.AddressTextBox);
            this.Controls.Add(this.listView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "SVT Server Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.NotifyIconContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon MonitorNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip NotifyIconContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox AddressTextBox;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.ColumnHeader DateColumnHeader;
        private System.Windows.Forms.ColumnHeader TimeColumnHeader;
        private System.Windows.Forms.ColumnHeader ClientAddressColumnHeader;
        private System.Windows.Forms.ColumnHeader TargetAddressColumnHeader;
        private System.ComponentModel.BackgroundWorker ConnectBackgroundWorker;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader LogColumnHeader;
        private System.Windows.Forms.Label InterfaceLabel;
        private System.Windows.Forms.ToolStripMenuItem ShowMonitorStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
    }
}

