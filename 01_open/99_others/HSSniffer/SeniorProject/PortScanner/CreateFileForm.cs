using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Xml;
using System.Windows.Forms;

namespace PortScannerNS
{
	/// <summary>
	/// Summary description for CreateSessionFile.
	/// </summary>
	public class CreateFileForm : System.Windows.Forms.Form
	{
		public  PortScanner Owener;
		private XmlDocument doc = new XmlDocument();
		private Configuration conf = new Configuration();

		private System.Windows.Forms.GroupBox gbSessionSettings;
		private System.Windows.Forms.GroupBox gbMailServer;
		private System.Windows.Forms.Label lblMailServer;
		private System.Windows.Forms.Label lblmailSenderEMailAddress;
		private System.Windows.Forms.TextBox txtMailServer;
		private System.Windows.Forms.TextBox txtmailSenderEMailAddress;
		private System.Windows.Forms.GroupBox gbProcessOptions;
		private System.Windows.Forms.Label lblLoopCount;
		private System.Windows.Forms.Label lblLoopInterval;
		private System.Windows.Forms.TextBox txtLoopCount;
		private System.Windows.Forms.TextBox txtLoopInterval;
		private System.Windows.Forms.GroupBox gbBottom;
		private System.Windows.Forms.Button btnRemovePort;
		private System.Windows.Forms.Button btnAddPort;
		private System.Windows.Forms.ListBox lstboxPort;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPort;
		private System.Windows.Forms.GroupBox gbServers;
		private System.Windows.Forms.Label lblAdd;
		private System.Windows.Forms.TextBox txtServer;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.TextBox txtMailReceiver;
		private System.Windows.Forms.Label lblMailReciecer;
		private System.Windows.Forms.ListBox lstboxMailReciever;
		private System.Windows.Forms.Button btnMailRecieverRemove;
		private System.Windows.Forms.Button btnMailRecieverAdd;
		private System.Windows.Forms.TextBox txtPortBegin;
		private System.Windows.Forms.CheckBox chckboxEnableRange;
		private System.Windows.Forms.TextBox txtPortEnd;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox gbMailReciever;
		private System.Windows.Forms.GroupBox gbPort;
		private System.Windows.Forms.CheckBox chckboxMailEnabled;
		private System.Windows.Forms.CheckedListBox checkedListBoxServers;
		private System.Windows.Forms.Label lblError;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CreateFileForm()
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
			this.chckboxMailEnabled = new System.Windows.Forms.CheckBox();
			this.gbBottom = new System.Windows.Forms.GroupBox();
			this.gbMailReciever = new System.Windows.Forms.GroupBox();
			this.btnMailRecieverRemove = new System.Windows.Forms.Button();
			this.btnMailRecieverAdd = new System.Windows.Forms.Button();
			this.lstboxMailReciever = new System.Windows.Forms.ListBox();
			this.txtMailReceiver = new System.Windows.Forms.TextBox();
			this.lblMailReciecer = new System.Windows.Forms.Label();
			this.gbServers = new System.Windows.Forms.GroupBox();
			this.checkedListBoxServers = new System.Windows.Forms.CheckedListBox();
			this.lblAdd = new System.Windows.Forms.Label();
			this.txtServer = new System.Windows.Forms.TextBox();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.gbPort = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblError = new System.Windows.Forms.Label();
			this.txtPortEnd = new System.Windows.Forms.TextBox();
			this.chckboxEnableRange = new System.Windows.Forms.CheckBox();
			this.txtPortBegin = new System.Windows.Forms.TextBox();
			this.btnRemovePort = new System.Windows.Forms.Button();
			this.btnAddPort = new System.Windows.Forms.Button();
			this.lstboxPort = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.gbProcessOptions = new System.Windows.Forms.GroupBox();
			this.txtLoopInterval = new System.Windows.Forms.TextBox();
			this.txtLoopCount = new System.Windows.Forms.TextBox();
			this.lblLoopInterval = new System.Windows.Forms.Label();
			this.lblLoopCount = new System.Windows.Forms.Label();
			this.gbMailServer = new System.Windows.Forms.GroupBox();
			this.txtmailSenderEMailAddress = new System.Windows.Forms.TextBox();
			this.txtMailServer = new System.Windows.Forms.TextBox();
			this.lblmailSenderEMailAddress = new System.Windows.Forms.Label();
			this.lblMailServer = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.gbSessionSettings.SuspendLayout();
			this.gbBottom.SuspendLayout();
			this.gbMailReciever.SuspendLayout();
			this.gbServers.SuspendLayout();
			this.gbPort.SuspendLayout();
			this.gbProcessOptions.SuspendLayout();
			this.gbMailServer.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbSessionSettings
			// 
			this.gbSessionSettings.Controls.Add(this.chckboxMailEnabled);
			this.gbSessionSettings.Controls.Add(this.gbBottom);
			this.gbSessionSettings.Controls.Add(this.gbProcessOptions);
			this.gbSessionSettings.Controls.Add(this.gbMailServer);
			this.gbSessionSettings.Location = new System.Drawing.Point(8, 8);
			this.gbSessionSettings.Name = "gbSessionSettings";
			this.gbSessionSettings.Size = new System.Drawing.Size(528, 352);
			this.gbSessionSettings.TabIndex = 0;
			this.gbSessionSettings.TabStop = false;
			this.gbSessionSettings.Text = "Session Settings File";
			// 
			// chckboxMailEnabled
			// 
			this.chckboxMailEnabled.Location = new System.Drawing.Point(16, 16);
			this.chckboxMailEnabled.Name = "chckboxMailEnabled";
			this.chckboxMailEnabled.Size = new System.Drawing.Size(104, 16);
			this.chckboxMailEnabled.TabIndex = 1;
			this.chckboxMailEnabled.Text = "Mail Enabled";
			this.chckboxMailEnabled.CheckedChanged += new System.EventHandler(this.chckboxMailEnabled_CheckedChanged);
			// 
			// gbBottom
			// 
			this.gbBottom.Controls.Add(this.gbMailReciever);
			this.gbBottom.Controls.Add(this.gbServers);
			this.gbBottom.Controls.Add(this.gbPort);
			this.gbBottom.Location = new System.Drawing.Point(8, 112);
			this.gbBottom.Name = "gbBottom";
			this.gbBottom.Size = new System.Drawing.Size(512, 232);
			this.gbBottom.TabIndex = 2;
			this.gbBottom.TabStop = false;
			// 
			// gbMailReciever
			// 
			this.gbMailReciever.Controls.Add(this.btnMailRecieverRemove);
			this.gbMailReciever.Controls.Add(this.btnMailRecieverAdd);
			this.gbMailReciever.Controls.Add(this.lstboxMailReciever);
			this.gbMailReciever.Controls.Add(this.txtMailReceiver);
			this.gbMailReciever.Controls.Add(this.lblMailReciecer);
			this.gbMailReciever.Location = new System.Drawing.Point(0, 144);
			this.gbMailReciever.Name = "gbMailReciever";
			this.gbMailReciever.Size = new System.Drawing.Size(512, 88);
			this.gbMailReciever.TabIndex = 12;
			this.gbMailReciever.TabStop = false;
			this.gbMailReciever.Text = "Mail Recievers";
			this.gbMailReciever.Enter += new System.EventHandler(this.gbMailReciever_Enter);
			// 
			// btnMailRecieverRemove
			// 
			this.btnMailRecieverRemove.Enabled = false;
			this.btnMailRecieverRemove.Location = new System.Drawing.Point(248, 64);
			this.btnMailRecieverRemove.Name = "btnMailRecieverRemove";
			this.btnMailRecieverRemove.Size = new System.Drawing.Size(24, 23);
			this.btnMailRecieverRemove.TabIndex = 17;
			this.btnMailRecieverRemove.Text = "<";
			this.btnMailRecieverRemove.Click += new System.EventHandler(this.btnMailRecieverRemove_Click);
			// 
			// btnMailRecieverAdd
			// 
			this.btnMailRecieverAdd.Enabled = false;
			this.btnMailRecieverAdd.Location = new System.Drawing.Point(248, 40);
			this.btnMailRecieverAdd.Name = "btnMailRecieverAdd";
			this.btnMailRecieverAdd.Size = new System.Drawing.Size(24, 23);
			this.btnMailRecieverAdd.TabIndex = 16;
			this.btnMailRecieverAdd.Text = ">";
			this.btnMailRecieverAdd.Click += new System.EventHandler(this.btnMailRecieverAdd_Click);
			// 
			// lstboxMailReciever
			// 
			this.lstboxMailReciever.Enabled = false;
			this.lstboxMailReciever.Location = new System.Drawing.Point(280, 16);
			this.lstboxMailReciever.Name = "lstboxMailReciever";
			this.lstboxMailReciever.Size = new System.Drawing.Size(216, 69);
			this.lstboxMailReciever.TabIndex = 50;
			// 
			// txtMailReceiver
			// 
			this.txtMailReceiver.Enabled = false;
			this.txtMailReceiver.Location = new System.Drawing.Point(112, 16);
			this.txtMailReceiver.Name = "txtMailReceiver";
			this.txtMailReceiver.Size = new System.Drawing.Size(160, 20);
			this.txtMailReceiver.TabIndex = 15;
			this.txtMailReceiver.Text = "";
			// 
			// lblMailReciecer
			// 
			this.lblMailReciecer.Location = new System.Drawing.Point(32, 16);
			this.lblMailReciecer.Name = "lblMailReciecer";
			this.lblMailReciecer.Size = new System.Drawing.Size(80, 16);
			this.lblMailReciecer.TabIndex = 0;
			this.lblMailReciecer.Text = "Mail Receicer";
			// 
			// gbServers
			// 
			this.gbServers.Controls.Add(this.checkedListBoxServers);
			this.gbServers.Controls.Add(this.lblAdd);
			this.gbServers.Controls.Add(this.txtServer);
			this.gbServers.Controls.Add(this.btnRemove);
			this.gbServers.Controls.Add(this.btnAdd);
			this.gbServers.Location = new System.Drawing.Point(0, 0);
			this.gbServers.Name = "gbServers";
			this.gbServers.Size = new System.Drawing.Size(280, 144);
			this.gbServers.TabIndex = 7;
			this.gbServers.TabStop = false;
			this.gbServers.Text = "Servers";
			this.gbServers.Enter += new System.EventHandler(this.gbServers_Enter);
			// 
			// checkedListBoxServers
			// 
			this.checkedListBoxServers.Location = new System.Drawing.Point(32, 40);
			this.checkedListBoxServers.Name = "checkedListBoxServers";
			this.checkedListBoxServers.Size = new System.Drawing.Size(240, 64);
			this.checkedListBoxServers.TabIndex = 25;
			this.checkedListBoxServers.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxServers_SelectedIndexChanged);
			// 
			// lblAdd
			// 
			this.lblAdd.Location = new System.Drawing.Point(32, 16);
			this.lblAdd.Name = "lblAdd";
			this.lblAdd.Size = new System.Drawing.Size(56, 23);
			this.lblAdd.TabIndex = 10;
			this.lblAdd.Text = "Server";
			// 
			// txtServer
			// 
			this.txtServer.Location = new System.Drawing.Point(120, 16);
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new System.Drawing.Size(152, 20);
			this.txtServer.TabIndex = 6;
			this.txtServer.Text = "";
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(216, 112);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(56, 24);
			this.btnRemove.TabIndex = 8;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(152, 112);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(56, 23);
			this.btnAdd.TabIndex = 7;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// gbPort
			// 
			this.gbPort.Controls.Add(this.label3);
			this.gbPort.Controls.Add(this.label2);
			this.gbPort.Controls.Add(this.lblError);
			this.gbPort.Controls.Add(this.txtPortEnd);
			this.gbPort.Controls.Add(this.chckboxEnableRange);
			this.gbPort.Controls.Add(this.txtPortBegin);
			this.gbPort.Controls.Add(this.btnRemovePort);
			this.gbPort.Controls.Add(this.btnAddPort);
			this.gbPort.Controls.Add(this.lstboxPort);
			this.gbPort.Controls.Add(this.label1);
			this.gbPort.Controls.Add(this.txtPort);
			this.gbPort.Location = new System.Drawing.Point(280, 0);
			this.gbPort.Name = "gbPort";
			this.gbPort.Size = new System.Drawing.Size(232, 144);
			this.gbPort.TabIndex = 6;
			this.gbPort.TabStop = false;
			this.gbPort.Text = "Ports";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(88, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(20, 23);
			this.label3.TabIndex = 32;
			this.label3.Text = "to";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 23);
			this.label2.TabIndex = 31;
			this.label2.Text = "from";
			// 
			// lblError
			// 
			this.lblError.Location = new System.Drawing.Point(16, 120);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(200, 16);
			this.lblError.TabIndex = 19;
			// 
			// txtPortEnd
			// 
			this.txtPortEnd.Enabled = false;
			this.txtPortEnd.Location = new System.Drawing.Point(112, 96);
			this.txtPortEnd.Name = "txtPortEnd";
			this.txtPortEnd.Size = new System.Drawing.Size(48, 20);
			this.txtPortEnd.TabIndex = 14;
			this.txtPortEnd.Text = "";
			this.txtPortEnd.Leave += new System.EventHandler(this.txtPortEnd_Leave);
			// 
			// chckboxEnableRange
			// 
			this.chckboxEnableRange.Location = new System.Drawing.Point(24, 72);
			this.chckboxEnableRange.Name = "chckboxEnableRange";
			this.chckboxEnableRange.Size = new System.Drawing.Size(96, 24);
			this.chckboxEnableRange.TabIndex = 12;
			this.chckboxEnableRange.Text = "EnableRange";
			this.chckboxEnableRange.CheckedChanged += new System.EventHandler(this.chckboxEnableRange_CheckedChanged);
			// 
			// txtPortBegin
			// 
			this.txtPortBegin.Enabled = false;
			this.txtPortBegin.Location = new System.Drawing.Point(40, 96);
			this.txtPortBegin.Name = "txtPortBegin";
			this.txtPortBegin.Size = new System.Drawing.Size(48, 20);
			this.txtPortBegin.TabIndex = 13;
			this.txtPortBegin.Text = "";
			// 
			// btnRemovePort
			// 
			this.btnRemovePort.Location = new System.Drawing.Point(120, 56);
			this.btnRemovePort.Name = "btnRemovePort";
			this.btnRemovePort.Size = new System.Drawing.Size(24, 23);
			this.btnRemovePort.TabIndex = 11;
			this.btnRemovePort.Text = "<";
			this.btnRemovePort.Click += new System.EventHandler(this.btnRemovePort_Click);
			// 
			// btnAddPort
			// 
			this.btnAddPort.Location = new System.Drawing.Point(120, 32);
			this.btnAddPort.Name = "btnAddPort";
			this.btnAddPort.Size = new System.Drawing.Size(24, 23);
			this.btnAddPort.TabIndex = 10;
			this.btnAddPort.Text = ">";
			this.btnAddPort.Click += new System.EventHandler(this.btnAddPort_Click);
			// 
			// lstboxPort
			// 
			this.lstboxPort.Location = new System.Drawing.Point(168, 24);
			this.lstboxPort.Name = "lstboxPort";
			this.lstboxPort.Size = new System.Drawing.Size(48, 82);
			this.lstboxPort.TabIndex = 30;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 23);
			this.label1.TabIndex = 12;
			this.label1.Text = "Port";
			// 
			// txtPort
			// 
			this.txtPort.Location = new System.Drawing.Point(24, 40);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(64, 20);
			this.txtPort.TabIndex = 9;
			this.txtPort.Text = "";
			// 
			// gbProcessOptions
			// 
			this.gbProcessOptions.Controls.Add(this.txtLoopInterval);
			this.gbProcessOptions.Controls.Add(this.txtLoopCount);
			this.gbProcessOptions.Controls.Add(this.lblLoopInterval);
			this.gbProcessOptions.Controls.Add(this.lblLoopCount);
			this.gbProcessOptions.Location = new System.Drawing.Point(288, 32);
			this.gbProcessOptions.Name = "gbProcessOptions";
			this.gbProcessOptions.Size = new System.Drawing.Size(232, 80);
			this.gbProcessOptions.TabIndex = 1;
			this.gbProcessOptions.TabStop = false;
			this.gbProcessOptions.Text = "Process Options";
			// 
			// txtLoopInterval
			// 
			this.txtLoopInterval.Location = new System.Drawing.Point(120, 56);
			this.txtLoopInterval.Name = "txtLoopInterval";
			this.txtLoopInterval.Size = new System.Drawing.Size(96, 20);
			this.txtLoopInterval.TabIndex = 5;
			this.txtLoopInterval.Text = "3600";
			// 
			// txtLoopCount
			// 
			this.txtLoopCount.Location = new System.Drawing.Point(120, 24);
			this.txtLoopCount.Name = "txtLoopCount";
			this.txtLoopCount.Size = new System.Drawing.Size(96, 20);
			this.txtLoopCount.TabIndex = 4;
			this.txtLoopCount.Text = "0";
			// 
			// lblLoopInterval
			// 
			this.lblLoopInterval.Location = new System.Drawing.Point(16, 48);
			this.lblLoopInterval.Name = "lblLoopInterval";
			this.lblLoopInterval.Size = new System.Drawing.Size(104, 24);
			this.lblLoopInterval.TabIndex = 1;
			this.lblLoopInterval.Text = "Loop Interval           (in Seconds)";
			// 
			// lblLoopCount
			// 
			this.lblLoopCount.Location = new System.Drawing.Point(16, 24);
			this.lblLoopCount.Name = "lblLoopCount";
			this.lblLoopCount.TabIndex = 0;
			this.lblLoopCount.Text = "Loop Count";
			// 
			// gbMailServer
			// 
			this.gbMailServer.Controls.Add(this.txtmailSenderEMailAddress);
			this.gbMailServer.Controls.Add(this.txtMailServer);
			this.gbMailServer.Controls.Add(this.lblmailSenderEMailAddress);
			this.gbMailServer.Controls.Add(this.lblMailServer);
			this.gbMailServer.Location = new System.Drawing.Point(8, 32);
			this.gbMailServer.Name = "gbMailServer";
			this.gbMailServer.Size = new System.Drawing.Size(280, 80);
			this.gbMailServer.TabIndex = 0;
			this.gbMailServer.TabStop = false;
			this.gbMailServer.Text = "Mail Server";
			// 
			// txtmailSenderEMailAddress
			// 
			this.txtmailSenderEMailAddress.Enabled = false;
			this.txtmailSenderEMailAddress.Location = new System.Drawing.Point(112, 48);
			this.txtmailSenderEMailAddress.Name = "txtmailSenderEMailAddress";
			this.txtmailSenderEMailAddress.Size = new System.Drawing.Size(160, 20);
			this.txtmailSenderEMailAddress.TabIndex = 3;
			this.txtmailSenderEMailAddress.Text = "";
			// 
			// txtMailServer
			// 
			this.txtMailServer.Enabled = false;
			this.txtMailServer.Location = new System.Drawing.Point(112, 24);
			this.txtMailServer.Name = "txtMailServer";
			this.txtMailServer.Size = new System.Drawing.Size(160, 20);
			this.txtMailServer.TabIndex = 2;
			this.txtMailServer.Text = "";
			// 
			// lblmailSenderEMailAddress
			// 
			this.lblmailSenderEMailAddress.Location = new System.Drawing.Point(8, 48);
			this.lblmailSenderEMailAddress.Name = "lblmailSenderEMailAddress";
			this.lblmailSenderEMailAddress.Size = new System.Drawing.Size(104, 23);
			this.lblmailSenderEMailAddress.TabIndex = 1;
			this.lblmailSenderEMailAddress.Text = "Sender e-mail";
			// 
			// lblMailServer
			// 
			this.lblMailServer.Location = new System.Drawing.Point(8, 24);
			this.lblMailServer.Name = "lblMailServer";
			this.lblMailServer.TabIndex = 0;
			this.lblMailServer.Text = "Mail Server";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(368, 368);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 24);
			this.btnOK.TabIndex = 18;
			this.btnOK.Text = "Okay";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(456, 368);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 19;
			this.btnCancel.Text = "Cancel";
			// 
			// CreateSessionFile
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(544, 397);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.gbSessionSettings);
			this.Name = "CreateSessionFile";
			this.Text = "CreateSessionFile";
			this.gbSessionSettings.ResumeLayout(false);
			this.gbBottom.ResumeLayout(false);
			this.gbMailReciever.ResumeLayout(false);
			this.gbServers.ResumeLayout(false);
			this.gbPort.ResumeLayout(false);
			this.gbProcessOptions.ResumeLayout(false);
			this.gbMailServer.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if (conf.ServerNames.Count==0)
			{ 
				DialogResult result;
				result = MessageBox.Show("No server added so no change will be made to the file...","Warning",MessageBoxButtons.RetryCancel,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);
				if (result == DialogResult.Cancel)
					this.Close();
				return;
			}

			saveFileDialog1.CreatePrompt = true;
			saveFileDialog1.OverwritePrompt = true;
			
			saveFileDialog1.DefaultExt = "xml";
			saveFileDialog1.Filter = "Text files (*.xml)|*.xml";
			saveFileDialog1.InitialDirectory = Environment.CurrentDirectory;

			DialogResult result1 = saveFileDialog1.ShowDialog();
			
			if (result1 == DialogResult.OK)
			{
				Owener.newFileName = saveFileDialog1.FileName;
			}

			
			doc.LoadXml("<configuration><settings></settings></configuration>");
			XmlNode XMLconfiguration = doc.DocumentElement;
			XmlNode XMLsettings=XMLconfiguration.LastChild;

			if (chckboxMailEnabled.Checked)
			{	
				
				XmlAttribute XMLAtt =  doc.CreateAttribute("mailServer");
				XMLAtt.Value=txtMailServer.Text;
				XMLsettings.Attributes.Append(XMLAtt);

				XMLAtt = doc.CreateAttribute("mailSenderEMailAddress");
				XMLAtt.Value=txtmailSenderEMailAddress.Text;
				XMLsettings.Attributes.Append(XMLAtt);

				XMLAtt = doc.CreateAttribute("mailPortNotAvailableSubject");
				XMLAtt.Value="[PortScanner] $(Now): Some Ports on Host '$(HostName)' did not respond";
				XMLsettings.Attributes.Append(XMLAtt);

				XMLAtt = doc.CreateAttribute("loopCount");
				XMLAtt.Value=txtLoopCount.Text;
				XMLsettings.Attributes.Append(XMLAtt);

				XMLAtt = doc.CreateAttribute("loopIntervalSeconds");
				XMLAtt.Value=txtLoopInterval.Text;
				XMLsettings.Attributes.Append(XMLAtt);

				XmlNode XMLchildOfSettings = doc.CreateElement("mailNotAvailableBody");
				XMLchildOfSettings.AppendChild(doc.CreateCDataSection("The following ports on host '$(HostName)' did not respond:$(Ports)"));
				XMLsettings.AppendChild(XMLchildOfSettings);

				XMLchildOfSettings = doc.CreateElement("mailNotAvailableBodyPort");
				XMLchildOfSettings.AppendChild(doc.CreateCDataSection("Port '$(Port)', error message: $(ErrorMessage)."));
				XMLsettings.AppendChild(XMLchildOfSettings);
			}
			
			XmlNode XMLservers = doc.CreateElement("servers");
			XMLconfiguration.AppendChild(XMLservers);
			
			XmlNode XMLserver;

			for (int i=0 ; i<conf.ServerNames.Count;i++)
			{
				Server s = (Server)conf.ServerTable[((string)conf.ServerNames[i])];
				XMLserver = doc.CreateElement("server");
				
				XmlAttribute XMLserverAtt = doc.CreateAttribute("address");
				XMLserverAtt.Value=(string)conf.ServerNames[i];
				XMLserver.Attributes.Append(XMLserverAtt);
								
				bool isAlive = checkedListBoxServers.CheckedItems.Contains((string)conf.ServerNames[i]);
				XMLserverAtt = doc.CreateAttribute("isAlive");
				XMLserverAtt.Value=isAlive.ToString();
				XMLserver.Attributes.Append(XMLserverAtt);

				XMLservers.AppendChild(XMLserver);

				if (chckboxMailEnabled.Checked)
				{
					XmlNode XMLchildOfServer = doc.CreateElement("mailReceiverEMailAddresses");
					XmlNode XMLGchildOfServer;
					XmlAttribute XmlAtt;
					for (int index=0;index<s.mailAdressList.Count;index++)
					{
						XMLGchildOfServer = doc.CreateElement("address");
						XmlAtt = doc.CreateAttribute("address");
						XmlAtt.Value= (string)s.mailAdressList[index];
						XMLGchildOfServer.Attributes.Append(XmlAtt);
						XMLchildOfServer.AppendChild(XMLGchildOfServer);
					}
					XMLserver.AppendChild(XMLchildOfServer);
				}

				XmlNode XMLports = doc.CreateElement("ports");
				XMLserver.AppendChild(XMLports);
				
				XmlNode XMLport;
				XmlAttribute XMLportAtt;

				if (!chckboxEnableRange.Checked)
				{
					foreach (string key in s.PortTable.Keys)
					{						
						XMLport = doc.CreateElement("port");
					
						XMLportAtt = doc.CreateAttribute("number");
						XMLportAtt.Value= key.ToString();
						XMLport.Attributes.Append(XMLportAtt);

						XMLportAtt = doc.CreateAttribute("addressFamily");
						XMLportAtt.Value="InterNetwork";
						XMLport.Attributes.Append(XMLportAtt);

						XMLportAtt = doc.CreateAttribute("socketType");
						XMLportAtt.Value="Stream";
						XMLport.Attributes.Append(XMLportAtt);

						XMLportAtt = doc.CreateAttribute("protocolType");
						XMLportAtt.Value="Tcp";
						XMLport.Attributes.Append(XMLportAtt);

						XMLports.AppendChild(XMLport);
					}
				}
				else
				{
					foreach (string key in s.PortTable.Keys)
					{
						XMLport = doc.CreateElement("port");
						Port p = (Port) s.PortTable[key];
						XMLportAtt = doc.CreateAttribute("numberRangeBegin");
						XMLportAtt.Value=p.NumberRangeBegin.ToString();
						XMLport.Attributes.Append(XMLportAtt);

						XMLportAtt = doc.CreateAttribute("numberRangeEnd");
						XMLportAtt.Value=p.NumberRangeEnd.ToString();
						XMLport.Attributes.Append(XMLportAtt);

						XMLportAtt = doc.CreateAttribute("addressFamily");
						XMLportAtt.Value="InterNetwork";
						XMLport.Attributes.Append(XMLportAtt);

						XMLportAtt = doc.CreateAttribute("socketType");
						XMLportAtt.Value="Stream";
						XMLport.Attributes.Append(XMLportAtt);

						XMLportAtt = doc.CreateAttribute("protocolType");
						XMLportAtt.Value="Tcp";
						XMLport.Attributes.Append(XMLportAtt);

						XMLports.AppendChild(XMLport);
					}
				} // if
				
			} // for

			XmlTextWriter writer = new XmlTextWriter(Owener.newFileName,null);
			writer.Formatting = Formatting.Indented;
			doc.Save(writer);
			writer.Close();

			doc=null;
			writer=null;
			this.DialogResult = DialogResult.OK;
			this.Close();

		} // function

		private void chckboxMailEnabled_CheckedChanged(object sender, System.EventArgs e)
		{
			txtMailServer.Enabled                  = chckboxMailEnabled.Checked;
			txtmailSenderEMailAddress.Enabled      = chckboxMailEnabled.Checked;
			txtMailReceiver.Enabled                = chckboxMailEnabled.Checked;
			lstboxMailReciever.Enabled             = chckboxMailEnabled.Checked;
			lstboxMailReciever.Enabled             = chckboxMailEnabled.Checked;
			btnMailRecieverAdd.Enabled             = chckboxMailEnabled.Checked;
			btnMailRecieverRemove.Enabled          = chckboxMailEnabled.Checked;
			
			if (chckboxMailEnabled.Checked)
			{
				txtMailServer.Text="";
				txtmailSenderEMailAddress.Text="";
			}
			else
			{
				foreach (string key in conf.ServerTable.Keys)
				{
					((Server)conf.ServerTable[key]).mailAdressList.Clear();
				}
			}
			lstboxMailReciever.Items.Clear();

		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if (txtServer.Text=="")
				return;
			conf.ServerTable.Add(txtServer.Text,new Server());
			conf.ServerNames.Add(txtServer.Text);
			checkedListBoxServers.Items.Add(txtServer.Text,CheckState.Checked);
			checkedListBoxServers.SelectedIndex=checkedListBoxServers.Items.IndexOf(txtServer.Text);
			txtServer.Text="";
		}

		private void checkedListBoxServers_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			lstboxPort.Items.Clear();
			Hashtable temp = ((Server)(conf.ServerTable[(string)checkedListBoxServers.SelectedItem])).PortTable;
			if (temp.Count!=0)
			{
				foreach (string key in temp.Keys)
				{
					if (((Port)temp[key]).HasRange)
					{
						txtPortBegin.Text = ((Port)temp[key]).NumberRangeBegin.ToString();
						txtPortEnd.Text = ((Port)temp[key]).NumberRangeEnd.ToString();
					}
					else
					{
						lstboxPort.Items.Add(key);
					}
				}
			}
			if (chckboxMailEnabled.Checked)
			{
				lstboxMailReciever.Items.Clear();
				ArrayList tmp = ((Server)(conf.ServerTable[(string)checkedListBoxServers.SelectedItem])).mailAdressList;
				lstboxMailReciever.Items.AddRange((string[])tmp.ToArray(typeof(string)));
			} 
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			Hashtable temp = ((Server)(conf.ServerTable[(string)checkedListBoxServers.SelectedItem])).PortTable;
			temp.Clear();
			lstboxPort.Items.Clear();
			ArrayList tmp = ((Server)(conf.ServerTable[(string)checkedListBoxServers.SelectedItem])).mailAdressList;
			temp.Clear();
			lstboxMailReciever.Items.Clear();
			conf.ServerNames.Remove((string)checkedListBoxServers.SelectedItem);
			conf.ServerTable.Remove((string)checkedListBoxServers.SelectedItem);
			checkedListBoxServers.Items.Remove(checkedListBoxServers.SelectedItem);
			
		}

		private void chckboxEnableRange_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chckboxEnableRange.Checked)
			{
				txtPort.Text="";
				txtPort.Enabled=false;
				btnAddPort.Enabled=false;
				btnRemovePort.Enabled=false;
				txtPortBegin.Enabled=true;
				txtPortEnd.Enabled=true;
				lstboxPort.Items.Clear();
				
				if (checkedListBoxServers.SelectedIndex==-1)
				{				
					//MessageBox.Show("No server specified so no Mail Reciever can be added...","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
					return;
				}
				((Server)conf.ServerTable [checkedListBoxServers.SelectedItem]).PortTable.Clear();
			}
			else
			{
				txtPort.Enabled=true;
				txtPortBegin.Text="";
				txtPortEnd.Text="";
				btnAddPort.Enabled=true;
				btnRemovePort.Enabled=true;
				txtPortBegin.Enabled=false;
				txtPortEnd.Enabled=false;
				
				if (checkedListBoxServers.SelectedIndex==-1)
				{				
					//MessageBox.Show("No server specified so no Mail Reciever can be added...","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
					return;
				}
				((Server)conf.ServerTable [checkedListBoxServers.SelectedItem]).PortTable.Clear();
			}
		}

		private void btnAddPort_Click(object sender, System.EventArgs e)
		{
			if (checkedListBoxServers.SelectedIndex==-1)
			{				
				MessageBox.Show("No server specified so no Port can be added...","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}
			try 
			{
				Convert.ToUInt16(txtPort.Text);
			}
			catch(System.Exception)
			{
				lblError.Text= "Port must be number...";
				return;
			}
			lstboxPort.Items.Add(txtPort.Text);
			Port port = new Port();
			port.Number=Convert.ToUInt16(txtPort.Text);
			((Server)conf.ServerTable [checkedListBoxServers.SelectedItem]).PortTable[txtPort.Text]=port;
			txtPort.Text="";
		}

		private void btnRemovePort_Click(object sender, System.EventArgs e)
		{
			if (lstboxPort.SelectedIndex==-1)
				return;
			((Server)conf.ServerTable [checkedListBoxServers.SelectedItem]).PortTable.Remove(lstboxPort.SelectedItem);
			lstboxPort.Items.Remove( lstboxPort.SelectedItem );
		}



		private void txtPortEnd_Leave(object sender, System.EventArgs e)
		{
			if (checkedListBoxServers.SelectedIndex==-1)
			{				
				MessageBox.Show("No server specified so no Mail Reciever can be added...","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}
			try 
			{
				Convert.ToUInt16(txtPortBegin.Text);
				Convert.ToUInt16(txtPortEnd.Text);
			}
			catch(System.Exception)
			{
				lblError.Text= "Port must be positive number...";
				return;
			}
			if (Convert.ToUInt16(txtPortEnd.Text) <= Convert.ToUInt16(txtPortBegin.Text))
			{
				lblError.Text= "End must be larger...";
				return;
			}
			((Server)conf.ServerTable [checkedListBoxServers.SelectedItem]).PortTable.Remove("range");
			Port p = new Port();
			p.NumberRangeBegin=Convert.ToUInt16( txtPortBegin.Text );
			p.NumberRangeEnd=Convert.ToUInt16( txtPortEnd.Text );
			((Server)conf.ServerTable [checkedListBoxServers.SelectedItem]).PortTable["range"]=p;
		}

		private void btnMailRecieverAdd_Click(object sender, System.EventArgs e)
		{
			if (checkedListBoxServers.SelectedIndex==-1)
			{				
				MessageBox.Show("No server specified so no Mail Reciever can be added...","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}
			if (txtMailReceiver.Text=="")
				return;
			lstboxMailReciever.Items.Add(txtMailReceiver.Text);
			((Server)conf.ServerTable [checkedListBoxServers.SelectedItem]).mailAdressList.Add(txtMailReceiver.Text);
			txtMailReceiver.Text="";
		}

		private void btnMailRecieverRemove_Click(object sender, System.EventArgs e)
		{
			if(lstboxMailReciever.SelectedIndex==-1)
				return;
			((Server)conf.ServerTable [checkedListBoxServers.SelectedItem]).mailAdressList.Remove (txtMailReceiver.Text);
			lstboxMailReciever.Items.Remove( lstboxMailReciever.SelectedItem );
		}

		private void gbServers_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void gbMailReciever_Enter(object sender, System.EventArgs e)
		{
		
		}
	}
}
