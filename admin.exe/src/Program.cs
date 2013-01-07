/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 06.01.2013
 * Time: 19:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;

namespace AdminRightsLauncher
{
	class Program
	{
		
		[STAThread]
		public static void Main(string[] args)
		{
			
			WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
        	bool hasAdmin = pricipal.IsInRole(WindowsBuiltInRole.Administrator);   
        	bool Vista = IsWinVistaOrHigher();
			
			
			if (!Vista) {
				
				
				if (!hasAdmin) {
					
					ProcessStartInfo processStartInfo = new ProcessStartInfo(Application.ExecutablePath);
					processStartInfo.Verb = "runas";

					using (Process process = new Process())
					{
					   process.StartInfo = processStartInfo;
					   process.Start();
					   process.WaitForExit();
					}
					
					Application.Exit();
					return;
					
				}
				
				
				
			} 	
			
			
			String LauncherPath = Application.StartupPath + @"\ProjectSWG Launcher.exe";
			String AdminSeenFile = Application.StartupPath + @"\Admin.cfg";
			
			if ((Vista) && (!File.Exists(AdminSeenFile))) {
				
				ProgramInfoDialog pinfo = new ProgramInfoDialog();
				
				DialogResult dr = pinfo.ShowDialog();
				
				if (dr == DialogResult.Cancel) {
		        	Application.Exit();
		       		return;
				}
				
			}

    			
    			
    		Process[] LauncherProcesses = Process.GetProcessesByName("ProjectSWG Launcher");
	        	
		    foreach (Process p in LauncherProcesses) {
		    	Debug.WriteLine(p.ToString()  );
		        	
		        if (p.Id == Process.GetCurrentProcess().Id) {
		        	continue;
		        }
		        	
		        MessageBox.Show("ProjectSWG Launcher is already running. If it doesn't respond, close it manually through task manager. (Ctrl+Shift+Esc on some Windows versions).","ProjectSWG Launcher",MessageBoxButtons.OK);
		        Application.Exit();
		       	return;
	
		    }


    		if (!File.Exists(LauncherPath)) {
		        MessageBox.Show("ProjectSWG Launcher.exe was not found.","ProjectSWG Launcher.exe not found",MessageBoxButtons.OK);
		        Application.Exit();
		       	return;    			
    		}
    		

    		try {
    			
    			System.Diagnostics.Process.Start(LauncherPath);
    		} catch {}
    			
			
		}
		
		static bool IsWinVistaOrHigher()
		{
		    OperatingSystem OS = Environment.OSVersion;
    		return (OS.Platform == PlatformID.Win32NT) && (OS.Version.Major >= 6);
		}
		
	}
}