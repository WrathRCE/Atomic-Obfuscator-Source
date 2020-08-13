using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows;

namespace Atomic
{
	// Token: 0x02000010 RID: 16
	internal class API
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000030D8 File Offset: 0x000012D8
		public static void Log(string username, string action)
		{
			string[] array = new string[0];
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
					Encoding @default = Encoding.Default;
					WebClient webClient2 = webClient;
					string apiUrl = Constants.ApiUrl;
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection["token"] = Encryption.EncryptService(Constants.Token);
					nameValueCollection["aid"] = Encryption.APIService(OnProgramStart.AID);
					nameValueCollection["username"] = Encryption.APIService(username);
					nameValueCollection["pcuser"] = Encryption.APIService(Environment.UserName);
					nameValueCollection["session_id"] = Constants.IV;
					nameValueCollection["api_id"] = Constants.APIENCRYPTSALT;
					nameValueCollection["api_key"] = Constants.APIENCRYPTKEY;
					nameValueCollection["data"] = Encryption.APIService(action);
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.APIService(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.APIService("log");
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					Security.End();
				}
				catch
				{
					MessageBox.Show("Failed to connect! Please try again", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
					Process.GetCurrentProcess().Kill();
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003278 File Offset: 0x00001478
		public static bool AIO(string AIO)
		{
			bool flag = API.AIOLogin(AIO);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = API.AIORegister(AIO);
				if (flag2)
				{
					result = true;
				}
				else
				{
					MessageBox.Show("Invalid key", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000032C0 File Offset: 0x000014C0
		public static bool AIOLogin(string AIO)
		{
			bool flag = string.IsNullOrWhiteSpace(AIO);
			if (flag)
			{
				MessageBox.Show("Missing user login information!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Process.GetCurrentProcess().Kill();
			}
			string[] array = new string[0];
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
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
					nameValueCollection["username"] = Encryption.APIService(AIO);
					nameValueCollection["password"] = Encryption.APIService(AIO);
					nameValueCollection["hwid"] = Encryption.APIService(Constants.HWID());
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.APIService(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.APIService("login");
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					string text = array[2];
					string text2 = text;
					if (text2 != null)
					{
						if (text2 == "success")
						{
							Security.End();
							User.ID = array[3];
							User.Username = array[4];
							User.Password = array[5];
							User.Email = array[6];
							User.HWID = array[7];
							User.UserVariable = array[8];
							User.Rank = array[9];
							User.IP = array[10];
							User.Expiry = array[11];
							User.LastLogin = array[12];
							User.RegisterDate = array[13];
							string text3 = array[14];
							foreach (string text4 in text3.Split(new char[]
							{
								'~'
							}))
							{
								string[] array3 = text4.Split(new char[]
								{
									'^'
								});
								try
								{
									App.Variables.Add(array3[0], array3[1]);
								}
								catch
								{
								}
							}
							return true;
						}
						if (text2 == "invalid_details")
						{
							Security.End();
							return false;
						}
						if (text2 == "time_expired")
						{
							MessageBox.Show("Your subscription has expired!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
							Security.End();
							Process.GetCurrentProcess().Kill();
							return false;
						}
						if (text2 == "hwid_updated")
						{
							Security.End();
							User.ID = array[3];
							User.Username = array[4];
							User.Password = array[5];
							User.Email = array[6];
							User.HWID = array[7];
							User.UserVariable = array[8];
							User.Rank = array[9];
							User.IP = array[10];
							User.Expiry = array[11];
							User.LastLogin = array[12];
							User.RegisterDate = array[13];
							string text5 = array[14];
							foreach (string text6 in text5.Split(new char[]
							{
								'~'
							}))
							{
								string[] array5 = text6.Split(new char[]
								{
									'^'
								});
								try
								{
									App.Variables.Add(array5[0], array5[1]);
								}
								catch
								{
								}
							}
							return true;
						}
						if (text2 == "invalid_hwid")
						{
							MessageBox.Show("Invalid HWID! Contact hugzho if you changed PC/reset pc.", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
							Security.End();
							Process.GetCurrentProcess().Kill();
							return false;
						}
					}
				}
				catch
				{
					MessageBox.Show("Failed to connect! Please try again.", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
					Security.End();
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000375C File Offset: 0x0000195C
		public static bool AIORegister(string AIO)
		{
			bool flag = string.IsNullOrWhiteSpace(AIO);
			if (flag)
			{
				MessageBox.Show("Invalid key!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Process.GetCurrentProcess().Kill();
			}
			string[] array = new string[0];
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
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
					nameValueCollection["type"] = Encryption.APIService("register");
					nameValueCollection["username"] = Encryption.APIService(AIO);
					nameValueCollection["password"] = Encryption.APIService(AIO);
					nameValueCollection["email"] = Encryption.APIService(AIO);
					nameValueCollection["license"] = Encryption.APIService(AIO);
					nameValueCollection["hwid"] = Encryption.APIService(Constants.HWID());
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					Security.End();
					string text = array[2];
					string text2 = text;
					if (text2 != null)
					{
						if (text2 == "success")
						{
							return true;
						}
						if (text2 == "error")
						{
							return false;
						}
					}
				}
				catch
				{
					MessageBox.Show("Failed to connect! Please try again.", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}
	}
}
