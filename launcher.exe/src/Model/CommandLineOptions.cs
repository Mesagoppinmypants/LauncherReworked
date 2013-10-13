/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 29.07.2013
 * Time: 13:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PswgLauncher.Model
{
	/// <summary>
	/// Description of CommandLineOptions.
	/// </summary>
	public class CommandLineOptions
	{
		
		public string DNSFileRecord {
			get; private set;			
		}
		
		public string DNSFileRecordContents {
			get; private set;			
		}

		public string DNSPatchRecord {
			get; private set;			
		}
		
		public string DNSPatchRecordContents {
			get; private set;			
		}
		
		public string FileListName {
			get; private set;
		}
		
		public string WorkdirSetting {
			get; private set;
		}
		
		public int RunAsSetting {
			get; private set;
		}
		
		public String VersionOverride {
			get; private set;
		}
		
		public CommandLineOptions()
		{
			RunAsSetting = -1;
		}
		
		//FIXME: provide some sort of class implementation, maybe.
		//FIXME: produce somewhat more feedback.
		//FIXME: Fail on errors
		public bool Parse(String[] args) {
			StringBuilder sb = new StringBuilder();
			
			for (int i = 0; i< args.Length; i++) {
				//Debug.WriteLine(args[i].ToLower());
				switch(args[i].ToLower()) {
					case "--dnsfilerecord":
						if (i+1 >= args.Length) {
							ShowError("No DNS record given with --dnsfilerecord");
							return false;
						}
						if (!Regex.IsMatch(args[i+1], @"[-\.A-Za-z0-9]+")) {
							ShowError("dnsfilerecord wrong format.");
							return false;
						}
						DNSFileRecord = args[i+1];
						i++;
						break;
					case "--dnsfilerecordcontent":
						if (i+1 >= args.Length) {
							ShowError("No TXT record given with --dnsfilerecordcontent");
							return false;
						}
						if (!Regex.IsMatch(args[i+1], @"[-\s\.A-Za-z0-9]+")) {
							ShowError("dnsfilerecord wrong format.");
							return false;
						}
						DNSFileRecordContents = args[i+1];
						i++;
						break;						
					case "--dnspatchrecord":
						if (i+1 >= args.Length) {
							ShowError("No DNS record given with --dnspatchrecord");
							return false;
						}
						if (!Regex.IsMatch(args[i+1], @"[-\.A-Za-z0-9]+")) {
							ShowError("dnspatchrecord wrong format.");
							return false;
						}
						DNSPatchRecord = args[i+1];
						i++;
						break;
					case "--dnspatchrecordcontent":
						if (i+1 >= args.Length) {
							ShowError("No TXT record given with --dnspatchrecordcontent");
							return false;
						}
						if (!Regex.IsMatch(args[i+1], @"[-\s\.A-Za-z0-9]+")) {
							ShowError("dnspatchrecord wrong format.");
							return false;
						}
						DNSPatchRecordContents = args[i+1];
						i++;
						break;
					case "--filelist":
						if (i+1 >= args.Length) {
							ShowError("No filelist name given with --runas");
							return false;							
						}
						if (!Regex.IsMatch(args[i+1], @"[-\.A-Za-z0-9]+")) {
							ShowError("filelist wrong format.");
							return false;
						}
						FileListName = args[i+1];
						i++;
						break;						
					case "--runas":
						if (i+1 >= args.Length) {
							ShowError("No Username given with --runas");
							return false;
						}
						int x;
						if (Int32.TryParse(args[i+1], out x)) {
							RunAsSetting = x;
							i++;
						}
						break;
					case "--versionoverride":
						if (i+1 >= args.Length) {
							ShowError("No Version given to --versionoverride");
							return false;
						}
						if (!Regex.IsMatch(args[i+1], @"[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+")) {
							ShowError("Override Version wrong format.");
							return false;
						}
						VersionOverride = args[i+1];
						i++;
						break;
					case "--workdir":
						if (i+1 >= args.Length) {
							ShowError("No Workdir given to --workdir");
							return false;
						}
						if (!Directory.Exists(args[i+1])) {
							ShowError("Workdir override doesn't exist!");
							return false;
						}
						
						WorkdirSetting = args[i+1];
						i++;
						break;
					default:
						ShowError("Unknown parameter " + args[i]);
						return false;
						break;
				}
				
				
			}

			return true;
			
		}
		
		
		
				
		void ShowError(string err)
		{
			MessageBox.Show(err + "\n\nExiting.", "Command line argument error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		
		
		
				
	}
}
