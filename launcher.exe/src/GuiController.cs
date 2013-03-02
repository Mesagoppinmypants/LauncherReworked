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
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PswgLauncher
{
	/// <summary>
	/// Description of Class2.
	/// </summary>
	public class GuiController
	{
		
		public static string PatchServer = "patch1.projectswg.com";
		public static string LoginServer = "login1.projectswg.com";
		public static string WebServer = "www.projectswg.com";
		
		public static string MAINURL = "http://"+PatchServer+"/files/";
		public static string LAUNCHER = "http://"+PatchServer+"/launcher/";
		public static string ALTURL = "http://projectswg.com/download/";
		
		public static string LocalFilelist = Application.StartupPath + "\\launcherS.dl.dat";
		
		private static string HttpAuthUser = "pswglaunch";
		private static string HttpAuthPass = "wvQAxc5mGgF0";
		
		public static string EncKey = "eKgeg75J3pTBURgh";
		
		
		private bool _soundOption;
		
		public PrivateFontCollection pfc {
			get; private set;
		}
		
		public bool HasFont {
			get; private set;
		}
		
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
		
		private bool _localhostOption;

		public bool LocalhostOption	{
			get {
				return _localhostOption;
			}
			set {
				_localhostOption = value;
				setAppSetting("LocalhostEnable",((_localhostOption == true) ? "true" : "false" ));
			}
			
		}

		
		private bool _resumeOption;
		public bool ResumeOption {
			get {
				return _resumeOption;
			}
			set {
				_resumeOption = value;
				setAppSetting("ResumeEnable", ((_resumeOption == true) ? "true" : "false" ));
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
		private NetworkWindow _network;
		
		
		private ResourceManager PswgResourcesManager;
		private ResourceManager PswgResources2Manager;		
		
		public GuiController()
		{
			_soundOption = false;
			_SwgDir = "";
			_checksumOption = false;
			_localhostOption = false;
			_resumeOption = true;
			
			_DebugMessages = new List<String>();
			SWGFiles = new SWGFileList(this);
			
			PswgResourcesManager = new ResourceManager("PswgLauncher.PswgRes", Assembly.GetExecutingAssembly());
			PswgResources2Manager = new ResourceManager("PswgLauncher.PSWGButtons", Assembly.GetExecutingAssembly());
			
			pfc = new PrivateFontCollection();
			HasFont = false;
			LoadFont();
			
		}
		
		
		
		private void LoadFont() {
			
			try {
				String Resource = "Starjedi.ttf";
				Stream FontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Resource);
				
				// create an unsafe memory block for the font data
				System.IntPtr data = Marshal.AllocCoTaskMem((int)FontStream.Length);
				
				// create a buffer to read in to
				byte[] fontdata = new byte[FontStream.Length];
				
				// read the font data from the resource
				FontStream.Read(fontdata, 0, (int)FontStream.Length);
				
				// copy the bytes to the unsafe memory block
				Marshal.Copy(fontdata, 0, data, (int)FontStream.Length);
				
				// pass the font to the font collection
				pfc.AddMemoryFont(data, (int)FontStream.Length);
				
				// close the resource stream
				FontStream.Close();
				
				// free up the unsafe memory
				Marshal.FreeCoTaskMem(data);
				HasFont = true;
			
			} catch {
				
				AddDebugMessage("Couldn't load Starjedi.TTF.");
				
			}
			
		}
		
		
		public void readConfig() {
			
			AddDebugMessage("Using " + AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

			_soundOption = ( ( getAppSetting("SoundEnable")  == "true") ? true : false);
			_checksumOption = ( ( getAppSetting("ChecksumEnable")  == "true") ? true : false);
			_localhostOption = ( ( getAppSetting("LocalhostEnable")  == "true") ? true : false);
			_resumeOption = ( ( getAppSetting("ResumeEnable")  == "false") ? false : true);
			
			_SwgDir = getAppSetting("SwgDir");
			
			SWGFiles.ReadLocalConfig();

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
		    
		    try {
		    	config.Save(ConfigurationSaveMode.Modified);
		    } catch {
		    	ShowErrorPermissions("Error writing config.");
		    }
		}
		
		public static void ShowErrorPermissions(String issue) {
			MessageBox.Show(issue + "\nMake sure ProjectSWG launcher is running with sufficent permissions.\n\nRestart with admin rights if needed.", "Error writing config", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}


		public bool runPatchChecker() {
			
			PatchCheckerInfoWindow pciw = new PatchCheckerInfoWindow(this);
			
			PatchChecker patch = new PatchChecker(this);
			bool update = patch.UpdateNeeded;

			pciw.CloseInfo();
			
			if (patch.remoteError) {
				AddDebugMessage("Remote error while checking for updates.");
				return true;
			} else if (patch.localError) {
				AddDebugMessage("Local error while checking for updates, lpatchusr.cfg write problems?");
				return true;
			} 
			
			if (update) {
				System.Diagnostics.Process.Start(Application.StartupPath + "/launcher patcher.exe");
				AddDebugMessage("Update available!");
				return false;

			}
			
			AddDebugMessage("Launcher is uptodate.");
			return true;
			
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
		
		public void LaunchNetwork() {
			if (_network == null || _network.IsDisposed) {
				_network = new NetworkWindow(this);
				
			}
			
			if (_network.Visible == false) {
				_network.Visible = true;
			}
			
		}		
		
		
		public void AddDebugMessage(String msg) {
			_DebugMessages.Add(msg);
			
			if (_debug == null || _debug.IsDisposed) {
				return;
			}
			
			_debug.AddText(msg);
			
		}
		

		
		
		
		
		public String GetProgramVersion() {
		
			return Application.ProductVersion.ToString().Trim();
		
		}
		
		public NetworkCredential GetNetworkCredential() {
			
			return new NetworkCredential(HttpAuthUser, HttpAuthPass);
			
		}
		
		
		public System.Drawing.Image GetResourceImage(String filename) {

	        return ((System.Drawing.Image) PswgResourcesManager.GetObject(filename));
		}

		public System.Drawing.Image GetResource2Image(String filename) {

	        return ((System.Drawing.Image) PswgResources2Manager.GetObject(filename));
		}

		
		
		public System.Drawing.Icon GetAppIcon() {

        	
	        return ((System.Drawing.Icon) PswgResourcesManager.GetObject("ProjectSWG Launcher"));
			
		}

		
		public Stream GetSound(String filename) {
			
			byte[] x = ((byte[]) PswgResourcesManager.GetObject(filename));
			
			if (x == null) { return null; }
			
			return new MemoryStream(x);
			
		}
		
		public void PlaySound(String filename) {
			
			System.Media.SoundPlayer player = new System.Media.SoundPlayer(this.GetSound(filename));
            if (this.soundOption) { player.Play(); }
			
		}
		
		
		
		public LauncherButton SpawnBaseButton(String text, Point p) {
			
			LauncherButton lb = new LauncherButton();
			
			if (text != null) { lb.Text = text; }
			if (p != null) { lb.Location = p; }
			
        	lb.BackColor = System.Drawing.Color.Transparent;
        	//lb.BackColor = Color.FromArgb(0, 255, 255, 255);
        	
        	//lb.TextColorNormal = System.Drawing.ColorTranslator.FromHtml("#ffa838");

			
        	lb.TextColorNormal = System.Drawing.ColorTranslator.FromHtml("#fffba7");
        	lb.TextColorClick = System.Drawing.ColorTranslator.FromHtml("#ffea3b");
        	lb.TextColorHover = System.Drawing.ColorTranslator.FromHtml("#ffea3b");
        	lb.TextColorDisable = System.Drawing.ColorTranslator.FromHtml("#72502e");

        	
        	if (HasFont) {
        		lb.Font = new Font(pfc.Families[0], 22);
        	} 
        	
        	return lb;
        	
		}
		
		public LauncherButton SpawnStandardButton(String text, Point p) {

			LauncherButton lb = SpawnBaseButton(text,p);

			lb.ImageClick = GetResource2Image("ButtonClick");
        	lb.ImageHover = GetResource2Image("ButtonHover");
        	lb.ImageNormal = GetResource2Image("ButtonNormal");
        	lb.ImageDisable = GetResource2Image("ButtonDisabled");
			
        	lb.Width = 118;
        	lb.Height = 45;
			
        	return lb;
			
		}
		
		public LauncherButton SpawnPlayButton(String text, Point p) {

			LauncherButton lb = SpawnBaseButton(text,p);
			
			lb.ImageClick = GetResource2Image("RedClick");
        	lb.ImageHover = GetResource2Image("RedHighlight");
        	lb.ImageNormal = GetResource2Image("RedNormal");
        	lb.ImageDisable = GetResource2Image("RedNormal");
			
        	lb.Width = 100;
        	lb.Height = 35;

        	return lb;
			
		}
		
		public LauncherButton SpawnMinimizeButton(Point p)  {
			
			LauncherButton lb = new LauncherButton();
			if (p != null) { lb.Location = p; }
			
        	lb.BackColor = System.Drawing.Color.Transparent;

			lb.ImageClick = GetResource2Image("MinimizeClick");
        	lb.ImageHover = GetResource2Image("MinimizeHighlighted");
        	lb.ImageNormal = GetResource2Image("MinimizeNormal");
        	lb.ImageDisable = GetResource2Image("MinimizeNormal");
        	
        	lb.Width = 27;
        	lb.Height = 20;
        	
        	return lb;			
		}
		
		public LauncherButton SpawnCloseButton(Point p)  {
			
			LauncherButton lb = new LauncherButton();
			if (p != null) { lb.Location = p; }
			
        	lb.BackColor = System.Drawing.Color.Transparent;

			lb.ImageClick = GetResource2Image("CrossClick");
        	lb.ImageHover = GetResource2Image("CrossHighlighted");
        	lb.ImageNormal = GetResource2Image("CrossNormal");
        	lb.ImageDisable = GetResource2Image("CrossNormal");
        	
        	lb.Width = 29;
        	lb.Height = 32;
        	
        	return lb;			
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
