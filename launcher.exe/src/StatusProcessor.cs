/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 17.03.2013
 * Time: 14:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PswgLauncher
{
	/// <summary>
	/// Description of StatusProcessor.
	/// </summary>
	public class StatusProcessor
	{
		
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
		
		
	}
}
