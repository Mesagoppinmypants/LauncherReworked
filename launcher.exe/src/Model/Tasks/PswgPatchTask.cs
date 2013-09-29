/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 27.09.2013
 * Time: 10:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PswgLauncher.Model.Tasks
{
	/// <summary>
	/// Description of PswgPatchTask.
	/// </summary>
	public class PswgPatchTask : LauncherTask
	{
		public PswgPatchTask()
		{
			TaskName = "Patch SWG"; 
		}
		
		public override LauncherTask GetNextTask(GuiController Controller)
		{
			return new ReadyTask();
		}
	}
}
