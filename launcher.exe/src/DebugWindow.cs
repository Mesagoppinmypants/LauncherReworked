/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 26.12.2012
 * Time: 21:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

using PswgLauncher;

namespace PswgLauncher
{
	/// <summary>
	/// Description of DebugWindow.
	/// </summary>
	public partial class DebugWindow : Form
	{
		
		private GuiController Controller;
		
		public DebugWindow(GuiController gc)
		{
			
			Controller = gc;

			InitializeComponent();
			this.Icon= Controller.GetAppIcon();
			
			
		}
		
		private void ButtonCloseClick(object sender, EventArgs e)
		{
			this.Visible = false;
		}
		
		public void AddText(String message) {
			richTextBox1.AppendText(message + Environment.NewLine);
		}
		
	}
}
