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
				string FTP = "ftp://173.242.114.16/files/";
				WebClient wc = new WebClient();
	            wc.Credentials = new NetworkCredential("anonymous", "anonymous");
	            StreamReader upstreamVersionStreamReader = new StreamReader(wc.OpenRead(FTP + "lpatch.cfg"));
	            
	            lpatchsrv = upstreamVersionStreamReader.ReadToEnd();
	            Controller.AddDebugMessage("test" + lpatchsrv);
	            
            
            
			} catch (WebException e) {
				
				Controller.AddDebugMessage("Eek! Exception " + e.ToString() );
				
				remoteError = true;
				return;
				
			} 
			
			
			try {
			    
	            if (File.Exists(Application.StartupPath + "/lpatchusr.cfg")) {
	            	
	            		
	            	  lpatchusr = System.IO.File.ReadAllText(Application.StartupPath + "/lpatchusr.cfg");
	            	  
			    }
			    
	            	
			} catch (Exception e) {
				localError = true;
				return;
			}
		            
			
			
             if (lpatchsrv != lpatchusr) {
            	
            	UpdateNeeded = true;
            	
            } 
			
			//DEBUG
			//UpdateNeeded = true;
			
		}
		
		
		
	}
}
