using System;
using System.Linq;
using System.Security.Principal;

namespace Atomic
{
	// Token: 0x0200000C RID: 12
	internal class Constants
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002B88 File Offset: 0x00000D88
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002B8F File Offset: 0x00000D8F
		public static string Token { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002B97 File Offset: 0x00000D97
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002B9E File Offset: 0x00000D9E
		public static string Date { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002BA6 File Offset: 0x00000DA6
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002BAD File Offset: 0x00000DAD
		public static string APIENCRYPTKEY { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002BB5 File Offset: 0x00000DB5
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002BBC File Offset: 0x00000DBC
		public static string APIENCRYPTSALT { get; set; }

		// Token: 0x06000025 RID: 37 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public static string RandomString(int length)
		{
			return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length)
			select s[Constants.random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002C10 File Offset: 0x00000E10
		public static string HWID()
		{
			return WindowsIdentity.GetCurrent().User.Value;
		}

		// Token: 0x04000011 RID: 17
		public static bool Breached = false;

		// Token: 0x04000012 RID: 18
		public static bool Started = false;

		// Token: 0x04000013 RID: 19
		public static string IV = null;

		// Token: 0x04000014 RID: 20
		public static string Key = null;

		// Token: 0x04000015 RID: 21
		public static string ApiUrl = "https://api.auth.gg/csharp/";

		// Token: 0x04000016 RID: 22
		public static bool Initialized = false;

		// Token: 0x04000017 RID: 23
		public static Random random = new Random();
	}
}
