using System;
using System.Net;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace SnifferUI
{
//	using Checker.SessionScan;

	/// <summary>
	/// Summary description for SessionForm.
	/// </summary>
	public class CheckerForm : System.Windows.Forms.Form
	{		
		public string tool ="";
		public string IP="";
		public int Port=-1;
		private Thread thread=null;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;

		public CheckerForm(string intool)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			tool=intool;
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
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnNew = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox1.Location = new System.Drawing.Point(0, 0);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(560, 342);
			this.listBox1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.progressBar1);
			this.panel1.Controls.Add(this.btnClose);
			this.panel1.Controls.Add(this.btnNew);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 345);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(560, 32);
			this.panel1.TabIndex = 1;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(24, 4);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(352, 24);
			this.progressBar1.TabIndex = 2;
			this.progressBar1.Visible = false;
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(472, 8);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnNew
			// 
			this.btnNew.Location = new System.Drawing.Point(392, 8);
			this.btnNew.Name = "btnNew";
			this.btnNew.TabIndex = 0;
			this.btnNew.Text = "New";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.listBox1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(560, 345);
			this.panel2.TabIndex = 2;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.panel2);
			this.panel3.Controls.Add(this.panel1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(560, 377);
			this.panel3.TabIndex = 3;
			// 
			// CheckerForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(560, 377);
			this.Controls.Add(this.panel3);
			this.Name = "CheckerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "???";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.CheckerForm_Closing);
			this.Load += new System.EventHandler(this.CheckerForm_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		public void OnReturn(string str)
		{
			int lpos=-1;
			int pos=0;
			while ((pos = str.IndexOf("\n",lpos+1))!=-1)
			{
				listBox1.Items.Add(str.Substring(lpos+1,pos-lpos-1));
				lpos=pos;
			}
			listBox1.Items.Add(str.Substring(lpos+1));
		}

		public void StartTimer()
		{
			progressBar1.Visible=true;
			progressBar1.Minimum=0;
			progressBar1.Maximum=1000;
			progressBar1.Value=0;
			progressBar1.Step=1;

			CheckTool.isFinished=false;

			timer1.Tick+= new EventHandler(checkOpFinished);
			timer1.Interval= 100;
			timer1.Start();
		}
		public void checkOpFinished(Object obj, System.EventArgs arg)
		{
			timer1.Stop();

			progressBar1.PerformStep();
			Invalidate();
			progressBar1.Update();
			
			if (CheckTool.isFinished)
			{
				timer1.Dispose();
				progressBar1.Value=0;
				progressBar1.Visible=false;
				//this.Close();
				return;
			}
			timer1.Enabled=true;
		}

		private void CheckerForm_Load(object sender, System.EventArgs e)
		{
			CheckTool.checkToolCallback = new CheckToolCallback(OnReturn);
			switch(tool){
				case "Ping":
					this.Text="Ping";
					if (thread!=null)
						thread.Abort();
					listBox1.Items.Clear();
					thread = new Thread(new ThreadStart(CheckTool.Ping));
					thread.IsBackground=true;
					if(IP=="")
					{
						if(addIp())
							thread.Start();
						else
						{
							this.Close();
						}
					}
					else{
						try
						{
							CheckTool.host=IPAddress.Parse(IP);
							thread.Start();

						}
						catch(Exception){
							MessageBox.Show("Invalid IP Addreess");
							if(addIp())
							thread.Start();
							else
							this.Close();
						}
					}
					break;
				case "Trace":
					this.Text="Trace Route";
					if (thread!=null)
						thread.Abort();
					listBox1.Items.Clear();
					thread = new Thread(new ThreadStart(CheckTool.TraceRoute));
					thread.IsBackground=true;
					if(IP=="")
					{
						if(addIp())
						thread.Start();
						else
							this.Close();
					}
					else
					{
						try
						{
							CheckTool.host=IPAddress.Parse(IP);
							thread.Start();
						}
						catch(Exception)
						{
							MessageBox.Show("Invalid IP Addreess");
							if (addIp())
							thread.Start();
							else
							this.Close();
						}
					}
					break;
				case "Check":
					this.Text="Connection Checker";
					if (thread!=null)
						thread.Abort();
					listBox1.Items.Clear();
					thread = new Thread(new ThreadStart(CheckTool.Check));
					thread.IsBackground=true;
					if(IP=="")
					{
						if (addIp())
						{
							StartTimer();
							thread.Start();
						}
						else
							this.Close();
					}
					else
					{
						try
						{
							CheckTool.host=IPAddress.Parse(IP);
							CheckTool.port=Port;
							StartTimer();
							thread.Start();

						}
						catch(Exception)
						{
							MessageBox.Show("Invalid IP Addreess");
						}
					}
					break;
			}
		}

		private bool addIp()
		{   
			DialogResult result;	
			AddressInput input = new AddressInput();
			result = input.ShowDialog(this);
			if ( result == DialogResult.OK ) 
			{ 
				CheckTool.host=input.IP;
				CheckTool.port=0;
				input.Dispose();
				return true;
			}
			else if(result == DialogResult.Cancel)
			{
				input.Dispose();
				return false;
			}
			return false;
		}

		private void btnNew_Click(object sender, System.EventArgs e)
		{
			switch(tool)
			{
				case "Ping":
					if(addIp())
					{
						if (thread!=null)
							thread.Abort();
						listBox1.Items.Clear();
						thread = new Thread(new ThreadStart(CheckTool.Ping));
						thread.IsBackground=true;
						thread.Start();
					}
					break;
				case "Trace":
					if(addIp())
					{
						if (thread!=null)
							thread.Abort();
						listBox1.Items.Clear();
						thread = new Thread(new ThreadStart(CheckTool.TraceRoute));
						thread.IsBackground=true;
						thread.Start();
					}
					break;
				case "Check":
					if (addIp())
					{
						if (thread!=null)
							thread.Abort();
						listBox1.Items.Clear();
						thread = new Thread(new ThreadStart(CheckTool.Check));
						thread.IsBackground=true;
						StartTimer();
						thread.Start();
					}
					break;
			}
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void CheckerForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (thread!=null)
				thread.Abort();
			CheckTool.isFinished=true;
		}
	}
}
