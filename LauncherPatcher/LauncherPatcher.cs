/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 17.02.2013
 * Time: 18:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace LauncherPatcher
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class LauncherPatcher : Form
	{
		
		
		public LauncherPatcher()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.Show();
			
		}
		
		
		
		
		public void Patch(String path) {

			string PatchServer = "patch1.projectswg.com";
			
			string PATCHURL = "http://"+PatchServer+"/launcher/";
			string LAUNCHER = "ProjectSWG Launcher.exe";
			string LAUNCHER_DL = PATCHURL + LAUNCHER;
			string ChksumDL = LAUNCHER_DL + ".md5";

			String TargetFile = Application.StartupPath + @"\pswgpath";
        	Process[] LauncherProcesses;
        	String checksum;
        	
        	WebClient wc = new WebClient();
        	wc.Encoding = System.Text.Encoding.UTF8;
        	

        	do {
        	
				LauncherProcesses = Process.GetProcessesByName("ProjectSWG Launcher");
				
				if (LauncherProcesses.Length > 0  ) {
				
					StatusLabel.Text = "There are still ProjectSWG Launcher.exe processes running, waiting for them to be closed...";
					this.Refresh();
					
					Thread.Sleep(500);
					
				}
	       	
        	
        	} while (LauncherProcesses.Length > 0  );
        	
        	
			StatusLabel.Text = "Init...";
			this.Refresh();
					
			Thread.Sleep(500);


        	
        	
        	if (path == null) {

	        	try {
	        		
	        		using(StreamReader reader = new StreamReader(TargetFile)) {
	        			path = reader.ReadLine();
	        			
	        			if (!Directory.Exists(path)) {
	        				path = null;
	        			}
	        		}
	        		
	        	} catch {}
        		
        	}
        	
        	
        	//well, that's tough! :P
        	if (path == null) {
        		
        		path = Application.StartupPath;
        		
        	}
        	
        	
        	String Local = path + @"\" + LAUNCHER;
        	String LocalTmp = Local + ".part";

			StatusLabel.Text = "Downloading launcher to..." + LocalTmp;
			this.Refresh();
					
			Thread.Sleep(500);


        	
        	try {
				using (StreamReader upstreamVersionStreamReader = new StreamReader(wc.OpenRead(ChksumDL))) {
					checksum = Regex.Replace(upstreamVersionStreamReader.ReadToEnd(),"\n","");
				}

				bool IsGood = false;
	
				if (File.Exists(Local)) {
					
					IsGood = MatchChecksum(Local, checksum);
					
				}
				
				
				if (!IsGood) {
					wc.DownloadFile(LAUNCHER_DL, LocalTmp);
					
					IsGood = MatchChecksum(LocalTmp, checksum);
					
					if (IsGood) {
						
						if (File.Exists(Local)) {
							File.Delete(Local);
						}
						
						
						File.Move(LocalTmp,Local);
					}
						
				}
        		
        	} catch {
        		//don't retry, just try to launch if it didn't work.
				MessageBox.Show("Downloading Launcher update failed. We're trying to start the existing Launcher now.","Error Downloading Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        	}
        	
        	
        	if (File.Exists(Local)) {
        		
            	System.Diagnostics.Process.Start(Local);
        		
        	}
        	
        	
        	this.Hide();
        	
	        return;        		

		}
		
		
		void LauncherPatcherFormClosing(object sender, FormClosingEventArgs e)
		{
			
			Application.Exit();
		}
		
		
		

        public static bool MatchChecksum(string Path, string chk) {

			if (!File.Exists(Path)) {
				return false;
			}
			
			System.IO.FileStream FileCheck = System.IO.File.OpenRead(Path);
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] md5Hash = md5.ComputeHash(FileCheck);
			FileCheck.Close();
			                
			string Calc =   BitConverter.ToString(md5Hash).Replace("-", "").ToLower();
			
			if (Calc == chk.ToLower()) {
				return true;
			}
			
			return false;
		}

		
	}
}
