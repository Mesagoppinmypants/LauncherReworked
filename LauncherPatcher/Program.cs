/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 17.02.2013
 * Time: 18:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace LauncherPatcher
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			
			
        	Process[] PatcherProcesses = Process.GetProcessesByName("Launcher Patcher");
	        	
	        foreach (Process p in PatcherProcesses) {
	        	Debug.WriteLine(p.ToString()  );
	        	
	        	if (p.Id == Process.GetCurrentProcess().Id) {
	        		continue;
	        	}
	        	
	        	MessageBox.Show("ProjectSWG Launcher Patcher is already running. If it doesn't respond, close it manually through task manager. (Ctrl+Shift+Esc on some Windows versions).","ProjectSWG Launcher",MessageBoxButtons.OK);
	        	Application.Exit();
	        	return;

	        }
			
			
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			
			LauncherPatcher lp = new LauncherPatcher();
			
			string path = null;
			
			if (args.Length > 0) {
				
				path = args[0];
				
				if (!Directory.Exists(path)) {
					path = null;
				}
				
			}
			
			lp.Patch(path);
			
			
			
			Application.Exit();
			
			
		}
		
	}
}
