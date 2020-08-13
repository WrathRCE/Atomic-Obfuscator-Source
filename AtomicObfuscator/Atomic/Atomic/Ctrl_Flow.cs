﻿using System;
using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Atomic.Atomic
{
	// Token: 0x0200001A RID: 26
	internal class Ctrl_Flow
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00007684 File Offset: 0x00005884
		public static void Brs(ModuleDef md)
		{
			foreach (TypeDef typeDef in md.Types.ToArray<TypeDef>())
			{
				foreach (MethodDef methodDef in typeDef.Methods.ToArray<MethodDef>())
				{
					bool flag = methodDef.HasBody && methodDef.Body.Instructions.Count != 0;
					if (flag)
					{
						IList<Instruction> instructions = methodDef.Body.Instructions;
						int num = 0;
						if (num < instructions.Count)
						{
							Instruction instruction = Instruction.Create(OpCodes.Nop);
							Instruction instruction2 = OpCodes.Call.ToInstruction(md.Import(typeof(string).GetMethod("IsNullOrEmpty", new Type[]
							{
								typeof(string)
							})));
						}
					}
				}
			}
		}

		// Token: 0x0400005B RID: 91
		public static List<Instruction> instr = new List<Instruction>();

		// Token: 0x0400005C RID: 92
		private static List<int> Integers = new List<int>();

		// Token: 0x0400005D RID: 93
		private static List<int> Integers_Position = new List<int>();

		// Token: 0x0400005E RID: 94
		private static int Index = 0;

		// Token: 0x0400005F RID: 95
		private static Random rnd = new Random();
	}
}
