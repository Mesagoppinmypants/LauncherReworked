/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 17.03.2013
 * Time: 14:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;

namespace PswgLauncher
{
	/// <summary>
	/// Description of StatusProcessor.
	/// </summary>
	public class StatusProcessor
	{


		
		/* 
		 * FIXME: the mess was moved from launcher2 to this class.
		 * The data should probably get a proper object representation.
		 * 
		 */
		
        public enum StatusCodes :int {
        	
			NoStatus = 0,
        	NoChecksum = 1,
        	UpdatingChecksum = 2,
        	ChecksumFailed = 3,
        	ChecksumOK = 4,
        	Scanning = 5,
        	ScanningManual = 6,
        	ScanningFailed = 7,
        	ScanningOK = 8,
        	Patching = 9,
        	PatchingFailed = 10,
        	PatchingOK = 11
        	
        }
		
		
		
		private String[] PlayTexts = new String[] {
			
			"Get Checksums",
			"Get Checksums",
			"Getting Checksums",
			"Retry Checksums",
			"Scan",
			"Scanning",
			"Scanning",
			"Retry Scan",
			"Patch",
			"Patching",
			"Retry Patch",
			"Play"
			
		};
		
		private String[] LabelTexts = new String[] {
			
			"Checksums need Checking",
			"Checksums need Checking",
			"DL'ing Checksums",
			"Checksum DL failed.",
			"Checksums loaded.",
			"Scanning",
			"Scanning",
			"Scanning Failed",
			"Scanning complete!",
			"Patching",
			"Patching Failed",
			"Ready to play!"
			
		};
		
		
		private GuiController Controller;
		
		private int _status;

		public int Status {
			get {
				return _status;
			}
			set {
				if (!IsValidChange(value)) {
					return;
				}
				
				_status = value;
				
			}
		}
		
		public StatusProcessor(GuiController ctrl)
		{
			_status = (int) StatusCodes.NoStatus;
			this.Controller = ctrl;
		}
		
		public bool SetNewState(int newstatus) {
			
			if (!IsValidChange(newstatus)) {
				return false;
			}
			
			Status = newstatus;
			
			return true;
		}
		
		
		public bool IsValidChange(int newstatus) {

        	if (newstatus < (int) StatusCodes.NoStatus || newstatus > (int) StatusCodes.PatchingOK) {
        		return false;
        	}
			
			if (Array.IndexOf(GetValidTargetStatus(_status),newstatus) >= 0 ) {
				return true;
			}
			
			return false;
			
		}
		
		public int[] GetValidTargetStatus(int currentstatus) {
			
			switch (currentstatus) {
				case (int) StatusCodes.NoStatus:
					return new int[] { (int) StatusCodes.NoChecksum };
					break;
	        	case (int) StatusCodes.NoChecksum:
					return new int[] { (int) StatusCodes.UpdatingChecksum };
					break;
	        	case (int) StatusCodes.UpdatingChecksum:
					return new int[] { (int) StatusCodes.ChecksumOK, (int) StatusCodes.ChecksumFailed };
					break;
	        	case (int) StatusCodes.ChecksumFailed:
					return new int[] { (int) StatusCodes.UpdatingChecksum };
					break;
	        	case (int) StatusCodes.ChecksumOK:
					return new int[] { (int) StatusCodes.Scanning, (int) StatusCodes.ScanningManual };
					break;
	        	case (int) StatusCodes.Scanning:
					return new int[] { (int) StatusCodes.ScanningOK, (int) StatusCodes.ScanningFailed };
					break;
	        	case (int) StatusCodes.ScanningManual:
					return new int[] { (int) StatusCodes.ScanningOK, (int) StatusCodes.ScanningFailed };
					break;					
	        	case (int) StatusCodes.ScanningFailed:
					return new int[] { (int) StatusCodes.Scanning, (int) StatusCodes.ScanningManual };
					break;
	        	case (int) StatusCodes.ScanningOK:
					return new int[] { (int) StatusCodes.Patching };
					break;
	        	case (int) StatusCodes.Patching:
					return new int[] { (int) StatusCodes.PatchingOK, (int) StatusCodes.PatchingFailed };		
					break;
	        	case (int) StatusCodes.PatchingFailed:
					return new int[] { (int) StatusCodes.Patching, (int) StatusCodes.Scanning, (int) StatusCodes.ScanningManual };
					break;
	        	case (int) StatusCodes.PatchingOK:
					return new int[] { (int) StatusCodes.Scanning, (int) StatusCodes.ScanningManual };
					break;				
			}
			
			return new int[] { };
			
		}
		
		public int GetNextStatus() {

			switch (_status) {
				case (int) StatusCodes.NoStatus:
					return (int) StatusCodes.NoChecksum;
					break;
	        	case (int) StatusCodes.NoChecksum:
					return (int) StatusCodes.UpdatingChecksum;
					break;
	        	case (int) StatusCodes.ChecksumFailed:
					return (int) StatusCodes.UpdatingChecksum;
					break;
	        	case (int) StatusCodes.ChecksumOK:
					return (int) StatusCodes.Scanning;
					break;
	        	case (int) StatusCodes.ScanningFailed:
					return (int) StatusCodes.Scanning;
					break;
	        	case (int) StatusCodes.ScanningOK:
					return (int) StatusCodes.Patching;
					break;
	        	case (int) StatusCodes.PatchingFailed:
					return (int) StatusCodes.Patching;
					break;
			}
			
			return -1;
			
		}
		
		public bool GetPlayDisabled() {
			
			switch(_status) {
					
				case (int) StatusCodes.UpdatingChecksum:
				case (int) StatusCodes.Scanning:
				case (int) StatusCodes.ScanningManual:
				case (int) StatusProcessor.StatusCodes.Patching:
					return true;
					break;
			}
			
			return false;
			
		}
		
		
		public bool GetScanDisabled() {
			
			switch(_status) {

				case (int) StatusCodes.UpdatingChecksum:
				case (int) StatusCodes.ChecksumFailed:
				case (int) StatusCodes.Scanning:
				case (int) StatusCodes.ScanningManual:
				case (int) StatusProcessor.StatusCodes.Patching:
					return true;
					break;
			}
			
			return false;
		}
		
		
		public bool IsBusy() {

			switch(_status) {
					
				case (int) StatusCodes.UpdatingChecksum:
				case (int) StatusCodes.Scanning:
				case (int) StatusCodes.ScanningManual:
				case (int) StatusProcessor.StatusCodes.Patching:
					return true;
					break;
			}
			
			return false;			
		}
		
		public String GetPlayText() {
			
			return PlayTexts[_status];
			
		}
		
		public String GetLabelText() {
			return LabelTexts[_status] + "(" + _status + ")";
		}
		
		
		public Color GetStatusColor() {
			
			switch(_status) {
					
				case (int) StatusCodes.ChecksumFailed:
				case (int) StatusCodes.ScanningFailed:
				case (int) StatusCodes.PatchingFailed:
					return Color.Red;
					break;
				
				case (int) StatusCodes.PatchingOK:
					return Color.Aqua;
					break;
					
					
			}
			
			return Color.Blue;
			
		}
		
	}
	
}
