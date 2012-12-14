using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;
using swm = System.Windows.Media;

namespace Eto.Platform.Wpf.Drawing
{
	public class MatrixHandler : IMatrixHandler
	{
		swm.Matrix control;

		public swm.Matrix Control { get { return control; } }

		public object ControlObject { get { return control; } }

		public MatrixHandler ()
		{
		}

		public MatrixHandler (swm.Matrix matrix)
		{
			control = matrix;
		}

		public float[] Elements
		{
			get
			{
				return new float[] {
					(float)control.M11,
					(float)control.M12,
					(float)control.M21,
					(float)control.M22,
					(float)control.OffsetX,
					(float)control.OffsetY
				};
			}
		}

		public float Xx { get { return (float)control.M11; } set { control.M11 = value; } }

		public float Xy { get { return (float)control.M12; } set { control.M12 = value; } }

		public float Yx { get { return (float)control.M21; } set { control.M21 = value; } }

		public float Yy { get { return (float)control.M22; } set { control.M22 = value; } }

		public float X0 { get { return (float)control.OffsetX; } set { control.OffsetX = value; } }

		public float Y0 { get { return (float)control.OffsetY; } set { control.OffsetY = value; } }

		public void Rotate (float angle)
		{
			control.Rotate (angle);
		}

		public void RotateAt (float angle, float centerX, float centerY)
		{
			control.RotateAt (angle, centerX, centerY);
		}

		public void Translate (float x, float y)
		{
			control.Translate (x, y);
		}

		public void Scale (float scaleX, float scaleY)
		{
			control.Scale (scaleX, scaleY);
		}

		public void ScaleAt (float scaleX, float scaleY, float centerX, float centerY)
		{
			control.ScaleAt (scaleX, scaleY, centerX, centerY);
		}

		public void Skew (float skewX, float skewY)
		{
			control.Skew (skewX, skewY);
		}

		public void Append (IMatrix matrix)
		{
			var m2 = (swm.Matrix)matrix.ControlObject;
			control.Append (m2);
		}

		public void Prepend (IMatrix matrix)
		{
			var m2 = (swm.Matrix)matrix.ControlObject;
			control.Prepend (m2);
		}

		public void Create ()
		{
			control = swm.Matrix.Identity;
		}

		public void Create (float m11, float m12, float m21, float m22, float dx, float dy)
		{
			control = new swm.Matrix (m11, m12, m21, m22, dx, dy);
		}

		public void Invert ()
		{
			control.Invert ();
		}

		public PointF TransformPoint (Point p)
		{
			return control.Transform (p.ToWpf ()).ToEto ();
		}

		public PointF TransformPoint (PointF p)
		{
			return control.Transform (p.ToWpf ()).ToEto ();
		}

		public IMatrix Clone ()
		{
			return new MatrixHandler (control);
		}
	}
}