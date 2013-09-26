/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 20.09.2013
 * Time: 19:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using PswgLauncher.Model.Status;
using PswgLauncher.Util;

namespace PswgLauncher.Model.Tasks
{
	/// <summary>
	/// Description of DNSTask.
	/// </summary>
	public class DNSPatchserverTask : LauncherTask
	{
		
		public DNSPatchserverTask()
		{
			TaskName = "Patch Server Lookup";
		}

		public override void Work(System.ComponentModel.BackgroundWorker worker, GuiController Controller, object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			Controller.AddDebugMessage(TaskName + " Work()");
			run = false;
			busy = true;
			
			String PatchServers;
			// in case of error, exception is thrown
			PatchServers = DNSQuery.DnsGetTxtRecord(ProgramConstants.PatchRecord);
			//Debug.WriteLine("Found " + PatchServers + ";");
						
			if (PatchServers == null || PatchServers.Equals("")) {
				throw new Exception("Empty DNS response");
			}
			
			e.Result = PatchServers;
			
		}
				
		public override bool Complete(System.ComponentModel.BackgroundWorker worker, GuiController Controller, object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			
			run = true;
			busy = false;
			
			if (e.Cancelled || e.Error != null) {
				Controller.AddDebugMessage(TaskName + " Complete() (no success)");
				if (e.Error != null) { Controller.AddDebugMessage(e.Error.ToString()); }
				
				success = false;
				Controller.RefreshStatus(false);
				
				return false;
			}
			Controller.AddDebugMessage(TaskName + " Complete()");
			
			Controller.SetPatchServers(e.Result);
			
			success = true;
			Controller.RefreshStatus(false);
			return true;			
		}
				
		public override LauncherTask GetNextTask()
		{
			if (busy) { return this; }
			if (run && !success) {
				DialogResult dr = MessageBox.Show("We couldn't get the name of the launcher patch server. You may want to check your internet connection. If you want to try again, click \"Retry\", if you want to continue without checking for launcher patches click \"Ignore\"", "Launcher Patch Server Lookup Failure", MessageBoxButtons.AbortRetryIgnore) ;
				if (dr == DialogResult.Abort) { return null; }
				if (dr == DialogResult.Ignore) { return new DNSFileserverTask(); }
				if (dr == DialogResult.Retry) { return new DNSPatchserverTask(); }
			}
			return new LauncherPatchTask();
		}
				
		public override string GetLabelText()
		{
			if (busy) { return "Reading DNS Info"; }
			return base.GetLabelText();
		}
		
		public override string GetPlayText()
		{
			if (busy) { return "Working"; }
			if (!run) { return "Start"; }
			if (!success) { return "Next..."; }
			return "Continue"; 
		}

		
		public override int PlayClick(GuiController Controller)
		{
			if (busy) { return -1; }
			if (!run) { return 0; }
			return 1;
		}
		
	}
}
