using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SnifferUI
{
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
	public class Settings : System.Windows.Forms.Form
	{
		public string FileName;
		private System.Windows.Forms.GroupBox gbToolsSettings;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbPing;
		private System.Windows.Forms.ComboBox cbTraceRoute;
		private System.Windows.Forms.TabPage tpSettings;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Settings()
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
			this.gbToolsSettings = new System.Windows.Forms.GroupBox();
			this.cbTraceRoute = new System.Windows.Forms.ComboBox();
			this.cbPing = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpSettings = new System.Windows.Forms.TabPage();
			this.gbToolsSettings.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbToolsSettings
			// 
			this.gbToolsSettings.Controls.Add(this.cbTraceRoute);
			this.gbToolsSettings.Controls.Add(this.cbPing);
			this.gbToolsSettings.Controls.Add(this.label3);
			this.gbToolsSettings.Controls.Add(this.label2);
			this.gbToolsSettings.Location = new System.Drawing.Point(8, 8);
			this.gbToolsSettings.Name = "gbToolsSettings";
			this.gbToolsSettings.Size = new System.Drawing.Size(264, 88);
			this.gbToolsSettings.TabIndex = 1;
			this.gbToolsSettings.TabStop = false;
			this.gbToolsSettings.Text = "Tools Setings";
			// 
			// cbTraceRoute
			// 
			this.cbTraceRoute.Items.AddRange(new object[] {
															  "Echo Request",
															  "Time Stamp",
															  "Udp"});
			this.cbTraceRoute.Location = new System.Drawing.Point(112, 56);
			this.cbTraceRoute.Name = "cbTraceRoute";
			this.cbTraceRoute.Size = new System.Drawing.Size(128, 21);
			this.cbTraceRoute.TabIndex = 4;
			this.cbTraceRoute.SelectedIndexChanged += new System.EventHandler(this.cbTraceRoute_SelectedIndexChanged);
			// 
			// cbPing
			// 
			this.cbPing.Items.AddRange(new object[] {
														"Echo Request",
														"Time Stamp",
														"Udp"});
			this.cbPing.Location = new System.Drawing.Point(112, 24);
			this.cbPing.Name = "cbPing";
			this.cbPing.Size = new System.Drawing.Size(128, 21);
			this.cbPing.TabIndex = 3;
			this.cbPing.SelectedIndexChanged += new System.EventHandler(this.cbPing_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 56);
			this.label3.Name = "label3";
			this.label3.TabIndex = 1;
			this.label3.Text = "Trace Route";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "Ping";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(112, 136);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "Okey";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(208, 136);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tpSettings);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(288, 128);
			this.tabControl1.TabIndex = 4;
			// 
			// tpSettings
			// 
			this.tpSettings.Controls.Add(this.gbToolsSettings);
			this.tpSettings.Location = new System.Drawing.Point(4, 22);
			this.tpSettings.Name = "tpSettings";
			this.tpSettings.Size = new System.Drawing.Size(280, 102);
			this.tpSettings.TabIndex = 0;
			this.tpSettings.Text = "Settings";
			// 
			// Settings
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(288, 158);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(296, 192);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(296, 192);
			this.Name = "Settings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.Settings_Load);
			this.gbToolsSettings.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tpSettings.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		

		private void Settings_Load(object sender, System.EventArgs e)
		{
			int index = cbPing.FindString(Setting.PingType);
			cbPing.SelectedIndex = index;
			index = cbTraceRoute.FindString(Setting.TraceRouteType);
			cbTraceRoute.SelectedIndex = index;
		}

		private void cbPing_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Setting.PingType=cbPing.SelectedItem.ToString();
		}

		private void cbTraceRoute_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Setting.TraceRouteType=cbTraceRoute.SelectedItem.ToString();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{			
			this.DialogResult = DialogResult.OK;
			this.Close();
		}



	}
}
