// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using RhodeIsland.Arknights;
using Torappu.Resource;

namespace RhodeIsland.RemoteTerminal.Resources
{
	public static class BundleRouter
	{
		public static string arknightsPersistentResPath { get; set; } = null;

		public static string arknightsStreamResPath { get; set; } = null;

		public static string persistentResPath { get; private set; } = GeneratePersistentResPath();

		/*public static void InitPersistResInfo()
		{
			if (arknightsPersistentResPath != null)
            {
				string path = FileUtil.Combine(arknightsPersistentResPath, REF_INFO_NAME);
				if (File.Exists(path))
                {
					m_arknightsPersistRefInfo = JsonConvert.DeserializeObject<HotUpdateInfo>(File.ReadAllText(path)).abInfos;
                }
			}
		}*/

		public static IEnumerator InitStreamIndex()
		{
			if (arknightsStreamResPath != null)
			{
				if (File.Exists(arknightsStreamResPath))
				{
					string path = "jar:file://" + FileUtil.Combine(arknightsStreamResPath + '!', "assets/AB/Android/torappu_index.ab");
					AssetBundleCreateRequest index = AssetBundle.LoadFromFileAsync(path);
					yield return index;
					AssetBundle assetBundle = index.assetBundle;
					AssetBundleRequest assetBundleRequest = assetBundle.LoadAssetAsync<ResourceIndex>("torappu_index");
					yield return assetBundleRequest;
					ResourceIndex streamResourceIndex = (ResourceIndex)assetBundleRequest.asset;
                    foreach (var item in streamResourceIndex.bundles)
                    {
						m_streamBundles.Add(item.name);
					}
					yield return assetBundle.UnloadAsync(true);
					Debug.Log("[BundleRouter] Successfully loaded stream assets index: " + path);
				}
			}
		}

		public static ExistState CheckABExistState(string path)
		{
			if (!string.IsNullOrEmpty(arknightsPersistentResPath))
			{
				if (File.Exists(FileUtil.Combine(arknightsPersistentResPath, path)))
				{
					return ExistState.InArknightsPersistentPath;
				}
			}
			if (m_streamBundles.Contains(path))
            {
				return ExistState.InArknightsStreamPath;
			}
			return File.Exists(FileUtil.Combine(persistentResPath, path)) ? ExistState.InPersistentPath : ExistState.NotExist;
		}

		public static string GetRawPath(string resPath)
		{
			if (!string.IsNullOrEmpty(arknightsPersistentResPath))
			{
				string rawPath = FileUtil.Combine(arknightsPersistentResPath, resPath);
				if (File.Exists(rawPath))
				{
					return rawPath;
				}
			}
			if (m_streamBundles.Contains(resPath))
			{
				return "jar:file://" + FileUtil.Combine(arknightsStreamResPath + '!', FileUtil.Combine("assets/AB/Android", resPath));
			}
			return FileUtil.Combine(persistentResPath, resPath);
		}

		public static string GeneratePersistentResPath()
		{
			return Application.persistentDataPath;
		}

		public enum ExistState
		{
			InArknightsPersistentPath,
			InArknightsStreamPath,
			InPersistentPath,
			NotExist
		}

		//private const string REF_INFO_NAME = "";
		private static HashSet<string> m_streamBundles = new();
		//private static Dictionary<string, HotUpdateInfo.ABInfo> m_arknightsPersistRefInfo;
	}
}