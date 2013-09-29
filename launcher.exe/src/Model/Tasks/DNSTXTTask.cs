/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 27.09.2013
 * Time: 10:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using PswgLauncher.Util;

namespace PswgLauncher.Model.Tasks
{
	/// <summary>
	/// Description of DNSTXTTask.
	/// </summary>
	public abstract class DNSTXTTask : LauncherTask
	{
		
		public override void Work(System.ComponentModel.BackgroundWorker worker, GuiController Controller, object sender, System.ComponentModel.DoWorkEventArgs e)
		{

			if (SkipLookup(Controller)) {
				Controller.AddDebugMessage(TaskName + "Skip Work()");
				return;
			}			
			
			Controller.AddDebugMessage(TaskName + " Work()");
			run = false;
			busy = true;
			
			String Servers;
			// in case of error, exception is thrown
			Servers = DNSQuery.DnsGetTxtRecord(GetLookupString(Controller));
						
			if (Servers == null || Servers.Equals("")) {
				throw new Exception("Empty DNS response");
			}
			
			e.Result = Servers;
		}
				
		public override bool Complete(System.ComponentModel.BackgroundWorker worker, GuiController Controller, object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			
			if (SkipLookup(Controller)) {
				success = true; 
				Controller.RefreshStatus(false);
				return true;
			}
			
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
			
			SetServers(Controller, e.Result);
			
			success = true;
			Controller.RefreshStatus(false);
			return true;			
		}
								
		public override string GetTaskText()
		{
			if (busy) { return "Reading DNS Info"; }
			return base.GetTaskText();
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
		
		public abstract void SetServers(GuiController Controller, object Servers);
		public abstract string GetLookupString(GuiController Controller);
		public abstract bool SkipLookup(GuiController Controller);
		
	}
}
