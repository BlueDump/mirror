using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IpHlpApidotnet;
using System.Diagnostics;

namespace iphlpapidemo
{
    public partial class Form1 : Form
    {
        private IpHlpApidotnet.IPHelper _IpHlpAPI;
        public AddNewItem m_AddNewItem;
        public InsertItem m_InsertItem;
        public ChangedItem m_ChangedItem;
        public DeletedItem m_DeletedItem;

        public delegate void AddNewItem(TCPUDPConnection item);
        public delegate void InsertItem(TCPUDPConnection item, int Position);
        public delegate void ChangedItem(TCPUDPConnection item, int Position);
        public delegate void DeletedItem(TCPUDPConnection item, int Position);

        private TCPUDPConnections conns = new TCPUDPConnections();
        public Form1()
        {
            m_AddNewItem = new AddNewItem(this.AddItem);
            m_InsertItem = new InsertItem(this.AddOrInsertItem);
            m_ChangedItem = new ChangedItem(this.ChangeItem);
            m_DeletedItem = new DeletedItem(this.DeleteItem);

            InitializeComponent();
            _IpHlpAPI = new IpHlpApidotnet.IPHelper();
            conns.ItemAdded +=new TCPUDPConnections.ItemAddedEvent(conns_ItemAdded);
            conns.ItemInserted += new TCPUDPConnections.ItemInsertedEvent(conns_ItemInserted);
            conns.ItemChanged += new TCPUDPConnections.ItemChangedEvent(conns_ItemChanged);
            conns.ItemDeleted += new TCPUDPConnections.ItemDeletedEvent(conns_ItemDeleted);
            RefreshLower();

            //hack to initially try to reduce the memory footprint of the app (admin only)
            try
            {
                Process loProcess = Process.GetCurrentProcess();
                loProcess.MaxWorkingSet = loProcess.MaxWorkingSet;
                loProcess.Dispose();
            }
            catch { }
            System.Timers.Timer ShrinkTimer = new System.Timers.Timer();
            ShrinkTimer.Interval = 60000;
            ShrinkTimer.Elapsed += new System.Timers.ElapsedEventHandler(ShrinkTimer_Elapsed);
            ShrinkTimer.Start();
        }

        void ShrinkTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //hack to initially try to reduce the memory footprint of the app (admin only)
            try
            {
                Process loProcess = Process.GetCurrentProcess();
                loProcess.MaxWorkingSet = loProcess.MaxWorkingSet;
                loProcess.Dispose();
            }
            catch { }
        }

        void conns_ItemDeleted(object sender, TCPUDPConnection item, int Position)
        {
            try
            {
                this.Invoke(m_DeletedItem, item, Position);
            }
            catch { }
        }

        void conns_ItemChanged(object sender, TCPUDPConnection item, int Position)
        {
            try
            {
                this.Invoke(m_ChangedItem, item, Position);
            }
            catch { }
        }

        void conns_ItemInserted(object sender, TCPUDPConnection item, int Position)
        {
            try
            {
                this.Invoke(m_InsertItem, item, Position);
            }
            catch { }
        }

        void conns_ItemAdded(object sender, TCPUDPConnection item)
        {
            try
            {
                this.Invoke(m_AddNewItem, item);
            }
            catch {}
        }

        private void AddItem(TCPUDPConnection conn)
        {
            AddOrInsertItem(conn, -1);
        }

        private void AddOrInsertItem(TCPUDPConnection conn, int Position)
        {
            string state = String.Empty;
            if (conn.Protocol == Protocol.TCP)
                state = conn.State;
            ListViewItem lvi = null;
            if (Position != -1)
            {
                lvi = listView1.Items.Insert(Position, state);
            }
            else
            {
                lvi = listView1.Items.Add(state);
            }
            lvi.SubItems.Add(conn.LocalAddress);// lvi.SubItems.Add(conn.Local.ToString());
            if (conn.Protocol == Protocol.TCP)
                lvi.SubItems.Add(conn.RemoteAddress);//lvi.SubItems.Add(conn.Remote.ToString());
            else
                lvi.SubItems.Add("");
            lvi.SubItems.Add(conn.PID.ToString());
            lvi.SubItems.Add(conn.ProcessName);
            lvi.SubItems.Add(conn.Protocol.ToString());
            tsGrid1.Text = "Grid1 items: " +listView1.Items.Count.ToString();
        }

