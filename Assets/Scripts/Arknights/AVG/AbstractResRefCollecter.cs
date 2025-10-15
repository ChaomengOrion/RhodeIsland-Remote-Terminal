// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;

namespace RhodeIsland.Arknights.AVG
{
	public abstract class AbstractResRefCollecter
	{
		protected AbstractResRefCollecter()
		{
		}
		public abstract void GatherResRefs(Command command, HashSet<string> references);
		public virtual void GatherResFilenames(Command command, HashSet<string> filenames)
		{
		}
		public virtual bool useForResBan
		{
			get
			{
				return default(bool);
			}
		}
	}
}
