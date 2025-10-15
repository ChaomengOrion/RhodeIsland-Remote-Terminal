// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using UnityEngine;

namespace RhodeIsland.Arknights.Resource
{
	public class DirectAssetLoader : AbstractAssetLoader
	{
		public DirectAssetLoader()
		{
		}
		public override void ClearAll()
		{
		}
		protected override void OnAssetLoaded(string path, UnityEngine.Object asset)
		{
		}
		protected override void OnAssetUnloading(UnityEngine.Object asset)
		{
		}
		private List<int> m_instanceIds;
	}
}