        private void ChangeItem(TCPUDPConnection conn, int Position)
        {
            string state = String.Empty;
            if (conn.Protocol == Protocol.TCP)
                state = conn.State;
            ListViewItem lvi = listView1.Items[Position];
            lvi.SubItems["colStateUpper"].Text = state;
            lvi.SubItems["colPIDUpper"].Text = conn.PID.ToString();
            lvi.SubItems["colProcessUpper"].Text = conn.ProcessName;
        }

        private void DeleteItem(TCPUDPConnection conn, int Position)
        {
            listView1.Items.RemoveAt(Position);
            tsGrid1.Text = "Grid1 items: " + listView1.Items.Count.ToString();
        }

        private void RefreshLower()
        {
            listView2.Items.Clear();
            _IpHlpAPI.GetExTcpConnections();
            //TCPUDPConnections.LocalHostName;
            for (int i = 0; i < _IpHlpAPI.TcpExConnections.dwNumEntries; i++)
            {
                ListViewItem lvi = listView2.Items.Add(_IpHlpAPI.TcpExConnections.table[i].StrgState);
                if (resolveIP.Checked)
                {
                    //lvi.SubItems.Add(_IpHlpAPI.TcpExConnections.table[i].Local.ToString());
                    //lvi.SubItems.Add(_IpHlpAPI.TcpExConnections.table[i].Remote.ToString());
                    lvi.SubItems.Add(Utils.GetHostName(_IpHlpAPI.TcpExConnections.table[i].Local, conns.LocalHostName));
                    lvi.SubItems.Add(Utils.GetHostName(_IpHlpAPI.TcpExConnections.table[i].Remote, conns.LocalHostName));
                }
                else
                {
                    lvi.SubItems.Add(_IpHlpAPI.TcpExConnections.table[i].Local.ToString());
                    lvi.SubItems.Add(_IpHlpAPI.TcpExConnections.table[i].Remote.ToString());
                }
                lvi.SubItems.Add(_IpHlpAPI.TcpExConnections.table[i].dwProcessId.ToString());
                lvi.SubItems.Add(_IpHlpAPI.TcpExConnections.table[i].ProcessName);
                lvi.SubItems.Add("TCP");
            }
            _IpHlpAPI.GetExUdpConnections();
            for (int i = 0; i < _IpHlpAPI.UdpExConnections.dwNumEntries; i++)
            {
                ListViewItem lvi = listView2.Items.Add("");
                if (resolveIP.Checked)
                {
                    //lvi.SubItems.Add(_IpHlpAPI.UdpExConnections.table[i].Local.ToString());
                    lvi.SubItems.Add(Utils.GetHostName(_IpHlpAPI.UdpExConnections.table[i].Local, conns.LocalHostName));
                }
                else
                {
                    lvi.SubItems.Add(_IpHlpAPI.UdpExConnections.table[i].Local.ToString());
                }
                lvi.SubItems.Add("");
                lvi.SubItems.Add(_IpHlpAPI.UdpExConnections.table[i].dwProcessId.ToString());
                lvi.SubItems.Add(_IpHlpAPI.UdpExConnections.table[i].ProcessName);
                lvi.SubItems.Add("UDP");
            }
            tsGrid2.Text = "Grid2 items: " + listView2.Items.Count.ToString();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void sortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            conns.Sort();
            this.RefreshUpper();
        }

        private void RefreshUpper()
        {
            listView1.Items.Clear();
            foreach (TCPUDPConnection conn in conns)
            {
                AddItem(conn);
            }
            tsGrid1.Text = "Grid1 items: " + listView1.Items.Count.ToString();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RefreshUpper();
        }

        private void refreshLowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RefreshLower();
        }
    }
}