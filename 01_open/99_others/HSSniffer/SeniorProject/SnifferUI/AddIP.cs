using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SnifferUI
{
	/// <summary>
	/// Summary description for AddIP.
	/// </summary>
	public class AddIP : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Label label1;
		public ArrayList ips=new ArrayList();
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddIP(ArrayList Ips)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			for(int i=0;i<Ips.Count;i++)
			{
				ips.Add(Ips[i]);
			}

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
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.Location = new System.Drawing.Point(16, 24);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(200, 173);
			this.listBox1.TabIndex = 0;
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(8, 208);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(48, 23);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(56, 208);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(56, 23);
			this.btnRemove.TabIndex = 2;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(200, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Ip Addresses To Sniff";
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(176, 208);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(48, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(128, 208);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(48, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "Ok";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// AddIP
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(232, 230);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.listBox1);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(240, 264);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(240, 264);
			this.Name = "AddIP";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AddIP";
			this.Load += new System.EventHandler(this.AddIP_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void AddIP_Load(object sender, System.EventArgs e)
		{
			for(int i=0;i<ips.Count;i++)
			{
				listBox1.Items.Add((string)ips[i]);
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			DialogResult result;
			String ip = null;
			AddressInput input = new AddressInput();
			result = input.ShowDialog(this);
			if ( result == DialogResult.OK ) 
			{ 
				ip = input.IP.ToString();
			}
			else if(result == DialogResult.Cancel)
			{
				return;
			}
			input.Dispose();
			if (ip!=null)
			{
				if (!ips.Contains(ip))
				{
					ips.Add(ip);
					listBox1.Items.Add(ip);
				}
			}
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			if(listBox1.SelectedIndex!=-1){
				ips.Remove(listBox1.Items[listBox1.SelectedIndex]);
				listBox1.Items.RemoveAt(listBox1.SelectedIndex);
			}
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
