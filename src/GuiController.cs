/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 23.12.2012
 * Time: 14:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PswgLauncher
{
	/// <summary>
	/// Description of Class2.
	/// </summary>
	public class GuiController
	{
	
		private bool _soundOption;
		
		public bool soundOption { 
			get {
				return _soundOption;
			}
			set {
				_soundOption = value;
				setAppSetting("SoundEnable",((_soundOption == true) ? "true" : "false" )) ;
				
			}
		}
		
		private bool _checksumOption;
		
		public bool checksumOption {
			get {
				return _checksumOption;
			}
			set {
				_checksumOption = value;
				setAppSetting("ChecksumEnable",((_checksumOption == true) ? "true" : "false" ));
			}
		}


		private String _SwgDir;
		
		public String SwgDir {
			get {
				
				return _SwgDir;
			}
			
			set {
				_SwgDir = value;
				setAppSetting("SwgDir", _SwgDir);
			}
		}
		
		private Configuration config;
		
		public SWGFileList SWGFiles {get; set;}
		
		private List<String> _DebugMessages;
		private List<String> _DebugMessages2;
		
		private DebugWindow _debug;
		
		
		
		
		
		public GuiController()
		{
			_soundOption = false;
			_SwgDir = "";
			_checksumOption = true;
			
			_DebugMessages = new List<String>();
			SWGFiles = new SWGFileList(this);
			

		}
		
		
		public void readConfig() {
			
			AddDebugMessage("Using " + AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

			_soundOption = ( ( getAppSetting("SoundEnable")  == "true") ? true : false);
			_checksumOption = ( ( getAppSetting("ChecksumEnable")  == "false") ? false : true);
					
			_SwgDir = getAppSetting("SwgDir");
			
			String FileList = Application.StartupPath + "\\launcher.dl.dat";
			if (File.Exists(FileList)) {
				try {
					AddDebugMessage("Reading " + FileList);
					StreamReader sr = new StreamReader(FileList);
					SWGFiles.CreateFileList(sr,false);
				} catch (Exception e) {
					AddDebugMessage("No luck reading " + FileList + ", needs downloading.");
					//do nothing.. there's still downloading ;)
				}
			} else {
				AddDebugMessage("No " + FileList + " present, needs downloading.");
			}

		}
		
		public string getAppSetting(string key)
		{
		    Configuration config = ConfigurationManager.OpenExeConfiguration(
		                            System.Reflection.Assembly.GetExecutingAssembly().Location);
			if (config.AppSettings.Settings[key] != null) {
		    	return config.AppSettings.Settings[key].Value;
			}
			
			return null;
		}
		 
		public void setAppSetting(string key, string value)
		{
		    Configuration config = ConfigurationManager.OpenExeConfiguration(
		                            System.Reflection.Assembly.GetExecutingAssembly().Location);
		    if (config.AppSettings.Settings[key] != null)
		    {
		        config.AppSettings.Settings.Remove(key);
		    }
		    config.AppSettings.Settings.Add(key, value);
		    config.Save(ConfigurationSaveMode.Modified);
		}
		
		
		
		public bool runPatchChecker() {
			
			PatchCheckerDialog pcd = new PatchCheckerDialog(this);
			
			DialogResult result = pcd.ShowDialog();
			
			AddDebugMessage(result.ToString());
			
			if (result == DialogResult.OK) {
				return true;
			}
			
			if (result == DialogResult.Yes) {
				
				System.Diagnostics.Process.Start(Application.StartupPath + "/launcher patcher.exe");

			}
			
			return false;
			
		}
		
		
		public bool runDirSearch() {
			
			if (SwgDir == null || SwgDir == "" || !Directory.Exists(SwgDir)) {
			
				DirSearch ds = new DirSearch(this);
	            DialogResult result = ds.ShowDialog();
	            
	            if (result == DialogResult.OK) {
					return true;
				}
	            
	            
	            return false;
            
			}
			
			return true;

		}
		
		public bool runLauncher() {
			
			launcher2 lt = new launcher2(this);
			lt.Show();
			
			return true;
		}
		
		
		public void LaunchDebug() {
			if (_debug == null || _debug.IsDisposed) {
				_debug = new DebugWindow(this);
				
				foreach (String msg in _DebugMessages ) {
					_debug.AddText(msg);
				}
				
			}
			
			if (_debug.Visible == false) {
				_debug.Visible = true;
			}
			
		}
		
		
		public void AddDebugMessage(String msg) {
			_DebugMessages.Add(msg);
			
			if (_debug != null) {
				_debug.AddText(msg);
			}
			
		}
		

		
		
		
		
		public String GetProgramVersion() {
		
			return Application.ProductVersion.ToString().Trim();
		
		}
		
		
		public System.Drawing.Image GetResourceImage(String filename) {

		   	ResourceManager rm = new ResourceManager("PswgLauncher.PswgRes", Assembly.GetExecutingAssembly());
        	
	        return ((System.Drawing.Image) rm.GetObject(filename));
		}
		
		public System.Drawing.Icon GetAppIcon() {

		   	ResourceManager rm = new ResourceManager("PswgLauncher.PswgRes", Assembly.GetExecutingAssembly());
        	
	        return ((System.Drawing.Icon) rm.GetObject("ProjectSWG Launcher"));
			
		}
		
		[DllImport("Gdi32.dll", EntryPoint="CreateRoundRectRgn")]
		public static extern IntPtr CreateRoundRectRgn
		(
			int nLeftRect, // x-coordinate of upper-left corner
			int nTopRect, // y-coordinate of upper-left corner
			int nRightRect, // x-coordinate of lower-right corner
			int nBottomRect, // y-coordinate of lower-right corner
			int nWidthEllipse, // height of ellipse
			int nHeightEllipse // width of ellipse
		);

		
	}
	
	
	
	
	
}
