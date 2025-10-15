// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RhodeIsland.Arknights.Resource
{
	public static class ResourceManager
	{
		private static IResourceManager instance
		{
			get
			{
				if (s_instance == null)
                {
					s_instance = RemoteTerminal.Resources.RTResourceManager.instance;
				}
				return s_instance;
			}
		}

		public static bool inited
		{
			get
			{
				return instance.inited;
			}
		}

		public static void InitIfNot()
		{
			instance.InitIfNot();
		}

		public static void ForceReInit()
		{
			instance.ForceReInit();
		}

		public static string GetDebugStr()
		{
			return instance.GetDebugStr();
		}

		public static T Load<T>(string path) where T : UnityEngine.Object
		{
			return instance.Load<T>(path);
		}

		public static UnityEngine.Object Load(string path)
		{
			return instance.Load(path);
		}

		public static T LoadRaw<T>(string path, string asset) where T : UnityEngine.Object
        {
			return instance.LoadRaw<T>(path, asset);
        }

		public static AsyncResource LoadAsync<T>(string path) where T : UnityEngine.Object
		{
			return instance.LoadAsync<T>(path);
		}

		public static AsyncResource LoadAsync(string path)
		{
			return instance.LoadAsync(path);
		}

		public static void LoadAsync<T>(string path, Action<bool, T> cb) where T : UnityEngine.Object
		{
			instance.LoadAsync<T>(path);
		}

		public static void LoadAsync(string path, Action<bool, UnityEngine.Object> cb)
		{
			instance.LoadAsync(path);
		}

		public static T[] LoadAll<T>(string path) where T : UnityEngine.Object
		{
			return instance.LoadAll<T>(path);
		}

		public static UnityEngine.Object[] LoadAll(string path)
		{
			return instance.LoadAll(path);
		}

		public static bool TryLoadAsset<T>(string path, out T obj) where T : UnityEngine.Object
		{
			return instance.TryLoad(path, out obj);
		}
		public static bool TryLoadAsset<T>(string path, out UnityEngine.Object obj)
		{
			return instance.TryLoad(path, out obj);
		}

		public static void UnloadAsset(UnityEngine.Object obj)
		{
			instance.UnloadAsset(obj);
		}

		public static void UnloadAssetByInstanceId(int instanceId)
		{
			instance.UnloadAssetByInstanceId(instanceId);
		}

		public static bool CheckExists(string path)
		{
			return instance.CheckExists(path);
		}

		public static AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
		{
			return instance.LoadSceneAsync(sceneName, mode);
		}

		public static void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
		{
			instance.LoadScene(sceneName, mode);
		}

		public static void UnloadScene(string sceneName, bool forceUnloadEvenUsed)
		{
			instance.UnloadScene(sceneName, forceUnloadEvenUsed);
		}

		public static AsyncOperation UnloadUnusedAssets()
		{
			return instance.UnloadUnusedAssets();
		}

		[DebuggerHidden]
		public static IEnumerator UnloadAllAssets(bool forceUnloadEvenUsed)
		{
			return instance.UnloadAllAssets(forceUnloadEvenUsed);
		}

		[DebuggerHidden]
		public static IEnumerator UnloadAllAssetsExcept(string[] excludedPrefixes, bool forceUnloadEvenUsed)
		{
			return instance.UnloadAllAssetsExcept(excludedPrefixes, forceUnloadEvenUsed);
		}

		public static void RegisterListener(IResourceListener listener)
		{
			instance.RegisterListener(listener);
		}

		public static void UnregisterListener(IResourceListener listener)
		{
			instance.UnregisterListener(listener);
		}

		private static IResourceManager s_instance;
	}
}
