using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;

namespace SnifferUI
{
	/// <summary>
	/// Summary description for AddressInput.
	/// </summary>
	public class AddressInput : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private IPAddress IPAdrees = null;

		public IPAddress IP { 
			get { return IPAdrees; }
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox editIP;

		public AddressInput() 
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
			this.label1 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.editIP = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "IP Address Or Host Name";
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(136, 72);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "OK";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(224, 72);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "CANCEL";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// editIP
			// 
			this.editIP.Location = new System.Drawing.Point(144, 32);
			this.editIP.Name = "editIP";
			this.editIP.Size = new System.Drawing.Size(152, 20);
			this.editIP.TabIndex = 7;
			this.editIP.Text = "";
			// 
			// AddressInput
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(330, 103);
			this.Controls.Add(this.editIP);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "AddressInput";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AddressInput";
			this.Load += new System.EventHandler(this.AddressInput_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void AddressInput_Load(object sender, System.EventArgs e) {			
			this.editIP.Text=Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();			
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			String ip = editIP.Text;
			bool   valid = false;
			try 
			{ 
				IPAdrees = IPAddress.Parse(ip);
				valid = true; 
			} 
			catch ( Exception ) 
			{ 
				try 
				{
					IPAdrees=Dns.Resolve(ip).AddressList[0];
					valid=true;
				}
				catch(Exception)
				{
					MessageBox.Show(this,"You must enter a valid IP Address","Invalid IP",MessageBoxButtons.OK,MessageBoxIcon.Error);
					editIP.Text = "";
					editIP.Focus();
				}
			}
			
			if ( valid ) 
			{ 
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

	}
}
