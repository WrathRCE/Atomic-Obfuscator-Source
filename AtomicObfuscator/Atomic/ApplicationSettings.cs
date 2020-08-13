using System;

namespace Atomic
{
	// Token: 0x0200000E RID: 14
	internal class ApplicationSettings
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002D1C File Offset: 0x00000F1C
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002D23 File Offset: 0x00000F23
		public static bool Status { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002D2B File Offset: 0x00000F2B
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002D32 File Offset: 0x00000F32
		public static bool DeveloperMode { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002D3A File Offset: 0x00000F3A
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002D41 File Offset: 0x00000F41
		public static string Hash { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002D49 File Offset: 0x00000F49
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002D50 File Offset: 0x00000F50
		public static string Version { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002D58 File Offset: 0x00000F58
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002D5F File Offset: 0x00000F5F
		public static string Update_Link { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002D67 File Offset: 0x00000F67
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002D6E File Offset: 0x00000F6E
		public static bool Freemode { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D76 File Offset: 0x00000F76
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002D7D File Offset: 0x00000F7D
		public static bool Login { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002D85 File Offset: 0x00000F85
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002D8C File Offset: 0x00000F8C
		public static string Name { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002D94 File Offset: 0x00000F94
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002D9B File Offset: 0x00000F9B
		public static bool Register { get; set; }
	}
}
