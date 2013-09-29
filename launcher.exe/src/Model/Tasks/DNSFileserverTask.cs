/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 25.09.2013
 * Time: 16:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace PswgLauncher.Model.Tasks
{
	/// <summary>
	/// Description of DNSFileserverTask.
	/// </summary>
	public class DNSFileserverTask : DNSTXTTask
	{
		public DNSFileserverTask()
		{
			TaskName = "Fileserver Lookup";
		}
		
		public override LauncherTask GetNextTask(GuiController Controller)
		{
			if (busy) { return this; }
			if (run && !success) {
				DialogResult dr = MessageBox.Show("We couldn't get the name of the pswg patch server. You may want to check your internet connection. If you want to try again, click \"Retry\", if you want to try to continue without checking for PSWG patches click \"Ignore\"", "PSWG Patch Server Lookup Failure", MessageBoxButtons.AbortRetryIgnore) ;
				if (dr == DialogResult.Abort) { return null; }
				if (dr == DialogResult.Ignore) { return new FilelistUpdateTask(); }
				if (dr == DialogResult.Retry) { return new DNSFileserverTask(); }
			}
			return new FilelistUpdateTask();
		}
		
		public override int GetTotalProgress() {
			return 20;
		}
		
		public override string GetLookupString(GuiController Controller)
		{
			return Controller.FileRecord;
		}
		
		public override void SetServers(GuiController Controller, object Servers)
		{
			Controller.SetFileServers(Servers);
		}
				
		public override bool SkipLookup(GuiController Controller)
		{
			if (Controller.FileServers.Count > 0) {
				return true;
			}
			return false;
		}
	}
}
