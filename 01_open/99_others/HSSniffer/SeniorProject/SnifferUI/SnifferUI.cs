using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Sniffer;
using System.Threading;
using System.Text;

namespace SnifferUI
{

	public delegate void TabPageCallback(TabPage tp);

	/// <summary>
	/// Summary description for SnifferUI.
	/// </summary>
	public class SnifferUI : System.Windows.Forms.Form
	{	
		private Thread thrdTrv;
		private Thread thrd;
		private Thread thrdC;
		bool isPaused = false;
		int MouseX=0;
		int MouseY=0;
		int totalCount=0,tcpCount=0,udpCount=0,icmpCount=0,otherCount=0;
		int totalByte=0,tcpByte=0,udpByte=0,icmpByte=0,otherByte=0;
		private treeViewFuncs objTVF;
		private SnifferSocket Socket_    = null;
		public Hashtable     Counter_ =null;
		public Hashtable     IdentTable_ =null;
		private Hashtable     TabPageTable_ =null;
		private Hashtable	  CurrentSniffing_=null;
		private Hashtable ForCounter=null;
		private string thisComputerIp ="";

		private IPv4Datagram tempDatagram;

		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.ColumnHeader Source;
		private System.Windows.Forms.ColumnHeader Destination;
		private System.Windows.Forms.ColumnHeader Identification;
		private System.Windows.Forms.ColumnHeader Protocal;
		private System.Windows.Forms.ListView lstwMain;
		private System.Windows.Forms.ColumnHeader Source_;
		private System.Windows.Forms.ColumnHeader Protocol_;
		private System.Windows.Forms.ContextMenu CnmLstwMain;
		private System.Windows.Forms.ColumnHeader Process_;
		private System.Windows.Forms.TabControl tcMain;
		private System.Windows.Forms.TabPage tpSniffing;
		private System.Windows.Forms.Panel pnlSniffing;
		private System.Windows.Forms.ToolBar tlbMain;
		private System.Windows.Forms.ToolBar tlbSniffing;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.Splitter splitter3;
		private System.Windows.Forms.Panel pnlToolBarTab;
		private System.Windows.Forms.Panel pnlSubTab;
		private System.Windows.Forms.Panel pnlbottom;
		private System.Windows.Forms.Panel pnlBottomLeft;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.TreeView trvPacket;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel pnlBottomRight;
		private System.Windows.Forms.ToolBarButton btnOpentrv;
		private System.Windows.Forms.ToolBarButton btnClosetrv;
		private System.Windows.Forms.ToolBarButton btnStopSniffing;
		private System.Windows.Forms.Panel pnlTV;
		private System.Windows.Forms.ImageList imltlbSniffing;
		private System.Windows.Forms.Panel pnlBottomRightSub;
		private System.Windows.Forms.Panel pnlRichText;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.RichTextBox RTBPacket;
		private System.Windows.Forms.ImageList imlMain;
		private System.Windows.Forms.ToolBarButton btnStartSniff;
		private System.Windows.Forms.ToolBarButton btnCheck;
		private System.Windows.Forms.TabControl tcSniffing;
		private System.Windows.Forms.MenuItem miStartSniffing;
		private System.Windows.Forms.MenuItem miConCheck;
		private System.Windows.Forms.ToolBarButton btnPause;
		private System.Windows.Forms.MainMenu mmain;
		private System.Windows.Forms.MenuItem miFile;
		private System.Windows.Forms.Panel pnlUp;
		private System.Windows.Forms.Panel pnlUpLeft;
		private System.Windows.Forms.Splitter splitter4;
		private System.Windows.Forms.Panel pnlUpRight;
		private System.Windows.Forms.Label lbln1;
		private System.Windows.Forms.Label lblv1;
		private System.Windows.Forms.Label lblv2;
		private System.Windows.Forms.Label lbln2;
		private System.Windows.Forms.Label lblv3;
		private System.Windows.Forms.Label lbln3;
		private System.Windows.Forms.Label lblv4;
		private System.Windows.Forms.Label lbln4;
		private System.Windows.Forms.Label lbln5;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label lbln6;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label lbln7;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label lbln8;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label lbln9;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ColumnHeader Count_;
		private System.Windows.Forms.ImageList imltblTreeView;
		private System.Windows.Forms.Panel pnlAscii;
		private System.Windows.Forms.Panel pnlPacket;
		private System.Windows.Forms.Splitter splitter5;
		private System.Windows.Forms.RichTextBox RTBAscii;
		private System.Windows.Forms.MenuItem miAddProtocol;
		private System.Windows.Forms.MenuItem miEdit;
		private System.Windows.Forms.Panel pnlIcmpTlb;
		private System.Windows.Forms.ToolBar tblIcmp;
		private System.Windows.Forms.ToolBarButton OpenTrv;
		private System.Windows.Forms.ToolBarButton CloseTrv;
		private System.Windows.Forms.Panel pnlIcmpView;
		private icmpView CtrlMsgs;
		private System.Windows.Forms.TabPage tpCtrl;
		private System.Windows.Forms.Panel pnlError;
		private System.Windows.Forms.MenuItem midelete;
		private System.Windows.Forms.MenuItem mideleteall;
		private System.Windows.Forms.ColumnHeader SPort_;
		private System.Windows.Forms.ColumnHeader DPort_;
		private System.Windows.Forms.ColumnHeader ProcessId_;
		private System.Windows.Forms.MenuItem miTools;
		private System.Windows.Forms.MenuItem miNetStat;
		private System.Windows.Forms.MenuItem miFiltering;
		private System.Windows.Forms.Label lblv5;
		private System.Windows.Forms.ColumnHeader SourceName_;
		private System.Windows.Forms.ColumnHeader DestName_;
		private System.Windows.Forms.MenuItem miAddIP;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem miMode;
		private System.Windows.Forms.MenuItem miManual;
		private System.Windows.Forms.MenuItem miAutomatic;
		private System.Windows.Forms.MenuItem miStopSniffing;
		private System.Windows.Forms.ToolBarButton btnStopSniff;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label lblb5;
		private System.Windows.Forms.Label lblb4;
		private System.Windows.Forms.Label lblb3;
		private System.Windows.Forms.Label lblb2;
		private System.Windows.Forms.Label lblb1;
		private System.Windows.Forms.ToolBarButton btnQuit;
		private System.Windows.Forms.MenuItem miExit;
		private System.Windows.Forms.MenuItem miPing;
		private System.Windows.Forms.MenuItem miTraceR;
		private System.Windows.Forms.MenuItem miCheckCon;
		private System.Windows.Forms.MenuItem miSetting;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Diagnostics.Process scanner;
		private System.Windows.Forms.ColumnHeader Destination_;
		private System.Windows.Forms.ToolBarButton btnFilter;
		private System.ComponentModel.IContainer components;

		public SnifferUI()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			objTVF=new treeViewFuncs();
			IdentTable_= new Hashtable();
			Counter_=new Hashtable();
			TabPageTable_ = new Hashtable();
			CurrentSniffing_= new Hashtable();
			ForCounter=new Hashtable();
			lstwMain.FullRowSelect=true;
			lstwMain.GridLines=true;
			tempDatagram =new IPv4Datagram();
			
			thisComputerIp =Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
			bool enterence = true;

			while (enterence)
			{
				if ( thisComputerIp != null ) 
				{
					Socket_ = new SnifferSocket();
					try
					{
						Socket_.Sniff(thisComputerIp);
						Socket_.IPs.Add(thisComputerIp);
						enterence=false;
					}
					catch(SnifferException e)
					{
						HandleSnifferError(e);
						thisComputerIp=GetIPToSniff();
					}
					LoadOptions();
				}
				else{
					thisComputerIp=GetIPToSniff();
				}
			}

			thrd = new Thread(new ThreadStart(Counter));
			thrd.IsBackground = true;
			thrd.Start();

