using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PortScannerNS
{
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
	public class SettingForm : System.Windows.Forms.Form
	{
		public string FileName;
		private Configuration configuration;

		private System.Windows.Forms.GroupBox gbSessionSettings;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpSettings;
		private System.Windows.Forms.GroupBox gbPortScannerViewSetting;
		private System.Windows.Forms.TextBox txtDefaultFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnBrowse;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SettingForm()
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
			this.gbSessionSettings = new System.Windows.Forms.GroupBox();
			this.txtDefaultFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpSettings = new System.Windows.Forms.TabPage();
			this.gbSessionSettings.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbSessionSettings
			// 
			this.gbSessionSettings.Controls.Add(this.txtDefaultFile);
			this.gbSessionSettings.Controls.Add(this.label1);
			this.gbSessionSettings.Controls.Add(this.btnBrowse);
			this.gbSessionSettings.Location = new System.Drawing.Point(8, 8);
			this.gbSessionSettings.Name = "gbSessionSettings";
			this.gbSessionSettings.Size = new System.Drawing.Size(304, 112);
			this.gbSessionSettings.TabIndex = 0;
			this.gbSessionSettings.TabStop = false;
			this.gbSessionSettings.Text = "Port Scanner Settings";
			// 
			// txtDefaultFile
			// 
			this.txtDefaultFile.Location = new System.Drawing.Point(12, 53);
			this.txtDefaultFile.Name = "txtDefaultFile";
			this.txtDefaultFile.ReadOnly = true;
			this.txtDefaultFile.Size = new System.Drawing.Size(240, 20);
			this.txtDefaultFile.TabIndex = 4;
			this.txtDefaultFile.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Default File :";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(268, 53);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(24, 23);
			this.btnBrowse.TabIndex = 6;
			this.btnBrowse.Text = "...";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(152, 160);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "Okey";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(248, 160);
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
			this.tabControl1.Size = new System.Drawing.Size(328, 152);
			this.tabControl1.TabIndex = 4;
			// 
			// tpSettings
			// 
			this.tpSettings.Controls.Add(this.gbSessionSettings);
			this.tpSettings.Location = new System.Drawing.Point(4, 22);
			this.tpSettings.Name = "tpSettings";
			this.tpSettings.Size = new System.Drawing.Size(320, 126);
			this.tpSettings.TabIndex = 0;
			this.tpSettings.Text = "Settings";
			// 
			// SettingForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(328, 189);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Load += new System.EventHandler(this.PortScannerSettings_Load);
			this.gbSessionSettings.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tpSettings.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		
		private void PortScannerSettings_Load(object sender, System.EventArgs e)
		{			
			txtDefaultFile.Text = Setting.FileName;
			FileName            = Setting.FileName;
		}
		
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			Setting.FileName = txtDefaultFile.Text;
			FileName          = txtDefaultFile.Text;
			Setting.WriteToFile();
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

	
		private void btnBrowse_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
			
			DialogResult result = openFileDialog1.ShowDialog();
			
			if(result == DialogResult.OK) 
			{
				txtDefaultFile.Text=openFileDialog1.FileName;
				FileName=openFileDialog1.FileName;
			}
			else
			{
				txtDefaultFile.Text="";
				FileName="";
			}
		}



	}
}
