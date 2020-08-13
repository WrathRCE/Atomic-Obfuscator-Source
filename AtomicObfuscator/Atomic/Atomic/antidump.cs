using System;
using System.Collections.Generic;
using System.Linq;
using AtomicProtector.Atomic;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Atomic.Atomic
{
	// Token: 0x02000017 RID: 23
	internal class antidump
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00006D7C File Offset: 0x00004F7C
		public static void InjectClass(ModuleDef module)
		{
			ModuleDefMD moduleDefMD = ModuleDefMD.Load(typeof(anti_dump_runtime).Module);
			TypeDef typeDef = moduleDefMD.ResolveTypeDef(MDToken.ToRID(typeof(anti_dump_runtime).MetadataToken));
			IEnumerable<IDnlibDef> source = InjectHelper1.InjectHelper.Inject(typeDef, module.GlobalType, module);
			antidump.init = (MethodDef)source.Single((IDnlibDef method) => method.Name == "Initialize");
			foreach (MethodDef methodDef in module.GlobalType.Methods)
			{
				bool flag = methodDef.Name == ".ctor";
				if (flag)
				{
					module.GlobalType.Remove(methodDef);
					break;
				}
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006E64 File Offset: 0x00005064
		public static void Execute(ModuleDef module)
		{
			antidump.InjectClass(module);
			MethodDef methodDef = module.GlobalType.FindOrCreateStaticConstructor();
			methodDef.Body.Instructions.Insert(0, Instruction.Create(OpCodes.Call, antidump.init));
		}

		// Token: 0x04000055 RID: 85
		public static MethodDef init;
	}
}
