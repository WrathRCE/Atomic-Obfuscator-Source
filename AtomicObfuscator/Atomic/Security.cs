using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;

namespace Atomic
{
	// Token: 0x02000011 RID: 17
	internal class Security
	{
		// Token: 0x0600005B RID: 91 RVA: 0x000039AC File Offset: 0x00001BAC
		private static string Session(int length)
		{
			Random random = new Random();
			return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz", length)
			select s[random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000039F8 File Offset: 0x00001BF8
		public static string Obfuscate(int length)
		{
			Random random = new Random();
			return new string((from s in Enumerable.Repeat<string>("gd8JQ57nxXzLLMPrLylVhxoGnWGCFjO4knKTfRE6mVvdjug2NF/4aptAsZcdIGbAPmcx0O+ftU/KvMIjcfUnH3j+IMdhAW5OpoX3MrjQdf5AAP97tTB5g1wdDSAqKpq9gw06t3VaqMWZHKtPSuAXy0kkZRsc+DicpcY8E9+vWMHXa3jMdbPx4YES0p66GzhqLd/heA2zMvX8iWv4wK7S3QKIW/a9dD4ALZJpmcr9OOE=", length)
			select s[random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003A44 File Offset: 0x00001C44
		public static void Start()
		{
			Constants.Token = Guid.NewGuid().ToString();
			ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(Security.PinPublicKey));
			Constants.APIENCRYPTKEY = Convert.ToBase64String(Encoding.Default.GetBytes(Security.Session(32)));
			Constants.APIENCRYPTSALT = Convert.ToBase64String(Encoding.Default.GetBytes(Security.Session(16)));
			Constants.IV = Convert.ToBase64String(Encoding.Default.GetBytes(Constants.RandomString(16)));
			Constants.Key = Convert.ToBase64String(Encoding.Default.GetBytes(Constants.RandomString(32)));
			Constants.Started = true;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003B00 File Offset: 0x00001D00
		public static void End()
		{
			bool flag = !Constants.Started;
			if (flag)
			{
				MessageBox.Show("No session has been started, closing for security reasons!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				Process.GetCurrentProcess().Kill();
			}
			else
			{
				Constants.Token = null;
				ServicePointManager.ServerCertificateValidationCallback = ((object <p0>, X509Certificate <p1>, X509Chain <p2>, SslPolicyErrors <p3>) => true);
				Constants.APIENCRYPTKEY = null;
				Constants.APIENCRYPTSALT = null;
				Constants.IV = null;
				Constants.Key = null;
				Constants.Started = false;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003B8C File Offset: 0x00001D8C
		private static bool PinPublicKey(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return certificate != null && certificate.GetPublicKeyString() == "046EECD33E469E9E1958D6BEEDE0A71843202724A5758BD1723F6C340C5E98EDE06FF5C21B35F359C65B850744729B3AA999B0B6392DA69EDB278EB31DBCE85774";
		}

		// Token: 0x04000031 RID: 49
		private const string _key = "046EECD33E469E9E1958D6BEEDE0A71843202724A5758BD1723F6C340C5E98EDE06FF5C21B35F359C65B850744729B3AA999B0B6392DA69EDB278EB31DBCE85774";
	}
}
