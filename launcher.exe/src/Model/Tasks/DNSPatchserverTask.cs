/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 20.09.2013
 * Time: 19:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using PswgLauncher.Util;

namespace PswgLauncher.Model.Tasks
{
	/// <summary>
	/// Description of DNSTask.
	/// </summary>
	public class DNSPatchserverTask : DNSTXTTask
	{
		
		public DNSPatchserverTask()
		{
			TaskName = "Patch Server Lookup";
		}
		
		public override LauncherTask GetNextTask(GuiController Controller)
		{
			if (busy) { return this; }
			if (run && !success) {
				DialogResult dr = MessageBox.Show("We couldn't get the name of the launcher patch server. You may want to check your internet connection. If you want to try again, click \"Retry\", if you want to continue without checking for launcher patches click \"Ignore\"", "Launcher Patch Server Lookup Failure", MessageBoxButtons.AbortRetryIgnore) ;
				if (dr == DialogResult.Abort) { return null; }
				if (dr == DialogResult.Ignore) { return new DNSFileserverTask(); }
				if (dr == DialogResult.Retry) { return new DNSPatchserverTask(); }
			}
			return new LauncherPatchTask();
		}
		
		public override int GetTotalProgress() {
			return 10;
		}
		
		public override string GetLookupString(GuiController Controller)
		{
			return Controller.PatchRecord;
		}
		
		public override void SetServers(GuiController Controller, object Servers)
		{
			Controller.SetPatchServers(Servers);
		}
		
		public override bool SkipLookup(GuiController Controller)
		{
			if (Controller.PatchServers.Count > 0) { return true; }
			return false;
		}
	}
}
