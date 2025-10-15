// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using RhodeIsland.Arknights.Resource;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGAssetLoader : CachedAssetLoader
	{
		public override T Load<T>(string path)
		{
			return base.Load<T>(path);
		}
	}
}
