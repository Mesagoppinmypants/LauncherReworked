/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 06.01.2013
 * Time: 20:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdminRightsLauncher
{
	/// <summary>
	/// Description of ProgramInfoDialog.
	/// </summary>
	public partial class ProgramInfoDialog : Form
	{
		public ProgramInfoDialog()
		{
			InitializeComponent();
			
		}
		
		void ButtonOKClick(object sender, EventArgs e)
		{
			
			TryWrite();
			this.DialogResult = DialogResult.OK;
			
		}
		
		void ButtonCancelClick(object sender, EventArgs e)
		{

			TryWrite();
			this.DialogResult = DialogResult.Cancel;
			
		}
		
		void TryWrite() {
			
			if (!checkBoxSeen.Checked) {
				
				return;
				
			}

			
			try {
				System.IO.StreamWriter file = new System.IO.StreamWriter(Application.StartupPath + @"\Admin.cfg");
				file.WriteLine("1");
				file.Close();
				
			} catch {}
			
			
		}
		
	}
}
