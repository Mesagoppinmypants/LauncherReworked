using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

using Microsoft.Win32;
using PswgLauncher.Model;

namespace PswgLauncher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
        	//FIXME: add better argument handling
        	
        	CommandLineOptions opts = new CommandLineOptions();
        	bool rv = opts.Parse(args);
        	if (!rv) {
        		Application.Exit();
        		return;
        	}
        	
        	//this is hackish, but need it to pass args to next instance for runas
        	for (int i = 0; i<args.Length;i++) {
        		if (args[i].Contains(" ")) {
        			args[i] = "\"" + args[i] +  "\"";
        		}
        	}
        	
        	String ArgString = string.Join(" ", args);
        	String Workdir = GetRegistryWorkdirSetting(opts.WorkdirSetting);
        	
			int RunAsMode = GetRegistryRunAsSetting(opts.RunAsSetting);
			bool RegNeedsSetting = false;
			if (RunAsMode < 0) {
				RegNeedsSetting = true;
			}
        	
        	WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
			bool hasAdministrativeRight = principal.IsInRole(WindowsBuiltInRole.Administrator);
        	
			if (RunAsMode == 1)  {
			    if (!hasAdministrativeRight) {
					
					if (RunElevated(Application.ExecutablePath, ArgString)) {

					    Application.Exit();
					    return;
					}
					
				} else {
					// let the old process finish
					System.Threading.Thread.Sleep(500);
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
        	
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            GuiController gc = new GuiController(RunAsMode, Workdir, ArgString, opts.VersionOverride, opts.DNSPatchRecord, opts.DNSPatchRecordContents, opts.DNSFileRecord, opts.DNSFileRecordContents);
            gc.ReadConfig();
            gc.CheckScanNeeded();
            gc.RunLauncher();
            if (RegNeedsSetting) {
            	gc.LaunchAdmSettings();
            }            
            Application.Run();

        }

		public static int GetRegistryRunAsSetting(int RunasOverride) {
			
        	Debug.WriteLine(RunasOverride);
        	
        	if ((RunasOverride != null) && RunasOverride >=0 && RunasOverride <= 1) {
        		return RunasOverride;
        	}
        	
        	RegistryKey TheKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ProjectSWG", false);
        	if (TheKey == null) { return -1; }
			
			object TheSetting = TheKey.GetValue("RunAsBehaviour");
			if (TheSetting == null) { return -1; }
			switch ((int) TheSetting) {
				case 0:
					return 0;
					break;
				case 1:
					return 1;
					break;
			}
			
			return -1;
		}
        
		public static string GetRegistryWorkdirSetting(String WorkdirOverride) {
        	
        	if ((WorkdirOverride != null) && Directory.Exists(WorkdirOverride)) {
        		return WorkdirOverride;
        	}
        	
        	RegistryKey TheKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ProjectSWG", false);
        	if (TheKey == null) {
        		return Application.StartupPath;
        	}
			
			object TheSetting = TheKey.GetValue("Location");
			if (TheSetting == null) {
				return Application.StartupPath;
			}
			
			if (!Directory.Exists((string) TheSetting)) {
				return Application.StartupPath;
			}
			
			return (string) TheSetting;
			
		}        

        private static bool RunElevated(string fileName, string args)
		{
		    ProcessStartInfo processInfo = new ProcessStartInfo();
		    processInfo.Verb = "runas";
		    processInfo.FileName = fileName;
		    processInfo.Arguments = args;
		    try
		    {
		        Process.Start(processInfo);
		        return true;
		    }
		    catch (Win32Exception)
		    {
		        //Do nothing. Probably the user canceled the UAC window
		    }
		    return false;
		}        

    }
}
