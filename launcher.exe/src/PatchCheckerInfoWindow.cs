/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 02.03.2013
 * Time: 10:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PswgLauncher
{
	/// <summary>
	/// Description of PatchCheckerInfoWindow.
	/// </summary>
	public partial class PatchCheckerInfoWindow : Form
	{
		
		public PatchCheckerInfoWindow(GuiController controller)
		{
 
			
			InitializeComponent();
			this.Icon=controller.GetAppIcon();
			this.Refresh();
			this.Show();

		}
		
		public void CloseInfo(){
			this.Visible = false;
			this.DialogResult = DialogResult.Yes;
			this.Close();
		}
	}
}
