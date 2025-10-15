// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;

namespace RhodeIsland.Arknights.AVG
{
	[Serializable]
	public struct SkipNodeLabel
	{
		public bool IsEmpty()
		{
			return default(bool);
		}

		public static readonly SkipNodeLabel EMPTY;

		public int LineNum;

		public AVGSkipMode Mode;
	}
}