			thrdC = new Thread(new ThreadStart(Counter2));
			thrdC.IsBackground = true;
			thrdC.Start();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SnifferUI));
			System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
			this.lstwMain = new System.Windows.Forms.ListView();
			this.Count_ = new System.Windows.Forms.ColumnHeader();
			this.Source_ = new System.Windows.Forms.ColumnHeader();
			this.SourceName_ = new System.Windows.Forms.ColumnHeader();
			this.SPort_ = new System.Windows.Forms.ColumnHeader();
			this.Destination_ = new System.Windows.Forms.ColumnHeader();
			this.DestName_ = new System.Windows.Forms.ColumnHeader();
			this.DPort_ = new System.Windows.Forms.ColumnHeader();
			this.Protocol_ = new System.Windows.Forms.ColumnHeader();
			this.Process_ = new System.Windows.Forms.ColumnHeader();
			this.ProcessId_ = new System.Windows.Forms.ColumnHeader();
			this.CnmLstwMain = new System.Windows.Forms.ContextMenu();
			this.miStartSniffing = new System.Windows.Forms.MenuItem();
			this.miStopSniffing = new System.Windows.Forms.MenuItem();
			this.miConCheck = new System.Windows.Forms.MenuItem();
			this.midelete = new System.Windows.Forms.MenuItem();
			this.mideleteall = new System.Windows.Forms.MenuItem();
			this.imlMain = new System.Windows.Forms.ImageList(this.components);
			this.imltblTreeView = new System.Windows.Forms.ImageList(this.components);
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.Source = new System.Windows.Forms.ColumnHeader();
			this.Destination = new System.Windows.Forms.ColumnHeader();
			this.Identification = new System.Windows.Forms.ColumnHeader();
			this.Protocal = new System.Windows.Forms.ColumnHeader();
			this.tcSniffing = new System.Windows.Forms.TabControl();
			this.tcMain = new System.Windows.Forms.TabControl();
			this.tpSniffing = new System.Windows.Forms.TabPage();
			this.pnlSniffing = new System.Windows.Forms.Panel();
			this.pnlSubTab = new System.Windows.Forms.Panel();
			this.pnlToolBarTab = new System.Windows.Forms.Panel();
			this.tlbSniffing = new System.Windows.Forms.ToolBar();
			this.btnStopSniffing = new System.Windows.Forms.ToolBarButton();
			this.btnOpentrv = new System.Windows.Forms.ToolBarButton();
			this.btnClosetrv = new System.Windows.Forms.ToolBarButton();
			this.imltlbSniffing = new System.Windows.Forms.ImageList(this.components);
			this.tpCtrl = new System.Windows.Forms.TabPage();
			this.pnlError = new System.Windows.Forms.Panel();
			this.pnlIcmpView = new System.Windows.Forms.Panel();
			this.CtrlMsgs = new icmpView();
			this.pnlIcmpTlb = new System.Windows.Forms.Panel();
			this.tblIcmp = new System.Windows.Forms.ToolBar();
			this.OpenTrv = new System.Windows.Forms.ToolBarButton();
			this.CloseTrv = new System.Windows.Forms.ToolBarButton();
			this.tlbMain = new System.Windows.Forms.ToolBar();
			this.btnStartSniff = new System.Windows.Forms.ToolBarButton();
			this.btnStopSniff = new System.Windows.Forms.ToolBarButton();
			this.btnPause = new System.Windows.Forms.ToolBarButton();
			this.btnCheck = new System.Windows.Forms.ToolBarButton();
			this.btnFilter = new System.Windows.Forms.ToolBarButton();
			this.btnQuit = new System.Windows.Forms.ToolBarButton();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.pnlbottom = new System.Windows.Forms.Panel();
			this.pnlBottomRight = new System.Windows.Forms.Panel();
			this.pnlBottomRightSub = new System.Windows.Forms.Panel();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.pnlRichText = new System.Windows.Forms.Panel();
			this.splitter5 = new System.Windows.Forms.Splitter();
			this.pnlPacket = new System.Windows.Forms.Panel();
			this.RTBPacket = new System.Windows.Forms.RichTextBox();
			this.pnlAscii = new System.Windows.Forms.Panel();
			this.RTBAscii = new System.Windows.Forms.RichTextBox();
			this.pnlTV = new System.Windows.Forms.Panel();
			this.trvPacket = new System.Windows.Forms.TreeView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.pnlBottomLeft = new System.Windows.Forms.Panel();
			this.splitter3 = new System.Windows.Forms.Splitter();
			this.pnlUp = new System.Windows.Forms.Panel();
			this.pnlUpRight = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lblb5 = new System.Windows.Forms.Label();
			this.lblb4 = new System.Windows.Forms.Label();
			this.lblb3 = new System.Windows.Forms.Label();
			this.lblb2 = new System.Windows.Forms.Label();
			this.lblb1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.lbln9 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.lbln8 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.lbln7 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.lbln6 = new System.Windows.Forms.Label();
			this.lblv5 = new System.Windows.Forms.Label();
			this.lbln5 = new System.Windows.Forms.Label();
			this.lblv4 = new System.Windows.Forms.Label();
			this.lbln4 = new System.Windows.Forms.Label();
			this.lblv3 = new System.Windows.Forms.Label();
			this.lbln3 = new System.Windows.Forms.Label();
			this.lblv2 = new System.Windows.Forms.Label();
			this.lbln2 = new System.Windows.Forms.Label();
			this.lblv1 = new System.Windows.Forms.Label();
			this.lbln1 = new System.Windows.Forms.Label();
			this.splitter4 = new System.Windows.Forms.Splitter();
			this.pnlUpLeft = new System.Windows.Forms.Panel();
			this.mmain = new System.Windows.Forms.MainMenu();
			this.miFile = new System.Windows.Forms.MenuItem();
			this.miExit = new System.Windows.Forms.MenuItem();
			this.miEdit = new System.Windows.Forms.MenuItem();
			this.miAddIP = new System.Windows.Forms.MenuItem();
			this.miAddProtocol = new System.Windows.Forms.MenuItem();
			this.miFiltering = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.miMode = new System.Windows.Forms.MenuItem();
			this.miManual = new System.Windows.Forms.MenuItem();
			this.miAutomatic = new System.Windows.Forms.MenuItem();
			this.miTools = new System.Windows.Forms.MenuItem();
			this.miNetStat = new System.Windows.Forms.MenuItem();
			this.miPing = new System.Windows.Forms.MenuItem();
			this.miTraceR = new System.Windows.Forms.MenuItem();
			this.miCheckCon = new System.Windows.Forms.MenuItem();
			this.miSetting = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.scanner = new System.Diagnostics.Process();
			this.tcMain.SuspendLayout();
			this.tpSniffing.SuspendLayout();
			this.pnlSniffing.SuspendLayout();
			this.pnlSubTab.SuspendLayout();
			this.pnlToolBarTab.SuspendLayout();
			this.tpCtrl.SuspendLayout();
			this.pnlError.SuspendLayout();
			this.pnlIcmpView.SuspendLayout();
			this.pnlIcmpTlb.SuspendLayout();
			this.pnlMain.SuspendLayout();
			this.pnlbottom.SuspendLayout();
			this.pnlBottomRight.SuspendLayout();
			this.pnlBottomRightSub.SuspendLayout();
			this.pnlRichText.SuspendLayout();
			this.pnlPacket.SuspendLayout();
			this.pnlAscii.SuspendLayout();
			this.pnlTV.SuspendLayout();
			this.pnlBottomLeft.SuspendLayout();
			this.pnlUp.SuspendLayout();
			this.pnlUpRight.SuspendLayout();
			this.pnlUpLeft.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstwMain
			// 
			this.lstwMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.Count_,
																					   this.Source_,
																					   this.SourceName_,
																					   this.SPort_,
																					   this.Destination_,
																					   this.DestName_,
																					   this.DPort_,
																					   this.Protocol_,
																					   this.Process_,
																					   this.ProcessId_});
			this.lstwMain.ContextMenu = this.CnmLstwMain;
			this.lstwMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstwMain.Location = new System.Drawing.Point(0, 0);
			this.lstwMain.MultiSelect = false;
			this.lstwMain.Name = "lstwMain";
			this.lstwMain.Size = new System.Drawing.Size(904, 176);
			this.lstwMain.SmallImageList = this.imlMain;
			this.lstwMain.TabIndex = 5;
			this.lstwMain.View = System.Windows.Forms.View.Details;
			this.lstwMain.DoubleClick += new System.EventHandler(this.lstwMain_DoubleClick);
			this.lstwMain.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstwMain_ColumnClick);
			this.lstwMain.SelectedIndexChanged += new System.EventHandler(this.lstwMain_SelectedIndexChanged);
			// 
			// Count_
			// 
			this.Count_.Text = "Count";
			this.Count_.Width = 56;
			// 
			// Source_
			// 
			this.Source_.Text = "Source";
			this.Source_.Width = 94;
			// 
			// SourceName_
			// 
			this.SourceName_.Text = "Name";
			this.SourceName_.Width = 68;
			// 
			// SPort_
			// 
			this.SPort_.Text = "Source Port";
			this.SPort_.Width = 150;
			// 
			// Destination_
			// 
			this.Destination_.Text = "Destination";
			this.Destination_.Width = 102;
			// 
			// DestName_
			// 
			this.DestName_.Text = "Name";
			this.DestName_.Width = 64;
			// 
			// DPort_
			// 
			this.DPort_.Text = "Dest. Port";
			this.DPort_.Width = 162;
			// 
			// Protocol_
			// 
			this.Protocol_.Text = "Protocol";
			// 
			// Process_
			// 
			this.Process_.Text = "Process";
			this.Process_.Width = 74;
			// 
			// ProcessId_
			// 
			this.ProcessId_.Text = "PID";
			this.ProcessId_.Width = 50;
			// 
			// CnmLstwMain
			// 
			this.CnmLstwMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.miStartSniffing,
																						this.miStopSniffing,
																						this.miConCheck,
																						this.midelete,
																						this.mideleteall});
			// 
			// miStartSniffing
			// 
			this.miStartSniffing.DefaultItem = true;
			this.miStartSniffing.Index = 0;
			this.miStartSniffing.Text = "Start Sniffing";
			this.miStartSniffing.Click += new System.EventHandler(this.miStartSniffing_Click);
			// 
			// miStopSniffing
			// 
			this.miStopSniffing.Index = 1;
			this.miStopSniffing.Text = "Stop Sniffing";
			this.miStopSniffing.Click += new System.EventHandler(this.miStopSniffing_Click);
			// 
			// miConCheck
			// 
			this.miConCheck.Index = 2;
			this.miConCheck.Text = "Check Connection";
			this.miConCheck.Click += new System.EventHandler(this.miConCheck_Click);
			// 
			// midelete
			// 
			this.midelete.Index = 3;
			this.midelete.Text = "Delete";
			this.midelete.Click += new System.EventHandler(this.midelete_Click);
			// 
			// mideleteall
			// 
			this.mideleteall.Index = 4;
			this.mideleteall.Text = "Delete All";
			this.mideleteall.Click += new System.EventHandler(this.mideleteall_Click);
			// 
			// imlMain
			// 
			this.imlMain.ImageSize = new System.Drawing.Size(16, 16);
			this.imlMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlMain.ImageStream")));
			this.imlMain.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// imltblTreeView
			// 
			this.imltblTreeView.ImageSize = new System.Drawing.Size(16, 16);
			this.imltblTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imltblTreeView.ImageStream")));
			this.imltblTreeView.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 428);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(1028, 22);
			this.statusBar1.TabIndex = 8;
			// 
			// Source
			// 
			this.Source.Text = "Source";
			this.Source.Width = 98;
			// 
			// Destination
			// 
			this.Destination.Text = "Destination";
			this.Destination.Width = 115;
			// 
			// Identification
			// 
			this.Identification.Text = "Identification";
			this.Identification.Width = 111;
			// 
			// Protocal
			// 
			this.Protocal.Text = "Protocal";
			this.Protocal.Width = 130;
			// 
			// tcSniffing
			// 
			this.tcSniffing.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcSniffing.Location = new System.Drawing.Point(0, 0);
			this.tcSniffing.Name = "tcSniffing";
			this.tcSniffing.SelectedIndex = 0;
			this.tcSniffing.Size = new System.Drawing.Size(536, 163);
			this.tcSniffing.TabIndex = 11;
			// 
			// tcMain
			// 
			this.tcMain.Controls.Add(this.tpSniffing);
			this.tcMain.Controls.Add(this.tpCtrl);
			this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcMain.Location = new System.Drawing.Point(0, 0);
			this.tcMain.Name = "tcMain";
			this.tcMain.SelectedIndex = 0;
			this.tcMain.Size = new System.Drawing.Size(544, 221);
			this.tcMain.TabIndex = 12;
			// 
			// tpSniffing
			// 
			this.tpSniffing.Controls.Add(this.pnlSniffing);
			this.tpSniffing.Location = new System.Drawing.Point(4, 22);
			this.tpSniffing.Name = "tpSniffing";
			this.tpSniffing.Size = new System.Drawing.Size(536, 195);
			this.tpSniffing.TabIndex = 0;
			this.tpSniffing.Text = "Current Sniffing";
			// 
			// pnlSniffing
			// 
			this.pnlSniffing.Controls.Add(this.pnlSubTab);
			this.pnlSniffing.Controls.Add(this.pnlToolBarTab);
			this.pnlSniffing.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlSniffing.Location = new System.Drawing.Point(0, 0);
			this.pnlSniffing.Name = "pnlSniffing";
			this.pnlSniffing.Size = new System.Drawing.Size(536, 195);
			this.pnlSniffing.TabIndex = 0;
			// 
			// pnlSubTab
			// 
			this.pnlSubTab.Controls.Add(this.tcSniffing);
			this.pnlSubTab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlSubTab.Location = new System.Drawing.Point(0, 32);
			this.pnlSubTab.Name = "pnlSubTab";
			this.pnlSubTab.Size = new System.Drawing.Size(536, 163);
			this.pnlSubTab.TabIndex = 14;
			// 
			// pnlToolBarTab
			// 
			this.pnlToolBarTab.Controls.Add(this.tlbSniffing);
			this.pnlToolBarTab.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlToolBarTab.Location = new System.Drawing.Point(0, 0);
			this.pnlToolBarTab.Name = "pnlToolBarTab";
			this.pnlToolBarTab.Size = new System.Drawing.Size(536, 32);
			this.pnlToolBarTab.TabIndex = 13;
			// 
			// tlbSniffing
			// 
			this.tlbSniffing.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						   this.btnStopSniffing,
																						   this.btnOpentrv,
																						   this.btnClosetrv});
			this.tlbSniffing.ButtonSize = new System.Drawing.Size(20, 20);
			this.tlbSniffing.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlbSniffing.DropDownArrows = true;
			this.tlbSniffing.ImageList = this.imltlbSniffing;
			this.tlbSniffing.Location = new System.Drawing.Point(0, 0);
			this.tlbSniffing.Name = "tlbSniffing";
			this.tlbSniffing.ShowToolTips = true;
			this.tlbSniffing.Size = new System.Drawing.Size(536, 32);
			this.tlbSniffing.TabIndex = 12;
			this.tlbSniffing.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tlbSniffing_ButtonClick);
			// 
			// btnStopSniffing
			// 
			this.btnStopSniffing.ImageIndex = 0;
			this.btnStopSniffing.ToolTipText = "Stop Selected Page Sniffing";
			// 
			// btnOpentrv
			// 
			this.btnOpentrv.ImageIndex = 1;
			this.btnOpentrv.ToolTipText = "Open TreeView";
			// 
			// btnClosetrv
			// 
			this.btnClosetrv.ImageIndex = 2;
			this.btnClosetrv.ToolTipText = "Close TreeView";
			// 
			// imltlbSniffing
			// 
			this.imltlbSniffing.ImageSize = new System.Drawing.Size(20, 20);
			this.imltlbSniffing.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imltlbSniffing.ImageStream")));
			this.imltlbSniffing.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tpCtrl
			// 
			this.tpCtrl.Controls.Add(this.pnlError);
			this.tpCtrl.Location = new System.Drawing.Point(4, 22);
			this.tpCtrl.Name = "tpCtrl";
			this.tpCtrl.Size = new System.Drawing.Size(536, 195);
			this.tpCtrl.TabIndex = 1;
			this.tpCtrl.Text = "Control Messages";
			// 
			// pnlError
			// 
			this.pnlError.Controls.Add(this.pnlIcmpView);
			this.pnlError.Controls.Add(this.pnlIcmpTlb);
			this.pnlError.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlError.Location = new System.Drawing.Point(0, 0);
			this.pnlError.Name = "pnlError";
			this.pnlError.Size = new System.Drawing.Size(536, 195);
			this.pnlError.TabIndex = 0;
			// 
			// pnlIcmpView
			// 
			this.pnlIcmpView.Controls.Add(this.CtrlMsgs);
			this.pnlIcmpView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlIcmpView.Location = new System.Drawing.Point(0, 32);
			this.pnlIcmpView.Name = "pnlIcmpView";
			this.pnlIcmpView.Size = new System.Drawing.Size(536, 163);
			this.pnlIcmpView.TabIndex = 1;
			// 
			// CtrlMsgs
			// 
			this.CtrlMsgs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CtrlMsgs.Location = new System.Drawing.Point(0, 0);
			this.CtrlMsgs.Name = "CtrlMsgs";
			this.CtrlMsgs.Size = new System.Drawing.Size(536, 163);
			this.CtrlMsgs.TabIndex = 0;
			this.CtrlMsgs.IcmpDoubleClick += new DoubleClickCallback(this.ControlDoubleClick);
			// 
			// pnlIcmpTlb
			// 
			this.pnlIcmpTlb.Controls.Add(this.tblIcmp);
			this.pnlIcmpTlb.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlIcmpTlb.Location = new System.Drawing.Point(0, 0);
			this.pnlIcmpTlb.Name = "pnlIcmpTlb";
			this.pnlIcmpTlb.Size = new System.Drawing.Size(536, 32);
			this.pnlIcmpTlb.TabIndex = 0;
			// 
			// tblIcmp
			// 
			this.tblIcmp.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.OpenTrv,
																					   this.CloseTrv});
			this.tblIcmp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblIcmp.DropDownArrows = true;
			this.tblIcmp.ImageList = this.imltlbSniffing;
			this.tblIcmp.Location = new System.Drawing.Point(0, 0);
			this.tblIcmp.Name = "tblIcmp";
			this.tblIcmp.ShowToolTips = true;
			this.tblIcmp.Size = new System.Drawing.Size(536, 42);
			this.tblIcmp.TabIndex = 13;
			this.tblIcmp.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tblIcmp_ButtonClick);
			// 
			// OpenTrv
			// 
			this.OpenTrv.ImageIndex = 1;
			this.OpenTrv.ToolTipText = "Open TreeView";
			// 
			// CloseTrv
			// 
			this.CloseTrv.ImageIndex = 2;
			this.CloseTrv.ToolTipText = "Close TreeView";
			// 
			// tlbMain
			// 
			this.tlbMain.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.btnStartSniff,
																					   this.btnStopSniff,
																					   this.btnPause,
																					   this.btnCheck,
																					   this.btnFilter,
																					   this.btnQuit});
			this.tlbMain.ButtonSize = new System.Drawing.Size(20, 20);
			this.tlbMain.DropDownArrows = true;
			this.tlbMain.ImageList = this.imlMain;
			this.tlbMain.Location = new System.Drawing.Point(0, 0);
			this.tlbMain.Name = "tlbMain";
			this.tlbMain.ShowToolTips = true;
			this.tlbMain.Size = new System.Drawing.Size(1028, 28);
			this.tlbMain.TabIndex = 13;
			this.tlbMain.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tlbMain_ButtonClick);
			// 
			// btnStartSniff
			// 
			this.btnStartSniff.ImageIndex = 2;
			this.btnStartSniff.ToolTipText = "Start Sniffing Selected Pair";
			// 
			// btnStopSniff
			// 
			this.btnStopSniff.ImageIndex = 6;
			this.btnStopSniff.ToolTipText = "Stop Sniffing Selected Pair";
			// 
			// btnPause
			// 
			this.btnPause.ImageIndex = 4;
			this.btnPause.ToolTipText = "Pause Sniffing";
			// 
			// btnCheck
			// 
			this.btnCheck.ImageIndex = 3;
			this.btnCheck.ToolTipText = "Check Connection Selected Pair";
			// 
			// btnFilter
			// 
			this.btnFilter.ImageIndex = 10;
			this.btnFilter.ToolTipText = "Filter Selected Pair";
			// 
			// btnQuit
			// 
			this.btnQuit.ImageIndex = 7;
			this.btnQuit.ToolTipText = "Exit";
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.pnlbottom);
			this.pnlMain.Controls.Add(this.splitter3);
			this.pnlMain.Controls.Add(this.pnlUp);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 28);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(1028, 400);
			this.pnlMain.TabIndex = 14;
			// 
			// pnlbottom
			// 
			this.pnlbottom.Controls.Add(this.pnlBottomRight);
			this.pnlbottom.Controls.Add(this.splitter1);
			this.pnlbottom.Controls.Add(this.pnlBottomLeft);
			this.pnlbottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlbottom.Location = new System.Drawing.Point(0, 179);
			this.pnlbottom.Name = "pnlbottom";
			this.pnlbottom.Size = new System.Drawing.Size(1028, 221);
			this.pnlbottom.TabIndex = 2;
			// 
			// pnlBottomRight
			// 
			this.pnlBottomRight.Controls.Add(this.pnlBottomRightSub);
			this.pnlBottomRight.Controls.Add(this.panel1);
			this.pnlBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBottomRight.Location = new System.Drawing.Point(547, 0);
			this.pnlBottomRight.Name = "pnlBottomRight";
			this.pnlBottomRight.Size = new System.Drawing.Size(481, 221);
			this.pnlBottomRight.TabIndex = 15;
			// 
			// pnlBottomRightSub
			// 
			this.pnlBottomRightSub.Controls.Add(this.splitter2);
			this.pnlBottomRightSub.Controls.Add(this.pnlRichText);
			this.pnlBottomRightSub.Controls.Add(this.pnlTV);
			this.pnlBottomRightSub.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBottomRightSub.Location = new System.Drawing.Point(0, 20);
			this.pnlBottomRightSub.Name = "pnlBottomRightSub";
			this.pnlBottomRightSub.Size = new System.Drawing.Size(481, 201);
			this.pnlBottomRightSub.TabIndex = 3;
			// 
			// splitter2
			// 
			this.splitter2.Location = new System.Drawing.Point(280, 0);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(4, 201);
			this.splitter2.TabIndex = 4;
			this.splitter2.TabStop = false;
			// 
			// pnlRichText
			// 
			this.pnlRichText.Controls.Add(this.splitter5);
			this.pnlRichText.Controls.Add(this.pnlPacket);
			this.pnlRichText.Controls.Add(this.pnlAscii);
			this.pnlRichText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlRichText.Location = new System.Drawing.Point(280, 0);
			this.pnlRichText.Name = "pnlRichText";
			this.pnlRichText.Size = new System.Drawing.Size(201, 201);
			this.pnlRichText.TabIndex = 3;
			// 
			// splitter5
			// 
			this.splitter5.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter5.Location = new System.Drawing.Point(0, 160);
			this.splitter5.Name = "splitter5";
			this.splitter5.Size = new System.Drawing.Size(201, 3);
			this.splitter5.TabIndex = 5;
			this.splitter5.TabStop = false;
			// 
			// pnlPacket
			// 
			this.pnlPacket.Controls.Add(this.RTBPacket);
			this.pnlPacket.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlPacket.Location = new System.Drawing.Point(0, 160);
			this.pnlPacket.Name = "pnlPacket";
			this.pnlPacket.Size = new System.Drawing.Size(201, 41);
			this.pnlPacket.TabIndex = 4;
			// 
			// RTBPacket
			// 
			this.RTBPacket.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RTBPacket.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.RTBPacket.Location = new System.Drawing.Point(0, 0);
			this.RTBPacket.Name = "RTBPacket";
			this.RTBPacket.Size = new System.Drawing.Size(201, 41);
			this.RTBPacket.TabIndex = 0;
			this.RTBPacket.Text = "";
			this.RTBPacket.WordWrap = false;
			// 
			// pnlAscii
			// 
			this.pnlAscii.Controls.Add(this.RTBAscii);
			this.pnlAscii.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlAscii.Location = new System.Drawing.Point(0, 0);
			this.pnlAscii.Name = "pnlAscii";
			this.pnlAscii.Size = new System.Drawing.Size(201, 160);
			this.pnlAscii.TabIndex = 3;
			// 
			// RTBAscii
			// 
			this.RTBAscii.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RTBAscii.Location = new System.Drawing.Point(0, 0);
			this.RTBAscii.Name = "RTBAscii";
			this.RTBAscii.Size = new System.Drawing.Size(201, 160);
			this.RTBAscii.TabIndex = 1;
			this.RTBAscii.Text = "";
			// 
			// pnlTV
			// 
			this.pnlTV.Controls.Add(this.trvPacket);
			this.pnlTV.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlTV.Location = new System.Drawing.Point(0, 0);
			this.pnlTV.Name = "pnlTV";
			this.pnlTV.Size = new System.Drawing.Size(280, 201);
			this.pnlTV.TabIndex = 2;
			// 
			// trvPacket
			// 
			this.trvPacket.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trvPacket.ImageList = this.imltblTreeView;
			this.trvPacket.Location = new System.Drawing.Point(0, 0);
			this.trvPacket.Name = "trvPacket";
			this.trvPacket.Size = new System.Drawing.Size(280, 201);
			this.trvPacket.TabIndex = 0;
			this.trvPacket.Click += new System.EventHandler(this.trvPacket_Click);
			this.trvPacket.MouseMove += new System.Windows.Forms.MouseEventHandler(this.trvPacket_MouseMove);
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(481, 20);
			this.panel1.TabIndex = 1;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(544, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 221);
			this.splitter1.TabIndex = 14;
			this.splitter1.TabStop = false;
			// 
			// pnlBottomLeft
			// 
			this.pnlBottomLeft.Controls.Add(this.tcMain);
			this.pnlBottomLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlBottomLeft.Location = new System.Drawing.Point(0, 0);
			this.pnlBottomLeft.Name = "pnlBottomLeft";
			this.pnlBottomLeft.Size = new System.Drawing.Size(544, 221);
			this.pnlBottomLeft.TabIndex = 13;
			// 
			// splitter3
			// 
			this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter3.Location = new System.Drawing.Point(0, 176);
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new System.Drawing.Size(1028, 3);
			this.splitter3.TabIndex = 1;
			this.splitter3.TabStop = false;
			// 
			// pnlUp
			// 
			this.pnlUp.Controls.Add(this.pnlUpRight);
			this.pnlUp.Controls.Add(this.splitter4);
			this.pnlUp.Controls.Add(this.pnlUpLeft);
			this.pnlUp.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlUp.Location = new System.Drawing.Point(0, 0);
			this.pnlUp.Name = "pnlUp";
			this.pnlUp.Size = new System.Drawing.Size(1028, 176);
			this.pnlUp.TabIndex = 0;
			// 
			// pnlUpRight
			// 
			this.pnlUpRight.Controls.Add(this.label4);
			this.pnlUpRight.Controls.Add(this.label5);
			this.pnlUpRight.Controls.Add(this.label6);
			this.pnlUpRight.Controls.Add(this.label7);
			this.pnlUpRight.Controls.Add(this.lblb5);
			this.pnlUpRight.Controls.Add(this.lblb4);
			this.pnlUpRight.Controls.Add(this.lblb3);
			this.pnlUpRight.Controls.Add(this.lblb2);
			this.pnlUpRight.Controls.Add(this.lblb1);
			this.pnlUpRight.Controls.Add(this.label3);
			this.pnlUpRight.Controls.Add(this.label2);
			this.pnlUpRight.Controls.Add(this.label1);
			this.pnlUpRight.Controls.Add(this.label15);
			this.pnlUpRight.Controls.Add(this.lbln9);
			this.pnlUpRight.Controls.Add(this.label13);
			this.pnlUpRight.Controls.Add(this.lbln8);
			this.pnlUpRight.Controls.Add(this.label11);
			this.pnlUpRight.Controls.Add(this.lbln7);
			this.pnlUpRight.Controls.Add(this.label9);
			this.pnlUpRight.Controls.Add(this.lbln6);
			this.pnlUpRight.Controls.Add(this.lblv5);
			this.pnlUpRight.Controls.Add(this.lbln5);
			this.pnlUpRight.Controls.Add(this.lblv4);
			this.pnlUpRight.Controls.Add(this.lbln4);
			this.pnlUpRight.Controls.Add(this.lblv3);
			this.pnlUpRight.Controls.Add(this.lbln3);
			this.pnlUpRight.Controls.Add(this.lblv2);
			this.pnlUpRight.Controls.Add(this.lbln2);
			this.pnlUpRight.Controls.Add(this.lblv1);
			this.pnlUpRight.Controls.Add(this.lbln1);
			this.pnlUpRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlUpRight.Location = new System.Drawing.Point(907, 0);
			this.pnlUpRight.Name = "pnlUpRight";
			this.pnlUpRight.Size = new System.Drawing.Size(121, 176);
			this.pnlUpRight.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(152, 160);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 16);
			this.label4.TabIndex = 29;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(152, 144);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 16);
			this.label5.TabIndex = 28;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(152, 128);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(88, 16);
			this.label6.TabIndex = 27;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(152, 112);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(88, 16);
			this.label7.TabIndex = 26;
			// 
			// lblb5
			// 
			this.lblb5.Location = new System.Drawing.Point(152, 96);
			this.lblb5.Name = "lblb5";
			this.lblb5.Size = new System.Drawing.Size(88, 16);
			this.lblb5.TabIndex = 25;
			this.lblb5.Text = "0";
			// 
			// lblb4
			// 
			this.lblb4.Location = new System.Drawing.Point(152, 80);
			this.lblb4.Name = "lblb4";
			this.lblb4.Size = new System.Drawing.Size(88, 16);
			this.lblb4.TabIndex = 24;
			this.lblb4.Text = "0";
			// 
			// lblb3
			// 
			this.lblb3.Location = new System.Drawing.Point(152, 64);
			this.lblb3.Name = "lblb3";
			this.lblb3.Size = new System.Drawing.Size(88, 16);
			this.lblb3.TabIndex = 23;
			this.lblb3.Text = "0";
			// 
			// lblb2
			// 
			this.lblb2.Location = new System.Drawing.Point(152, 48);
			this.lblb2.Name = "lblb2";
			this.lblb2.Size = new System.Drawing.Size(88, 16);
			this.lblb2.TabIndex = 22;
			this.lblb2.Text = "0";
			// 
			// lblb1
			// 
			this.lblb1.Location = new System.Drawing.Point(152, 32);
			this.lblb1.Name = "lblb1";
			this.lblb1.Size = new System.Drawing.Size(88, 16);
			this.lblb1.TabIndex = 21;
			this.lblb1.Text = "0";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.label3.Location = new System.Drawing.Point(152, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 20;
			this.label3.Text = "Bytes";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.label2.Location = new System.Drawing.Point(72, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 19;
			this.label2.Text = "Count";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 18;
			this.label1.Text = "Protocol";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(80, 160);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(64, 16);
			this.label15.TabIndex = 17;
			// 
			// lbln9
			// 
			this.lbln9.Location = new System.Drawing.Point(16, 160);
			this.lbln9.Name = "lbln9";
			this.lbln9.Size = new System.Drawing.Size(48, 16);
			this.lbln9.TabIndex = 16;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(80, 144);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(64, 16);
			this.label13.TabIndex = 15;
			// 
			// lbln8
			// 
			this.lbln8.Location = new System.Drawing.Point(16, 144);
			this.lbln8.Name = "lbln8";
			this.lbln8.Size = new System.Drawing.Size(48, 16);
			this.lbln8.TabIndex = 14;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(80, 128);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(64, 16);
			this.label11.TabIndex = 13;
			// 
			// lbln7
			// 
			this.lbln7.Location = new System.Drawing.Point(16, 128);
			this.lbln7.Name = "lbln7";
			this.lbln7.Size = new System.Drawing.Size(48, 16);
			this.lbln7.TabIndex = 12;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(80, 112);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(64, 16);
			this.label9.TabIndex = 11;
			// 
			// lbln6
			// 
			this.lbln6.Location = new System.Drawing.Point(16, 112);
			this.lbln6.Name = "lbln6";
			this.lbln6.Size = new System.Drawing.Size(48, 16);
			this.lbln6.TabIndex = 10;
			// 
			// lblv5
			// 
			this.lblv5.Location = new System.Drawing.Point(80, 96);
			this.lblv5.Name = "lblv5";
			this.lblv5.Size = new System.Drawing.Size(64, 16);
			this.lblv5.TabIndex = 9;
			this.lblv5.Text = "0";
			// 
			// lbln5
			// 
			this.lbln5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.lbln5.Location = new System.Drawing.Point(16, 96);
			this.lbln5.Name = "lbln5";
			this.lbln5.Size = new System.Drawing.Size(48, 16);
			this.lbln5.TabIndex = 8;
			this.lbln5.Text = "Other";
			// 
			// lblv4
			// 
			this.lblv4.Location = new System.Drawing.Point(80, 80);
			this.lblv4.Name = "lblv4";
			this.lblv4.Size = new System.Drawing.Size(64, 16);
			this.lblv4.TabIndex = 7;
			this.lblv4.Text = "0";
			// 
			// lbln4
			// 
			this.lbln4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.lbln4.Location = new System.Drawing.Point(16, 80);
			this.lbln4.Name = "lbln4";
			this.lbln4.Size = new System.Drawing.Size(48, 16);
			this.lbln4.TabIndex = 6;
			this.lbln4.Text = "Icmp";
			// 
			// lblv3
			// 
			this.lblv3.Location = new System.Drawing.Point(80, 64);
			this.lblv3.Name = "lblv3";
			this.lblv3.Size = new System.Drawing.Size(64, 16);
			this.lblv3.TabIndex = 5;
			this.lblv3.Text = "0";
			// 
			// lbln3
			// 
			this.lbln3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.lbln3.Location = new System.Drawing.Point(16, 64);
			this.lbln3.Name = "lbln3";
			this.lbln3.Size = new System.Drawing.Size(48, 16);
			this.lbln3.TabIndex = 4;
			this.lbln3.Text = "Udp";
			// 
			// lblv2
			// 
			this.lblv2.Location = new System.Drawing.Point(80, 48);
			this.lblv2.Name = "lblv2";
			this.lblv2.Size = new System.Drawing.Size(64, 16);
			this.lblv2.TabIndex = 3;
			this.lblv2.Text = "0";
			// 
			// lbln2
			// 
			this.lbln2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.lbln2.Location = new System.Drawing.Point(16, 48);
			this.lbln2.Name = "lbln2";
			this.lbln2.Size = new System.Drawing.Size(48, 16);
			this.lbln2.TabIndex = 2;
			this.lbln2.Text = "Tcp";
			// 
			// lblv1
			// 
			this.lblv1.Location = new System.Drawing.Point(80, 32);
			this.lblv1.Name = "lblv1";
			this.lblv1.Size = new System.Drawing.Size(64, 16);
			this.lblv1.TabIndex = 1;
			this.lblv1.Text = "0";
			// 
			// lbln1
			// 
			this.lbln1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.lbln1.Location = new System.Drawing.Point(16, 32);
			this.lbln1.Name = "lbln1";
			this.lbln1.Size = new System.Drawing.Size(48, 16);
			this.lbln1.TabIndex = 0;
			this.lbln1.Text = "ALL";
			// 
			// splitter4
			// 
			this.splitter4.Location = new System.Drawing.Point(904, 0);
			this.splitter4.Name = "splitter4";
			this.splitter4.Size = new System.Drawing.Size(3, 176);
			this.splitter4.TabIndex = 7;
			this.splitter4.TabStop = false;
			// 
			// pnlUpLeft
			// 
			this.pnlUpLeft.Controls.Add(this.lstwMain);
			this.pnlUpLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlUpLeft.Location = new System.Drawing.Point(0, 0);
			this.pnlUpLeft.Name = "pnlUpLeft";
			this.pnlUpLeft.Size = new System.Drawing.Size(904, 176);
			this.pnlUpLeft.TabIndex = 6;
			// 
			// mmain
			// 
			this.mmain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				  this.miFile,
																				  this.miEdit,
																				  this.miTools});
			// 
			// miFile
			// 
			this.miFile.Index = 0;
			this.miFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.miExit});
			this.miFile.Text = "File";
			// 
			// miExit
			// 
			this.miExit.Index = 0;
			this.miExit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
			this.miExit.Text = "Exit";
			this.miExit.Click += new System.EventHandler(this.miExit_Click);
			// 
			// miEdit
			// 
			this.miEdit.Index = 1;
			this.miEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.miAddIP,
																				   this.miAddProtocol,
																				   this.miFiltering,
																				   this.menuItem1,
																				   this.miMode});
			this.miEdit.Text = "Edit";
			// 
			// miAddIP
			// 
			this.miAddIP.Index = 0;
			this.miAddIP.Text = "Sniffing Addresses";
			this.miAddIP.Click += new System.EventHandler(this.miAddIP_Click);
			// 
			// miAddProtocol
			// 
			this.miAddProtocol.Index = 1;
			this.miAddProtocol.Text = "Add Protocol";
			this.miAddProtocol.Click += new System.EventHandler(this.miAddProtocol_Click);
			// 
			// miFiltering
			// 
			this.miFiltering.Index = 2;
			this.miFiltering.Text = "Filter Manager";
			this.miFiltering.Click += new System.EventHandler(this.miFiltering_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 3;
			this.menuItem1.Text = "-";
			// 
			// miMode
			// 
			this.miMode.Index = 4;
			this.miMode.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.miManual,
																				   this.miAutomatic});
			this.miMode.Text = "Mode";
			// 
			// miManual
			// 
			this.miManual.Checked = ((bool)(configurationAppSettings.GetValue("miManual.Checked", typeof(bool))));
			this.miManual.Index = 0;
			this.miManual.Text = "Manual";
			this.miManual.Click += new System.EventHandler(this.miManual_Click);
			// 
			// miAutomatic
			// 
			this.miAutomatic.Checked = ((bool)(configurationAppSettings.GetValue("miAutomatic.Checked", typeof(bool))));
			this.miAutomatic.Index = 1;
			this.miAutomatic.Text = "Automatic";
			this.miAutomatic.Click += new System.EventHandler(this.miAutomatic_Click);
			// 
			// miTools
			// 
			this.miTools.Index = 2;
			this.miTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.miNetStat,
																					this.miPing,
																					this.miTraceR,
																					this.miCheckCon,
																					this.miSetting,
																					this.menuItem2,
																					this.menuItem3});
			this.miTools.Text = "Tools";
			// 
			// miNetStat
			// 
			this.miNetStat.Index = 0;
			this.miNetStat.Text = "NetStat";
			this.miNetStat.Click += new System.EventHandler(this.miNetStat_Click);
			// 
			// miPing
			// 
			this.miPing.Index = 1;
			this.miPing.Text = "Ping";
			this.miPing.Click += new System.EventHandler(this.miPing_Click);
			// 
			// miTraceR
			// 
			this.miTraceR.Index = 2;
			this.miTraceR.Text = "Trace Route";
			this.miTraceR.Click += new System.EventHandler(this.miTraceR_Click);
			// 
			// miCheckCon
			// 
			this.miCheckCon.Index = 3;
			this.miCheckCon.Text = "Check Connection";
			this.miCheckCon.Click += new System.EventHandler(this.miCheckCon_Click);
			// 
			// miSetting
			// 
			this.miSetting.Index = 4;
			this.miSetting.Text = "Settings";
			this.miSetting.Click += new System.EventHandler(this.miSetting_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 5;
			this.menuItem2.Text = "-";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 6;
			this.menuItem3.Text = "Start Port Scanner";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// scanner
			// 
			this.scanner.StartInfo.FileName = "PortScanner.exe";
			this.scanner.SynchronizingObject = this;
			// 
			// SnifferUI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(1028, 450);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.tlbMain);
			this.Controls.Add(this.statusBar1);
			this.Menu = this.mmain;
			this.MinimumSize = new System.Drawing.Size(800, 477);
			this.Name = "SnifferUI";
			this.Text = "SnifferUI";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.SnifferUI_Load);
			this.tcMain.ResumeLayout(false);
			this.tpSniffing.ResumeLayout(false);
			this.pnlSniffing.ResumeLayout(false);
			this.pnlSubTab.ResumeLayout(false);
			this.pnlToolBarTab.ResumeLayout(false);
			this.tpCtrl.ResumeLayout(false);
			this.pnlError.ResumeLayout(false);
			this.pnlIcmpView.ResumeLayout(false);
			this.pnlIcmpTlb.ResumeLayout(false);
			this.pnlMain.ResumeLayout(false);
			this.pnlbottom.ResumeLayout(false);
			this.pnlBottomRight.ResumeLayout(false);
			this.pnlBottomRightSub.ResumeLayout(false);
			this.pnlRichText.ResumeLayout(false);
			this.pnlPacket.ResumeLayout(false);
			this.pnlAscii.ResumeLayout(false);
			this.pnlTV.ResumeLayout(false);
			this.pnlBottomLeft.ResumeLayout(false);
			this.pnlUp.ResumeLayout(false);
			this.pnlUpRight.ResumeLayout(false);
			this.pnlUpLeft.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private String GetIPToSniff() 
		{ 
			DialogResult result;
			String ip = null;
			AddressInput input = new AddressInput();
			result = input.ShowDialog(this);
			if ( result == DialogResult.OK ) 
			{ 
				ip = input.IP.ToString();
			}
			else if(result == DialogResult.Cancel){
				System.Environment.Exit(1);
			}
			input.Dispose();
			return ip;
		}


		private void addIp(){
			DialogResult result;
			AddIP input = new AddIP(Socket_.IPs);
			result = input.ShowDialog(this);
			if ( result == DialogResult.OK ) 
			{ 
				Socket_.IPs.Clear();
				for(int i=0;i<input.ips.Count;i++)
					Socket_.IPs.Add(input.ips[i]);
			}
			else if(result == DialogResult.Cancel)
			{
				return;
			}
			input.Dispose();
		}


		private void miAddIP_Click(object sender, System.EventArgs e)
		{
			addIp();
		}


		private void PauseSniffing()
		{
			if (!isPaused)
			{
				Socket_.Pause=true;
				isPaused=true;
			}
		}


		private void ResumeSniffing()
		{
			if (isPaused)
			{
				Socket_.resumeSocket();
				isPaused=false;
			}
		}

		void Counter2()
		{
			while(true)
			{
				foreach (string key in IdentTable_.Keys )
				{
					ListViewItem item = (ListViewItem)IdentTable_[key];
					item.SubItems[0].Text=Convert.ToString(Counter_[key]);
				}
				Thread.Sleep(1200);
			}
		
		}

		private void HandleIPv4Datagram(IPv4Datagram datagram) 
		{ 
			string ident = datagram.GetHashString();

			if ( !IdentTable_.Contains(ident) ) 
			{
				datagram.Proc=tablesForProc.getProcessId(datagram,this.thisComputerIp);
				datagram.ProcStr=tablesForProc.getProcessName(datagram.Proc);

				ListViewItem item = new ListViewItem("1");
				item.SubItems.Add(datagram.SourceIP);
				item.SubItems.Add(datagram.SourceName);
				item.SubItems.Add(datagram.SPort.ToString()+":{ "+consts.GetTcpPorts(datagram.SPort)+" }");
				item.SubItems.Add(datagram.DestinationIP);
				item.SubItems.Add(datagram.DestinationName);
				item.SubItems.Add(datagram.DPort.ToString()+":{ "+consts.GetTcpPorts(datagram.DPort)+" }");
				item.SubItems.Add(datagram.GetUpperProtocol());
				item.SubItems.Add(datagram.ProcStr);
				item.SubItems.Add(datagram.Proc.ToString());
				item.ImageIndex=8;

				Counter_.Add(ident,1);

				IdentTable_.Add(ident,item);					
				lstwMain.Items.Add(item);
				lstwMain.Update();
				
			}
			else
			{
				//int ind =lstwMain.Items.IndexOf((ListViewItem)IdentTable_[ident]);
				//lstwMain.Items[ind].SubItems[0].Text=Convert.ToString(Convert.ToInt32(lstwMain.Items[ind].SubItems[0].Text)+1);
				//datagram.ProcStr=lstwMain.Items[ind].SubItems[8].Text;
				//datagram.Proc=Convert.ToInt32(lstwMain.Items[ind].SubItems[9].Text);

				ListViewItem item = (ListViewItem)IdentTable_[ident];

				Counter_[ident]=(object)((int)Counter_[ident]+1);

				datagram.ProcStr=item.SubItems[8].Text;
				datagram.Proc=Convert.ToInt32(item.SubItems[9].Text);
			}

			if (miAutomatic.Checked)
			{
				addThisDatagram(datagram,ident);
			}
			else
			{
				if (CurrentSniffing_.Contains(ident))
				{
					addThisDatagram(datagram,ident);
				}
			}
			switch ( datagram.Protocol ) 
			{ 
				case 1:
					CtrlMsgs.HandleIcmpPacket(datagram);
					icmpCount++;
					icmpByte+=datagram.Length;
					break;
				case 6:
					tcpCount++;
					tcpByte+=datagram.Length;
					break;
				case 17:
					udpCount++;
					udpByte+=datagram.Length;
					break;
				default:
					otherCount++;
					otherByte+=datagram.Length;
					break;
			}
			totalCount++;
			totalByte+=datagram.Length;
		}


		private void addThisDatagram(IPv4Datagram datagram,string identIpV4)
		{
			String ident="";
			switch ( datagram.Protocol ) 
			{ 
				case 1://ICMP
					ident = datagram.GetPacketHashString();
					icmpView icmpview = null;
					if ( TabPageTable_.Contains(identIpV4) ) 
					{ 
						icmpview = (icmpView)TabPageTable_[identIpV4];
					} 
					else 
					{ 
						icmpview = new icmpView(/*datagram.Source,datagram.Destination*/);
						icmpview.IcmpDoubleClick+=new DoubleClickCallback(this.OpenTreeView);
						TabPage tp = new TabPage(ident);
						tp.Tag=identIpV4;
						TabPageTable_.Add(identIpV4,icmpview);
						tp.Controls.Add(icmpview);
						this.Invoke(new TabPageCallback(this.AddTabPage),new object[]{tp});
						
					}
					icmpview.HandleIcmpPacket(datagram);	
					break;
				case 6://TCP
					ident = datagram.GetPacketHashString();
					tcpView tview = null;
					if ( TabPageTable_.Contains(identIpV4) ) 
					{ 
						tview = (tcpView)TabPageTable_[identIpV4];
					} 
					else 
					{ 
						tview = new tcpView(datagram.Source,datagram.Destination);
						tview.TcpDoubleClick+=new DoubleClickCallback(this.OpenTreeView);
						TabPage tp = new TabPage(ident);
						tp.Tag=identIpV4;
						TabPageTable_.Add(identIpV4,tview);
						tp.Controls.Add(tview);
						this.Invoke(new TabPageCallback(this.AddTabPage),new object[]{tp});
						
					}
					tview.HandleTcpPacket(datagram);
					break;
				case 17://UDP
					ident = datagram.GetPacketHashString();
					udpView uview = null;
					if ( TabPageTable_.Contains(identIpV4) ) 
					{ 
						uview = (udpView)TabPageTable_[identIpV4];
					} 
					else 
					{ 
						uview = new udpView(datagram.Source,datagram.Destination);
						uview.UdpDoubleClick+=new DoubleClickCallback(this.OpenTreeView);
						TabPage tp = new TabPage(ident);
						tp.Tag=identIpV4;
						TabPageTable_.Add(identIpV4,uview);
						tp.Controls.Add(uview);
						this.Invoke(new TabPageCallback(this.AddTabPage),new object[]{tp});
					}
					uview.HandleUdpDatagram(datagram);
					break;
				default:
//***********************************************************
					ident = datagram.GetPacketHashString();
					OtherProtocols Oview = null;
					if ( TabPageTable_.Contains(identIpV4) ) 
					{ 
						Oview = (OtherProtocols)TabPageTable_[identIpV4];
					} 
					else 
					{ 
						Oview = new OtherProtocols(datagram.Source,datagram.Destination,datagram.GetUpperProtocol());
						Oview.OtherDoubleClick+=new DoubleClickCallback(this.OpenTreeView);
						TabPage tp = new TabPage(ident);
						tp.Tag=identIpV4;
						TabPageTable_.Add(identIpV4,Oview);
						tp.Controls.Add(Oview);
						this.Invoke(new TabPageCallback(this.AddTabPage),new object[]{tp});
					}
					Oview.HandleOthers(datagram);
//***********************************************************
					break;
			}
		}


		private void HandleSnifferError(SnifferException e ) 
		{ 
			//MessageBox.Show(this,e.Message+System.Environment.NewLine+e.StackTrace,"Sniffer Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
		}


		private void LoadOptions() 
		{ 
			lock ( Socket_ ) 
			{ 
				Socket_.IPv4DatagramReceived += new IPv4DatagramCallback(this.HandleIPv4Datagram);
				Socket_.SnifferError += new SnifferErrorCallback(this.HandleSnifferError);
			}
		}


		[STAThread]
		static void Main ()
		{
			Application.Run(new SnifferUI());
		}


		private void changeVisible(bool show){
			if (show)
			{
				this.pnlBottomLeft.Dock=System.Windows.Forms.DockStyle.Left;
				this.pnlBottomRight.Visible=true;
			}
			else
			{
				this.pnlBottomLeft.Dock=System.Windows.Forms.DockStyle.Fill;
				this.pnlBottomRight.Visible=false;
			}

		}


		private void SnifferUI_Load(object sender, System.EventArgs e)
		{
			this.changeVisible(false);
		}


