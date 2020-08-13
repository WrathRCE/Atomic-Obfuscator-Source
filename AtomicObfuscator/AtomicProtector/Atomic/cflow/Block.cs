using System;
using System.Collections.Generic;
using dnlib.DotNet.Emit;

namespace AtomicProtector.Atomic.cflow
{
	// Token: 0x02000007 RID: 7
	public class Block
	{
		// Token: 0x04000005 RID: 5
		public int ID = 0;

		// Token: 0x04000006 RID: 6
		public int nextBlock = 0;

		// Token: 0x04000007 RID: 7
		public List<Instruction> instructions = new List<Instruction>();
	}
}
