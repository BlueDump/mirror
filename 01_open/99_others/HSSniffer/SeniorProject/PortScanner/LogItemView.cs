using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PortScannerNS
{
	/// <summary>
	/// Summary description for LogItemView.
	/// </summary>
	public class LogItemView : System.Windows.Forms.Form
	{
		public string type = "";
		public string LogMessage="";
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.GroupBox gbLogItem;
		private System.Windows.Forms.Label lblTag;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public LogItemView()
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.gbLogItem = new System.Windows.Forms.GroupBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.lblTag = new System.Windows.Forms.Label();
			this.lblMessage = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.gbLogItem.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.gbLogItem);
			this.panel1.Controls.Add(this.btnOK);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(384, 357);
			this.panel1.TabIndex = 0;
			// 
			// gbLogItem
			// 
			this.gbLogItem.Controls.Add(this.textBox1);
			this.gbLogItem.Controls.Add(this.lblTag);
			this.gbLogItem.Controls.Add(this.lblMessage);
			this.gbLogItem.Location = new System.Drawing.Point(8, 8);
			this.gbLogItem.Name = "gbLogItem";
			this.gbLogItem.Size = new System.Drawing.Size(368, 312);
			this.gbLogItem.TabIndex = 5;
			this.gbLogItem.TabStop = false;
			this.gbLogItem.Text = "Log Item View";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(8, 32);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(352, 272);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "";
			this.textBox1.WordWrap = false;
			// 
			// lblTag
			// 
			this.lblTag.Location = new System.Drawing.Point(8, 16);
			this.lblTag.Name = "lblTag";
			this.lblTag.TabIndex = 1;
			this.lblTag.Text = "Message";
			// 
			// lblMessage
			// 
			this.lblMessage.Location = new System.Drawing.Point(16, 40);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(344, 80);
			this.lblMessage.TabIndex = 0;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(286, 326);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "Okey";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// LogItemView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(384, 357);
			this.Controls.Add(this.panel1);
			this.Name = "LogItemView";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "LogItemView";
			this.Load += new System.EventHandler(this.LogItemView_Load);
			this.panel1.ResumeLayout(false);
			this.gbLogItem.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void LogItemView_Load(object sender, System.EventArgs e)
		{
			if (type == "Port")
			{
				lblMessage.Visible=true;
				textBox1.Visible=false;
				lblMessage.Text=LogMessage;
			}
			else
			{
				lblMessage.Visible=false;
				textBox1.Visible=true;
//				textBox1.Text=LogMessage;
				int lpos=-1;
				int pos=0;
				string str="";
				string str1= "";
				while ((pos = LogMessage.IndexOf("\n",lpos+1))!=-1)
				{
					str1="";
					str1+=LogMessage.Substring(lpos+1,pos-lpos-1)+Environment.NewLine;
					str+=str1;
					lpos=pos;
				}
			
//				str += LogMessage.Substring(lpos+1);

				
//				pos = LogMessage.IndexOf("body",lpos);
//				str +=LogMessage.Substring(0,pos+5)+Environment.NewLine;
//				pos +=5;
//				lpos = pos;
				lpos=lpos+1;
				while ((pos = LogMessage.IndexOf("Port",lpos+1))!=-1)
				{
					str1="";
					str1 += LogMessage.Substring(lpos,pos-lpos)+Environment.NewLine;
					str += str1;
					lpos = pos;
				}
//				while ((pos = str.IndexOf("Port",pos) != -1)
//				{
//					 
//				}
				str += LogMessage.Substring(lpos);
				textBox1.Text = str;
			}
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void lblMessage_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
