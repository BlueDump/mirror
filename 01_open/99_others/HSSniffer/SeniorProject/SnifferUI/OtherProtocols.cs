using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Sniffer;

namespace SnifferUI
{
	/// <summary>
	/// Summary description for OtherProtocols.
	/// </summary>
	public class OtherProtocols : System.Windows.Forms.UserControl
	{
		public event DoubleClickCallback OtherDoubleClick;

		private bool check=false;
		private Hashtable packets =null;
		private IPEndPoint source = null;
		private IPEndPoint destination = null;
		private string Protocol="";
		private int count=0;
		private System.Windows.Forms.ColumnHeader PCount;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView lstwOther;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.Label label3;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public OtherProtocols(IPAddress src, IPAddress dest,string protocol)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			Protocol=protocol;
			lstwOther.FullRowSelect=true;
			lstwOther.MultiSelect=false;
			lstwOther.GridLines=true;
			packets=new Hashtable();
			source=new IPEndPoint(src,0);
			destination=new IPEndPoint(dest,0);
			label2.Text="Source : "+source.Address.ToString()+" And ";
			label2.Text=label2.Text+"Destination : "+destination.Address.ToString();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PCount = new System.Windows.Forms.ColumnHeader();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lstwOther = new System.Windows.Forms.ListView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lblCount = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// PCount
			// 
			this.PCount.Text = "Number";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lstwOther);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 32);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(768, 344);
			this.panel2.TabIndex = 7;
			// 
			// lstwOther
			// 
			this.lstwOther.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.PCount});
			this.lstwOther.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstwOther.Location = new System.Drawing.Point(0, 0);
			this.lstwOther.MultiSelect = false;
			this.lstwOther.Name = "lstwOther";
			this.lstwOther.Size = new System.Drawing.Size(768, 344);
			this.lstwOther.TabIndex = 9;
			this.lstwOther.View = System.Windows.Forms.View.Details;
			this.lstwOther.DoubleClick += new System.EventHandler(this.lstwOther_DoubleClick);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblCount);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(768, 32);
			this.panel1.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(112, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(296, 23);
			this.label2.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Packets Between ";
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
			// OtherProtocols
			// 
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "OtherProtocols";
			this.Size = new System.Drawing.Size(768, 376);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		public void HandleOthers(IPv4Datagram datagram) 
		{ 		
			ProtocolTemplate packet=datagram.HandleOthers();
			if (packet!=null)
			{				
				ListViewItem item = new ListViewItem(count.ToString());

				for(int i=0;i<packet.GetFieldCount();i++)
				{
					if (!check)
						lstwOther.Columns.Add(packet.GetFieldName(i),80,HorizontalAlignment.Left);
					item.SubItems.Add((packet.GetField(i)).ToString());
				}
				lstwOther.Items.Add(item);
				packets.Add(GetHashString(),datagram);
				count++;
				lblCount.Text=count.ToString();
				check=true;
				this.Update();
			}
		}
		private string GetHashString()
		{
			String format = "{0}:{1}:{2}:{3}";
			return String.Format(format,Protocol, source.Address.ToString(),destination.Address.ToString(),count.ToString());
		}
		private string GetHashString(int SCount)
		{
			String format = "{0}:{1}:{2}:{3}";
			return String.Format(format,Protocol,source.Address.ToString(),destination.Address.ToString(),SCount.ToString());
		}

		public IPv4Datagram GetSelectedItem()
		{
			int SCount=-1;

			ListView.SelectedListViewItemCollection sItems =lstwOther.SelectedItems;
			foreach ( ListViewItem item in sItems )
			{
				SCount = Convert.ToInt32(item.SubItems[0].Text);
			}
			if (SCount==-1)return null;
			return (IPv4Datagram)packets[this.GetHashString(SCount)];
		}

		private void lstwOther_DoubleClick(object sender, System.EventArgs e)
		{
			if (OtherDoubleClick != null ) 
			{ 
				OtherDoubleClick();
			}
		}
	}
}
