/*
 * Created by SharpDevelop.
 * User: rdo
 * Date: 09.01.2013
 * Time: 21:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace PswgLauncher
{
	/// <summary>
	/// Description of LauncherProgressBar.
	/// </summary>
	public class LauncherProgressBar : ProgressBar
	{
		
		private Color _textcolor;
		
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Appearance")]
		[Description("Text to display in center")]
		public override String Text {
			
			get { return base.Text; }
			set {
				
				base.Text = value;
				
			}
		}

		[Category("Appearance")]
		[Description("Color for Text")]
		public Color TextColor {
			get { return _textcolor; }
			set {
				_textcolor = value;
				TriggerChange();
			}
		}

		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]		
		[Category("Appearance")]
		[Description("Font for text")]
		public override Font Font {
			get { return base.Font; }
			set {
				base.Font = value;
			}
		}

		
		
		
		
		protected void TriggerChange() {
			
			Invalidate();
		}
		
		
		
		public LauncherProgressBar()
		{
			
			// Modify the ControlStyles flags
        	//http://msdn.microsoft.com/en-us/library/system.windows.forms.controlstyles.aspx
        	this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true); 
		}
		
		/*
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Refresh();
		}
		
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			Refresh();
		}*/
				

		
		protected override void OnPaint(PaintEventArgs pe) {
			
			base.OnPaint(pe);
			Graphics g = pe.Graphics;
			
			// draw background
			if (Application.RenderWithVisualStyles) {	
				ProgressBarRenderer.DrawHorizontalBar(g,this.ClientRectangle);
			}
			
	        if ( this.Value > 0 )
	        {
	        	Rectangle clip = new Rectangle( 0+2, 0+2, ( int )Math.Round( ( ( float )this.Value / this.Maximum ) * (Width-4) ), (Height-4) );
	            if (Application.RenderWithVisualStyles) {	
	            	ProgressBarRenderer.DrawHorizontalChunks(pe.Graphics, clip);
	            } else {
	            	SolidBrush brush = new SolidBrush(this.ForeColor);
	            	g.FillRectangle(brush, clip);
	            }
	        }			

			DrawText(pe);
			
		}

		

			

			
		
		
	

		
		private void DrawText(PaintEventArgs pe) {
			
			Graphics g = pe.Graphics;
			
			if (base.Text == null || base.Text.Trim() == "" || pe == null || base.Font == null || _textcolor == null) {
				return;
			}
			
			
			
			SolidBrush Brush = new SolidBrush(_textcolor);
			SizeF StringSize = g.MeasureString(base.Text, base.Font);
			PointF DrawPoint;
			

			DrawPoint = new PointF(base.Width /2 - StringSize.Width /2, base.Height/2 - StringSize.Height /2);
			
			
			g.DrawString(base.Text, base.Font, Brush, DrawPoint);		
		}
		
		
	}
}
