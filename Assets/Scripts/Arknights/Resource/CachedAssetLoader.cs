// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections.Generic;
using UnityEngine;

namespace RhodeIsland.Arknights.Resource
{
	public class CachedAssetLoader : AbstractAssetLoader
	{
		public CachedAssetLoader()
		{
		}
		public override void ClearAll()
		{
		}
		public void ClearAll(bool unloadUnusedAssets)
		{
		}
		protected override void OnAssetLoaded(string path, UnityEngine.Object asset)
		{
		}
		protected override void OnAssetUnloading(UnityEngine.Object asset)
		{
		}
		protected override bool TryGetAsset(string path, out UnityEngine.Object asset)
		{
			asset = null;
			return default(bool);
		}
		protected override bool TryGetAsset<T>(string path, out T asset)
		{
			asset = null;
			return default(bool);
		}
		protected override bool TryGetAssets(string path, out UnityEngine.Object[] assets)
		{
			assets = null;
			return default(bool);
		}
		protected override bool TryGetAssets<T>(string path, out T[] assets)
		{
			assets = null;
			return default(bool);
		}
		private HashSet<UnityEngine.Object> _EnsureCachedAsset(string key)
		{
			return null;
		}
		private string _GetKeyFromPath(string path)
		{
			return null;
		}
		private Dictionary<string, HashSet<UnityEngine.Object>> m_cachedAssets;
		private Dictionary<int, string> m_instanceIdToName;
	}
}
