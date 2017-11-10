using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SnifferUI
{
	/// <summary>
	/// Summary description for Filtering.
	/// </summary>
	public class Filtering : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.Button btnDone;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.ListBox lstFilter;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Filtering()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Filtering));
			this.pnlMain = new System.Windows.Forms.Panel();
			this.lstFilter = new System.Windows.Forms.ListBox();
			this.btnDown = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.btnDone = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.pnlMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.lstFilter);
			this.pnlMain.Controls.Add(this.btnDown);
			this.pnlMain.Controls.Add(this.btnUp);
			this.pnlMain.Controls.Add(this.label1);
			this.pnlMain.Controls.Add(this.btnDone);
			this.pnlMain.Controls.Add(this.btnClear);
			this.pnlMain.Controls.Add(this.btnRemove);
			this.pnlMain.Controls.Add(this.btnAdd);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 0);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(676, 277);
			this.pnlMain.TabIndex = 0;
			// 
			// lstFilter
			// 
			this.lstFilter.Location = new System.Drawing.Point(8, 32);
			this.lstFilter.Name = "lstFilter";
			this.lstFilter.Size = new System.Drawing.Size(624, 186);
			this.lstFilter.TabIndex = 15;
			// 
			// btnDown
			// 
			this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
			this.btnDown.Location = new System.Drawing.Point(640, 128);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(28, 28);
			this.btnDown.TabIndex = 14;
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// btnUp
			// 
			this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
			this.btnUp.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.btnUp.Location = new System.Drawing.Point(640, 96);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(28, 28);
			this.btnUp.TabIndex = 13;
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(248, 24);
			this.label1.TabIndex = 12;
			this.label1.Text = "Filter Set";
			// 
			// btnDone
			// 
			this.btnDone.Location = new System.Drawing.Point(408, 232);
			this.btnDone.Name = "btnDone";
			this.btnDone.TabIndex = 11;
			this.btnDone.Text = "Done";
			this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(328, 232);
			this.btnClear.Name = "btnClear";
			this.btnClear.TabIndex = 10;
			this.btnClear.Text = "Remove All";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(248, 232);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.TabIndex = 9;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(168, 232);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.TabIndex = 8;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// Filtering
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(676, 277);
			this.Controls.Add(this.pnlMain);
			this.MaximumSize = new System.Drawing.Size(684, 304);
			this.MinimumSize = new System.Drawing.Size(684, 304);
			this.Name = "Filtering";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Filtering";
			this.Load += new System.EventHandler(this.Filtering_Load);
			this.pnlMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			FilterCreator FCreater =new FilterCreator();
			if (FCreater.ShowDialog(this)==DialogResult.OK){
				lstFilter.Items.Clear();
				for (int i=0;i<Sniffer.FilterManager.FilterCount();i++){
					lstFilter.Items.Add(Sniffer.FilterManager.ToString(i));
				}
			}
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			if (lstFilter.SelectedIndex!=-1)
			{
				Sniffer.FilterManager.removeAtFilter(lstFilter.SelectedIndex);
				lstFilter.Items.RemoveAt(lstFilter.SelectedIndex);				
			}
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			lstFilter.Items.Clear();
			Sniffer.FilterManager.removeAll();
		}

		private void btnDone_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnUp_Click(object sender, System.EventArgs e)
		{
			if (lstFilter.SelectedIndex!=-1){
				if (lstFilter.SelectedIndex>0){
					Sniffer.FilterManager.swap(lstFilter.SelectedIndex-1,lstFilter.SelectedIndex);
					string str =(string)lstFilter.Items[lstFilter.SelectedIndex-1];
					lstFilter.Items[lstFilter.SelectedIndex-1]=lstFilter.Items[lstFilter.SelectedIndex];
					lstFilter.Items[lstFilter.SelectedIndex]=str;
					lstFilter.SelectedIndex=lstFilter.SelectedIndex-1;
				}
			}
		}

		private void btnDown_Click(object sender, System.EventArgs e)
		{
			if (lstFilter.SelectedIndex!=-1)
			{
				if (lstFilter.SelectedIndex<lstFilter.Items.Count-1)
				{
					Sniffer.FilterManager.swap(lstFilter.SelectedIndex+1,lstFilter.SelectedIndex);
					string str =(string)lstFilter.Items[lstFilter.SelectedIndex+1];
					lstFilter.Items[lstFilter.SelectedIndex+1]=lstFilter.Items[lstFilter.SelectedIndex];
					lstFilter.Items[lstFilter.SelectedIndex]=str;
					lstFilter.SelectedIndex=lstFilter.SelectedIndex+1;
				}
			}
		}

		private void Filtering_Load(object sender, System.EventArgs e)
		{
			lstFilter.Items.Clear();
			for (int i=0;i<Sniffer.FilterManager.FilterCount();i++)
			{
				lstFilter.Items.Add(Sniffer.FilterManager.ToString(i));
			}
		}
	}
}
