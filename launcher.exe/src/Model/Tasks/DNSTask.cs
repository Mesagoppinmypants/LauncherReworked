/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 20.09.2013
 * Time: 19:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using PswgLauncher.Model.Status;

namespace PswgLauncher.Model.Tasks
{
	/// <summary>
	/// Description of DNSTask.
	/// </summary>
	public class DNSTask : LauncherTask
	{
		public DNSTask()
		{
			TaskName = "DNS";
		}
				
		public override bool Work(System.ComponentModel.BackgroundWorker worker, GuiController Controller, object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			Controller.AddDebugMessage(TaskName + " Work()");
			busy = true;
			
			
			
			busy = false;			
			return true;
		}
				
		public override bool Complete(System.ComponentModel.BackgroundWorker worker, GuiController Controller, object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			
			return base.Complete(worker, Controller, sender, e);
		}
				
		public override LauncherTask GetNextTask()
		{
			if ((busy) || (run && !success)) { return this; }
			return new ReadyTask();
		}
				
		public override string GetLabelText()
		{
			if (busy) { return "Reading DNS Info"; }
			return base.GetLabelText();
		}
		
				
	}
}
