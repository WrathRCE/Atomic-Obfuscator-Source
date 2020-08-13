using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Atomic
{
	// Token: 0x02000012 RID: 18
	internal class Encryption
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public static string APIService(string value)
		{
			string @string = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
			SHA256 sha = SHA256.Create();
			byte[] key = sha.ComputeHash(Encoding.ASCII.GetBytes(@string));
			byte[] bytes = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
			return Encryption.EncryptString(value, key, bytes);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003C30 File Offset: 0x00001E30
		public static string EncryptService(string value)
		{
			string @string = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
			SHA256 sha = SHA256.Create();
			byte[] key = sha.ComputeHash(Encoding.ASCII.GetBytes(@string));
			byte[] bytes = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
			string str = Encryption.EncryptString(value, key, bytes);
			int length = int.Parse(OnProgramStart.AID.Substring(0, 2));
			return str + Security.Obfuscate(length);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public static string DecryptService(string value)
		{
			string @string = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
			SHA256 sha = SHA256.Create();
			byte[] key = sha.ComputeHash(Encoding.ASCII.GetBytes(@string));
			byte[] bytes = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
			return Encryption.DecryptString(value, key, bytes);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003D34 File Offset: 0x00001F34
		public static string EncryptString(string plainText, byte[] key, byte[] iv)
		{
			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Key = key;
			aes.IV = iv;
			MemoryStream memoryStream = new MemoryStream();
			ICryptoTransform transform = aes.CreateEncryptor();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			byte[] bytes = Encoding.ASCII.GetBytes(plainText);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			byte[] array = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			return Convert.ToBase64String(array, 0, array.Length);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public static string DecryptString(string cipherText, byte[] key, byte[] iv)
		{
			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Key = key;
			aes.IV = iv;
			MemoryStream memoryStream = new MemoryStream();
			ICryptoTransform transform = aes.CreateDecryptor();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			string result = string.Empty;
			try
			{
				byte[] array = Convert.FromBase64String(cipherText);
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.FlushFinalBlock();
				byte[] array2 = memoryStream.ToArray();
				result = Encoding.ASCII.GetString(array2, 0, array2.Length);
			}
			finally
			{
				memoryStream.Close();
				cryptoStream.Close();
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003E74 File Offset: 0x00002074
		public static string Decode(string text)
		{
			text = text.Replace('_', '/').Replace('-', '+');
			int num = text.Length % 4;
			int num2 = num;
			if (num2 != 2)
			{
				if (num2 == 3)
				{
					text += "=";
				}
			}
			else
			{
				text += "==";
			}
			return Encoding.UTF8.GetString(Convert.FromBase64String(text));
		}
	}
}
