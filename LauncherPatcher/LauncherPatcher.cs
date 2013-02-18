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
using System.Threading;
using System.Windows.Forms;

namespace LauncherPatcher
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class LauncherPatcher : Form
	{
		

		public static string FTPURL = "ftp://patch1.projectswg.com/files/";
		
		private int LauncherSize = 0;
		private String LauncherChecksum;
		
		public LauncherPatcher()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.Show();
			
		}
		
		
		
		
		public void Patch(bool force) {

			
			buttonRetry.Enabled = false;
			buttonForceDL.Enabled = false;
			
        	Process[] LauncherProcesses;
        	
        	

        	do {
        	
				LauncherProcesses = Process.GetProcessesByName("ProjectSWG Launcher");
				
				if (LauncherProcesses.Length > 0  ) {
				
					StatusLabel.Text = "There are still ProjectSWG Launcher.exe processes running, waiting for them to be closed...";
					this.Refresh();
					
					Thread.Sleep(500);
					
				}
	       	
        	
        	} while (LauncherProcesses.Length > 0  );


			WebClient wc = new WebClient();
		    wc.Credentials = new NetworkCredential("anonymous", "anonymous");

        	

		    String remoteinfo;
		    
		    if (!force) {
		    
	        	if (LauncherSize == 0 || LauncherChecksum == null) {
	        		StatusLabel.Text = "Downloading Launcher info.";
	        		this.Refresh();
	        		
	        		try {
						
			            StreamReader upstreamVersionStreamReader = new StreamReader(wc.OpenRead(FTPURL + "launcherinfo.dat"));
			            
			            remoteinfo = upstreamVersionStreamReader.ReadToEnd();
		
		            
					} catch  {
	        			
						
	        			StatusLabel.Text = "Remote Launcher Info download failed.";
	        			buttonRetry.Enabled = true;
	        			buttonForceDL.Enabled = true;
						return;
						
					}
					
					remoteinfo = remoteinfo.Trim();
	
	        		
					String[] tokens = remoteinfo.Split(' ');
					
					if (tokens.Length >= 2) {
						LauncherSize = int.Parse(tokens[0]);
						LauncherChecksum = tokens[1];
						StatusLabel.Text = LauncherSize + LauncherChecksum;
					}
	        		
	        		
	        	}
			    
	        	if (LauncherSize == 0 || LauncherChecksum == null) {
	        		StatusLabel.Text = "Reading Launcher info failed.";
					buttonRetry.Enabled = true;
					buttonForceDL.Enabled = true;
	        		this.Refresh();
	        		return;
			    }
	        	
		    }
		    
		    
		    if (File.Exists(Application.StartupPath + "ProjectSWG Launcher.exe.part")) {
		    	try { 
		    		StatusLabel.Text = "Delete existing Launcher.";
		    		this.Refresh();
		    		File.Delete(Application.StartupPath + "ProjectSWG Launcher.exe.part");
		    	} catch {
		    		StatusLabel.Text = "Deleting existing file ProjectSWG Launcher.exe.part failed.";
					buttonRetry.Enabled = true;
	        		this.Refresh();
	        		return;
		    	}
		    }
		    
		    try {
		    	StatusLabel.Text = "Downloading new Launcher.";
		    	this.Refresh();
		    	wc.DownloadFile(FTPURL + "ProjectSWG Launcher.exe", Application.StartupPath + "ProjectSWG Launcher.exe.part");
		    } catch {
		    	StatusLabel.Text = "Downloading ProjectSWG Launcher.exe failed.";
				buttonRetry.Enabled = true;
	        	this.Refresh();
	        	return;
		    	
		    }
		    
		    
		    if (!force) { 
		    	StatusLabel.Text = "Verifying downloaded Launcher.";
		    	this.Refresh();
		    	
		    	if (!LauncherPatcher.compareCheckSum(Application.StartupPath + "ProjectSWG Launcher.exe.part", LauncherChecksum) )  {
		    	
			    	StatusLabel.Text = "Verification of ProjectSWG Launcher.exe failed.";
					buttonRetry.Enabled = true;
					buttonForceDL.Enabled = true;
		        	this.Refresh();
			    	return;
		    	}
		    	
		    }
		    
		    
		    try {
		    	StatusLabel.Text = "Installing new Launcher.";
		    	this.Refresh();
		    	File.Move(Application.StartupPath + "ProjectSWG Launcher.exe.part", Application.StartupPath + "ProjectSWG Launcher.exe");
		    } catch {
		    	
		    	StatusLabel.Text = "Copying ProjectSWG Launcher.exe failed.";
				buttonRetry.Enabled = true;
	        	this.Refresh();
		    	return;
		    	
		    	
		    }
		    
		    StatusLabel.Text = "Running new Launcher.";
		    System.Diagnostics.Process.Start(Application.StartupPath + "/ProjectSWG Launcher.exe");
		    Application.Exit();
			
		}
		
		
		
		void LauncherPatcherFormClosing(object sender, FormClosingEventArgs e)
		{
			Application.Exit();
		}
		
		void ButtonCancelClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		void ButtonRetryClick(object sender, EventArgs e)
		{
			Patch(false);
		}
		
		void ButtonForceDLClick(object sender, EventArgs e)
		{
			Patch(true);
		}
		
		
        private static bool compareCheckSum(string Filepath,string Checksum) {
        	
        	if (!File.Exists(Filepath)) {
        		return false;
        	}

			System.IO.FileStream FileCheck = System.IO.File.OpenRead(Filepath);                
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] md5Hash = md5.ComputeHash(FileCheck);                
			FileCheck.Close();
			                
			string Calc =   BitConverter.ToString(md5Hash).Replace("-", "").ToLower();
			if (Calc == Checksum.ToLower()) {
				return true;
			}
			return false;                  
		}
		
		
		
		
	}
}
