/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 29.07.2013
 * Time: 15:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace PswgLauncher.GUI.Forms
{
	/// <summary>
	/// Description of FileListForm.
	/// </summary>
	public partial class FileListForm : Form
	{
		
		GuiController controller;
		private Font StdFont;
		private Font SWFont;
		
		public FileListForm(GuiController gc)
		{
			
			controller = gc;
			InitializeComponent();
			this.Icon= controller.GetAppIcon();
			InitializeComponent2();
			Refresh();
		}
		
		protected void InitializeComponent2() {
			
			StdFont = dataGridView1.Font;
			
			if (controller.HasFont) {
				SWFont = new Font(controller.pfc.Families[0], 8);
			} else {
				SWFont = StdFont;
			}
			
			checkBoxHide.Enabled = false;
			
			List<SWGFile> list = new List<SWGFile>();
			list.AddRange( controller.SWGFiles.SwgFileTable.Values);
			dataGridView1.DataSource = list;
			dataGridView1.Columns[1].DefaultCellStyle.Font = SWFont;
			dataGridView1.Columns[1].Visible = false;
			dataGridView1.Columns[4].Visible = false;
			dataGridView1.Columns[0].Width = 300;
			dataGridView1.Columns[1].Width = 300;
			dataGridView1.Columns[2].Width = 100;
			dataGridView1.Columns[3].Width = 100;
		}
		
		
		
		void CheckBoxFontCheckedChanged(object sender, EventArgs e)
		{			
			dataGridView1.Columns[0].Visible = checkBoxFont.Checked;
			dataGridView1.Columns[1].Visible = ! checkBoxFont.Checked;
		}
		
		void CheckBoxHideCheckedChanged(object sender, EventArgs e)
		{
		}
	}
}
