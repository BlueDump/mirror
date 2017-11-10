using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using Sniffer;

namespace SnifferUI
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class AddProtocols : System.Windows.Forms.Form
	{
		private ArrayList fields =new ArrayList();
		private XmlDocument doc = new XmlDocument();
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox editProcName;
		private System.Windows.Forms.TextBox editProcId;
		private System.Windows.Forms.TextBox editFieldname;
		private System.Windows.Forms.TextBox editFieldOffset;
		private System.Windows.Forms.TextBox editFieldLen;
		private System.Windows.Forms.Button btnAddProc;
		private System.Windows.Forms.Button btnAddField;
		private System.Windows.Forms.Panel pnlProc;
		private System.Windows.Forms.Panel pnlField;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label lblError;
		private System.Windows.Forms.Button newf;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Panel pnlAdd;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.ListBox lstProtocols;
		private System.Windows.Forms.ListBox lstFields;
		private System.Windows.Forms.Button AddNew;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddProtocols()
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
				if (components != null) 
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
			this.editProcName = new System.Windows.Forms.TextBox();
			this.editProcId = new System.Windows.Forms.TextBox();
			this.editFieldname = new System.Windows.Forms.TextBox();
			this.editFieldOffset = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.editFieldLen = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.btnAddProc = new System.Windows.Forms.Button();
			this.btnAddField = new System.Windows.Forms.Button();
			this.pnlProc = new System.Windows.Forms.Panel();
			this.pnlField = new System.Windows.Forms.Panel();
			this.newf = new System.Windows.Forms.Button();
			this.cancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.lblError = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.pnlAdd = new System.Windows.Forms.Panel();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.AddNew = new System.Windows.Forms.Button();
			this.lstFields = new System.Windows.Forms.ListBox();
			this.lstProtocols = new System.Windows.Forms.ListBox();
			this.pnlProc.SuspendLayout();
			this.pnlField.SuspendLayout();
			this.pnlAdd.SuspendLayout();
			this.pnlMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// editProcName
			// 
			this.editProcName.Location = new System.Drawing.Point(128, 32);
			this.editProcName.Name = "editProcName";
			this.editProcName.Size = new System.Drawing.Size(104, 20);
			this.editProcName.TabIndex = 2;
			this.editProcName.Text = "";
			// 
			// editProcId
			// 
			this.editProcId.Location = new System.Drawing.Point(128, 56);
			this.editProcId.Name = "editProcId";
			this.editProcId.Size = new System.Drawing.Size(104, 20);
			this.editProcId.TabIndex = 3;
			this.editProcId.Text = "";
			// 
			// editFieldname
			// 
			this.editFieldname.Location = new System.Drawing.Point(120, 24);
			this.editFieldname.Name = "editFieldname";
			this.editFieldname.Size = new System.Drawing.Size(104, 20);
			this.editFieldname.TabIndex = 4;
			this.editFieldname.Text = "";
			// 
			// editFieldOffset
			// 
			this.editFieldOffset.Location = new System.Drawing.Point(120, 48);
			this.editFieldOffset.Name = "editFieldOffset";
			this.editFieldOffset.ReadOnly = true;
			this.editFieldOffset.Size = new System.Drawing.Size(104, 20);
			this.editFieldOffset.TabIndex = 5;
			this.editFieldOffset.Text = "0";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Protocol Name";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Protocol Id";
			// 
			// editFieldLen
			// 
			this.editFieldLen.Location = new System.Drawing.Point(120, 72);
			this.editFieldLen.Name = "editFieldLen";
			this.editFieldLen.Size = new System.Drawing.Size(104, 20);
			this.editFieldLen.TabIndex = 8;
			this.editFieldLen.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "field name";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 48);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "offset";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 72);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 16);
			this.label5.TabIndex = 11;
			this.label5.Text = "length";
			// 
			// btnAddProc
			// 
			this.btnAddProc.Location = new System.Drawing.Point(136, 88);
			this.btnAddProc.Name = "btnAddProc";
			this.btnAddProc.Size = new System.Drawing.Size(88, 23);
			this.btnAddProc.TabIndex = 12;
			this.btnAddProc.Text = "Add Protocol";
			this.btnAddProc.Click += new System.EventHandler(this.btnAddProc_Click);
			// 
			// btnAddField
			// 
			this.btnAddField.Location = new System.Drawing.Point(128, 104);
			this.btnAddField.Name = "btnAddField";
			this.btnAddField.TabIndex = 13;
			this.btnAddField.Text = "Add Filed";
			this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);
			// 
			// pnlProc
			// 
			this.pnlProc.Controls.Add(this.label1);
			this.pnlProc.Controls.Add(this.label2);
			this.pnlProc.Controls.Add(this.editProcId);
			this.pnlProc.Controls.Add(this.editProcName);
			this.pnlProc.Controls.Add(this.btnAddProc);
			this.pnlProc.Location = new System.Drawing.Point(16, 32);
			this.pnlProc.Name = "pnlProc";
			this.pnlProc.Size = new System.Drawing.Size(248, 128);
			this.pnlProc.TabIndex = 14;
			// 
			// pnlField
			// 
			this.pnlField.Controls.Add(this.newf);
			this.pnlField.Controls.Add(this.btnAddField);
			this.pnlField.Controls.Add(this.editFieldOffset);
			this.pnlField.Controls.Add(this.label3);
			this.pnlField.Controls.Add(this.editFieldLen);
			this.pnlField.Controls.Add(this.label5);
			this.pnlField.Controls.Add(this.editFieldname);
			this.pnlField.Controls.Add(this.label4);
			this.pnlField.Location = new System.Drawing.Point(16, 32);
			this.pnlField.Name = "pnlField";
			this.pnlField.Size = new System.Drawing.Size(248, 128);
			this.pnlField.TabIndex = 15;
			this.pnlField.Visible = false;
			// 
			// newf
			// 
			this.newf.Location = new System.Drawing.Point(128, 104);
			this.newf.Name = "newf";
			this.newf.TabIndex = 15;
			this.newf.Text = "New";
			this.newf.Visible = false;
			this.newf.Click += new System.EventHandler(this.newf_Click);
			// 
			// cancel
			// 
			this.cancel.Location = new System.Drawing.Point(448, 176);
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size(72, 23);
			this.cancel.TabIndex = 16;
			this.cancel.Text = "Cancel";
			this.cancel.Click += new System.EventHandler(this.cancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(320, 176);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(72, 23);
			this.btnOk.TabIndex = 14;
			this.btnOk.Text = "OK";
			this.btnOk.Visible = false;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// lblError
			// 
			this.lblError.ForeColor = System.Drawing.Color.Red;
			this.lblError.Location = new System.Drawing.Point(56, 168);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(152, 40);
			this.lblError.TabIndex = 16;
			// 
			// listBox1
			// 
			this.listBox1.Location = new System.Drawing.Point(320, 32);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(232, 134);
			this.listBox1.TabIndex = 17;
			// 
			// pnlAdd
			// 
			this.pnlAdd.Controls.Add(this.listBox1);
			this.pnlAdd.Controls.Add(this.lblError);
			this.pnlAdd.Controls.Add(this.btnOk);
			this.pnlAdd.Controls.Add(this.cancel);
			this.pnlAdd.Controls.Add(this.pnlField);
			this.pnlAdd.Controls.Add(this.pnlProc);
			this.pnlAdd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlAdd.Location = new System.Drawing.Point(0, 0);
			this.pnlAdd.Name = "pnlAdd";
			this.pnlAdd.Size = new System.Drawing.Size(568, 253);
			this.pnlAdd.TabIndex = 18;
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.button2);
			this.pnlMain.Controls.Add(this.button1);
			this.pnlMain.Controls.Add(this.AddNew);
			this.pnlMain.Controls.Add(this.lstFields);
			this.pnlMain.Controls.Add(this.lstProtocols);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 0);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(568, 253);
			this.pnlMain.TabIndex = 19;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(328, 216);
			this.button2.Name = "button2";
			this.button2.TabIndex = 4;
			this.button2.Text = "Close";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(248, 216);
			this.button1.Name = "button1";
			this.button1.TabIndex = 3;
			this.button1.Text = "Delete";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// AddNew
			// 
			this.AddNew.Location = new System.Drawing.Point(168, 216);
			this.AddNew.Name = "AddNew";
			this.AddNew.TabIndex = 2;
			this.AddNew.Text = "Add";
			this.AddNew.Click += new System.EventHandler(this.AddNew_Click);
			// 
			// lstFields
			// 
			this.lstFields.Location = new System.Drawing.Point(304, 24);
			this.lstFields.Name = "lstFields";
			this.lstFields.Size = new System.Drawing.Size(232, 173);
			this.lstFields.TabIndex = 1;
			// 
			// lstProtocols
			// 
			this.lstProtocols.Location = new System.Drawing.Point(24, 24);
			this.lstProtocols.Name = "lstProtocols";
			this.lstProtocols.Size = new System.Drawing.Size(240, 173);
			this.lstProtocols.TabIndex = 0;
			this.lstProtocols.SelectedIndexChanged += new System.EventHandler(this.lstProtocols_SelectedIndexChanged);
			// 
			// AddProtocols
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(568, 253);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.pnlAdd);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(576, 280);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(576, 280);
			this.Name = "AddProtocols";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Protocols";
			this.Load += new System.EventHandler(this.AddProtocols_Load);
			this.pnlProc.ResumeLayout(false);
			this.pnlField.ResumeLayout(false);
			this.pnlAdd.ResumeLayout(false);
			this.pnlMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>


		private void btnAddProc_Click(object sender, System.EventArgs e)
		{
			this.listBox1.Items.Clear();
			try 
			{
				Convert.ToInt32(editProcId.Text);
			}
			catch(System.Exception){
				lblError.Text= "Id must be number.";
				return; 
			}
			if (editProcName.Text=="")
			{
				lblError.Text= "Please Enter Protocol Name.";
				return; 
			}
			if (Protocols.ContainsProtocol(editProcId.Text))
			{
				lblError.Text= "Protocol Id Exist in Protocol List.";
				return; 
			}

			doc.LoadXml("<protocol><name>"+editProcName.Text+"</name><id>"+editProcId.Text+"</id><fields></fields></protocol>");
			this.pnlField.Visible=true;
			this.pnlProc.Visible=false;
			lblError.Text= "";
			this.listBox1.Items.Add("Protocol Name :"+this.editProcName.Text+"  Protocol ID :"+this.editProcId.Text);
		}

		private void btnAddField_Click(object sender, System.EventArgs e)
		{
			if(fields.Contains(editFieldname.Text))
			{
				lblError.Text="You Entered This Field Before";
				return;
			}
			else
			{
				try 
				{
					Convert.ToInt32(editFieldLen.Text);
					Convert.ToInt32(editFieldOffset.Text);
				}
				catch(Exception)
				{
					lblError.Text= "Length and Offset must be number.";
					return; 
				}
				if (editFieldname.Text=="")
				{
					lblError.Text= "Please Enter Field Name.";
					return; 
				}
				// Add a price element.
				XmlNode root = doc.DocumentElement;
				root=root.LastChild;

				XmlElement newElem = doc.CreateElement("field");
				newElem.InnerText = "";
				root.AppendChild(newElem);

				root=root.LastChild;

				newElem=doc.CreateElement("name");
				newElem.InnerText = editFieldname.Text;
				root.AppendChild(newElem);

				newElem=doc.CreateElement("offset");
				newElem.InnerText = editFieldOffset.Text;
				root.AppendChild(newElem);

				newElem=doc.CreateElement("length");
				newElem.InnerText = editFieldLen.Text;
				root.AppendChild(newElem);

				fields.Add(editFieldname.Text);

				this.newf.Visible=true;
				this.btnOk.Visible=true;
				this.btnAddField.Visible=false;
				lblError.Text= "";
				this.listBox1.Items.Add("Field :"+this.editFieldname.Text+"  Offset :"+this.editFieldOffset.Text+"  Length :"+this.editFieldLen.Text);
			}

		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			// Save the document to a file and auto-indent the output.
			XmlTextWriter writer = new XmlTextWriter(this.editProcId.Text+".xml",null);
			writer.Formatting = Formatting.Indented;
			doc.Save(writer);
			writer.Close();
			lblError.Text=this.editProcName.Text+" Protocol added";


			XmlDocument tempdoc = new XmlDocument();
			string str=Protocols.xmlReader("Protocols");
			tempdoc.LoadXml(str);
			XmlElement newElem = tempdoc.CreateElement("name");
			newElem.InnerText = this.editProcId.Text;
			tempdoc.DocumentElement.AppendChild(newElem);
			
			// Save the document to a file and auto-indent the output.
			writer = new XmlTextWriter("Protocols.xml",null);
			writer.Formatting = Formatting.Indented;
			tempdoc.Save(writer);
			writer.Close();

			Protocols.addProtocoltoSys(this.editProcId.Text);

			pnlMain.Visible=true;
			pnlAdd.Visible=false;
			showProtocols();

		}

		private void newf_Click(object sender, System.EventArgs e)
		{			
			editFieldOffset.Text= Convert.ToString(Convert.ToInt32(editFieldOffset.Text)+Convert.ToInt32(editFieldLen.Text));
			this.editFieldLen.Text="";
			this.editFieldname.Text="";		
			this.btnAddField.Visible=true;
			this.newf.Visible=false;
			this.btnOk.Visible=false;
			lblError.Text= "";
		}

		private void cancel_Click(object sender, System.EventArgs e)
		{			
			pnlMain.Visible=true;
			pnlAdd.Visible=false;
		}

		private void AddProtocols_Load(object sender, System.EventArgs e)
		{
			showProtocols();
		}

		private void AddNew_Click(object sender, System.EventArgs e)
		{
			this.editFieldLen.Text="";
			this.editFieldname.Text="";		
			this.editFieldOffset.Text="0";	
			this.editProcName.Text="";
			this.editProcId.Text="";		
			this.btnAddField.Visible=true;
			this.newf.Visible=false;
			this.btnOk.Visible=false;
			this.pnlField.Visible=false;
			this.pnlProc.Visible=true;
			lblError.Text= "";
			pnlMain.Visible=false;
			pnlAdd.Visible=true;
		}

		private void showProtocols(){
			Hashtable temp=new Hashtable();
			lstProtocols.Items.Clear();
			for (int i=0;i<Protocols.Keys.Count;i++){
				temp = (Hashtable)Protocols.protoDefs[Protocols.Keys[i]];
				this.lstProtocols.Items.Add("#"+(string)temp["name"]+"  "+Protocols.Keys[i]);
			}
		}

		private void lstProtocols_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lstProtocols.SelectedIndex!=-1){
				showFields(lstProtocols.SelectedIndex);
			}
		}

		private void showFields(int index)
		{
			int [] x=new int[2];
			Hashtable temp=new Hashtable();
			Hashtable temp2=new Hashtable();
			ArrayList arr=new ArrayList();
			temp=(Hashtable)Protocols.protoDefs[Protocols.Keys[index]];

			this.lstFields.Items.Clear();
			this.lstFields.Items.Add("Protocol Name : "+temp["name"]+" Protocol Id : "+temp["id"]);

			foreach (string key in temp.Keys)
			{
				if (key!="name" && key!="id")
				{
					x=(int[])temp[key];
					temp2.Add(x[0].ToString(),"Field : "+key+ "  Offset : "+x[0].ToString()+" Length : "+x[1].ToString());
					arr.Add(x[0]);
				} 
			}

			arr.Sort();

			for (int i=0; i<arr.Count;i++)
			{
				this.lstFields.Items.Add(temp2[arr[i].ToString()]);
			}

		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (lstProtocols.SelectedIndex!=-1)
			{
				if ((string)Protocols.Keys[lstProtocols.SelectedIndex]!="1"&&
					(string)Protocols.Keys[lstProtocols.SelectedIndex]!="6"&&
					(string)Protocols.Keys[lstProtocols.SelectedIndex]!="17")
				{
					Protocols.removeProtocol((string)Protocols.Keys[lstProtocols.SelectedIndex]);
					showProtocols();
					this.lstFields.Items.Clear();
				}
				else{
					MessageBox.Show(this,"This Protocol cannot be deleted.");
				}
			}
		}
	}
}
