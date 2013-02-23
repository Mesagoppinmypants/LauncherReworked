/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 22.12.2012
 * Time: 16:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace PswgLauncher
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class PatchChecker
	{
		
		public bool UpdateNeeded { get; private set; }
		public bool remoteError { get; private set; }
		public bool localError { get; private set; }
		
		GuiController Controller;
		
		public PatchChecker(GuiController gc)
		{
			
			this.Controller = gc;
			UpdateNeeded = false;
			localError = false;
			remoteError = false;
			this.runCheck();
			
		}
		
		
	
		
		protected void runCheck() {
			
			string lpatchsrv = "";
			string lpatchusr = "";
			
			Controller.AddDebugMessage("checking for updates...");
			
			try {
				
				//TODO this should be stored centrally somewhere.
				
				WebClient wc = new WebClient();
				wc.Encoding = System.Text.Encoding.UTF8;

	            StreamReader upstreamVersionStreamReader = new StreamReader(wc.OpenRead(GuiController.LAUNCHER + "lpatch.cfg"));
	            
	            lpatchsrv = upstreamVersionStreamReader.ReadToEnd();

            
			} catch (WebException e) {
				
				Controller.AddDebugMessage("Eek! Exception " + e.ToString() );
				
				remoteError = true;
				return;
				
			}
			
			lpatchsrv = lpatchsrv.Trim();


			ProgramVersion ThisVersion = new ProgramVersion(Controller.GetProgramVersion());
			ProgramVersion ServerVersion = new ProgramVersion(lpatchsrv);
			
			Controller.AddDebugMessage("Server Launcher Version" + Controller.GetProgramVersion() + "|" + ThisVersion.ToCompactString() + " |" + ThisVersion.ToString());
			Controller.AddDebugMessage("Local Launcher Version" + lpatchsrv + "|" + ServerVersion.ToCompactString() + " |" + ServerVersion.ToString());
	        
	        //backward compatibility for launcher patcher for the time being.
	        try {
	        	System.IO.StreamWriter file = new System.IO.StreamWriter(Application.StartupPath + @"\lpatchusr.cfg");
	        	file.WriteLine(Controller.GetProgramVersion());
	        	file.Close();
	        		
	        } catch (Exception e) {
	        	localError = true;
	        	return;
	        }
			
			
	        if (!ThisVersion.IsNewerThan(ServerVersion)) {
            	
            	UpdateNeeded = true;
            	
            } 
			
			//DEBUG
			//UpdateNeeded = true;
			
		}
		
		
		
	}
}
