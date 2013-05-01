/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 16.03.2013
 * Time: 10:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Microsoft.Win32;

namespace AdmSettings
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class AdmSettingsForm : Form
	{
		
		protected String OptRunNormal = "Run from\n " + Application.StartupPath + "\nwith no additional privileges (Default Behaviour)";
		protected String OptRunElevate = "Run from\n " + Application.StartupPath + "\nand ask for Admin privileges (Recommended)";
		protected String OptRunHome = "Run from\n " + Application.StartupPath + "\nwith no additional privileges and store all files and settings in\n " + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PSWG" + ".\n Note: this will take up a few GB of duplicate data *per user*.";
		
		protected String LocateSettings = "Note, to change PSWG Launcher's behaviour, you can always start \n" + Application.ExecutablePath + "\n manually or use the Launcher Options button in PSWG Launcher.";
		
		protected String RegistrySubkey = "SOFTWARE\\ProjectSWG";
		protected String RegistryVName = "RunAsBehaviour";
		
		
		public AdmSettingsForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			InitializeComponent2();

		}
		
		protected void InitializeComponent2() {	
			
			radioRunNormal.Text = OptRunNormal;
			radioRunElevated.Text = OptRunElevate;
			radioHome.Text = OptRunHome;
			LocateSettingsLabel.Text = LocateSettings;
			
			RegistryKey TheKey = Registry.CurrentUser.OpenSubKey(RegistrySubkey, false);
			
			bool needsset = true;
			if (TheKey != null) {
				
				object TheSetting = TheKey.GetValue(RegistryVName);
				
				if (TheSetting != null) {
	
					switch ((int) TheSetting) {
						case 0:
							radioRunNormal.Checked = true;
							needsset = false;
							break;
						case 1:
							radioRunElevated.Checked = true;
							needsset = false;
							break;
						case 2:
							radioHome.Checked = true;
							needsset = false;
							break;
					}
					
				}
			}
			
			if (needsset) {
				radioNoSetting.Checked = true;
			}
		}
		
		void ButtonCancelClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			Application.Exit();
		}
		
		void ButtonSaveClick(object sender, EventArgs e)
		{
			
			int setting = -1;
			
			if (radioRunNormal.Checked) {
				
				setting = 0;
				
			} else if (radioRunElevated.Checked) {
				
				setting = 1;
				
			} else if (radioHome.Checked) {
				
				setting = 2;
				
			}
			
			if (setting >= 0) {
				
				try {
					
					RegistryKey TheKey = Registry.CurrentUser.OpenSubKey(RegistrySubkey, true);
					
					if (TheKey == null) {
						TheKey = Registry.CurrentUser.CreateSubKey(RegistrySubkey);
					}
					
					TheKey.SetValue(RegistryVName, setting);
					
				} catch (Exception ex) {
					MessageBox.Show("Error saving setting to registry." + ex.ToString(),"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			} else {
				try {
					
					RegistryKey TheKey = Registry.CurrentUser.OpenSubKey(RegistrySubkey, true);
					
					if (TheKey != null) {
						TheKey.DeleteValue(RegistryVName);
					}
					
				} catch {}
			}
			
			Application.Exit();
		}
	}
}
