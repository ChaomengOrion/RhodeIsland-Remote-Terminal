// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-03-26 18:18:06

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Torappu.Resource
{
	public class ResourceIndex : ScriptableObject
	{
		public string versionId;
		public string indexBundle;
		public string manifestBundle;
		public int rawCount;
		public List<BundleMeta> bundles = new();
		public List<AssetToBundleMeta> assetToBundleList = new();

		[Serializable]
		public struct BundleMeta
		{
			public override string ToString()
			{
				return name;
			}
			public string name;
			public string hash;
			public string md5;
			public bool isCacheable;
			public int sccIndex;
		}

		[Serializable]
		public struct AssetToBundleMeta
		{
			public override string ToString()
			{
				return assetName;
			}
			public string assetName;
			public string bundleName;
		}
	}
}
