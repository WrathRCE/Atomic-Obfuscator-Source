using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Writer;

namespace Atomic.packer
{
	// Token: 0x02000016 RID: 22
	internal class context
	{
		// Token: 0x06000098 RID: 152 RVA: 0x0000682C File Offset: 0x00004A2C
		public static void LoadModule(string filename)
		{
			try
			{
				context.FileName = filename;
				byte[] data = File.ReadAllBytes(filename);
				ModuleContext context = ModuleDef.CreateModuleContext();
				context.module = ModuleDefMD.Load(data, context);
				foreach (AssemblyRef assemblyRef in context.module.GetAssemblyRefs())
				{
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000068B0 File Offset: 0x00004AB0
		public static void SaveModule()
		{
			try
			{
				string filename = string.Concat(new string[]
				{
					Path.GetDirectoryName(context.FileName),
					"\\",
					Path.GetFileNameWithoutExtension(context.FileName),
					"_Packed",
					Path.GetExtension(context.FileName)
				});
				bool isILOnly = context.module.IsILOnly;
				if (isILOnly)
				{
					ModuleWriterOptions moduleWriterOptions = new ModuleWriterOptions(context.module);
					moduleWriterOptions.MetaDataOptions.Flags = MetaDataFlags.PreserveAll;
					moduleWriterOptions.MetaDataLogger = DummyLogger.NoThrowInstance;
					context.module.Write(filename, moduleWriterOptions);
				}
				else
				{
					NativeModuleWriterOptions nativeModuleWriterOptions = new NativeModuleWriterOptions(context.module);
					nativeModuleWriterOptions.MetaDataOptions.Flags = MetaDataFlags.PreserveAll;
					nativeModuleWriterOptions.MetaDataLogger = DummyLogger.NoThrowInstance;
					context.module.NativeWrite(filename, nativeModuleWriterOptions);
				}
			}
			catch (ModuleWriterException ex)
			{
			}
			Console.ReadLine();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000069A0 File Offset: 0x00004BA0
		public static byte[] Compress(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionLevel.Optimal))
			{
				deflateStream.Write(data, 0, data.Length);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000069F4 File Offset: 0x00004BF4
		public static void PackerPhase()
		{
			ModuleDefMD moduleDefMD = ModuleDefMD.Load(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Atomic.exe");
			moduleDefMD.Characteristics = context.module.Characteristics;
			moduleDefMD.Cor20HeaderFlags = context.module.Cor20HeaderFlags;
			moduleDefMD.Cor20HeaderRuntimeVersion = context.module.Cor20HeaderRuntimeVersion;
			moduleDefMD.DllCharacteristics = context.module.DllCharacteristics;
			moduleDefMD.EncBaseId = context.module.EncBaseId;
			moduleDefMD.EncId = context.module.EncId;
			moduleDefMD.Generation = context.module.Generation;
			moduleDefMD.Kind = context.module.Kind;
			moduleDefMD.Machine = context.module.Machine;
			moduleDefMD.RuntimeVersion = context.module.RuntimeVersion;
			moduleDefMD.TablesHeaderVersion = context.module.TablesHeaderVersion;
			moduleDefMD.Win32Resources = context.module.Win32Resources;
			MethodDef entryPoint = moduleDefMD.EntryPoint;
			string text = context.RandomString(20);
			Instruction instruction = (from op in entryPoint.Body.Instructions
			where op.OpCode == OpCodes.Ldc_I4 && op.GetLdcI4Value() == 123456789
			select op).First<Instruction>();
			Instruction instruction2 = (from op in entryPoint.Body.Instructions
			where op.OpCode == OpCodes.Ldstr && op.Operand.ToString().Equals("Name_Resource")
			select op).First<Instruction>();
			instruction.Operand = entryPoint.MDToken.ToInt32();
			instruction2.Operand = text;
			byte[] ilasByteArray = Assembly.Load(context.GetCurrentModule(moduleDefMD)).ManifestModule.ResolveMethod(entryPoint.MDToken.ToInt32()).GetMethodBody().GetILAsByteArray();
			moduleDefMD.Resources.Add(new EmbeddedResource(text, context.Encrypt(context.GetCurrentModule(context.module), ilasByteArray)));
			context.module = moduleDefMD;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006BE8 File Offset: 0x00004DE8
		public static byte[] GetCurrentModule(ModuleDefMD module)
		{
			MemoryStream memoryStream = new MemoryStream();
			bool isILOnly = module.IsILOnly;
			if (isILOnly)
			{
				module.Write(memoryStream, new ModuleWriterOptions(module)
				{
					MetaDataOptions = 
					{
						Flags = MetaDataFlags.PreserveAll
					},
					MetaDataLogger = DummyLogger.NoThrowInstance
				});
			}
			else
			{
				module.NativeWrite(memoryStream, new NativeModuleWriterOptions(module)
				{
					MetaDataOptions = 
					{
						Flags = MetaDataFlags.PreserveAll
					},
					MetaDataLogger = DummyLogger.NoThrowInstance
				});
			}
			byte[] array = new byte[memoryStream.Length];
			memoryStream.Position = 0L;
			memoryStream.Read(array, 0, (int)memoryStream.Length);
			return array;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006C98 File Offset: 0x00004E98
		public static byte[] Encrypt(byte[] plain, byte[] Key)
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

		// Token: 0x0600009E RID: 158 RVA: 0x00006D10 File Offset: 0x00004F10
		public static string RandomString(int length)
		{
			return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length)
			select s[context.random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x04000052 RID: 82
		private static Random random = new Random();

		// Token: 0x04000053 RID: 83
		public static ModuleDefMD module = null;

		// Token: 0x04000054 RID: 84
		public static string FileName = null;
	}
}
