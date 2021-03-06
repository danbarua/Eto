using System;
using Eto.Forms;
using Eto.Platform.GtkSharp.Drawing;
using Eto.Drawing;

namespace Eto.Platform.GtkSharp
{
	public class ImageViewHandler : GtkControl<Gtk.DrawingArea, ImageView>, IImageView
	{
		Image image;
		bool widthSet;
		bool heightSet;

		public override Gtk.DrawingArea CreateControl ()
		{
			var control = new Gtk.DrawingArea {
				CanFocus = false,
				CanDefault = true
			};
			control.ExposeEvent += control_ExposeEvent;
			control.Events |= Gdk.EventMask.ExposureMask;
			return control;
		}

		void control_ExposeEvent (object o, Gtk.ExposeEventArgs args)
		{
			Gdk.EventExpose ev = args.Event;
			using (var graphics = new Graphics (Widget.Generator, new GraphicsHandler (Control, ev.Window))) {
				var widgetSize = new Size(Control.Allocation.Width, Control.Allocation.Height);
				var imageSize = (SizeF)image.Size;
				var scaleWidth = widgetSize.Width / imageSize.Width;
				var scaleHeight = widgetSize.Height / imageSize.Height;
				imageSize *= Math.Min (scaleWidth, scaleHeight);
				var location = new PointF((widgetSize.Width - imageSize.Width) / 2, (widgetSize.Height - imageSize.Height) / 2);

				var destRect = new Rectangle(Point.Round(location), Size.Truncate (imageSize));
				graphics.DrawImage (image, destRect);
			}
		}

		public override Eto.Drawing.Size Size {
			get {
				return base.Size;
			}
			set {
				base.Size = value;
				widthSet = value.Width >= 0;
				heightSet = value.Height >= 0;
			}
		}
		
		public Image Image {
			get { return image; }
			set {
				image = value;
				if (image != null && !widthSet || !heightSet) {
					Control.SetSizeRequest (widthSet ? Size.Width : image.Size.Width, heightSet ? Size.Height : image.Size.Height);
				}
				if (Control.Visible)
					Control.QueueDraw ();
			}
		}
	}
}

