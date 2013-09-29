/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 26.09.2013
 * Time: 11:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PswgLauncher.Util
{
	/// <summary>
	/// Description of ProgramConstants.
	/// </summary>
	public class ProgramConstants
	{
		private ProgramConstants()
		{
		}
		
		public static string LookupDomain = "projectswg.com";
		
		public static string FileRecord = "lsrv." + LookupDomain;
		public static string LoginRecord = "login." + LookupDomain;
		
		public static string LoginServer = "login1.projectswg.com";
		public static string WebServer = "www.projectswg.com";
		
		public static string PatchInstallerFile = "PSWGInstaller.exe";
		public static string PatchInstallerChecksum = "PSWGInstaller.exe.md5";
		//public static string MAINURL = "http://"+PatchServer+"/files/";
		public static string PatchInstallerUrl = "/launcher/" + PatchInstallerFile;
		public static string PatchInstallerChecksumUrl = "/launcher/" + PatchInstallerChecksum;
		public static string PatchInstallerVersionUrl = "/launcher/lpatchinst.cfg";

		public static string UPDATENOTES = "http://www.projectswg.com/update_notes.php";
		
		public static string HttpAuthUser = "pswglaunch";
		public static string HttpAuthPass = "wvQAxc5mGgF0";
		
		public static string EncKey = "eKgeg75J3pTBURgh";

		
	}
}
