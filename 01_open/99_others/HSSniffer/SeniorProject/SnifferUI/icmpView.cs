using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Net;
using Sniffer;

namespace SnifferUI
{
	/// <summary>
	/// Summary description for icmpView.
	/// </summary>
	public class icmpView : System.Windows.Forms.UserControl
	{

		public event DoubleClickCallback IcmpDoubleClick;

		private Hashtable packets =null;
		private int count=0;
		private System.Windows.Forms.ColumnHeader Type;
		private System.Windows.Forms.ColumnHeader Code;
		private System.Windows.Forms.ColumnHeader PCount;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ListView lstwICMP;
		private System.Windows.Forms.ColumnHeader Checksum;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ColumnHeader SAddress;
		private System.Windows.Forms.ColumnHeader DAddress;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.Label label3;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public icmpView()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			lstwICMP.FullRowSelect=true;
			lstwICMP.MultiSelect=false;
			lstwICMP.GridLines=true;
			packets=new Hashtable();
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
					packets.Clear();
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Type = new System.Windows.Forms.ColumnHeader();
			this.Code = new System.Windows.Forms.ColumnHeader();
			this.PCount = new System.Windows.Forms.ColumnHeader();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lstwICMP = new System.Windows.Forms.ListView();
			this.SAddress = new System.Windows.Forms.ColumnHeader();
			this.DAddress = new System.Windows.Forms.ColumnHeader();
			this.Checksum = new System.Windows.Forms.ColumnHeader();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.lblCount = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Type
			// 
			this.Type.Text = "Type";
			this.Type.Width = 120;
			// 
			// Code
			// 
			this.Code.Text = "Code";
			this.Code.Width = 120;
			// 
			// PCount
			// 
			this.PCount.Text = "Number";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lstwICMP);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 32);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(680, 264);
			this.panel2.TabIndex = 7;
			// 
			// lstwICMP
			// 
			this.lstwICMP.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.PCount,
																					   this.SAddress,
																					   this.DAddress,
																					   this.Type,
																					   this.Code,
																					   this.Checksum});
			this.lstwICMP.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstwICMP.Location = new System.Drawing.Point(0, 0);
			this.lstwICMP.MultiSelect = false;
			this.lstwICMP.Name = "lstwICMP";
			this.lstwICMP.Size = new System.Drawing.Size(680, 264);
			this.lstwICMP.TabIndex = 9;
			this.lstwICMP.View = System.Windows.Forms.View.Details;
			this.lstwICMP.DoubleClick += new System.EventHandler(this.lstwICMP_DoubleClick);
			// 
			// SAddress
			// 
			this.SAddress.Text = "Source Address";
			this.SAddress.Width = 100;
			// 
			// DAddress
			// 
			this.DAddress.Text = "Dest. Address";
			this.DAddress.Width = 100;
			// 
			// Checksum
			// 
			this.Checksum.Text = "Checksum";
			this.Checksum.Width = 120;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblCount);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(680, 32);
			this.panel1.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "ICMP Packets";
			// 
			// lblCount
			// 
			this.lblCount.Location = new System.Drawing.Point(480, 8);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(100, 16);
			this.lblCount.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(440, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Count : ";
			// 
			// icmpView
			// 
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "icmpView";
			this.Size = new System.Drawing.Size(680, 296);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void HandleIcmpPacket(IPv4Datagram datagram) 
		{ 		
			IcmpPacket packet=datagram.HandleIcmpPacket();			
			ListViewItem item = new ListViewItem(count.ToString());
			item.SubItems.Add(datagram.SourceName);
			item.SubItems.Add(datagram.DestinationName);
			item.SubItems.Add(consts.GetTypeForIcmp(packet.Type)+" {"+packet.Type.ToString()+"}");
			item.SubItems.Add(consts.GetCodeForIcmp(packet.Type,packet.Code)+" {"+packet.Code.ToString()+"}");
			item.SubItems.Add(packet.Checksum.ToString());
			lstwICMP.Items.Add(item);

			packets.Add(GetHashString(),datagram);

			count++;
			lblCount.Text=count.ToString();
			this.Update();
		}
		private string GetHashString()
		{
			String format = "ICMP:{0}";
			return String.Format(format,count.ToString());
		}
		private string GetHashString(int SCount)
		{
			String format = "ICMP:{0}";
			return String.Format(format,SCount.ToString());
		}

		public IPv4Datagram GetSelectedItem()
		{
			int SCount=-1;

			ListView.SelectedListViewItemCollection sItems =lstwICMP.SelectedItems;
			foreach ( ListViewItem item in sItems )
			{
				SCount = Convert.ToInt32(item.SubItems[0].Text);
			}
			if (SCount==-1)return null;
			return (IPv4Datagram)packets[this.GetHashString(SCount)];
		}

		private void lstwICMP_DoubleClick(object sender, System.EventArgs e)
		{
			if (IcmpDoubleClick != null ) 
			{ 
				IcmpDoubleClick();
			}
		}
	}
}



