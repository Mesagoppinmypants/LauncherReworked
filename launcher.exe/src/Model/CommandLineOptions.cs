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
using System.Text;

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
		
		public String Arguments {
			get; private set;
		}
		
		public CommandLineOptions()
		{
			RunAsSetting = -1;
		}
		
		//FIXME: produce somewhat more feedback.
		public void Parse(String[] args) {
			StringBuilder sb = new StringBuilder();
			
			for (int i = 0; i< args.Length; i++) {
				//Debug.WriteLine(args[i].ToLower());
				switch(args[i].ToLower()) {
					case "--workdir":
						if (i+1 >= args.Length) {
							return;
						}
						WorkdirSetting = args[i+1];
						sb.Append(args[i] + " " + args[i+1]);
						i++;
						break;
					case "--runas":
						if (i+1 >= args.Length) {
							return;
						}
						int x;
						if (Int32.TryParse(args[i+1], out x)) {
							RunAsSetting = x;
							sb.Append(args[i] + " " + args[i+1]);
							i++;
						}
						break;
				}
				
				Arguments = sb.ToString();
				
			}
			
		}
				
	}
}