/***************** Context menu funtions******************/

		private void miStartSniffing_Click(object sender, System.EventArgs e)//sniff
		{
			this.startSniffing();
		}


		private void miStopSniffing_Click(object sender, System.EventArgs e)
		{
			stopSniffing2();
		}


		private void miConCheck_Click(object sender, System.EventArgs e)
		{
			checkCon();
		}


		private void midelete_Click(object sender, System.EventArgs e)
		{
            deleteFromMainlist();
		}


		private void mideleteall_Click(object sender, System.EventArgs e)
		{
			deleteAllFromMainlist();
		}


/***************** End Context menu funtions******************/

		private void deleteFromMainlist()
		{
			trio objTrio = GetSelectedItem(lstwMain);
			if(objTrio.checkAvailable())
			{
				string ident = objTrio.GetHashString();
				if (IdentTable_.Contains(ident))
				{
					this.lstwMain.Items.Remove((ListViewItem)IdentTable_[ident]);
					IdentTable_.Remove(ident);
					Counter_.Remove(ident);
					lstwMain.Update();
					if (CurrentSniffing_.Contains(ident))
					{
						CurrentSniffing_.Remove(ident);
						for (int i=0;i<this.tcSniffing.TabCount;i++)
						{
							if (this.tcSniffing.TabPages[i].Tag.ToString()==ident)
							{
								TabPageTable_.Remove(ident);
								tcSniffing.TabPages.Remove(this.tcSniffing.TabPages[i]);
								break;
							}
						}
					}
				}
			}
		
		}


		private void deleteAllFromMainlist()
		{
			this.lstwMain.Items.Clear();
			IdentTable_.Clear();
			Counter_.Clear();
			lstwMain.Update();
			CurrentSniffing_.Clear();
			TabPageTable_.Clear();
			tcSniffing.TabPages.Clear();

			totalCount=0;totalByte=0;
			tcpCount=0;tcpByte=0;
			udpCount=0;udpByte=0;
			icmpCount=0;icmpByte=0;
			otherCount=0;otherByte=0;
			lblv1.Text=totalCount.ToString();
			lblv2.Text=tcpCount.ToString();
			lblv3.Text=udpCount.ToString();
			lblv4.Text=icmpCount.ToString();
			lblv5.Text=otherCount.ToString();
			lblb1.Text=totalByte.ToString()+" Bytes";
			lblb2.Text=tcpByte.ToString()+" Bytes";
			lblb3.Text=udpByte.ToString()+" Bytes";
			lblb4.Text=icmpByte.ToString()+" Bytes";
			lblb5.Text=otherByte.ToString()+" Bytes";
		}


		private trio GetSelectedItem(ListView LV){
			trio objTrio = new trio();
			ListView.SelectedListViewItemCollection sItems =LV.SelectedItems;
			foreach ( ListViewItem item in sItems )
			{
				objTrio.Source = item.SubItems[1].Text;
				objTrio.SourcePort = item.SubItems[3].Text.Substring(0,item.SubItems[3].Text.IndexOf(":"));
				objTrio.Destination = item.SubItems[4].Text;
				objTrio.DestinationPort = item.SubItems[6].Text.Substring(0,item.SubItems[6].Text.IndexOf(":"));
				objTrio.Protocol = item.SubItems[7].Text;
				objTrio.ProcStr=item.SubItems[8].Text;
				objTrio.Proc=Convert.ToInt32(item.SubItems[9].Text);
			}
			return objTrio;
		}

		
		private void AddTabPage(TabPage tp) 
		{ 
			tp.Resize += new System.EventHandler(this.TabResized);
			tcSniffing.Controls.Add(tp);
		}


		private void TabResized(Object sender, EventArgs e) 
		{ 
			TabPage tp = (TabPage)sender;
			Control c = tp.Controls[0];
			c.Size = tp.Size;
		}


		private void tlbSniffing_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			// Evaluate the Button property to determine which button was clicked.
			switch(tlbSniffing.Buttons.IndexOf(e.Button))
			{
				case 0:
					stopSniffing();
					break; 
				case 1:
					OpenTreeView();
					break; 
				case 2:
					CloseTreeView();
					break; 
			}

		}


		private void startSniffing()
		{
			trio objTrio = GetSelectedItem(lstwMain);
			if(objTrio.checkAvailable())
			{
				string ident = objTrio.GetHashString();
				if (!CurrentSniffing_.Contains(ident))
				{
					CurrentSniffing_.Add(ident,objTrio.Protocol);
					lstwMain.SelectedItems[0].ImageIndex=9;
					lstwMain.Update();
				}
			}
		
		}


		private void stopSniffing()
		{
			if (this.tcSniffing.TabCount==0) return;
			//string ident=this.tcSniffing.SelectedTab.Text;
			string identIpV4=this.tcSniffing.SelectedTab.Tag.ToString();

			if (CurrentSniffing_.Contains(identIpV4))
			{
				lstwMain.Items[lstwMain.Items.IndexOf((ListViewItem)IdentTable_[identIpV4])].ImageIndex=8;
				CurrentSniffing_.Remove(identIpV4);
				TabPageTable_.Remove(identIpV4);
				tcSniffing.TabPages.Remove(tcSniffing.SelectedTab);
				GC.Collect();
			}
			else{
				TabPageTable_.Remove(identIpV4);
				tcSniffing.TabPages.Remove(tcSniffing.SelectedTab);
				GC.Collect();
			}
		}


		private void stopSniffing2()
		{
			trio objTrio = GetSelectedItem(lstwMain);
			if(objTrio.checkAvailable())
			{
				string ident = objTrio.GetHashString();
				if (CurrentSniffing_.Contains(ident))
				{
					CurrentSniffing_.Remove(ident);
					lstwMain.SelectedItems[0].ImageIndex=8;
					lstwMain.Update();
					for (int i=0;i<this.tcSniffing.TabCount;i++)
					{
						if (this.tcSniffing.TabPages[i].Tag.ToString()==ident)
						{
							TabPageTable_.Remove(ident);
							tcSniffing.TabPages.Remove(this.tcSniffing.TabPages[i]);
							break;
						}
					}
				}
			}
		}


		private void OpenTreeView()
		{
			if (this.tcSniffing.TabCount==0) return;
			string ident=this.tcSniffing.SelectedTab.Text;
			string identIpV4=this.tcSniffing.SelectedTab.Tag.ToString();
			IPv4Datagram datagram=null;
			
			switch (ident.Substring(0,ident.IndexOf(":")))
			{
				case "Tcp":
					tcpView tview=(tcpView)TabPageTable_[identIpV4];
					datagram=tview.GetSelectedItem();
					if (datagram==null)return;
					pnlBottomLeft.Dock = System.Windows.Forms.DockStyle.Left;
					pnlBottomRight.Visible = true;
					objTVF.addPacket(datagram,ref this.trvPacket);
					break;
				case "Udp":
					udpView uview=(udpView)TabPageTable_[identIpV4];
					datagram=uview.GetSelectedItem();
					if (datagram==null)return;
					pnlBottomLeft.Dock = System.Windows.Forms.DockStyle.Left;
					pnlBottomRight.Visible = true;
					objTVF.addPacket(datagram,ref this.trvPacket);

					break;
				case "ICMP":
					icmpView iview=(icmpView)TabPageTable_[identIpV4];
					datagram=iview.GetSelectedItem();
					if (datagram==null)return;
					pnlBottomLeft.Dock = System.Windows.Forms.DockStyle.Left;
					pnlBottomRight.Visible = true;
					objTVF.addPacket(datagram,ref this.trvPacket);
					break;
				default:

					OtherProtocols oview=(OtherProtocols)TabPageTable_[identIpV4];
					datagram=oview.GetSelectedItem();
					if (datagram==null)return;
					pnlBottomLeft.Dock = System.Windows.Forms.DockStyle.Left;
					pnlBottomRight.Visible = true;
					objTVF.addPacket(datagram,ref this.trvPacket);

					//****************************************************************
					break;
			}
			if (tempDatagram.Header!=null)
			{
				thrdTrv.Abort();
			}
			tempDatagram=datagram;

			thrdTrv = new Thread(new ThreadStart(writeToRTB));
			thrdTrv.IsBackground = true;
			thrdTrv.Start();
		}


		private void OpenTreeView(bool check)
		{
			IPv4Datagram datagram=null;
			datagram=CtrlMsgs.GetSelectedItem();
			if (datagram==null)return;
			pnlBottomLeft.Dock = System.Windows.Forms.DockStyle.Left;
			pnlBottomRight.Visible = true;
			objTVF.addPacket(datagram,ref this.trvPacket);

			if (tempDatagram.Header!=null)
			{
				thrdTrv.Abort();
			}

			tempDatagram=datagram;

			thrdTrv = new Thread(new ThreadStart(writeToRTB));
			thrdTrv.IsBackground = true;
			thrdTrv.Start();
		}


		private void writeToRTB()
		{
			string str1="",str2="";

			string [] hexTable= {"0","1","2","3","4","5","6","7","8","9","A","B","C","D","E","F"};
			byte[]byteA=tempDatagram.GetBinaryPacket();
			int innerDataLenght = tempDatagram.GetInnerDataLength();
			BitArray ba=new BitArray(byteA);

			for (int i=0;i<byteA.Length/*-innerDataLenght*/;i++)
			{
				if (i<byteA.Length-innerDataLenght)
				{
					str1=str1+hexTable[(int)byteA[i]/16];
					str1=str1+hexTable[(int)byteA[i]%16]+" ";
				}
				else{
					if (byteA[i] > 31)
						str1+=(char)byteA[i];
					else
						str1+=".";
					str1+=" ";
				}

				for (int j=7;j>=0;j--)
				{
					str2=str2+(ba[i*8+j]?"1":"0");
				}

				if ((i+1) % 4==0) str2=str2+'\n';
			}

			RTBPacket.Text=str2;
			RTBAscii.Text=str1;
		}


		private void CloseTreeView()
		{
			pnlBottomLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			pnlBottomRight.Visible = false;
		}


		private void trvPacket_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			MouseX = e.X;
			MouseY = e.Y;
		}


		private void trvPacket_Click(object sender, System.EventArgs e)
		{
			TreeNode mNode;

			mNode = trvPacket.GetNodeAt( MouseX , MouseY );			
			if( mNode == null ) return;

			objTVF.HighlightText( RTBPacket ,RTBAscii, mNode);

		}


		private void tlbMain_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch(tlbMain.Buttons.IndexOf(e.Button))
			{
				case 0:
					this.startSniffing();
					break; 
				case 1:
					this.stopSniffing2();
					break; 
				case 2:
					if (tlbMain.Buttons[2].ImageIndex==4)
					{
						this.PauseSniffing();
						tlbMain.Buttons[2].ImageIndex=5;
						tlbMain.Buttons[2].ToolTipText="Resume Sniffing";
					}
					else
					{
						this.ResumeSniffing();
						tlbMain.Buttons[2].ImageIndex=4;
						tlbMain.Buttons[2].ToolTipText="Pause Sniffing";
					}
					break;
				case 3:
					checkCon();
					break;
				case 4:
					AddFilter();
					break;
				case 5:
					if (MessageBox.Show(this,"Do You Want To Quit?","QUIT",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
						System.Environment.Exit(0);
					break;
			}
		}


		private void AddFilter()
		{




			trio objTrio = GetSelectedItem(lstwMain);
			if(objTrio.checkAvailable())
			{
				FilterCreator FCreater = new FilterCreator();
				//pause
				this.PauseSniffing();
				tlbMain.Buttons[2].ImageIndex=5;
				tlbMain.Buttons[2].ToolTipText="Resume Sniffing";

				FCreater.All_.Enabled=false;

				FCreater.editSourceAddr.Text=objTrio.Source;
				FCreater.editSourcePort.Text=objTrio.SourcePort;

				FCreater.editDestinationAddr.Text=objTrio.Destination;
				FCreater.editDestinationPort.Text=objTrio.DestinationPort;

				switch(objTrio.Protocol)
				{
					case "Tcp":
						FCreater.Protocol_.SelectedIndex=0;
						break;
					case "Udp":
						FCreater.Protocol_.SelectedIndex=1;
						break;
					case "ICMP":
						FCreater.Protocol_.SelectedIndex=2;
						FCreater.checkPort=false;
						FCreater.GroupPort_.Hide();
						break;
				}
				FCreater.Protocol_.Enabled=false;

				if (FCreater.ShowDialog(this)==DialogResult.OK)
				{
					Filtering FManager = new Filtering();
					FManager.ShowDialog(this);
				}

				//resume
				this.ResumeSniffing();
				tlbMain.Buttons[2].ImageIndex=4;
				tlbMain.Buttons[2].ToolTipText="Pause Sniffing";
			}
		}


		private void checkCon(){
			trio objTrio = GetSelectedItem(lstwMain);
			CheckerForm Checker = new CheckerForm("Check");
			if(objTrio.checkAvailable())
			{
				if (objTrio.Source==this.thisComputerIp)
				{
					Checker.IP=objTrio.Destination;
					Checker.Port=Convert.ToInt32(objTrio.DestinationPort);

					Checker.StartTimer();
					
					Checker.ShowDialog();
					Checker.Dispose();
				}
				else
				{
					Checker.IP= objTrio.Source;
					Checker.Port=Convert.ToInt32(objTrio.SourcePort);
					
					Checker.StartTimer();
					
					Checker.ShowDialog();
					Checker.Dispose();
				}
			}
			else
			{
				Checker.StartTimer();
				Checker.ShowDialog();
				Checker.Dispose();
			}
		}


		private void ping()
		{
			trio objTrio = GetSelectedItem(lstwMain);
			CheckerForm Checker = new CheckerForm("Ping");
			if(objTrio.checkAvailable())
			{
				if (objTrio.Source==this.thisComputerIp)
				{
					Checker.IP=objTrio.Destination;
					Checker.ShowDialog();
					Checker.Dispose();
				}
				else
				{
					Checker.IP= objTrio.Source;
					Checker.ShowDialog();
					Checker.Dispose();
				}
			}
			else
			{
				Checker.ShowDialog();
				Checker.Dispose();
			}
		}


		private void trace()
		{
			trio objTrio = GetSelectedItem(lstwMain);
			CheckerForm Checker = new CheckerForm("Trace");
			if(objTrio.checkAvailable())
			{
				if (objTrio.Source==this.thisComputerIp)
				{
					Checker.IP=objTrio.Destination;
					Checker.ShowDialog();
					Checker.Dispose();
				}
				else
				{
					Checker.IP= objTrio.Source;
					Checker.ShowDialog();
					Checker.Dispose();
				}
			}
			else
			{
				Checker.ShowDialog();
				Checker.Dispose();
			}
		}


		void Counter(){
			while(true)
			{
				lblv1.Text=totalCount.ToString();
				lblv2.Text=tcpCount.ToString();
				lblv3.Text=udpCount.ToString();
				lblv4.Text=icmpCount.ToString();
				lblv5.Text=otherCount.ToString();

				lblb1.Text=totalByte.ToString()+" Bytes";
				lblb2.Text=tcpByte.ToString()+" Bytes";
				lblb3.Text=udpByte.ToString()+" Bytes";
				lblb4.Text=icmpByte.ToString()+" Bytes";
				lblb5.Text=otherByte.ToString()+" Bytes";
				Thread.Sleep(1000);
			}
		
		}


/********************* main menu *************************/

		private void miExit_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show(this,"Do You Want To Quit?","QUIT",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
				System.Environment.Exit(0);
		}


		private void miAddProtocol_Click(object sender, System.EventArgs e)
		{
			AddProtocols AddProto = new AddProtocols();
			AddProto.Show();		
		}


		private void miFiltering_Click(object sender, System.EventArgs e)
		{
			Filtering FManager = new Filtering();
			//pause
			this.PauseSniffing();
			tlbMain.Buttons[2].ImageIndex=5;
			tlbMain.Buttons[2].ToolTipText="Resume Sniffing";

            FManager.ShowDialog(this);

			//resume
			this.ResumeSniffing();
			tlbMain.Buttons[2].ImageIndex=4;
			tlbMain.Buttons[2].ToolTipText="Pause Sniffing";
		}


		private void miNetStat_Click(object sender, System.EventArgs e)
		{
			NetStat netStat= new NetStat();
			netStat.Show();			
		}


		private void miCheckCon_Click(object sender, System.EventArgs e)
		{
			checkCon();
		}


		private void miTraceR_Click(object sender, System.EventArgs e)
		{
			trace();
		}


		private void miPing_Click(object sender, System.EventArgs e)
		{
			ping();
		}


		private void miSetting_Click(object sender, System.EventArgs e)
		{
			Settings settings = new Settings();
			DialogResult result = settings.ShowDialog();

			if(result == DialogResult.OK) 
			{	
				Setting.WriteToFile();
			}
			return;			
		}


		private void menuItem3_Click(object sender, System.EventArgs e)//scanner
		{
			scanner.Start();
		}


/********************* main menu *************************/

		private void tblIcmp_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch(tblIcmp.Buttons.IndexOf(e.Button))
			{
				case 0:
					this.OpenTreeView(true);
					break; 
				case 1:
					this.CloseTreeView();
					break;
			}
		}


		private void ControlDoubleClick()
		{
			this.OpenTreeView(true);
		}


		private void miManual_Click(object sender, System.EventArgs e)
		{
			if (!miManual.Checked)
			{
				miManual.Checked=true;
				miAutomatic.Checked=false;
				tlbSniffing.Buttons[0].Enabled=true;
				tlbMain.Buttons[0].Enabled=true;
				tlbMain.Buttons[1].Enabled=true;

				CnmLstwMain.MenuItems[0].Enabled=true;
				CnmLstwMain.MenuItems[1].Enabled=true;
				
				string ident="";
				for (int i=this.tcSniffing.TabCount-1;i>=0;i--)
				{
					ident =this.tcSniffing.TabPages[i].Tag.ToString();
					if (!CurrentSniffing_.Contains(ident))
					{
						TabPageTable_.Remove(ident);
						tcSniffing.TabPages.Remove(this.tcSniffing.TabPages[i]);						
					}
				}
				GC.Collect();
			}
		}


		private void miAutomatic_Click(object sender, System.EventArgs e)
		{
			if (!miAutomatic.Checked)
			{
				miAutomatic.Checked=true;
				miManual.Checked=false;
				tlbSniffing.Buttons[0].Enabled=false;
				tlbMain.Buttons[0].Enabled=false;
				tlbMain.Buttons[1].Enabled=false;
				CnmLstwMain.MenuItems[0].Enabled=false;
				CnmLstwMain.MenuItems[1].Enabled=false;
			}
		}


		private void lstwMain_DoubleClick(object sender, System.EventArgs e)
		{
			this.startSniffing();
		}


		private void lstwMain_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			trio objTrio = GetSelectedItem(lstwMain);
			if(objTrio.checkAvailable())
			{
				string ident = objTrio.GetHashString();
				if (miAutomatic.Checked)
				{
					for (int i=0;i<this.tcSniffing.TabCount;i++)
					{
						if (this.tcSniffing.TabPages[i].Tag.ToString()==ident)
						{
							tcMain.SelectedIndex=0;
							tcSniffing.SelectedIndex=i;
							break;
						}
					}
				}
				else
				{
					if (CurrentSniffing_.Contains(ident))
					{
						for (int i=0;i<this.tcSniffing.TabCount;i++)
						{
							if (this.tcSniffing.TabPages[i].Tag.ToString()==ident)
							{
								tcSniffing.SelectedIndex=i;
								break;
							}
						}
					}
				}
				
			}
		}


		private void lstwMain_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			this.lstwMain.ListViewItemSorter = new ListViewItemComparer(e.Column);
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
