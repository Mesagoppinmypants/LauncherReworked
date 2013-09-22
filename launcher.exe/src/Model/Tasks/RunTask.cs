/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 20.09.2013
 * Time: 17:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using PswgLauncher.Model.Status;

namespace PswgLauncher.Model.Tasks
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class RunTask : LauncherTask
	{
		public RunTask()
		{
			TaskName = "Startup";
		}

		public override LauncherTask GetNextTask()
		{
			return new DNSTask();
		}
		

	}
}
