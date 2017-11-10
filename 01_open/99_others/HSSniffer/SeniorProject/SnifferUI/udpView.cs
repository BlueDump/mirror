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
	public delegate void DoubleClickCallback();
	/// <summary>
	/// Summary description for udpView.
	/// </summary>
	public class udpView : System.Windows.Forms.UserControl
	{

		public event DoubleClickCallback UdpDoubleClick;

		private Hashtable packets =null;
		private IPEndPoint source = null;
		private IPEndPoint destination = null;
		private int count=0;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ColumnHeader SPort;
		private System.Windows.Forms.ColumnHeader DPort;
		private System.Windows.Forms.ColumnHeader Data;
		private System.Windows.Forms.ColumnHeader PCount;
		private System.Windows.Forms.ColumnHeader SourceIP;
		private System.Windows.Forms.ColumnHeader Identification;
		private System.Windows.Forms.ListView lstwUdp;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.Label label3;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public udpView(IPAddress src, IPAddress dest)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			lstwUdp.FullRowSelect=true;
			lstwUdp.MultiSelect=false;
			lstwUdp.GridLines=true;
			packets=new Hashtable();
			source=new IPEndPoint(src,0);
			destination=new IPEndPoint(dest,0);
			label2.Text="Source : "+source.Address.ToString()+" and ";
			label2.Text=label2.Text+"Destination : "+destination.Address.ToString();

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
			this.panel2 = new System.Windows.Forms.Panel();
			this.lstwUdp = new System.Windows.Forms.ListView();
			this.PCount = new System.Windows.Forms.ColumnHeader();
			this.SourceIP = new System.Windows.Forms.ColumnHeader();
			this.SPort = new System.Windows.Forms.ColumnHeader();
			this.Data = new System.Windows.Forms.ColumnHeader();
			this.DPort = new System.Windows.Forms.ColumnHeader();
			this.Identification = new System.Windows.Forms.ColumnHeader();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lblCount = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lstwUdp);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 32);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(728, 344);
			this.panel2.TabIndex = 5;
			// 
			// lstwUdp
			// 
			this.lstwUdp.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.PCount,
																					  this.SourceIP,
																					  this.SPort,
																					  this.Data,
																					  this.DPort,
																					  this.Identification});
			this.lstwUdp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstwUdp.Location = new System.Drawing.Point(0, 0);
			this.lstwUdp.MultiSelect = false;
			this.lstwUdp.Name = "lstwUdp";
			this.lstwUdp.Size = new System.Drawing.Size(728, 344);
			this.lstwUdp.TabIndex = 9;
			this.lstwUdp.View = System.Windows.Forms.View.Details;
			this.lstwUdp.DoubleClick += new System.EventHandler(this.lstwUdp_DoubleClick);
			// 
			// PCount
			// 
			this.PCount.Text = "Number";
			// 
			// SourceIP
			// 
			this.SourceIP.Text = "Source Address";
			this.SourceIP.Width = 120;
			// 
			// SPort
			// 
			this.SPort.Text = "S. Port";
			this.SPort.Width = 182;
			// 
			// Data
			// 
			this.Data.Text = "Destination Address";
			this.Data.Width = 120;
			// 
			// DPort
			// 
			this.DPort.Text = "D. Port";
			this.DPort.Width = 168;
			// 
			// Identification
			// 
			this.Identification.Text = "Identification";
			this.Identification.Width = 80;
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
			this.panel1.Size = new System.Drawing.Size(728, 32);
			this.panel1.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(136, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(304, 23);
			this.label2.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "UDP Packets Between ";
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
			// udpView
			// 
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "udpView";
			this.Size = new System.Drawing.Size(728, 376);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void HandleUdpDatagram(IPv4Datagram datagram) 
		{ 		
			UdpDatagram packet=datagram.HandleUdpDatagram();			
			ListViewItem item = new ListViewItem(count.ToString());
			item.SubItems.Add(datagram.SourceName);
			item.SubItems.Add(packet.SourcePort.ToString()+"  "+consts.GetTcpPorts(packet.SourcePort));
			item.SubItems.Add(datagram.DestinationName);
			item.SubItems.Add(packet.DestinationPort.ToString()+"  "+consts.GetTcpPorts(packet.DestinationPort));
			item.SubItems.Add(datagram.Identification.ToString());
			lstwUdp.Items.Add(item);

			packets.Add(GetHashString(),datagram);

			count++;
			lblCount.Text=count.ToString();
			this.Update();
		}
		private string GetHashString()
		{
			String format = "Udp:{0}:{1}:{2}";
			return String.Format(format,source.Address.ToString(),destination.Address.ToString(),count.ToString());
		}
		private string GetHashString(int SCount)
		{
			String format = "Udp:{0}:{1}:{2}";
			return String.Format(format,source.Address.ToString(),destination.Address.ToString(),SCount.ToString());
		}

		public IPv4Datagram GetSelectedItem()
		{
			int SCount=-1;

			ListView.SelectedListViewItemCollection sItems =lstwUdp.SelectedItems;
			foreach ( ListViewItem item in sItems )
			{
				SCount = Convert.ToInt32(item.SubItems[0].Text);
			}
			if (SCount==-1)return null;
			return (IPv4Datagram)packets[this.GetHashString(SCount)];
		}

		private void lstwUdp_DoubleClick(object sender, System.EventArgs e)
		{
			if (UdpDoubleClick != null ) 
			{ 
				UdpDoubleClick();
			}
		}
	}
}
