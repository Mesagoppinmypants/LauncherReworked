/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 25.09.2013
 * Time: 16:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

using PswgLauncher.Util;

namespace PswgLauncher.Model.Tasks
{
	/// <summary>
	/// Description of LauncherPatchTask.
	/// </summary>
	public class LauncherPatchTask : DownloadTask
	{
		public LauncherPatchTask()
		{
			TaskName = "Launcher Patch";
		}
		
		public override LauncherTask GetNextTask(GuiController Controller)
		{
			
			
			return new DNSFileserverTask();
		}

		public override void Work(System.ComponentModel.BackgroundWorker worker, GuiController Controller, object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			
			List<String> servers = Controller.PatchServers;
			Controller.AddDebugMessage(TaskName + " Work()");
			run = false;
			busy = true;
			
			String version = "";
			String checksum = "";
			String UseServer = "";
			
			foreach (String server in servers) {
				using (MemoryStream ms = new MemoryStream()) {
					String URL = "http://"+ server + ProgramConstants.PatchInstallerVersionUrl;
					String URL2 = "http://"+ server + ProgramConstants.PatchInstallerChecksumUrl;
					Controller.AddDebugMessage("Trying " + server + " for launcher version");
					
					//get the launcher version
					StreamReader sr = new StreamReader(ms);
					Downloader.HTTPDownload(Controller, ms, "launcher_version", URL, 0, worker);
					ms.Position = 0;
					version = sr.ReadToEnd().Trim();
					
					//get the checksum
					ms.Position = 0;
					Downloader.HTTPDownload(Controller, ms, "launcher_checksum", URL2, 0, worker);
					ms.Position = 0;
					checksum = sr.ReadToEnd().Trim();
			    }
				
				Debug.WriteLine(version);
				Debug.WriteLine(checksum);
				if (Regex.IsMatch(version, @"^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$") &&
				    Regex.IsMatch(checksum, @"^[a-z0-9]+$")) {
					Controller.AddDebugMessage("...is usable.");
					UseServer = server;
					break;
				}
				
				checksum = "";
				version = "";
			}
			
			if (version == "" || UseServer == "") {
				throw new Exception("No usable servers found.");
			}
			//FIXME: consider cancellation
			
			if (new ProgramVersion(Controller.GetProgramVersion()).IsNewerThan(new ProgramVersion(version))) {
				e.Result = "";
				Controller.AddDebugMessage("Launcher is newer than upstream version.");
				return;
			}
			
			//check if launcher installer exists and is usable
			
			String filename = Controller.SwgSavePath + @"\" + ProgramConstants.PatchInstallerFile;
			
			//download otherwise
			String URL3 = "http://"+ UseServer + ProgramConstants.PatchInstallerFile;
			bool resume = false;
			for (int i = 0; i < 10; i++) {
				
				if (SWGFile.MatchChecksum(filename, checksum)) {
					e.Result = "update";
					Controller.AddDebugMessage("There is a new installer available that matches the checksum.");
					return;					
				}
				Downloader.HTTPDownload(Controller, filename + ".part",URL3, 0, false, worker);
			}
			
			throw new Exception("Download failed more than 10 times.");
			
		}
		
		
		
		public override bool Complete(System.ComponentModel.BackgroundWorker worker, GuiController Controller, object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			return true;
		}
		
		public override int GetTotalProgress() {
			if (success) { return 25; }
			return 20;
		}
	}
}
