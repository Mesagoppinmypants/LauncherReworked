/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 22.12.2012
 * Time: 18:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace PswgLauncher
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class SWGFileList
	{
		
		private GuiController Controller;

		private Dictionary<String,SWGFile> _swgfiletable;
		public Dictionary<String,SWGFile> SwgFileTable {
			get {
				return _swgfiletable;
			}
		}
		
		private int _timestamp;
		public int FileTableTimestamp {
			get {
				return _timestamp;
			}
		}
		
		private Dictionary<String,String> GotFiles;
		
		private bool _hasFileList = false;
		public bool HasFileList {
			get {
				return _hasFileList;
			}
		}
		
		
		
		public SWGFileList(GuiController gc)
		{
			
			Controller = gc;
			
			_swgfiletable = new Dictionary<String, SWGFile>();
			GotFiles = new Dictionary<String, String>();
			
			
			
		}
		
		
		
		//FIXME: I think the next 5 methods can be simplified...
		private String ReadMasterChecksum(StreamReader SR) {
			
			String line;
			
			Regex r1 = new Regex(@"^([0-9a-fA-F]{32})\s+(-)$");
			
			// bug out if we can't read a line...
			if ( ( line=SR.ReadLine() ) ==null) {
				return null;
			}
			Match m1 = r1.Match(line);
			//bug out if first line doesn't hold the whole things checksum...
			if (!m1.Success) {
				return null;
			}
			
			return m1.Groups[1].Value;
			
		}
		
		private int RetrieveLineNumber(StreamReader SR) {
			String line;

			if ( ( line=SR.ReadLine()) == null) {
				return -1;
			}
			
			if (!System.Text.RegularExpressions.Regex.IsMatch(line, @"^[0-9]+$")) {
				return -1;
			}
			
			return int.Parse(line);
			
		}
		
		private int RetrieveTimestamp(StreamReader SR) {
			String line;

			if ( ( line=SR.ReadLine()) == null) {
				return -1;
			}
			
			if (!System.Text.RegularExpressions.Regex.IsMatch(line, @"^[0-9]{10,}$")) {
				return -1;
			}
			
			return int.Parse(line);
			
		}
		
		private bool HasBegin(StreamReader SR) {
			
			String line;
			
			//next line should be "BEGIN"
			if ( ( line=SR.ReadLine()) == null) {
				return false;
			}
			
			if (!line.Equals("BEGIN")) {
				return false;
			}
			
			return true;
		}
		
		private bool IsEnd(String line) {
			
			
			//next line should be "END"
			if ( line == null) {
				return false;
			}
			
			if (!line.Equals("END")) {
				return false;
			}
			
			return true;
		}
		
		public Dictionary<String,SWGFile> LoopChecksums(StreamReader SR) {
			
			String line;
			List<String> text = new List<String>();
			Dictionary<String,SWGFile> NewChecksums = new Dictionary<String,SWGFile>();
			
			Regex regex = new Regex(@"^([0-9]+)\s+([0-9a-fA-F]{32})\s+([0-9]+)\s+(\S+)$");
			
			while ((line = SR.ReadLine()) != null) {
        			Match match = regex.Match(line);
        			
        			if (match.Success) {
        				//Debug.WriteLine("Found " + match.Groups[2].Value + ":" + match.Groups[1].Value);
        				NewChecksums.Add(match.Groups[4].Value, new SWGFile(match.Groups[4].Value, (! match.Groups[1].Value.Equals("0") ) , match.Groups[2].Value, int.Parse(match.Groups[3].Value)) );
        				
        				text.Add(line) ;
        				continue;
        			}
        			
        			if (IsEnd(line)) {
        				break;
        			} 
        		
        			Controller.AddDebugMessage("malformed Checksum list." + line);
        			return null;
        			
        	}
			
			return NewChecksums;
			
		}
		

		public void CreateFileList(StreamReader SR, bool isDownload) {
			
			String Checksum;
			int LineNumber;
			int MasterTimestamp;
			if ( (LineNumber = RetrieveLineNumber(SR)) <= 0 )  { return; }
			if ( (MasterTimestamp = RetrieveTimestamp(SR)) <= 0)  { return;}
			
			if (this.HasFileList && this._timestamp != null && this._timestamp >= MasterTimestamp) {
				Controller.AddDebugMessage("new list timestamp less or equal, ignoring new list. " + _timestamp + " vs " + MasterTimestamp);
				return;
			}
			
			if (!HasBegin(SR)) { return; }

			Dictionary<String,SWGFile> NewDictionary = LoopChecksums(SR);
			
			if (NewDictionary == null)  {
				Controller.AddDebugMessage("Couldn't read file list from disk."  + ((isDownload) ? "server" : "disk") );
				return;
			}
			
			if (NewDictionary.Count != LineNumber) {
				Controller.AddDebugMessage("Line Number mismatch" );
				return;				
			}

			this._swgfiletable = NewDictionary;
			this._timestamp = MasterTimestamp;
			this._hasFileList = true;
			Controller.AddDebugMessage("Successfully read file list from " + ((isDownload) ? "server" : "disk"));
			
		}
		
		public bool WriteConfig(String filename) {
			if (!this._hasFileList) {
				Controller.AddDebugMessage("There's no file list here.");
				return false;
			}
			
			try {
				using (StreamWriter file = new StreamWriter(filename)) {
				
					file.WriteLine(this._swgfiletable.Count.ToString());
					file.WriteLine(this._timestamp.ToString());
					file.WriteLine("BEGIN");
					
					foreach (KeyValuePair<String,SWGFile> kv in this._swgfiletable) {
						file.WriteLine(kv.Value.ToString());
					}
					file.WriteLine("END");
				} 
			} catch (Exception e) {
				Controller.AddDebugMessage("Something went wrong writing file list.");
				return false;
			}
			
			return true;
		}
		
		
		public void AddGoodFile(String filename) {
			if (filename != null) {
				//meh...
				
				this.GotFiles.Add(filename.ToLower(), "1");
			}
		}
		
		public bool isGood(String filename) {
			if (filename != null) {
				
				return GotFiles.ContainsKey(filename.ToLower());
			}
			return false;
		}
		
		public void ResetGoodFiles() {
			GotFiles = new Dictionary<String, String>();
		}
		
		//FIXME: move to utility class...
		static byte[] GetBytes(String str)
		{
			
		    byte[] bytes = new byte[str.Length * sizeof(char)];
    		System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
    		
    		return bytes;

		}
		
		

		
	
	}
	

	
}
