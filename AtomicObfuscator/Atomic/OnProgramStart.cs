using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows;

namespace Atomic
{
	// Token: 0x0200000F RID: 15
	internal class OnProgramStart
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002DAC File Offset: 0x00000FAC
		public static void Initialize(string name, string aid, string secret, string version)
		{
			OnProgramStart.AID = aid;
			OnProgramStart.Secret = secret;
			OnProgramStart.Version = version;
			OnProgramStart.Name = name;
			string[] array = new string[0];
			using (WebClient webClient = new WebClient())
			{
				try
				{
					webClient.Proxy = null;
					Security.Start();
					Encoding @default = Encoding.Default;
					WebClient webClient2 = webClient;
					string apiUrl = Constants.ApiUrl;
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection["token"] = Encryption.EncryptService(Constants.Token);
					nameValueCollection["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString());
					nameValueCollection["aid"] = Encryption.APIService(OnProgramStart.AID);
					nameValueCollection["session_id"] = Constants.IV;
					nameValueCollection["api_id"] = Constants.APIENCRYPTSALT;
					nameValueCollection["api_key"] = Constants.APIENCRYPTKEY;
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.APIService(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.APIService("start");
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					string text = array[2];
					string text2 = text;
					if (text2 != null)
					{
						if (!(text2 == "success"))
						{
							if (text2 == "binderror")
							{
								MessageBox.Show(Encryption.Decode("RmFpbGVkIHRvIGJpbmQgdG8gc2VydmVyLCBjaGVjayB5b3VyIEFJRCAmIFNlY3JldCBpbiB5b3VyIGNvZGUh"), "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
								Process.GetCurrentProcess().Kill();
								return;
							}
							if (text2 == "banned")
							{
								MessageBox.Show("outbuilt is gay and banned our application, when I realize I will send out new version", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
								Process.GetCurrentProcess().Kill();
								return;
							}
						}
						else
						{
							Constants.Initialized = true;
							bool flag = array[3] == "Enabled";
							if (flag)
							{
								ApplicationSettings.Status = true;
							}
							bool flag2 = array[4] == "Enabled";
							if (flag2)
							{
								ApplicationSettings.DeveloperMode = true;
							}
							ApplicationSettings.Hash = array[5];
							ApplicationSettings.Version = array[6];
							ApplicationSettings.Update_Link = array[7];
							bool flag3 = array[8] == "Enabled";
							if (flag3)
							{
								ApplicationSettings.Freemode = true;
							}
							bool flag4 = array[9] == "Enabled";
							if (flag4)
							{
								ApplicationSettings.Login = true;
							}
							ApplicationSettings.Name = array[10];
							bool flag5 = array[11] == "Enabled";
							if (flag5)
							{
								ApplicationSettings.Register = true;
							}
							bool flag6 = !ApplicationSettings.Status;
							if (flag6)
							{
								MessageBox.Show("Application disabled", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
								Process.GetCurrentProcess().Kill();
							}
						}
					}
					Security.End();
				}
				catch
				{
					MessageBox.Show("Failed to connect! Try again.", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
					Process.GetCurrentProcess().Kill();
				}
			}
		}

		// Token: 0x0400002C RID: 44
		public static string AID = null;

		// Token: 0x0400002D RID: 45
		public static string Secret = null;

		// Token: 0x0400002E RID: 46
		public static string Version = null;

		// Token: 0x0400002F RID: 47
		public static string Name = null;

		// Token: 0x04000030 RID: 48
		public static string Salt = null;
	}
}
