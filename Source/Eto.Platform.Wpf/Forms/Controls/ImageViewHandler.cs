using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using swc = System.Windows.Controls;
using swm = System.Windows.Media;
using Eto.Forms;
using Eto.Drawing;
using Eto.Platform.Wpf.Drawing;

namespace Eto.Platform.Wpf.Forms.Controls
{
	public class ImageViewHandler : WpfFrameworkElement<swc.Image, ImageView>, IImageView
	{
		Image image;

		public override Color BackgroundColor
		{
			get
			{
				return Colors.Transparent;
			}
			set
			{
				
			}
		}


		public ImageViewHandler ()
		{
			Control = new Eto.Platform.Wpf.CustomControls.MultiSizeImage {
				Stretch = swm.Stretch.Uniform,
				StretchDirection = swc.StretchDirection.Both
			};
		}

		public override Size Size
		{
			get { return base.Size;	}
			set { base.Size = value; }
		}

		public Image Image
		{
			get { return image; }
			set
			{
				image = value;
				if (image != null)
					Control.Source = image.ControlObject as swm.ImageSource;
				else
					Control.Source = null;
			}
		}
	}
}
