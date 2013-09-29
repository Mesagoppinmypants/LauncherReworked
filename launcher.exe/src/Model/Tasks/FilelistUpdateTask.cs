/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 27.09.2013
 * Time: 10:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PswgLauncher.Model.Tasks
{
	/// <summary>
	/// Description of FilelistTask.
	/// </summary>
	public class FilelistUpdateTask : LauncherTask
	{
		public FilelistUpdateTask()
		{
			TaskName = "Update Filelist";
		}
		
		public override LauncherTask GetNextTask(GuiController Controller)
		{
			return new PswgPatchTask();
		}
	}
}
