/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 19.02.2013
 * Time: 19:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PswgLauncher
{
	/// <summary>
	/// Description of SWGFile.
	/// </summary>
	public class SWGFile
	{
		
		public bool Strict {
			get; set;
		}
		
		public String Checksum {
			get; set;
		}
		
		public int Filesize {
			get; set;
		}
		
		public SWGFile(bool IsStrict, String MDFiveSum, int Size)
		{
			Strict = IsStrict;
			Checksum = MDFiveSum;
			Filesize = Size;
		}
		
		public override String ToString() {
			return ((Strict) ? "1" : "0") + " " + Checksum + " " + Filesize.ToString();
		}
	}
}
