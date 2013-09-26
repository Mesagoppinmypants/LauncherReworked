/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 25.09.2013
 * Time: 16:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using PswgLauncher.Model.Status;

namespace PswgLauncher.Model.Tasks
{
	/// <summary>
	/// Description of LauncherPatchTask.
	/// </summary>
	public class LauncherPatchTask : LauncherTask
	{
		public LauncherPatchTask()
		{
			TaskName = "Launcher Patch";
		}
		
		public override LauncherTask GetNextTask()
		{
			return new ReadyTask();
		}

		public override void Work(System.ComponentModel.BackgroundWorker worker, GuiController Controller, object sender, System.ComponentModel.DoWorkEventArgs e)
		{
		
		}
	}
}
