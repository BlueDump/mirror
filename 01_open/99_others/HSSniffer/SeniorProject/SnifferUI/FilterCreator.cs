using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using Sniffer;

namespace SnifferUI
{
	public enum FilterType { 
		Allow,
		Deny
	}

	/// <summary>
	/// Summary description for FilterCreator.
	/// </summary>
	public class FilterCreator : System.Windows.Forms.Form
	{
		public bool checkIP=true;
		public bool checkPort=true;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private FilterType Type_ = FilterType.Deny;
		public System.Windows.Forms.TextBox editDestinationPort;
		public System.Windows.Forms.TextBox editSourcePort;
		public System.Windows.Forms.TextBox editDestinationAddr;
		public System.Windows.Forms.TextBox editSourceAddr;
		private System.Windows.Forms.Label LabelDestination_;
		private System.Windows.Forms.Label LabelSource_;
		private System.Windows.Forms.RadioButton Deny_;
		private System.Windows.Forms.RadioButton Allow_;
		public System.Windows.Forms.RadioButton Select_;
		public System.Windows.Forms.RadioButton All_;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.ComboBox Protocol_;
		public System.Windows.Forms.GroupBox GroupPort_;
		private bool FilterAll_ = false;

		public FilterType Type { 
			get { return Type_; }
			set { Type_ = value; }
		}


		private bool FilterAll { 
			get { return FilterAll_; }
			set { FilterAll_ = value; }
		}


