using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;

using IpHlpApidotnet;

namespace SnifferUI
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class NetStat : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader remote;
		private System.Windows.Forms.ColumnHeader columnHeader2;

		private IpHlpApidotnet.IPHelper MyAPI;

		private const int MIB_TCP_RTO_CONSTANT=2;
		private const int MIB_TCP_RTO_OTHER=1;
		private const int MIB_TCP_RTO_RSRE=3;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader ProcessName;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem miRun;
		private System.Windows.Forms.MenuItem miSTcp;
		private System.Windows.Forms.MenuItem miSUdp;
		private System.Windows.Forms.MenuItem miTcpT;
		private System.Windows.Forms.MenuItem miUdpT;
		private System.Windows.Forms.MenuItem miTcpTXP;
		private System.Windows.Forms.MenuItem miUdpTXP;
		private System.Windows.Forms.Button btnSTcp;
		private System.Windows.Forms.Button btnTcpT;
		private System.Windows.Forms.Button btnUdpT;
		private System.Windows.Forms.Button btnTcpTXP;
		private System.Windows.Forms.Button btnUdpTXP;
		private System.Windows.Forms.Button btnSUdp;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.MenuItem miClose;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem miClear;
		private System.Windows.Forms.Panel pnlUP;
		private System.Windows.Forms.Panel pnlBtm;
		private System.Windows.Forms.Panel pnlBtmBtm;
		private System.Windows.Forms.Panel pnlBtmUp;
		private const int MIB_TCP_RTO_VANJ=4;



		public NetStat()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			MyAPI = new IpHlpApidotnet.IPHelper();
			listView1.GridLines=true;
		}

		
	     #region Windows Form Designer generated code
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

		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnSTcp = new System.Windows.Forms.Button();
			this.btnTcpT = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.remote = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.ProcessName = new System.Windows.Forms.ColumnHeader();
			this.btnUdpT = new System.Windows.Forms.Button();
			this.btnTcpTXP = new System.Windows.Forms.Button();
			this.btnUdpTXP = new System.Windows.Forms.Button();
			this.btnSUdp = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.miRun = new System.Windows.Forms.MenuItem();
			this.miSTcp = new System.Windows.Forms.MenuItem();
			this.miSUdp = new System.Windows.Forms.MenuItem();
			this.miTcpT = new System.Windows.Forms.MenuItem();
			this.miUdpT = new System.Windows.Forms.MenuItem();
			this.miTcpTXP = new System.Windows.Forms.MenuItem();
			this.miUdpTXP = new System.Windows.Forms.MenuItem();
			this.btnClear = new System.Windows.Forms.Button();
			this.miClose = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.miClear = new System.Windows.Forms.MenuItem();
			this.pnlUP = new System.Windows.Forms.Panel();
			this.pnlBtm = new System.Windows.Forms.Panel();
			this.pnlBtmBtm = new System.Windows.Forms.Panel();
			this.pnlBtmUp = new System.Windows.Forms.Panel();
			this.pnlUP.SuspendLayout();
			this.pnlBtm.SuspendLayout();
			this.pnlBtmBtm.SuspendLayout();
			this.pnlBtmUp.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSTcp
			// 
			this.btnSTcp.Location = new System.Drawing.Point(8, 8);
			this.btnSTcp.Name = "btnSTcp";
			this.btnSTcp.Size = new System.Drawing.Size(88, 23);
			this.btnSTcp.TabIndex = 1;
			this.btnSTcp.Text = "Tcp Stat";
			this.btnSTcp.Click += new System.EventHandler(this.btnSTcp_Click);
			// 
			// btnTcpT
			// 
			this.btnTcpT.Location = new System.Drawing.Point(216, 8);
			this.btnTcpT.Name = "btnTcpT";
			this.btnTcpT.Size = new System.Drawing.Size(88, 23);
			this.btnTcpT.TabIndex = 2;
			this.btnTcpT.Text = "Tcp Table";
			this.btnTcpT.Click += new System.EventHandler(this.btnTcpT_Click);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.remote,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.ProcessName});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(640, 380);
			this.listView1.TabIndex = 4;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Local";
			this.columnHeader1.Width = 186;
			// 
			// remote
			// 
			this.remote.Text = "remote";
			this.remote.Width = 183;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "State";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader2.Width = 75;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "ProcessID";
			this.columnHeader3.Width = 68;
			// 
			// ProcessName
			// 
			this.ProcessName.Text = "ProcessName";
			this.ProcessName.Width = 93;
			// 
			// btnUdpT
			// 
			this.btnUdpT.Location = new System.Drawing.Point(320, 8);
			this.btnUdpT.Name = "btnUdpT";
			this.btnUdpT.Size = new System.Drawing.Size(88, 24);
			this.btnUdpT.TabIndex = 5;
			this.btnUdpT.Text = "Udp Table";
			this.btnUdpT.Click += new System.EventHandler(this.btnUdpT_Click);
			// 
			// btnTcpTXP
			// 
			this.btnTcpTXP.Location = new System.Drawing.Point(424, 8);
			this.btnTcpTXP.Name = "btnTcpTXP";
			this.btnTcpTXP.Size = new System.Drawing.Size(88, 23);
			this.btnTcpTXP.TabIndex = 6;
			this.btnTcpTXP.Text = "Tcp Table XP";
			this.btnTcpTXP.Click += new System.EventHandler(this.btnTcpTXP_Click);
			// 
			// btnUdpTXP
			// 
			this.btnUdpTXP.Location = new System.Drawing.Point(528, 8);
			this.btnUdpTXP.Name = "btnUdpTXP";
			this.btnUdpTXP.Size = new System.Drawing.Size(88, 23);
			this.btnUdpTXP.TabIndex = 8;
			this.btnUdpTXP.Text = "UDP Table XP";
			this.btnUdpTXP.Click += new System.EventHandler(this.btnUdpTXP_Click);
			// 
			// btnSUdp
			// 
			this.btnSUdp.Location = new System.Drawing.Point(112, 8);
			this.btnSUdp.Name = "btnSUdp";
			this.btnSUdp.Size = new System.Drawing.Size(88, 23);
			this.btnSUdp.TabIndex = 9;
			this.btnSUdp.Text = "Udp Stat";
			this.btnSUdp.Click += new System.EventHandler(this.btnSUdp_Click);
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(88, 8);
			this.button8.Name = "button8";
			this.button8.TabIndex = 10;
			this.button8.Text = "Close";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.miRun});
			// 
			// miRun
			// 
			this.miRun.Index = 0;
			this.miRun.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				  this.miSTcp,
																				  this.miSUdp,
																				  this.menuItem1,
																				  this.miTcpT,
																				  this.miUdpT,
																				  this.menuItem2,
																				  this.miTcpTXP,
																				  this.miUdpTXP,
																				  this.menuItem3,
																				  this.miClear,
																				  this.menuItem4,
																				  this.miClose});
			this.miRun.Text = "Run";
			// 
			// miSTcp
			// 
			this.miSTcp.Index = 0;
			this.miSTcp.Text = "Tcp Stat";
			this.miSTcp.Click += new System.EventHandler(this.miSTcp_Click);
			// 
			// miSUdp
			// 
			this.miSUdp.Index = 1;
			this.miSUdp.Text = "Udp Stat";
			this.miSUdp.Click += new System.EventHandler(this.miSUdp_Click);
			// 
			// miTcpT
			// 
			this.miTcpT.Index = 3;
			this.miTcpT.Text = "Tcp Table";
			this.miTcpT.Click += new System.EventHandler(this.miTcpT_Click);
			// 
			// miUdpT
			// 
			this.miUdpT.Index = 4;
			this.miUdpT.Text = "Udp Table";
			this.miUdpT.Click += new System.EventHandler(this.miUdpT_Click);
			// 
			// miTcpTXP
			// 
			this.miTcpTXP.Index = 6;
			this.miTcpTXP.Text = "Tcp Table XP";
			this.miTcpTXP.Click += new System.EventHandler(this.miTcpTXP_Click);
			// 
			// miUdpTXP
			// 
			this.miUdpTXP.Index = 7;
			this.miUdpTXP.Text = "Udp Table XP";
			this.miUdpTXP.Click += new System.EventHandler(this.miUdpTXP_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(8, 8);
			this.btnClear.Name = "btnClear";
			this.btnClear.TabIndex = 11;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// miClose
			// 
			this.miClose.Index = 11;
			this.miClose.Text = "Close";
			this.miClose.Click += new System.EventHandler(this.miClose_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.Text = "-";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 5;
			this.menuItem2.Text = "-";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 8;
			this.menuItem3.Text = "-";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 10;
			this.menuItem4.Text = "-";
			// 
			// miClear
			// 
			this.miClear.Index = 9;
			this.miClear.Text = "Clear";
			this.miClear.Click += new System.EventHandler(this.miClear_Click);
			// 
			// pnlUP
			// 
			this.pnlUP.Controls.Add(this.btnSTcp);
			this.pnlUP.Controls.Add(this.btnTcpT);
			this.pnlUP.Controls.Add(this.btnUdpT);
			this.pnlUP.Controls.Add(this.btnTcpTXP);
			this.pnlUP.Controls.Add(this.btnUdpTXP);
			this.pnlUP.Controls.Add(this.btnSUdp);
			this.pnlUP.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlUP.Location = new System.Drawing.Point(0, 0);
			this.pnlUP.Name = "pnlUP";
			this.pnlUP.Size = new System.Drawing.Size(640, 40);
			this.pnlUP.TabIndex = 12;
			// 
			// pnlBtm
			// 
			this.pnlBtm.Controls.Add(this.pnlBtmUp);
			this.pnlBtm.Controls.Add(this.pnlBtmBtm);
			this.pnlBtm.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBtm.Location = new System.Drawing.Point(0, 40);
			this.pnlBtm.Name = "pnlBtm";
			this.pnlBtm.Size = new System.Drawing.Size(640, 420);
			this.pnlBtm.TabIndex = 13;
			// 
			// pnlBtmBtm
			// 
			this.pnlBtmBtm.Controls.Add(this.button8);
			this.pnlBtmBtm.Controls.Add(this.btnClear);
			this.pnlBtmBtm.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlBtmBtm.Location = new System.Drawing.Point(0, 380);
			this.pnlBtmBtm.Name = "pnlBtmBtm";
			this.pnlBtmBtm.Size = new System.Drawing.Size(640, 40);
			this.pnlBtmBtm.TabIndex = 0;
			// 
			// pnlBtmUp
			// 
			this.pnlBtmUp.Controls.Add(this.listView1);
			this.pnlBtmUp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBtmUp.Location = new System.Drawing.Point(0, 0);
			this.pnlBtmUp.Name = "pnlBtmUp";
			this.pnlBtmUp.Size = new System.Drawing.Size(640, 380);
			this.pnlBtmUp.TabIndex = 1;
			// 
			// NetStat
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(640, 481);
			this.Controls.Add(this.pnlBtm);
			this.Controls.Add(this.pnlUP);
			this.Menu = this.mainMenu1;
			this.MinimumSize = new System.Drawing.Size(648, 250);
			this.Name = "NetStat";
			this.Text = "NetStat";
			this.pnlUP.ResumeLayout(false);
			this.pnlBtm.ResumeLayout(false);
			this.pnlBtmBtm.ResumeLayout(false);
			this.pnlBtmUp.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		#endregion

		private void Stat_Tcp()
		{
			this.listView1.Items.Clear();
			MyAPI.GetTcpStats();
			string m_algo="";
			switch(MyAPI.TcpStats.dwRtoAlgorithm)
			{
				case 1 : m_algo="Other";break;
				case 2 : m_algo="Constant Time-out";break;
				case 3 : m_algo="MIL-STD-1778 Appendix B";break;
				case 4 : m_algo="Van Jacobson's Algorithm";break;
			}
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"RtoAlgorithm",
					m_algo,
					""
					}));
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"RtoMin",
					MyAPI.TcpStats.dwRtoMin.ToString(),
					""
					}));
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"RtoMax",
					MyAPI.TcpStats.dwRtoMax.ToString(),
					""
					}));
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"Max Connexion",
					MyAPI.TcpStats.dwMaxConn.ToString(),
					""
					}));
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"Active Open Connexion",
					MyAPI.TcpStats.dwActiveOpens.ToString(),
					""
					}));
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"Passive Open Connexion",
					MyAPI.TcpStats.dwPassiveOpens.ToString(),
					""
					}));
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"Attempte Fail",
					MyAPI.TcpStats.dwAttemptFails.ToString(),
					""
					}));
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"Estabished connexion that have been reset",
					MyAPI.TcpStats.dwEstabResets.ToString(),
					""
					}));
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"Current estabished connexion",
					MyAPI.TcpStats.dwCurrEstab.ToString(),
					""
					}));
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"In Segments",
					MyAPI.TcpStats.dwInSegs.ToString(),
					""
					}));
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"Out Segement",
					MyAPI.TcpStats.dwOutSegs.ToString(),
					""
					}));
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"Segement Retransmitted",
					MyAPI.TcpStats.dwRetransSegs.ToString(),
					""
					}));
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"InErrors",
					MyAPI.TcpStats.dwInErrs.ToString(),
					""
					}));
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"number of segments transmitted with the reset flag set",
					MyAPI.TcpStats.dwRetransSegs.ToString(),
					""
					}));
			this.listView1.Items.Add(new ListViewItem(new string[] {
					"number of connexions",
					MyAPI.TcpStats.dwNumConns.ToString(),
					""
					}));
		}


		private void Stat_Udp()
		{
			this.listView1.Items.Clear();
			MyAPI.GetUdpStats();
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "In Datagrams",
																	   MyAPI.UdpStats.dwInDatagrams.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "Out Datagrams",
																	   MyAPI.UdpStats.dwOutDatagrams.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "In Errors",
																	   MyAPI.UdpStats.dwInErrors.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "No Ports",
																	   MyAPI.UdpStats.dwNoPorts.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "Num Address",
																	   MyAPI.UdpStats.dwNumAddrs.ToString(),
																	   ""
																   }));
		}

		private void Tcp_Table()
		{
			this.listView1.Items.Clear();
			MyAPI.GetTcpConnexions();
			for(int i=0;i<MyAPI.TcpConnexion.dwNumEntries;i++)
			{
				this.listView1.Items.Add(new ListViewItem(new string[] {
					MyAPI.TcpConnexion.table[i].Local.Address.ToString()+":"+MyAPI.TcpConnexion.table[i].Local.Port.ToString(),
					MyAPI.TcpConnexion.table[i].Remote.Address.ToString()+":"+MyAPI.TcpConnexion.table[i].Remote.Port.ToString(),
					MyAPI.TcpConnexion.table[i].StrgState.ToString()
					}));
			}
		}


		private void Udp_Table()
		{
			this.listView1.Items.Clear();
			MyAPI.GetUdpConnexions();
			for(int i=0;i<MyAPI.UdpConnexion.dwNumEntries;i++)
			{
				this.listView1.Items.Add(new ListViewItem(new string[] {
					MyAPI.UdpConnexion.table[i].Local.Address.ToString()+":"+MyAPI.UdpConnexion.table[i].Local.Port.ToString(),
					"",""
					}));
			}
		}


		private void Tcp_TableXP()
		{
			this.listView1.Items.Clear();
			MyAPI.GetExTcpConnexions();
			for(int i=0;i<MyAPI.TcpExConnexions.dwNumEntries;i++)
				{
					this.listView1.Items.Add(new ListViewItem(new string[] {

							MyAPI.TcpExConnexions.table[i].Local.Address.ToString()+":"+MyAPI.TcpExConnexions.table[i].Local.Port.ToString(),
							MyAPI.TcpExConnexions.table[i].Remote.Address.ToString()+":"+MyAPI.TcpExConnexions.table[i].Remote.Port.ToString(),
							MyAPI.TcpExConnexions.table[i].StrgState.ToString(),
							MyAPI.TcpExConnexions.table[i].dwProcessId.ToString(),
							MyAPI.TcpExConnexions.table[i].ProcessName

						}));
				}
		}


		private void Udp_TableXP()
		{
			this.listView1.Items.Clear();
			MyAPI.GetExUdpConnexions();
			for(int i=0;i<MyAPI.UdpExConnexion.dwNumEntries;i++)
			{
				this.listView1.Items.Add(new ListViewItem(new string[] {  
					MyAPI.UdpExConnexion.table[i].Local.Address.ToString()+":"+MyAPI.UdpExConnexion.table[i].Local.Port.ToString(),
					" ",
					"",
					MyAPI.UdpExConnexion.table[i].dwProcessId.ToString(),
					MyAPI.UdpExConnexion.table[i].ProcessName
				   }));
			}
		}	

		private void button8_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			this.listView1.Items.Clear();
		}

		private void btnSTcp_Click(object sender, System.EventArgs e)
		{
			Stat_Tcp();
		}

		private void btnSUdp_Click(object sender, System.EventArgs e)
		{
			Stat_Udp();
		}

		private void btnTcpT_Click(object sender, System.EventArgs e)
		{
			Tcp_Table();
		}

		private void btnUdpT_Click(object sender, System.EventArgs e)
		{
			Udp_Table();
		}

		private void btnTcpTXP_Click(object sender, System.EventArgs e)
		{
			Tcp_TableXP();
		}

		private void btnUdpTXP_Click(object sender, System.EventArgs e)
		{
			Udp_TableXP();
		}

		private void miSTcp_Click(object sender, System.EventArgs e)
		{
			Stat_Tcp();
		}

		private void miSUdp_Click(object sender, System.EventArgs e)
		{
			Stat_Udp();
		}

		private void miTcpT_Click(object sender, System.EventArgs e)
		{
			Tcp_Table();
		}

		private void miUdpT_Click(object sender, System.EventArgs e)
		{
			Udp_Table();
		}

		private void miTcpTXP_Click(object sender, System.EventArgs e)
		{
			Tcp_TableXP();
		}

		private void miUdpTXP_Click(object sender, System.EventArgs e)
		{
			Udp_TableXP();
		}

		private void miClear_Click(object sender, System.EventArgs e)
		{
			this.listView1.Items.Clear();
		}

		private void miClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
