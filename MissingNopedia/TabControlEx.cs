using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MissingNopedia
{
	/// <summary>
	/// Gère un ensemble de pages d'onglets liées entre elles.
	/// </summary>
	//[ToolboxItem(typeof(TabControl))]
	//[ToolboxItem("Test")]
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(TabControl))]
	public class TabControlEx : TabControl
	{
		protected override void WndProc(ref Message m)
		{
			// Corrige le margin sur les sections des onglets.
			if (m.Msg == 0x1300 + 40)
			{
				RECT rc = (RECT)m.GetLParam(typeof(RECT));
				rc.Left -= 4;
				rc.Right += 4;
				rc.Top -= 0;
				rc.Bottom += 3;
				Marshal.StructureToPtr(rc, m.LParam, true);
			}
			base.WndProc(ref m);
		}

	}
	internal struct RECT { public int Left, Top, Right, Bottom; }
}
