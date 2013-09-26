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
		
		public string WorkdirSetting {
			get; private set;
		}
		
		public int RunAsSetting {
			get; private set;
		}
		
		public String VersionOverride {
			get; private set;
		}
		
		public String Arguments {
			get; private set;
		}
		
		public CommandLineOptions()
		{
			RunAsSetting = -1;
		}
		
		//FIXME: produce somewhat more feedback.
		//FIXME: Fail on errors
		public bool Parse(String[] args) {
			StringBuilder sb = new StringBuilder();
			
			for (int i = 0; i< args.Length; i++) {
				//Debug.WriteLine(args[i].ToLower());
				switch(args[i].ToLower()) {
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
						sb.Append(args[i] + " " + args[i+1]);
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
							sb.Append(args[i] + " " + args[i+1]);
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
						sb.Append(args[i] + " " + args[i+1]);
						VersionOverride = args[i+1];
						i++;
						break;
				}
				
				
			}

			Arguments = sb.ToString();
			return true;
			
		}
		
		void ShowError(string err)
		{
			MessageBox.Show(err + "\n\nExiting.", "Command line argument error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
				
	}
}
