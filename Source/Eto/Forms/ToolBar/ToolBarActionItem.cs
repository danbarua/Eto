using System;
using Eto.Drawing;

namespace Eto.Forms
{
	
	public interface IToolBarActionItem : IToolBarItem
	{
		string Text { get; set; }
		string ToolTip { get; set; }
		Image Image { get; set; }
		bool Enabled { get; set; }
	}
	
	public class ToolBarActionItem : ToolBarItem
	{
		IToolBarActionItem inner;
		
		public ToolBarActionItem(Generator g, Type type)
			: base(g, type)
		{
			inner = (IToolBarActionItem)Handler;
		}
		
		public string Text
		{
			get { return inner.Text; }
			set { inner.Text = value; }
		}

		public string ToolTip
		{
			get { return inner.ToolTip; }
			set { inner.ToolTip = value; }
		}
		
		public Image Image
		{
			get { return inner.Image; }
			set { inner.Image = value; }
		}

		[Obsolete ("Use Image instead")]
		public Icon Icon
		{
			get { return Image as Icon; }
			set { Image = value; }
		}

		public bool Enabled
		{
			get { return inner.Enabled; }
			set { inner.Enabled = value; }
		}
	}
}

