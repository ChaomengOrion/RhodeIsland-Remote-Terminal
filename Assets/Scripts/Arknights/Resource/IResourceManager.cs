// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RhodeIsland.Arknights.Resource
{
	public interface IResourceManager
	{
		bool inited { get; }
		void InitIfNot();
		void ForceReInit();
		string GetDebugStr();
		T Load<T>(string path) where T : UnityEngine.Object;
		UnityEngine.Object Load(string path);
		T[] LoadAll<T>(string path) where T : UnityEngine.Object;
		UnityEngine.Object[] LoadAll(string path);
		T LoadRaw<T>(string path, string asset) where T : UnityEngine.Object;
		AsyncResource LoadAsync<T>(string path) where T : UnityEngine.Object;
		AsyncResource LoadAsync(string path);
		void LoadAsync<T>(string path, Action<bool, T> cb) where T : UnityEngine.Object;
		void LoadAsync(string path, Action<bool, UnityEngine.Object> cb);
		bool TryLoad<T>(string path, out T obj) where T : UnityEngine.Object;
		bool TryLoad(string path, out UnityEngine.Object obj);
		void UnloadAsset(UnityEngine.Object assetToUnload);
		void UnloadAssetByInstanceId(int instanceId);
		bool CheckExists(string path);
		AsyncOperation LoadSceneAsync(string path, LoadSceneMode mode);
		bool LoadScene(string path, LoadSceneMode mode);
		void UnloadScene(string path, bool forceUnloadEvenUsed);
		AsyncOperation UnloadUnusedAssets();
		IEnumerator UnloadAllAssets(bool forceUnloadEvenUsed);
		IEnumerator UnloadAllAssetsExcept(string[] excludedPrefixes, bool forceUnloadEvenUsed);
		void RegisterListener(IResourceListener listener);
		void UnregisterListener(IResourceListener listener);
	}
}
