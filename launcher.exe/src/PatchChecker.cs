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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PswgLauncher
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class PatchChecker
	{
		
		GuiController Controller;
		WebClient wc;
		
		public PatchChecker(GuiController gc)
		{
			
			this.Controller = gc;

			wc = new WebClient();
			wc.Encoding = System.Text.Encoding.UTF8;
			
		}
		
		
	
		//exceptions need handling in calling method
		public bool RunCheck() {
			
			string lpatchsrv = "";
			
			Controller.AddDebugMessage("checking for updates...");

				
			using (StreamReader upstreamVersionStreamReader = new StreamReader(wc.OpenRead(GuiController.PATCHURL + "lpatch.cfg"))) {

	        	lpatchsrv = upstreamVersionStreamReader.ReadToEnd();
					
			}
		

			lpatchsrv = lpatchsrv.Trim();

			if (lpatchsrv == "") {
				return false;
			}

			ProgramVersion ThisVersion = new ProgramVersion(Controller.GetProgramVersion());
			ProgramVersion ServerVersion = new ProgramVersion(lpatchsrv);
			
			Controller.AddDebugMessage("Local Launcher Version" + Controller.GetProgramVersion() + "|" + ThisVersion.ToCompactString() + " |" + ThisVersion.ToString());
			Controller.AddDebugMessage("Server Launcher Version" + lpatchsrv + "|" + ServerVersion.ToCompactString() + " |" + ServerVersion.ToString());
			
	        if (ThisVersion.IsNewerThan(ServerVersion)) {
            	
            	return false;
            	
            }
			
			return true;
			
		}
		
		
		//exception handling should be done in the calling method.
		public bool DownloadInstaller(String savepath, String URL, String filepath) {
			
			String checksum = "";
			
			String DL = URL + "/" + filepath;
			String ChksumDL = URL + "/" + filepath + ".md5";
			String Local = savepath + @"\" + filepath;
			String LocalTmp =  Local + ".part";

			using (StreamReader upstreamVersionStreamReader = new StreamReader(wc.OpenRead(ChksumDL))) {
				checksum = Regex.Replace(upstreamVersionStreamReader.ReadToEnd(),"\n","");
			}
			
			bool IsGood = false;

			wc.DownloadFile(DL, LocalTmp);
			
			IsGood = SWGFile.MatchChecksum(LocalTmp, checksum);
			
			if (IsGood) {
				if (File.Exists(Local)) {
					File.Delete(Local);
				}				
				File.Move(LocalTmp,Local);
			}
					
			return IsGood;
			
		}		

		
		
		
	}
}
