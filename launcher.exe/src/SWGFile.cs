/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 19.02.2013
 * Time: 19:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace PswgLauncher
{
	/// <summary>
	/// Description of SWGFile.
	/// </summary>
	public class SWGFile
	{
		public String Filename {
			get; private set; 
		}
		
		public bool Strict {
			get; private set;
		}
		
		public String Checksum {
			get; private set;
		}
		
		public int Filesize {
			get; private set;
		}
		
		public bool Exists {
			get; private set;
		}
		
		public bool SizeMatched {
			get; private set;
		}
		
		public bool CheckSummed {
			get; private set;
		}
		
		public bool IsGood {
			get; private set;
		}
		
		private GuiController Controller;
		
		public SWGFile(String File, bool IsStrict, String MDFiveSum, int Size, GuiController ctrl)
		{
			Filename = File;
			Strict = IsStrict;
			Checksum = MDFiveSum;
			Filesize = Size;
			
			IsGood = false;
			Exists = false;
			SizeMatched = false;
			CheckSummed = false;
			
			this.Controller = ctrl;
			
		}
		
		public override String ToString() {
			return ((Strict) ? "1" : "0") + " " + Checksum + " " + Filesize.ToString() + " " + Filename;
		}


		public void Reset() {
			IsGood = false;
			Exists = false; 
			CheckSummed = false;
			SizeMatched = false;
		}
		
		

		
		
		
		public bool FileExists(String PathPrefix) {
			return File.Exists(PathPrefix + @"\" + Filename);
		}
		
		public bool SwgDirHasFile() {
			return FileExists(Controller.SwgDir);
		}
		
		public bool SavePathHasFile() {
			return FileExists(Controller.SwgSavePath);
		}
		
		public bool AppPathHasFile() {
			return FileExists(GuiController.AppPath);
		}
		
		public bool SetExists() {
			Exists = SavePathHasFile();
			return Exists;
		}
		
		public void SetGood() {
			IsGood = true;
		}
		

		public bool FileMatchesSize(String PathPrefix) {
			
			if (!FileExists(PathPrefix)) {
				return false;
			}
			
			String __filename = PathPrefix + @"\" + Filename;
			FileInfo fi = new FileInfo(__filename);
			
			return (fi.Length == this.Filesize);
			
		}
		
		public bool SwgDirMatchesSize() {
			return FileMatchesSize(Controller.SwgDir);
		}
		
		public bool SavePathMatchesSize() {
			
			if (!Strict) {
				return FileExists(Controller.SwgSavePath);
			}
			
			return FileMatchesSize(Controller.SwgSavePath);
		}

		public bool AppPathMatchesSize() {
			return FileMatchesSize(GuiController.AppPath);
		}
		
		
		public bool SetSizeMatch() {
			Exists = SavePathHasFile();
			SizeMatched = SavePathMatchesSize();
			return SizeMatched;
		}


        public bool MatchChecksum(string PathPrefix) {
        	
			if (!FileExists(PathPrefix)) {
				return false;
			}
			
			// skip the expensive scan if sizes don't match.
			if (!FileMatchesSize(PathPrefix)) {
				return false;
			}
			
			String Filepath = PathPrefix + @"\" + Filename;

			System.IO.FileStream FileCheck = System.IO.File.OpenRead(Filepath);                
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] md5Hash = md5.ComputeHash(FileCheck);                
			FileCheck.Close();
			                
			string Calc =   BitConverter.ToString(md5Hash).Replace("-", "").ToLower();
			
			if (Calc == Checksum.ToLower()) {
				return true;
			}
			
			return false;
		}
		
		public bool SwgDirMatchesChecksum() {
			return MatchChecksum(Controller.SwgDir);
		}
		
		
		public bool SavePathMatchesChecksum() {

			if (!Strict) {
				return FileExists(Controller.SwgSavePath);
			}
			
			return MatchChecksum(Controller.SwgSavePath);
		}
		
		// this is specifically for verifying a copy, even if file is marked non-Strict
		public bool Verify() {
			return MatchChecksum(Controller.SwgSavePath);
		}

		public bool AppPathMatchesChecksum() {
			return MatchChecksum(GuiController.AppPath);
		}
		
		//FIXME: this is a bit dodgy. for a checksum check, file existence is checked three times.
		public bool SetChecksumMatch() {
			Exists = SavePathHasFile();
			SizeMatched = SavePathMatchesSize();
			CheckSummed = SavePathMatchesChecksum();
			return CheckSummed;
		}
		
		
		
	}
}
