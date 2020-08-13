using System;
using System.Collections.Generic;
using System.Linq;

namespace AtomicProtector.Atomic.cflow
{
	// Token: 0x02000008 RID: 8
	public class Blocks
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000023EC File Offset: 0x000005EC
		public Block getBlock(int id)
		{
			return this.blocks.Single((Block block) => block.ID == id);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002424 File Offset: 0x00000624
		public void Scramble(out Blocks incGroups)
		{
			Blocks blocks = new Blocks();
			foreach (Block item in this.blocks)
			{
				blocks.blocks.Insert(this.generator.Next(1, blocks.blocks.Count), item);
			}
			incGroups = blocks;
		}

		// Token: 0x04000008 RID: 8
		public List<Block> blocks = new List<Block>();

		// Token: 0x04000009 RID: 9
		private Random generator = new Random();
	}
}
