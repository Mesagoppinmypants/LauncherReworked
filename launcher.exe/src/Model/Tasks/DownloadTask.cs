/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 28.09.2013
 * Time: 12:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PswgLauncher.Model.Tasks
{
	/// <summary>
	/// Description of DownloadTask.
	/// </summary>
	public abstract class DownloadTask : LauncherTask
	{
		
		public override int GetFileProgress() {
			return 10;
		}

		public override bool ReportProgress(System.ComponentModel.BackgroundWorker worker, GuiController Controller,  object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			return false;
		}

		
	}
}
