using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Atomic.Atomic
{
	// Token: 0x0200001E RID: 30
	internal class Renamer
	{
		// Token: 0x0200002F RID: 47
		public interface IRenaming
		{
			// Token: 0x060000F7 RID: 247
			ModuleDefMD Rename(ModuleDefMD module);
		}

		// Token: 0x02000030 RID: 48
		public static class Generator
		{
			// Token: 0x060000F8 RID: 248 RVA: 0x0000A108 File Offset: 0x00008308
			public static string GenerateString()
			{
				int num = 2;
				byte[] array = new byte[num];
				new RNGCryptoServiceProvider().GetBytes(array);
				string str = null;
				return str + Renamer.Generator.EncodeString(array, Renamer.Generator.unicodeCharset);
			}

			// Token: 0x060000F9 RID: 249 RVA: 0x0000A144 File Offset: 0x00008344
			private static string EncodeString(byte[] buff, char[] charset)
			{
				int i = (int)buff[0];
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = 1; j < buff.Length; j++)
				{
					for (i = (i << 8) + (int)buff[j]; i >= charset.Length; i /= charset.Length)
					{
						stringBuilder.Append(charset[i % charset.Length]);
					}
				}
				bool flag = i != 0;
				if (flag)
				{
					stringBuilder.Append(charset[i % charset.Length]);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x060000FA RID: 250 RVA: 0x0000A1C4 File Offset: 0x000083C4
			public static byte[] GetBytes(int lenght)
			{
				byte[] array = new byte[lenght];
				new RNGCryptoServiceProvider().GetBytes(array);
				return array;
			}

			// Token: 0x04000080 RID: 128
			private static readonly char[] unicodeCharset = new char[0].Concat(from ord in Enumerable.Range(8203, 5)
			select (char)ord).Concat(from ord in Enumerable.Range(8233, 6)
			select (char)ord).Concat(from ord in Enumerable.Range(8298, 6)
			select (char)ord).Except(new char[]
			{
				'搷'
			}).ToArray<char>();
		}

		// Token: 0x02000031 RID: 49
		public static class Renamer3
		{
			// Token: 0x060000FC RID: 252 RVA: 0x0000A28C File Offset: 0x0000848C
			public static ModuleDef Rename(ModuleDef mod)
			{
				ModuleDefMD module = (ModuleDefMD)mod;
				Renamer.IRenaming renaming = new Renamer.NamespacesRenaming();
				module = renaming.Rename(module);
				renaming = new Renamer.ClassesRenaming();
				module = renaming.Rename(module);
				renaming = new Renamer.MethodsRenaming();
				module = renaming.Rename(module);
				renaming = new Renamer.PropertiesRenaming();
				module = renaming.Rename(module);
				renaming = new Renamer.FieldsRenaming();
				return renaming.Rename(module);
			}
		}

		// Token: 0x02000032 RID: 50
		public class FieldsRenaming : Renamer.IRenaming
		{
			// Token: 0x060000FD RID: 253 RVA: 0x0000A2EC File Offset: 0x000084EC
			public ModuleDefMD Rename(ModuleDefMD module)
			{
				foreach (TypeDef typeDef in module.GetTypes())
				{
					bool isGlobalModuleType = typeDef.IsGlobalModuleType;
					if (!isGlobalModuleType)
					{
						foreach (FieldDef fieldDef in typeDef.Fields)
						{
							string s;
							bool flag = Renamer.FieldsRenaming._names.TryGetValue(fieldDef.Name, out s);
							if (flag)
							{
								fieldDef.Name = s;
							}
							else
							{
								string text = Renamer.Generator.GenerateString();
								Renamer.FieldsRenaming._names.Add(fieldDef.Name, text);
								fieldDef.Name = text;
							}
						}
					}
				}
				return Renamer.FieldsRenaming.ApplyChangesToResources(module);
			}

			// Token: 0x060000FE RID: 254 RVA: 0x0000A3FC File Offset: 0x000085FC
			private static ModuleDefMD ApplyChangesToResources(ModuleDefMD module)
			{
				foreach (TypeDef typeDef in module.GetTypes())
				{
					bool isGlobalModuleType = typeDef.IsGlobalModuleType;
					if (!isGlobalModuleType)
					{
						foreach (MethodDef methodDef in typeDef.Methods)
						{
							bool flag = methodDef.Name != "InitializeComponent";
							if (!flag)
							{
								IList<Instruction> instructions = methodDef.Body.Instructions;
								for (int i = 0; i < instructions.Count - 3; i++)
								{
									bool flag2 = instructions[i].OpCode == OpCodes.Ldstr;
									if (flag2)
									{
										foreach (KeyValuePair<string, string> keyValuePair in Renamer.FieldsRenaming._names)
										{
											bool flag3 = keyValuePair.Key == instructions[i].Operand.ToString();
											if (flag3)
											{
												instructions[i].Operand = keyValuePair.Value;
											}
										}
									}
								}
							}
						}
					}
				}
				return module;
			}

			// Token: 0x04000081 RID: 129
			private static Dictionary<string, string> _names = new Dictionary<string, string>();
		}

		// Token: 0x02000033 RID: 51
		public class ClassesRenaming : Renamer.IRenaming
		{
			// Token: 0x06000101 RID: 257 RVA: 0x0000A5D0 File Offset: 0x000087D0
			public ModuleDefMD Rename(ModuleDefMD module)
			{
				foreach (TypeDef typeDef in module.GetTypes())
				{
					bool isGlobalModuleType = typeDef.IsGlobalModuleType;
					if (!isGlobalModuleType)
					{
						bool flag = typeDef.Name == "GeneratedInternalTypeHelper" || typeDef.Name == "Resources" || typeDef.Name == "Settings";
						if (!flag)
						{
							string s;
							bool flag2 = Renamer.ClassesRenaming._names.TryGetValue(typeDef.Name, out s);
							if (flag2)
							{
								typeDef.Name = s;
							}
							else
							{
								string text = Renamer.Generator.GenerateString();
								Renamer.ClassesRenaming._names.Add(typeDef.Name, text);
								typeDef.Name = text;
							}
						}
					}
				}
				return Renamer.ClassesRenaming.ApplyChangesToResources(module);
			}

			// Token: 0x06000102 RID: 258 RVA: 0x0000A6DC File Offset: 0x000088DC
			private static ModuleDefMD ApplyChangesToResources(ModuleDefMD module)
			{
				foreach (Resource resource in module.Resources)
				{
					foreach (KeyValuePair<string, string> keyValuePair in Renamer.ClassesRenaming._names)
					{
						bool flag = resource.Name.Contains(keyValuePair.Key);
						if (flag)
						{
							resource.Name = resource.Name.Replace(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
				foreach (TypeDef typeDef in module.GetTypes())
				{
					foreach (PropertyDef propertyDef in typeDef.Properties)
					{
						bool flag2 = propertyDef.Name != "ResourceManager";
						if (!flag2)
						{
							IList<Instruction> instructions = propertyDef.GetMethod.Body.Instructions;
							for (int i = 0; i < instructions.Count; i++)
							{
								bool flag3 = instructions[i].OpCode == OpCodes.Ldstr;
								if (flag3)
								{
									foreach (KeyValuePair<string, string> keyValuePair2 in Renamer.ClassesRenaming._names)
									{
										bool flag4 = instructions[i].Operand.ToString().Contains(keyValuePair2.Key);
										if (flag4)
										{
											instructions[i].Operand = instructions[i].Operand.ToString().Replace(keyValuePair2.Key, keyValuePair2.Value);
										}
									}
								}
							}
						}
					}
				}
				return module;
			}

			// Token: 0x04000082 RID: 130
			private static Dictionary<string, string> _names = new Dictionary<string, string>();
		}

		// Token: 0x02000034 RID: 52
		public class MethodsRenaming : Renamer.IRenaming
		{
			// Token: 0x06000105 RID: 261 RVA: 0x0000A994 File Offset: 0x00008B94
			public ModuleDefMD Rename(ModuleDefMD module)
			{
				foreach (TypeDef typeDef in module.GetTypes())
				{
					bool isGlobalModuleType = typeDef.IsGlobalModuleType;
					if (!isGlobalModuleType)
					{
						bool flag = typeDef.Name == "GeneratedInternalTypeHelper";
						if (!flag)
						{
							foreach (MethodDef methodDef in typeDef.Methods)
							{
								bool flag2 = !methodDef.HasBody;
								if (!flag2)
								{
									bool flag3 = methodDef.Name == ".ctor" || methodDef.Name == ".cctor";
									if (!flag3)
									{
										methodDef.Name = Renamer.Generator.GenerateString();
									}
								}
							}
						}
					}
				}
				return module;
			}
		}

		// Token: 0x02000035 RID: 53
		public class NamespacesRenaming : Renamer.IRenaming
		{
			// Token: 0x06000107 RID: 263 RVA: 0x0000AAB8 File Offset: 0x00008CB8
			public ModuleDefMD Rename(ModuleDefMD module)
			{
				foreach (TypeDef typeDef in module.GetTypes())
				{
					bool isGlobalModuleType = typeDef.IsGlobalModuleType;
					if (!isGlobalModuleType)
					{
						bool flag = typeDef.Namespace == "";
						if (!flag)
						{
							string s;
							bool flag2 = Renamer.NamespacesRenaming._names.TryGetValue(typeDef.Namespace, out s);
							if (flag2)
							{
								typeDef.Namespace = s;
							}
							else
							{
								string text = Renamer.Generator.GenerateString();
								Renamer.NamespacesRenaming._names.Add(typeDef.Namespace, text);
								typeDef.Namespace = text;
							}
						}
					}
				}
				return Renamer.NamespacesRenaming.ApplyChangesToResources(module);
			}

			// Token: 0x06000108 RID: 264 RVA: 0x0000AB98 File Offset: 0x00008D98
			private static ModuleDefMD ApplyChangesToResources(ModuleDefMD module)
			{
				foreach (Resource resource in module.Resources)
				{
					foreach (KeyValuePair<string, string> keyValuePair in Renamer.NamespacesRenaming._names)
					{
						bool flag = resource.Name.Contains(keyValuePair.Key);
						if (flag)
						{
							resource.Name = resource.Name.Replace(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
				foreach (TypeDef typeDef in module.GetTypes())
				{
					foreach (PropertyDef propertyDef in typeDef.Properties)
					{
						bool flag2 = propertyDef.Name != "ResourceManager";
						if (!flag2)
						{
							IList<Instruction> instructions = propertyDef.GetMethod.Body.Instructions;
							for (int i = 0; i < instructions.Count; i++)
							{
								bool flag3 = instructions[i].OpCode == OpCodes.Ldstr;
								if (flag3)
								{
									foreach (KeyValuePair<string, string> keyValuePair2 in Renamer.NamespacesRenaming._names)
									{
										bool flag4 = instructions[i].ToString().Contains(keyValuePair2.Key);
										if (flag4)
										{
											instructions[i].Operand = instructions[i].Operand.ToString().Replace(keyValuePair2.Key, keyValuePair2.Value);
										}
									}
								}
							}
						}
					}
				}
				return module;
			}

			// Token: 0x04000083 RID: 131
			private static Dictionary<string, string> _names = new Dictionary<string, string>();
		}

		// Token: 0x02000036 RID: 54
		public class PropertiesRenaming : Renamer.IRenaming
		{
			// Token: 0x0600010B RID: 267 RVA: 0x0000AE4C File Offset: 0x0000904C
			public ModuleDefMD Rename(ModuleDefMD module)
			{
				foreach (TypeDef typeDef in module.GetTypes())
				{
					bool isGlobalModuleType = typeDef.IsGlobalModuleType;
					if (!isGlobalModuleType)
					{
						foreach (PropertyDef propertyDef in typeDef.Properties)
						{
							propertyDef.Name = Renamer.Generator.GenerateString();
						}
					}
				}
				return module;
			}
		}
	}
}
