using System;
using System.Windows.Forms;

namespace Atomic
{
	// Token: 0x02000015 RID: 21
	internal static class Program
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00006811 File Offset: 0x00004A11
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
