using System;
using sd = System.Drawing;
using Eto.Drawing;
using Eto.Forms;
using Eto.Platform.Mac.Drawing;
using MonoMac.AppKit;
using MonoMac.Foundation;
using MonoMac;
using MonoMac.ObjCRuntime;

namespace Eto.Platform.Mac.Forms.Controls
{
	public class DrawableHandler : MacView<DrawableHandler.EtoDrawableView, Drawable>, IDrawable
	{
		Brush backgroundBrush;
		Color backgroundColor;

		public class EtoDrawableView : MacEventView
		{
			Drawable Drawable
			{
				get { return Widget as Drawable; }
			}
			
			public override void DrawRect (sd.RectangleF dirtyRect)
			{
				if (Widget == null)
					return;
				dirtyRect.Y = this.Frame.Height - dirtyRect.Y - dirtyRect.Height;
				if (dirtyRect.X % 1.0f > 0f)
					dirtyRect.Width += 1;
				if (dirtyRect.Y % 1.0f > 0f)
					dirtyRect.Height += 1;
				Drawable.Update (Rectangle.Ceiling (Eto.Platform.Conversions.ToEto (dirtyRect)));
			}
			
			public bool CanFocus { get; set; }

			public override bool AcceptsFirstResponder ()
			{
				return CanFocus;
			}

			public override bool AcceptsFirstMouse (NSEvent theEvent)
			{
				return CanFocus;
			}
		}
	
		public override bool Enabled { get; set; }
		
		public override Color BackgroundColor
		{
			get { return backgroundColor; }
			set 
			{
				backgroundColor = value;
				if (backgroundColor.A > 0)
					backgroundBrush = new SolidBrush (backgroundColor, Widget.Generator);
				else
					backgroundBrush = null;
			}
		}
		
		public void Create ()
		{
			Enabled = true;
			Control = new EtoDrawableView{ Handler = this };
		}
		
		public bool CanFocus
		{
			get { return Control.CanFocus; }
			set { Control.CanFocus = value; }
		}
		
		public void Update (Rectangle rect)
		{
			var context = NSGraphicsContext.CurrentContext;
			if (context != null) {
				var handler = new GraphicsHandler (context, Control.Frame.Height, Control.IsFlipped);
				var graphics = new Graphics (Widget.Generator, handler);
				if (backgroundBrush != null) {
					graphics.FillRectangle (backgroundBrush, rect);
				}
				var convertedBounds = Control.ConvertRectToView(Control.Bounds, null);
				handler.Control.SetPatternPhase (new sd.SizeF(convertedBounds.Left, convertedBounds.Bottom));

				Widget.OnPaint (new PaintEventArgs (graphics, rect));
			}
		}
		

	}
}
