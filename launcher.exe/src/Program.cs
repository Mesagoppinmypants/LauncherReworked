using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
            
            GuiController gc = new GuiController();
            
            
            if (!gc.runPatchChecker()) {
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
        

    }
}
