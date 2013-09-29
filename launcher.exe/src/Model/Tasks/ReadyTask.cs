/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 20.09.2013
 * Time: 19:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;

namespace PswgLauncher.Model.Tasks
{
	/// <summary>
	/// Description of ReadyTask.
	/// </summary>
	public class ReadyTask : LauncherTask
	{
		public ReadyTask()
		{
			TaskName = "Final";
		}
		
		public override LauncherTask GetNextTask(GuiController Controller)
		{
			return this;
		}
				
		public override String GetPlayText() {
			if (busy) { return base.GetPlayText(); }
			return "Play!";
		}
				
		public override string GetTaskText()
		{
			 if (busy) { return base.GetPlayText(); }
			 return "";
		}

		
		public override string GetTotalText() {
			return "Ready To Play!";			
		}
		
		public override bool Complete(System.ComponentModel.BackgroundWorker worker, GuiController Controller, object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
			base.Complete(worker, Controller, sender, e);
			return false;
		}
		
		public override int GetTotalProgress() {
			return 100;
		}
		
		public override System.Drawing.Color GetTaskColor()
		{
			return Color.Green;
		}
		
	}
}
