/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 20.09.2013
 * Time: 15:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;

namespace PswgLauncher.Model.Status
{
	/// <summary>
	/// Description of Interface1.
	/// </summary>
	public abstract class LauncherTask
	{
		
		protected String TaskName = "";
		protected bool busy = false;
		protected bool success = false;
		protected bool run = false;
		
		public virtual bool Init(System.ComponentModel.BackgroundWorker worker, GuiController Controller)
		{
			Controller.AddDebugMessage(TaskName + " init()");
			Controller.RefreshStatus(true);
			return true;
		}
		
		public virtual void Work(System.ComponentModel.BackgroundWorker worker, GuiController Controller, object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			Controller.AddDebugMessage(TaskName + " Work()");
			busy = true;
			worker.ReportProgress(0, "Running");
			worker.ReportProgress(100, "Done");
		}
		
		public virtual bool Complete(System.ComponentModel.BackgroundWorker worker, GuiController Controller, object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{	
			Controller.AddDebugMessage(TaskName + " Complete()");
			run = true;
			busy = false;
			
			if (e.Cancelled || e.Error != null) {
				success = false;
				Controller.RefreshStatus(false);
				return false;
			}
			
			success = true;
			Controller.RefreshStatus(false);
			return true;
		}
		
		public virtual bool ReportProgress(System.ComponentModel.BackgroundWorker worker, GuiController Controller,  object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			return false;
		}
		
		public abstract LauncherTask GetNextTask();
	
		public virtual bool GetPlayDisabled()
		{
			return busy;
		}
		
		public virtual bool GetScanDisabled()
		{
			return true;
		}
		
		public virtual bool GetBusy()
		{
			return busy;
		}
		
		public virtual String GetPlayText()
		{
			if (busy) { return "Working"; }
			if (!run) { return "Start"; }
			if (!success) { return "Retry"; }
			return "Continue";
		}
		
		public virtual String GetLabelText()
		{
			if (busy) { return TaskName + " Running"; }
			if (!run) { return TaskName +  " Waiting to start"; }
			if (!success) { return TaskName +  " Failed"; }
			return TaskName + " Success";
		}
		
		public virtual System.Drawing.Color GetStatusColor()
		{
			if (busy) { return Color.Blue; }
			if (!run) { return Color.Orange; }
			if (!success) { return Color.Red; }
			return Color.Green;
		}
		
		public virtual bool IsPausable()
		{
			return false;
		}
		
		public virtual int PlayClick(GuiController Controller)
		{
			if (busy) { return -1; }
			if ((!run) || (!success)) { return 0; }
			return 1;
		}
		
		public virtual void ScanClick(GuiController Controller)
		{
			return;			
		}		
		
        
		
	}
}
