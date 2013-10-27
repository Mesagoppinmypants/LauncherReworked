/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 26.09.2013
 * Time: 18:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace PswgLauncher.Util
{
	/// <summary>
	/// Description of Downloader.
	/// </summary>
	public class Downloader
	{
		private Downloader()
		{
		}
		
		
		public static void HTTPDownload(GuiController Controller, String filename, String remoteURL, long offset, bool resume, BackgroundWorker backgroundWorker) {

          	bool append = false;
          	FileMode mode;
          
          	String filenameshort = filename.Split(Path.DirectorySeparatorChar).Last().ToLower(); 
          	
      		if (Controller.ResumeOption && resume && offset > 0) {
            
          		backgroundWorker.ReportProgress(0 , "Debug " + "Resume from " + offset);
        		append = true;
        		mode = FileMode.Append;
        
      		} else {
        		mode = FileMode.Create;
        		offset = 0;
      		}

			using ( FileStream OutputStream = new FileStream(filename, mode) ) {
				HTTPDownload(Controller, OutputStream, filenameshort, remoteURL, offset, backgroundWorker);
			}
		}
		
		public static void HTTPDownload(GuiController Controller, Stream OutputStream, String filenameshort, String remoteURL, long offset, BackgroundWorker backgroundWorker) {
          
			if (backgroundWorker.CancellationPending) { return; }
            
            HttpWebRequest WebReq = (HttpWebRequest) HttpWebRequest.Create(new Uri(remoteURL));

            if ( offset > 0 ) {
            	WebReq.AddRange(offset);
          	}
            
        	WebReq.Credentials = Controller.GetNetworkCredential();
            
            using (HttpWebResponse response = (HttpWebResponse) WebReq.GetResponse()) {
            	using (Stream webStream = response.GetResponseStream()) {
            
            		long length = response.ContentLength;
			        int bufsiz = 2048;
			        byte[] buffer = new byte[bufsiz];
			                        
			        int readcount = webStream.Read(buffer, 0, bufsiz);
			        long totalread = offset+readcount;
			            
			        DateTime lastupdate = DateTime.Now.ToLocalTime();
			        DateTime thisupdate;
			        float displayprogress = 0;
			            
			        while (readcount > 0 && !backgroundWorker.CancellationPending) {
              
			        	thisupdate = DateTime.Now.ToLocalTime();
              			//backgroundWorker.ReportProgress(progress, "Debug " + thisupdate.Subtract(lastupdate).TotalSeconds);
              			if (thisupdate.Subtract(lastupdate).TotalSeconds > 1) {
                			backgroundWorker.ReportProgress((int) displayprogress, "DL|" + filenameshort + " " + totalread + "/"+length);
                			lastupdate = thisupdate;
              			}
              
              			OutputStream.Write(buffer,0,readcount);
              
              			readcount = webStream.Read(buffer, 0, bufsiz);
              			totalread +=readcount;
              			
                		displayprogress = totalread *100 / length;
              			
                		//System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                		//Debug.WriteLine( enc.GetString(buffer) );
          			}
        		}
        	
			}
        
		}
		
		
	}
	
}
