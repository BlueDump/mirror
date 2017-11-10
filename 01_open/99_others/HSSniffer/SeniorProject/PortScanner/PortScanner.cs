using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Diagnostics;
using System.Text;

namespace PortScannerNS
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>


	public class PortScanner : System.Windows.Forms.Form
	{		
		private FormWindowState ws=FormWindowState.Normal;
		public string CurrentFileName;
		public string newFileName;
		public Configuration configuration;
		private int loopCount=0;
		private bool isStoped=true;
		private int count=0;
		private System.ServiceProcess.ServiceController serviceController = 
			new System.ServiceProcess.ServiceController("SMTPSVC");
		public Logging mailLogger  = new Logging("Mail_Log","PortScannerLog");
		public Logging eventLogger = new Logging("Event_Log","PortScannerLog");

		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem mi_Quit;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem mi_LogFile;
		private System.Windows.Forms.MenuItem mi_Log;
		private System.Windows.Forms.MenuItem mi_LogRefresh;
		private System.Threading.Timer timer1;
		private System.Windows.Forms.MenuItem mi_Restore;
		private System.Windows.Forms.MenuItem mi_PortScannerSettings;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.MenuItem mi_LogDeleteLog;
		private System.Windows.Forms.MenuItem mi_MailService;
		private System.Windows.Forms.MenuItem mi_MailServiceStart;
		private System.Windows.Forms.MenuItem mi_MailServiceStop;
		private System.ServiceProcess.ServiceController serviceController1;
		private System.Windows.Forms.MenuItem mi_NewFile;
		private System.Windows.Forms.MenuItem mi_LoadFile;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem mi_PortScannerExit;
		private System.Windows.Forms.ImageList imageList2;
		private System.Windows.Forms.MenuItem mi_PortScannerStart;
		private System.Windows.Forms.MenuItem mi_PortScannerStop;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ListView listViewPorts;
		private System.Windows.Forms.ListView listViewMails;
		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.Splitter splitter1;
		private System.Diagnostics.EventLog eventLog1;
		private System.Windows.Forms.ColumnHeader Type;
		private System.Windows.Forms.ColumnHeader Index;
		private System.Windows.Forms.ColumnHeader Date;
		private System.Windows.Forms.ColumnHeader Time;
		private System.Windows.Forms.ColumnHeader Message;
		private System.Windows.Forms.ColumnHeader mType;
		private System.Windows.Forms.ColumnHeader mIndex;
		private System.Windows.Forms.ColumnHeader mDate;
		private System.Windows.Forms.ColumnHeader mTime;
		private System.Windows.Forms.ColumnHeader mMessage;
		private System.Windows.Forms.ColumnHeader ScannedHost;
		private System.Windows.Forms.ColumnHeader PScannedHost;
		private System.Windows.Forms.ColumnHeader HostPort;

		private System.ComponentModel.IContainer components;

		public PortScanner()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PortScanner));
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.mi_Restore = new System.Windows.Forms.MenuItem();
			this.mi_Quit = new System.Windows.Forms.MenuItem();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.mi_LogFile = new System.Windows.Forms.MenuItem();
			this.mi_NewFile = new System.Windows.Forms.MenuItem();
			this.mi_LoadFile = new System.Windows.Forms.MenuItem();
			this.mi_PortScannerExit = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.mi_PortScannerStart = new System.Windows.Forms.MenuItem();
			this.mi_PortScannerStop = new System.Windows.Forms.MenuItem();
			this.mi_Log = new System.Windows.Forms.MenuItem();
			this.mi_LogRefresh = new System.Windows.Forms.MenuItem();
			this.mi_LogDeleteLog = new System.Windows.Forms.MenuItem();
			this.mi_PortScannerSettings = new System.Windows.Forms.MenuItem();
			this.mi_MailService = new System.Windows.Forms.MenuItem();
			this.mi_MailServiceStart = new System.Windows.Forms.MenuItem();
			this.mi_MailServiceStop = new System.Windows.Forms.MenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.serviceController1 = new System.ServiceProcess.ServiceController();
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			this.panelMain = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panelBottom = new System.Windows.Forms.Panel();
			this.listViewPorts = new System.Windows.Forms.ListView();
			this.Type = new System.Windows.Forms.ColumnHeader();
			this.Index = new System.Windows.Forms.ColumnHeader();
			this.Date = new System.Windows.Forms.ColumnHeader();
			this.Time = new System.Windows.Forms.ColumnHeader();
			this.PScannedHost = new System.Windows.Forms.ColumnHeader();
			this.HostPort = new System.Windows.Forms.ColumnHeader();
			this.Message = new System.Windows.Forms.ColumnHeader();
			this.panelTop = new System.Windows.Forms.Panel();
			this.listViewMails = new System.Windows.Forms.ListView();
			this.mType = new System.Windows.Forms.ColumnHeader();
			this.mIndex = new System.Windows.Forms.ColumnHeader();
			this.mDate = new System.Windows.Forms.ColumnHeader();
			this.mTime = new System.Windows.Forms.ColumnHeader();
			this.ScannedHost = new System.Windows.Forms.ColumnHeader();
			this.mMessage = new System.Windows.Forms.ColumnHeader();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.eventLog1 = new System.Diagnostics.EventLog();
			this.panelMain.SuspendLayout();
			this.panelBottom.SuspendLayout();
			this.panelTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
			this.SuspendLayout();
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenu = this.contextMenu1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "Port Scanner";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mi_Restore,
																						 this.mi_Quit});
			// 
			// mi_Restore
			// 
			this.mi_Restore.Index = 0;
			this.mi_Restore.Text = "Restore";
			this.mi_Restore.Click += new System.EventHandler(this.mi_Restore_Click);
			// 
			// mi_Quit
			// 
			this.mi_Quit.Index = 1;
			this.mi_Quit.Text = "Quit";
			this.mi_Quit.Click += new System.EventHandler(this.mi_Quit_Click);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mi_LogFile,
																					  this.menuItem5,
																					  this.mi_Log,
																					  this.mi_PortScannerSettings,
																					  this.mi_MailService});
			// 
			// mi_LogFile
			// 
			this.mi_LogFile.Index = 0;
			this.mi_LogFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.mi_NewFile,
																					   this.mi_LoadFile,
																					   this.mi_PortScannerExit});
			this.mi_LogFile.Text = "File";
			// 
			// mi_NewFile
			// 
			this.mi_NewFile.Index = 0;
			this.mi_NewFile.Text = "New";
			this.mi_NewFile.Click += new System.EventHandler(this.mi_NewFile_Click);
			// 
			// mi_LoadFile
			// 
			this.mi_LoadFile.Index = 1;
			this.mi_LoadFile.Text = "Load";
			this.mi_LoadFile.Click += new System.EventHandler(this.mi_LoadFile_Click);
			// 
			// mi_PortScannerExit
			// 
			this.mi_PortScannerExit.Index = 2;
			this.mi_PortScannerExit.Text = "Exit";
			this.mi_PortScannerExit.Click += new System.EventHandler(this.mi_PortScannerExit_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mi_PortScannerStart,
																					  this.mi_PortScannerStop});
			this.menuItem5.Text = "Scan";
			// 
			// mi_PortScannerStart
			// 
			this.mi_PortScannerStart.Index = 0;
			this.mi_PortScannerStart.Text = "Start";
			this.mi_PortScannerStart.Click += new System.EventHandler(this.mi_PortScannerStart_Click);
			// 
			// mi_PortScannerStop
			// 
			this.mi_PortScannerStop.Index = 1;
			this.mi_PortScannerStop.Text = "Stop";
			this.mi_PortScannerStop.Click += new System.EventHandler(this.mi_PortScannerStop_Click);
			// 
			// mi_Log
			// 
			this.mi_Log.Index = 2;
			this.mi_Log.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.mi_LogRefresh,
																				   this.mi_LogDeleteLog});
			this.mi_Log.Text = "Log";
			// 
			// mi_LogRefresh
			// 
			this.mi_LogRefresh.Index = 0;
			this.mi_LogRefresh.Text = "Refresh";
			this.mi_LogRefresh.Click += new System.EventHandler(this.mi_LogRefresh_Click);
			// 
			// mi_LogDeleteLog
			// 
			this.mi_LogDeleteLog.Index = 1;
			this.mi_LogDeleteLog.Text = "Delete";
			this.mi_LogDeleteLog.Click += new System.EventHandler(this.mi_LogDeleteLog_Click);
			// 
			// mi_PortScannerSettings
			// 
			this.mi_PortScannerSettings.Index = 3;
			this.mi_PortScannerSettings.Text = "Settings";
			this.mi_PortScannerSettings.Click += new System.EventHandler(this.mi_PortScannerSettings_Click);
			// 
			// mi_MailService
			// 
			this.mi_MailService.Index = 4;
			this.mi_MailService.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.mi_MailServiceStart,
																						   this.mi_MailServiceStop});
			this.mi_MailService.Text = "Mail Service (SMTP)";
			// 
			// mi_MailServiceStart
			// 
			this.mi_MailServiceStart.Index = 0;
			this.mi_MailServiceStart.Text = "Start";
			this.mi_MailServiceStart.Click += new System.EventHandler(this.mi_MailServiceStart_Click);
			// 
			// mi_MailServiceStop
			// 
			this.mi_MailServiceStop.Index = 1;
			this.mi_MailServiceStop.Text = "Stop";
			this.mi_MailServiceStop.Click += new System.EventHandler(this.mi_MailServiceStop_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// serviceController1
			// 
			this.serviceController1.MachineName = "hakki";
			this.serviceController1.ServiceName = "SMTPSVC";
			// 
			// imageList2
			// 
			this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
			this.imageList2.TransparentColor = System.Drawing.Color.Silver;
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.splitter1);
			this.panelMain.Controls.Add(this.panelBottom);
			this.panelMain.Controls.Add(this.panelTop);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(600, 401);
			this.panelMain.TabIndex = 0;
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 200);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(600, 2);
			this.splitter1.TabIndex = 7;
			this.splitter1.TabStop = false;
			// 
			// panelBottom
			// 
			this.panelBottom.Controls.Add(this.listViewPorts);
			this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelBottom.Location = new System.Drawing.Point(0, 200);
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Size = new System.Drawing.Size(600, 201);
			this.panelBottom.TabIndex = 2;
			// 
			// listViewPorts
			// 
			this.listViewPorts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.Type,
																							this.Index,
																							this.Date,
																							this.Time,
																							this.PScannedHost,
																							this.HostPort,
																							this.Message});
			this.listViewPorts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewPorts.FullRowSelect = true;
			this.listViewPorts.GridLines = true;
			this.listViewPorts.Location = new System.Drawing.Point(0, 0);
			this.listViewPorts.Name = "listViewPorts";
			this.listViewPorts.Size = new System.Drawing.Size(600, 201);
			this.listViewPorts.SmallImageList = this.imageList1;
			this.listViewPorts.TabIndex = 0;
			this.listViewPorts.View = System.Windows.Forms.View.Details;
			this.listViewPorts.DoubleClick += new System.EventHandler(this.listViewPorts_DoubleClick_1);
			this.listViewPorts.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewPorts_ColumnClick);
			// 
			// Type
			// 
			this.Type.Text = "Type";
			this.Type.Width = 120;
			// 
			// Index
			// 
			this.Index.Text = "Index";
			this.Index.Width = 42;
			// 
			// Date
			// 
			this.Date.Text = "Date";
			this.Date.Width = 75;
			// 
			// Time
			// 
			this.Time.Text = "Time";
			this.Time.Width = 75;
			// 
			// PScannedHost
			// 
			this.PScannedHost.Text = "Host";
			this.PScannedHost.Width = 100;
			// 
			// HostPort
			// 
			this.HostPort.Text = "Port";
			this.HostPort.Width = 120;
			// 
			// Message
			// 
			this.Message.Text = "Message";
			this.Message.Width = 200;
			// 
			// panelTop
			// 
			this.panelTop.BackColor = System.Drawing.SystemColors.ControlLight;
			this.panelTop.Controls.Add(this.listViewMails);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(600, 200);
			this.panelTop.TabIndex = 6;
			// 
			// listViewMails
			// 
			this.listViewMails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.mType,
																							this.mIndex,
																							this.mDate,
																							this.mTime,
																							this.ScannedHost,
																							this.mMessage});
			this.listViewMails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewMails.FullRowSelect = true;
			this.listViewMails.GridLines = true;
			this.listViewMails.Location = new System.Drawing.Point(0, 0);
			this.listViewMails.Name = "listViewMails";
			this.listViewMails.Size = new System.Drawing.Size(600, 200);
			this.listViewMails.SmallImageList = this.imageList1;
			this.listViewMails.TabIndex = 0;
			this.listViewMails.View = System.Windows.Forms.View.Details;
			this.listViewMails.DoubleClick += new System.EventHandler(this.listViewMails_DoubleClick);
			this.listViewMails.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewMails_ColumnClick);
			// 
			// mType
			// 
			this.mType.Text = "Type";
			this.mType.Width = 75;
			// 
			// mIndex
			// 
			this.mIndex.Text = "Index";
			this.mIndex.Width = 40;
			// 
			// mDate
			// 
			this.mDate.Text = "Date";
			this.mDate.Width = 75;
			// 
			// mTime
			// 
			this.mTime.Text = "Time";
			this.mTime.Width = 75;
			// 
			// ScannedHost
			// 
			this.ScannedHost.Text = "Scanned Host";
			this.ScannedHost.Width = 120;
			// 
			// mMessage
			// 
			this.mMessage.Text = "Not Responding Ports";
			this.mMessage.Width = 200;
			// 
			// eventLog1
			// 
			this.eventLog1.SynchronizingObject = this;
			// 
			// PortScanner
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ClientSize = new System.Drawing.Size(600, 401);
			this.Controls.Add(this.panelMain);
			this.Menu = this.mainMenu1;
			this.Name = "PortScanner";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Port Scanner Utility";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.panelMain.ResumeLayout(false);
			this.panelBottom.ResumeLayout(false);
			this.panelTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new PortScanner());
		}



		private void mi_Quit_Click(object sender, System.EventArgs e)
		{
			if (timer1 != null)
				timer1.Dispose();
			this.Close();
		}

		private void mi_Restore_Click(object sender, System.EventArgs e)
		{
			this.Activate();
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{	
		
			CurrentFileName = Setting.FileName;			
		
			if (Setting.FileName=="")
			{
				mi_PortScannerStart.Enabled=false;
				mi_PortScannerStop.Enabled=false;
			}
			else
			{
				mi_PortScannerStart.Enabled=true;
				mi_PortScannerStop.Enabled=false;
			}
			timer1=null;
		}

		private void mi_PortScannerStart_Click(object sender, System.EventArgs e)
		{			
			if (isStoped && (CurrentFileName != ""))
			{
				if (timer1 == null)
				{
					
					if (!Utility.FileUtility.isFileExist(CurrentFileName))
					{
						MessageBox.Show("PortScanner file : "+Setting.FileName+" does not exist in the "+
							"specified place. Update place of file contain scanning info from Settings.",
							"File Unavailable",MessageBoxButtons.OK,MessageBoxIcon.Error);
						return;
					}

					configuration = new Configuration(CurrentFileName);
					timer1 = new System.Threading.Timer(new TimerCallback(checkLoopCount),
						null,0,configuration.Settings.LoopIntervalSeconds*1000);
				}
				mi_PortScannerStart.Enabled=false;
				mi_PortScannerStop.Enabled=true;

				isStoped=false;
			}
			
		}

		public void checkLoopCount(Object stateInfo)
		{			
			if (!configuration.Settings.LoopEndless)
			{			
				if (loopCount == configuration.Settings.LoopCount)
				{					
					timer1.Dispose();	
					timer1=null;
				}
			}						
			foreach (string key in configuration.ServerTable.Keys)
			{
				count=0;
				((Server)configuration.ServerTable[key]).ReachablePorts = new ArrayList();
				((Server)configuration.ServerTable[key]).UnreachablePorts= new ArrayList();
			}
			
			ScanPorts();

			loopCount++;
			return;
		}

		private void mi_PortScannerStop_Click(object sender, System.EventArgs e)
		{
			if (!isStoped)
			{
				mi_PortScannerStart.Enabled=true;
				mi_PortScannerStop.Enabled=false;

				timer1.Dispose();
				timer1 = null;
				isStoped=true;
			}
		}

		private void notifyIcon1_Click(object sender, System.EventArgs e)
		{
			this.Activate();
		}

		
		private void mi_PortScannerExit_Click(object sender, System.EventArgs e)
		{
			if (timer1 != null)
				timer1.Dispose();
			this.Close();
		}

		private void mi_LogRefresh_Click(object sender, System.EventArgs e)
		{	
			RefreshView();
		}

		private void mi_PortScannerSettings_Click(object sender, System.EventArgs e)
		{
			SettingForm portScannerSettings = new SettingForm();
			DialogResult result = portScannerSettings.ShowDialog();

			if(result == DialogResult.OK)
			{	
				Setting.WriteToFile();
				if (CurrentFileName == "")
				{
					CurrentFileName = Setting.FileName;
					if (CurrentFileName == "")
					{
						mi_PortScannerStart.Enabled=false;
						mi_PortScannerStop.Enabled=false;
					}
					else
					{
						mi_PortScannerStart.Enabled=true;
						mi_PortScannerStop.Enabled=false;
					}
				}
			}
			else
			{
				if (CurrentFileName == "")
				{
					mi_PortScannerStart.Enabled=false;
					mi_PortScannerStop.Enabled=false;
				}
			}
			return;	
		}

		private void mi_LogDeleteLog_Click(object sender, System.EventArgs e)
		{
			if (System.Diagnostics.EventLog.Exists("PortScannerLog"))
			{
				System.Diagnostics.EventLog.Delete("PortScannerLog");
			}
			listViewMails.Items.Clear();
			listViewPorts.Items.Clear();
		}

		private void listViewPorts_DoubleClick(object sender, System.EventArgs e)
		{
			
		}


		private void mi_MailServiceStart_Click(object sender, System.EventArgs e)
		{
			serviceController = new System.ServiceProcess.ServiceController("SMTPSVC");
			
			if (serviceController.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
			{
				try 
				{
					// Start the service, and wait until its status is "Running".
					serviceController.Start();
					serviceController.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running);					
					// Display the current service status.
					MessageBox.Show("Mail Service is Started ...",
						"Service Operation Notification",
						MessageBoxButtons.OK,MessageBoxIcon.Information);					
				}
				catch (InvalidOperationException)
				{				
					MessageBox.Show("Unable to Start Mail Service ...",
						"Service Operation Notification",
						MessageBoxButtons.OK,MessageBoxIcon.Error);
				}

			}
			else
			{		
				MessageBox.Show("Mail Service is alrerady Started ...",
					"Service Operation Notification",
					MessageBoxButtons.OK,MessageBoxIcon.Information);				
			}
		}

		private void mi_MailServiceStop_Click(object sender, System.EventArgs e)
		{	
			serviceController = new System.ServiceProcess.ServiceController("SMTPSVC");
			
			if (serviceController.Status == System.ServiceProcess.ServiceControllerStatus.Running)
			{
				try 
				{
					// Start the service, and wait until its status is "Running".
					serviceController.Stop();
					serviceController.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped);
					
					// Display the current service status.
					MessageBox.Show("Mail Service is Stopped successfully...",
						"Service Operation Notification",
						MessageBoxButtons.OK,MessageBoxIcon.Information);				
				}
				catch (InvalidOperationException)
				{
					MessageBox.Show("Unable to Stop Mail Service ...",
						"Service Operation Notification",
						MessageBoxButtons.OK,MessageBoxIcon.Error);												
				}

			}
			else
			{
				MessageBox.Show("Mail Service is alrerady Stopped ...",
					"Service Operation Notification",
					MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
		}



		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
		
		}

		private void mi_NewFile_Click(object sender, System.EventArgs e)
		{
			CreateFileForm newFile = new CreateFileForm();
			newFile.Owener = this;
			DialogResult result = newFile.ShowDialog();
			if (result == DialogResult.OK)
			{
				result = MessageBox.Show("Do you want this as Default Scan File","",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
				
				if (result == DialogResult.OK)
				{
					
					if (!isStoped)
					{
						mi_PortScannerStart.Enabled=true;
						mi_PortScannerStop.Enabled=false;

						timer1.Dispose();
						timer1 = null;
						isStoped=true;						
					}
					Setting.FileName = newFileName;
					CurrentFileName = newFileName;
					Setting.WriteToFile();
				}
			}
		}



		private void mi_LoadFile_Click(object sender, System.EventArgs e)
		{
			DialogResult  result = openFileDialog1.ShowDialog();
			
			if (result == DialogResult.OK)
			{
				if (!isStoped)
				{
					timer1.Dispose();
					timer1 = null;
					isStoped=true;
				}
				mi_PortScannerStart.Enabled=true;
				mi_PortScannerStop.Enabled=false;
				CurrentFileName = openFileDialog1.FileName;	
			}
			else
			{
				if (CurrentFileName == "")
				{
					mi_PortScannerStart.Enabled=false;
					mi_PortScannerStop.Enabled=false;
				}
			}
		}


		public void PortResponde(string server, int number)
		{
			lock(configuration)
			{
				Port p = new Port();
				p.Number = number;
				((Server)configuration.ServerTable[server]).ReachablePorts.Add(p);	
				lock (eventLogger)
				{
					eventLogger.LogEventLog("On Host : \'"+server+"\' Port : \'"+number.ToString()+"\'  is in Listening State");
				}
				count++;
				System.Collections.IDictionaryEnumerator itr = ((Server)configuration.ServerTable[server]).PortTable.GetEnumerator();
				itr.Reset();
				itr.MoveNext();
				
				Port pfirst = (Port)itr.Value;
				if (pfirst.HasNumber)
				{
					if (count== ((Server)configuration.ServerTable[server]).PortTable.Count)
					{
						//Thread.Sleep(500);
						Post(server);
						RefreshView();
					}
				}
				else
				{
					if ((pfirst.NumberRangeEnd-pfirst.NumberRangeBegin) == count)
					{
						//Thread.Sleep(500);
						Post(server);
						RefreshView();
					}
				}
			}
						
		}

		public void PortNotResponde(string server, int number, Exception e)
		{
			lock (configuration)
			{	
				Port p = new Port();
				p.Number = number;
				p.Exception= e;
				
				((Server)configuration.ServerTable[server]).UnreachablePorts.Add(p);
				lock (eventLogger)
				{
					eventLogger.LogEventLogError("On Host : \'"+server+"\' Port : \'"+number.ToString()+"\'  is Not Accessable. Error Message : "+e.Message);
				}
				count++;
				
				System.Collections.IDictionaryEnumerator itr = ((Server)configuration.ServerTable[server]).PortTable.GetEnumerator();
				itr.Reset();
				itr.MoveNext();
				
				Port pfirst = (Port)itr.Value;
				if (pfirst.HasNumber)
				{
					if (count == ((Server)configuration.ServerTable[server]).PortTable.Count)
					{
						//Thread.Sleep(500);
						Post(server);
						RefreshView();
					}
				}
				else
				{
					if ((pfirst.NumberRangeEnd-pfirst.NumberRangeBegin) == count)
					{
						//Thread.Sleep(500);
						Post(server);
						RefreshView();
					}
				}
				
			}
			
		}

		public void ScanPorts()
		{
			foreach (string key in configuration.ServerTable.Keys)
			{
				foreach (string pkey in ((Server)configuration.ServerTable[key]).PortTable.Keys)
				{
					int pno=-1;
					try
					{
						pno = Convert.ToInt32(pkey);
					}
					catch(Exception)
					{
						int pos = pkey.IndexOf("-");
						try
						{
							int start = Convert.ToUInt16(pkey.Substring(0,pos));
							int end   = Convert.ToUInt16(pkey.Substring(pos+1));
							
							for (int i=start;i<=end;i++)
							{
								if (!isStoped)
								{
									ServerPortPair spp = new ServerPortPair(key,i,new OnPortNotRespondeCallback (PortNotResponde),new  OnPortRespondeCallback( PortResponde));
					
									Thread t = new Thread(new ThreadStart(spp.Scan));
									t.Start();
								}
							}
							return;
						}
						catch(Exception)
						{
							MessageBox.Show("Invalid port range ...");
						}
					}
					if (pno<0 || pno >65535)
					{
						MessageBox.Show("Invalid port no ...");
						continue;
					}
					if (!isStoped)
					{
						ServerPortPair spp = new ServerPortPair(key,Convert.ToInt32(pkey),new OnPortNotRespondeCallback (PortNotResponde),new  OnPortRespondeCallback( PortResponde));
					
						Thread t = new Thread(new ThreadStart(spp.Scan));
						t.Start();
					}

				}
			}

		}

		public bool isMailServiceStarted
		{
			get 
			{
				return ((serviceController.Status == 
					System.ServiceProcess.ServiceControllerStatus.Running)?true:false);
			}
		}

		private void Post(string server)
		{
			if (isMailServiceStarted)
			{
				if (((Server)configuration.ServerTable[server]).UnreachablePorts.Count != 0)
				{

					string subject = ((Server)configuration.ServerTable[server]).ExpandVariables( configuration.Settings.EMailPortNotAvailableSubject );
					string body = ((Server)configuration.ServerTable[server]).ExpandVariables( configuration.Settings.EMailPortNotAvailableBody );
					
				foreach ( string to in ((Server)configuration.ServerTable[server]).mailAdressList )
					{
						try
						{
							SmtpClient.Send( 
								configuration.Settings.MailServer,
								configuration.Settings.EMailSenderAddress,
								to,
								subject,
								body );
						}
						catch(Exception e)

						{	
							string str=null;
							int pos =0;
							str = e.Message;
							while ((pos = str.IndexOf("\r")) != -1)
							{								
								str = str.Remove(pos,1);
							}
							
							while ((pos = str.IndexOf("\0")) != -1)
							{								
								str = str.Remove(pos,1);
							}
							mailLogger.LogEventLogError( "Failed sending notify mail \n Error Message :\n"+str+"\nOriginal Message: \nto :" + to + " \nfrom : " + configuration.Settings.EMailSenderAddress +"\nsubject:\n" + subject + "\nbody:\n" + body );
							return;
						}
						mailLogger.LogEventLog( "Send notify e-mail:\n"+ "\nto :" + to + "\nfrom : " + configuration.Settings.EMailSenderAddress +" \nsubject :\n" + subject + " \nbody:\n" + body );
					}
				}
			}
		}

		private void RefreshView()
		{
			RefreshEventView();
			RefreshMailView();			
		}
		
		private void RefreshEventView()
		{			
			listViewPorts.Items.Clear();
			if (!EventLog.Exists("PortScannerLog"))
				return;


			EventLog myEventLog = new EventLog();
			//myEventLog.Source="Event_Log";
			myEventLog.Log="PortScannerLog";
			
			EventLogEntryCollection myLogEntryCollection=myEventLog.Entries;
		
			foreach (EventLogEntry myLogEntry in myLogEntryCollection)
			{
				if (myLogEntry.Source == "Event_Log")
				{
					int imageindex=0;
					string type;
					if (myLogEntry.EntryType.ToString() == "Error")
					{
						imageindex=1;
						type= "Not Responding";
					}
					else
					{
						imageindex=0;
						type = "Listening";
					}
				
					ListViewItem item = new ListViewItem(type,imageindex);
					item.SubItems.Add(myLogEntry.Index.ToString());
					item.SubItems.Add(myLogEntry.TimeGenerated.Date.ToShortDateString());
					item.SubItems.Add(myLogEntry.TimeGenerated.TimeOfDay.ToString());
					
					string str = myLogEntry.Message;
					int pos = str.IndexOf("\'");
					int pos2 = str.IndexOf("\'",pos+1);
					string host = str.Substring(pos+1,pos2-pos-1);
					item.SubItems.Add(host);

					pos = str.IndexOf("\'",pos2+1);
					pos2 = str.IndexOf("\'",pos+1);
					string port = str.Substring(pos+1,pos2-pos-1);

					int portNo = Convert.ToInt32(port);
					string pro=null;
		
					if ((pro = consts.GetTcpPorts(portNo)) == "Unknown")
						pro = consts.GetUdpPorts(portNo);


					item.SubItems.Add(port+"   ( "+pro+" )");
					
					item.SubItems.Add(myLogEntry.Message.ToString());
					listViewPorts.Items.Add(item);

					
				}
			}
			listViewPorts_ColumnClick(this,new ColumnClickEventArgs(3));
			listViewPorts.Update();	
	
			
		}

		private void RefreshMailView()
		{
			listViewMails.Items.Clear();
			if (!EventLog.Exists("PortScannerLog"))
				return;
			EventLog myMailLog = new EventLog();
			//myMailLog.Source="Mail_Log";
			myMailLog.Log="PortScannerLog";
			
			EventLogEntryCollection myLogEntryCollection=myMailLog.Entries;
		
			foreach (EventLogEntry myLogEntry in myLogEntryCollection)
			{
				if (myLogEntry.Source == "Mail_Log")
				{
					int imageindex=0;
					string type;
					if (myLogEntry.EntryType.ToString() == "Error")
					{
						imageindex=3;
						type= "Failed";
					}
					else
					{
						imageindex=2;
						type = "Send";
					}
					ListViewItem item = new ListViewItem(type,imageindex);
					item.SubItems.Add(myLogEntry.Index.ToString());
					item.SubItems.Add(myLogEntry.TimeGenerated.Date.ToShortDateString());
					item.SubItems.Add(myLogEntry.TimeGenerated.TimeOfDay.ToString());

					string str = myLogEntry.Message;
					int pos = str.IndexOf("\'");
					int pos2 = str.IndexOf("\'",pos+1);
					string host = str.Substring(pos+1,pos2-pos-1);
					item.SubItems.Add(host);

					
					pos = str.IndexOf("\'",pos2+1);
					pos2 = str.IndexOf("\'",pos+1);
					string mes = "";
					while (true)
					{
						pos = str.IndexOf("\'",pos2+1);
						if (pos == -1)
							break;
						pos2 = str.IndexOf("\'",pos+1);
						mes += str.Substring(pos+1,pos2-pos-1);

						mes += " , ";
					}
					mes = mes.Remove(mes.LastIndexOf(",")-1,3);
					item.SubItems.Add(mes);

					item.SubItems.Add(myLogEntry.Message);
					listViewMails.Items.Add(item);
				}
			}
			listViewMails_ColumnClick(this,new ColumnClickEventArgs(3));
			listViewMails.Update();
		}

		private void listViewMails_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			this.listViewMails.ListViewItemSorter = new ListViewItemComparer(e.Column);
		}

		private void listViewPorts_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			this.listViewPorts.ListViewItemSorter = new ListViewItemComparer(e.Column);
		}

		

		private void listViewPorts_DoubleClick_1(object sender, System.EventArgs e)
		{
			ListView.SelectedListViewItemCollection sItems =listViewPorts.SelectedItems;
			foreach ( ListViewItem item in sItems )
			{					
				LogItemView logItemView = new LogItemView();
				logItemView.type="Port";
				logItemView.LogMessage=item.SubItems[item.SubItems.Count-1].Text;
				logItemView.ShowDialog(this);
				return;
			}
		}

		private void listViewMails_DoubleClick(object sender, System.EventArgs e)
		{
			ListView.SelectedListViewItemCollection sItems =listViewMails.SelectedItems;
			foreach ( ListViewItem item in sItems )
			{					
				LogItemView logItemView = new LogItemView();
				logItemView.type="Mail";
				logItemView.LogMessage=item.SubItems[item.SubItems.Count-1].Text;
				logItemView.ShowDialog(this);
				return;
			}
		}

		class ListViewItemComparer : IComparer
		{
			private int col;
			public ListViewItemComparer()
			{
				col = 0;
			}
			public ListViewItemComparer(int column)
			{
				col = column;
			}
			public int Compare(object x, object y)
			{
				return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
			}
		}
	}				
}