﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

using Microsoft.Win32;

namespace PswgLauncher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
        	

			int RunAsMode = 0;        	

        	RegistryKey TheKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ProjectSWG", false);
        	
			if (TheKey != null) {
				
				object TheSetting = TheKey.GetValue("RunAsBehaviour");
				
				if (TheSetting != null) {
	
					switch ((int) TheSetting) {
						case 1:
							RunAsMode = 1;
							break;
						case 2:
							RunAsMode = 2;
							break;
					}
					
				}
			}        	
        	
        	WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
			bool hasAdministrativeRight = principal.IsInRole(WindowsBuiltInRole.Administrator);
        	
			if (RunAsMode == 1)  {
			    if (!hasAdministrativeRight) {
					
					if (RunElevated(Application.ExecutablePath)) {

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
            
            GuiController gc = new GuiController(RunAsMode);
            
            
            if (!gc.RunPatchChecker()) {
            	Application.Exit();
            	return;
            }
            
            gc.readConfig();
           
            if (!gc.runDirSearch()) {
            	Application.Exit();
            	return;
            }
            
            gc.runLauncher();
            Application.Run();

        }
        
        
        
        
        private static bool RunElevated(string fileName)
		{
		    ProcessStartInfo processInfo = new ProcessStartInfo();
		    processInfo.Verb = "runas";
		    processInfo.FileName = fileName;
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
