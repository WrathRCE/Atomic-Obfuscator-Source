using System;
using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace AtomicProtector.Atomic.cflow
{
	// Token: 0x02000009 RID: 9
	internal class cfhelper
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000024C0 File Offset: 0x000006C0
		public static bool HasUnsafeInstructions(MethodDef methodDef)
		{
			bool hasBody = methodDef.HasBody;
			if (hasBody)
			{
				bool hasVariables = methodDef.Body.HasVariables;
				if (hasVariables)
				{
					return methodDef.Body.Variables.Any((Local x) => x.Type.IsPointer);
				}
			}
			return false;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002520 File Offset: 0x00000720
		public static Blocks GetBlocks(MethodDef method)
		{
			Blocks blocks = new Blocks();
			Block block = new Block();
			int num = 0;
			int num2 = 0;
			block.ID = num;
			num++;
			block.nextBlock = block.ID + 1;
			block.instructions.Add(Instruction.Create(OpCodes.Nop));
			blocks.blocks.Add(block);
			block = new Block();
			foreach (Instruction instruction in method.Body.Instructions)
			{
				int num3 = 0;
				int num4;
				instruction.CalculateStackUsage(out num4, out num3);
				block.instructions.Add(instruction);
				num2 += num4 - num3;
				bool flag = num4 == 0;
				if (flag)
				{
					bool flag2 = instruction.OpCode != OpCodes.Nop;
					if (flag2)
					{
						bool flag3 = num2 == 0 || instruction.OpCode == OpCodes.Ret;
						if (flag3)
						{
							block.ID = num;
							num++;
							block.nextBlock = block.ID + 1;
							blocks.blocks.Add(block);
							block = new Block();
						}
					}
				}
			}
			return blocks;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002668 File Offset: 0x00000868
		public static List<Instruction> Calc(int value)
		{
			List<Instruction> list = new List<Instruction>();
			int num = cfhelper.geneartor.Next(0, 100000);
			bool flag = Convert.ToBoolean(cfhelper.geneartor.Next(0, 2));
			int num2 = cfhelper.geneartor.Next(0, 100000);
			list.Add(Instruction.Create(OpCodes.Ldc_I4, value - num + (flag ? (0 - num2) : num2)));
			list.Add(Instruction.Create(OpCodes.Ldc_I4, num));
			list.Add(Instruction.Create(OpCodes.Add));
			list.Add(Instruction.Create(OpCodes.Ldc_I4, num2));
			list.Add(Instruction.Create(flag ? OpCodes.Add : OpCodes.Sub));
			return list;
		}

		// Token: 0x0400000A RID: 10
		private static Random geneartor = new Random();
	}
}
