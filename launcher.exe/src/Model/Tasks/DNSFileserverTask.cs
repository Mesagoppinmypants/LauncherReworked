/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 25.09.2013
 * Time: 16:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using PswgLauncher.Model.Status;

namespace PswgLauncher.Model.Tasks
{
	/// <summary>
	/// Description of DNSFileserverTask.
	/// </summary>
	public class DNSFileserverTask : LauncherTask
	{
		public DNSFileserverTask()
		{
			TaskName = "Fileserver Lookup";
		}
		
		public override LauncherTask GetNextTask()
		{
			return new ReadyTask();
		}		
		
	}
}