		public FilterCreator()
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
			this.editDestinationPort = new System.Windows.Forms.TextBox();
			this.editSourcePort = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.editDestinationAddr = new System.Windows.Forms.TextBox();
			this.editSourceAddr = new System.Windows.Forms.TextBox();
			this.LabelDestination_ = new System.Windows.Forms.Label();
			this.LabelSource_ = new System.Windows.Forms.Label();
			this.GroupPort_ = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.Deny_ = new System.Windows.Forms.RadioButton();
			this.Allow_ = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.Select_ = new System.Windows.Forms.RadioButton();
			this.All_ = new System.Windows.Forms.RadioButton();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.Protocol_ = new System.Windows.Forms.ComboBox();
			this.GroupPort_.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// editDestinationPort
			// 
			this.editDestinationPort.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.editDestinationPort.Location = new System.Drawing.Point(88, 48);
			this.editDestinationPort.Name = "editDestinationPort";
			this.editDestinationPort.Size = new System.Drawing.Size(48, 20);
			this.editDestinationPort.TabIndex = 23;
			this.editDestinationPort.Text = "";
			// 
			// editSourcePort
			// 
			this.editSourcePort.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.editSourcePort.Location = new System.Drawing.Point(88, 16);
			this.editSourcePort.Name = "editSourcePort";
			this.editSourcePort.Size = new System.Drawing.Size(48, 20);
			this.editSourcePort.TabIndex = 22;
			this.editSourcePort.Text = "";
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label4.Location = new System.Drawing.Point(8, 48);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 32);
			this.label4.TabIndex = 21;
			this.label4.Text = "Destination Port";
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label3.Location = new System.Drawing.Point(8, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 23);
			this.label3.TabIndex = 20;
			this.label3.Text = "Source Port";
			// 
			// editDestinationAddr
			// 
			this.editDestinationAddr.Location = new System.Drawing.Point(104, 136);
			this.editDestinationAddr.Name = "editDestinationAddr";
			this.editDestinationAddr.Size = new System.Drawing.Size(96, 20);
			this.editDestinationAddr.TabIndex = 19;
			this.editDestinationAddr.Text = "";
			// 
			// editSourceAddr
			// 
			this.editSourceAddr.Location = new System.Drawing.Point(104, 104);
			this.editSourceAddr.Name = "editSourceAddr";
			this.editSourceAddr.Size = new System.Drawing.Size(96, 20);
			this.editSourceAddr.TabIndex = 18;
			this.editSourceAddr.Text = "";
			// 
			// LabelDestination_
			// 
			this.LabelDestination_.Location = new System.Drawing.Point(24, 136);
			this.LabelDestination_.Name = "LabelDestination_";
			this.LabelDestination_.Size = new System.Drawing.Size(80, 23);
			this.LabelDestination_.TabIndex = 17;
			this.LabelDestination_.Text = "Destination IP";
			// 
			// LabelSource_
			// 
			this.LabelSource_.Location = new System.Drawing.Point(24, 104);
			this.LabelSource_.Name = "LabelSource_";
			this.LabelSource_.Size = new System.Drawing.Size(64, 23);
			this.LabelSource_.TabIndex = 16;
			this.LabelSource_.Text = "Source IP";
			// 
			// GroupPort_
			// 
			this.GroupPort_.Controls.Add(this.editSourcePort);
			this.GroupPort_.Controls.Add(this.editDestinationPort);
			this.GroupPort_.Controls.Add(this.label3);
			this.GroupPort_.Controls.Add(this.label4);
			this.GroupPort_.Location = new System.Drawing.Point(208, 88);
			this.GroupPort_.Name = "GroupPort_";
			this.GroupPort_.Size = new System.Drawing.Size(144, 88);
			this.GroupPort_.TabIndex = 24;
			this.GroupPort_.TabStop = false;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 23);
			this.label5.TabIndex = 26;
			this.label5.Text = "Protocol";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.Deny_);
			this.groupBox1.Controls.Add(this.Allow_);
			this.groupBox1.Location = new System.Drawing.Point(208, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(144, 80);
			this.groupBox1.TabIndex = 27;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Type";
			// 
			// Deny_
			// 
			this.Deny_.Checked = true;
			this.Deny_.Location = new System.Drawing.Point(24, 40);
			this.Deny_.Name = "Deny_";
			this.Deny_.TabIndex = 1;
			this.Deny_.TabStop = true;
			this.Deny_.Text = "Deny";
			this.Deny_.Click += new System.EventHandler(this.OnDenyClicked);
			// 
			// Allow_
			// 
			this.Allow_.Location = new System.Drawing.Point(24, 16);
			this.Allow_.Name = "Allow_";
			this.Allow_.TabIndex = 0;
			this.Allow_.Text = "Allow";
			this.Allow_.Click += new System.EventHandler(this.OnAllowClicked);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.Select_);
			this.groupBox2.Controls.Add(this.All_);
			this.groupBox2.Location = new System.Drawing.Point(24, 40);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(176, 48);
			this.groupBox2.TabIndex = 28;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Packet Selection";
			// 
			// Select_
			// 
			this.Select_.Checked = true;
			this.Select_.Location = new System.Drawing.Point(80, 16);
			this.Select_.Name = "Select_";
			this.Select_.Size = new System.Drawing.Size(72, 24);
			this.Select_.TabIndex = 1;
			this.Select_.TabStop = true;
			this.Select_.Text = "Individual";
			this.Select_.Click += new System.EventHandler(this.OnSelectClicked);
			// 
			// All_
			// 
			this.All_.Location = new System.Drawing.Point(16, 16);
			this.All_.Name = "All_";
			this.All_.Size = new System.Drawing.Size(40, 24);
			this.All_.TabIndex = 0;
			this.All_.Text = "All";
			this.All_.Click += new System.EventHandler(this.OnAllClicked);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(192, 184);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 29;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.OnOkClicked);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(272, 184);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 30;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCancelClicked);
			// 
			// Protocol_
			// 
			this.Protocol_.ItemHeight = 13;
			this.Protocol_.Items.AddRange(new object[] {
														   "Tcp",
														   "Udp",
														   "ICMP"});
			this.Protocol_.Location = new System.Drawing.Point(96, 16);
			this.Protocol_.Name = "Protocol_";
			this.Protocol_.Size = new System.Drawing.Size(104, 21);
			this.Protocol_.TabIndex = 25;
			this.Protocol_.Text = "Tcp";
			this.Protocol_.SelectedIndexChanged += new System.EventHandler(this.OnProtocolSelectionChanged);
			// 
			// FilterCreator
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(370, 215);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.Protocol_);
			this.Controls.Add(this.GroupPort_);
			this.Controls.Add(this.editDestinationAddr);
			this.Controls.Add(this.editSourceAddr);
			this.Controls.Add(this.LabelDestination_);
			this.Controls.Add(this.LabelSource_);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(376, 247);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(376, 247);
			this.Name = "FilterCreator";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FilterCreator";
			this.GroupPort_.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void OnProtocolSelectionChanged(object sender, System.EventArgs e) {
			Debug.Assert(Protocol_.SelectedItem is String,"The selected item is not a String");
			String text = (String)Protocol_.SelectedItem;
			switch ( text ) { 
				case "Tcp":
				case "Udp":
					if ( !this.FilterAll ) {
						this.ShowIPs();
						this.ShowPorts();
					}
					break;
				case "ICMP":
					if ( !this.FilterAll ) { 
						this.ShowIPs();
						this.HidePorts();
					}
					break;
				default:
					Debug.Assert(false,"Invalid type");
					break;
			}
		}

		private void OnAllClicked(object sender, System.EventArgs e) {
			FilterAll = true;
			this.HideIPs();
			this.HidePorts();
		}

		private void OnSelectClicked(object sender, System.EventArgs e) {
			FilterAll = false;
			this.OnProtocolSelectionChanged(null,null);
		}

		private void OnOkClicked(object sender, System.EventArgs e) {
			FilterItem FItem=new FilterItem();

			if (Type==FilterType.Allow)
				FItem.mode=true;
			else
				FItem.mode=false;
			if (checkIP)
			{
				if ( editSourceAddr.Text != "" ) 
				{ 
					IPAddress IPAdrees = null;
					String ip = editSourceAddr.Text;
					try 
					{ 
						IPAdrees = IPAddress.Parse(ip); 
						FItem.SourceAddress = editSourceAddr.Text;
					} 
					catch ( Exception ) 
					{ 
						MessageBox.Show(this,"You must enter a valid Source IP Address","Invalid IP",MessageBoxButtons.OK,MessageBoxIcon.Error);
						editSourceAddr.Text = "";
						editSourceAddr.Focus();
						return;
					}
				}
				if ( editDestinationAddr.Text != "" ) 
				{ 
					

					IPAddress IPAdrees = null;
					String ip = editDestinationAddr.Text;
					try 
					{ 
						IPAdrees = IPAddress.Parse(ip); 
						FItem.DestinationAddress = editDestinationAddr.Text;
					} 
					catch ( Exception ) 
					{ 
						MessageBox.Show(this,"You must enter a valid Destination IP Address","Invalid IP",MessageBoxButtons.OK,MessageBoxIcon.Error);
						editDestinationAddr.Text = "";
						editDestinationAddr.Focus();
						return;
					}
				}
			}
			if (checkPort)
			{
				try 
				{ 
					if ( editSourcePort.Text != "" ) 
					{ 
						FItem.SourcePort = Int32.Parse(editSourcePort.Text);
					}
					if ( editDestinationPort.Text != "") 
					{
						FItem.DestinationPort = Int32.Parse(editDestinationPort.Text);
					}
				} 
				catch ( Exception  ) 
				{ 
					MessageBox.Show(this.ParentForm,"Error.  Please enter valid port numbers");
					return;
				}
			}
			
			switch ( (String)Protocol_.SelectedItem) { 
				case "Tcp":
					FItem.protocol="Tcp";
					break;
				case "Udp":
					FItem.protocol="Udp";
					break;
				case "ICMP":
					FItem.protocol="ICMP";
					break;
				default:
					FItem.protocol="";
					break;
			}
			FilterManager.addFilter(FItem);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void OnCancelClicked(object sender, System.EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
		}

		private void HidePorts() { 
			checkPort=false;
			this.GroupPort_.Hide();
		}

		private void ShowPorts() { 
			checkPort=true;
			this.GroupPort_.Show();
		}

		private void HideIPs() {
			checkIP=false;
			this.editSourceAddr.Hide();
			this.editDestinationAddr.Hide();
			this.LabelDestination_.Hide();
			this.LabelSource_.Hide();
		}

		private void ShowIPs() { 
			checkIP=true;
			this.editSourceAddr.Show();
			this.editDestinationAddr.Show();
			this.LabelDestination_.Show();
			this.LabelSource_.Show();
		}

		private void OnAllowClicked(object sender, System.EventArgs e) {
			Type = FilterType.Allow;
		}

		private void OnDenyClicked(object sender, System.EventArgs e) {
			Type_ = FilterType.Deny;
		}
	}
}
