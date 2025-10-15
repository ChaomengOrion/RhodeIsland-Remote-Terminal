// Created by ChaomengOrion
// Create at 2022-06-02 19:08:01
// Last modified on 2022-08-01 19:07:50

using System;

namespace RhodeIsland.Arknights
{
	[Serializable]
	public struct SafeRect
	{
		public static readonly SafeRect EMPTY = new SafeRect { left = 0, right = 0, bottom = 0, top = 0};
		public int left;
		public int right;
		public int top;
		public int bottom;
	}
}