// Created by ChaomengOrion
// Create at 2022-06-02 19:23:37
// Last modified on 2022-08-01 19:07:51

using System;

namespace RhodeIsland.Arknights.SafeArea.Core
{
	public class SafeAreaImpl
	{
		public virtual SafeRect GetSafeRect()
		{
			return SafeRect.EMPTY;
		}

		public virtual void InitWindowLayout() { }
	}
}