// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-12 12:42:59

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using RhodeIsland.Arknights;
using RhodeIsland.Arknights.Resource;
using Torappu.Resource;

namespace RhodeIsland.RemoteTerminal.Resources
{
	public class RTResourceManager : PersistentSingleton<RTResourceManager>, IResourceManager
	{
		//ADD
		[SerializeField]
		private List<string> bundlesNeedForceCache = new();

		public bool inited
		{
			get
			{
				return m_inited;
			}
		}

		public int loadedBundleCnt
		{
			get
			{
				return m_manager != null ? m_manager.activeBundleCnt : 0;
			}
		}

		public int loadedAssetCnt
		{
			get
			{
				return m_loadedAssetInstanceIdMap.Count;
			}
		}

		internal IEnumerable<string> allAssets
		{
			get
			{
				return m_assetNameToBundleInfoMap.Keys;
			}
		}

		internal IEnumerable<BundleHolder> loadedBundles
		{
			get
			{
				if (m_manager != null)
                {
					return m_manager.activeBundles.Values;
				}
				else
                {
					return new BundleHolder[0];
                }
			}
		}

		internal IEnumerable<string> loadedBundleNames
		{
			get
			{
				if (m_manager != null)
				{
					return m_manager.activeBundles.Keys;
				}
				else
				{
					return new string[0];
				}
			}
		}

		//private ResourceOptions options { get; set; }

		private ResourceIndex index { get; set; }
		private AssetBundleManifest manifest { get; set; }

		public void InitIfNot()
		{
			_DoInitIfNot(true);
		}

		public void ForceReInit()
		{
			_DoInitIfNot(false);
		}

		public string GetDebugStr()
		{
			return string.Format("Loaded bundles: {0}, assets: {1}", index.bundles.Count, index.assetToBundleList.Count);
		}

		/// <summary>
		/// NOTUSED
		/// </summary>
		public ResourceIndex LoadStreamingIndex()
		{
			return null;
		}

		internal void FetchLoadedAssets(List<string> results)
		{
			results.Clear();
            foreach (KeyValuePair<int, LoadedAssetEntry> pair in m_loadedAssetInstanceIdMap)
            {
				results.Add(pair.Value.path);
			}
		}

		public T Load<T>(string path) where T : UnityEngine.Object
		{
			path = _PreprocessAssetPath(path);
			if (!m_assetNameToBundleInfoMap.TryGetValue(path, out BundleInfo bundleInfo) || !m_manager.TryGetOrLoadBundle(bundleInfo, out BundleHolder bundle))
			{
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			T res = bundle.Load<T>(System.IO.Path.GetFileName(path));
			_OnAssetLoaded(res, bundle, path);
			if (!res)
			{
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			return res;
		}

		public UnityEngine.Object Load(string path)
		{
			path = _PreprocessAssetPath(path);
			if (!m_assetNameToBundleInfoMap.TryGetValue(path, out BundleInfo bundleInfo) || !m_manager.TryGetOrLoadBundle(bundleInfo, out BundleHolder bundle))
			{
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			UnityEngine.Object res = bundle.Load(System.IO.Path.GetFileName(path));
			_OnAssetLoaded(res, bundle, path);
			if (!res)
			{
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			return res;
		}

		public T LoadRaw<T>(string path, string asset) where T : UnityEngine.Object
		{
			path = _PreprocessAssetPath(path);
			if (!m_assetNameToBundleInfoMap.TryGetValue(path, out BundleInfo bundleInfo))
			{
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			if (!m_manager.TryGetOrLoadBundle(bundleInfo, out BundleHolder bundle))
			{
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			T res = bundle.ab.LoadAsset<T>(asset);
			if (!res)
			{
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			return res;
		}

		public AsyncResource LoadAsync<T>(string path) where T : UnityEngine.Object
		{
			path = _PreprocessAssetPath(path);
			if (!m_assetNameToBundleInfoMap.TryGetValue(path, out BundleInfo bundleInfo) || !m_manager.TryGetOrLoadBundle(bundleInfo, out BundleHolder bundle))
			{
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			AsyncResource res = bundle.LoadAsync<T>(System.IO.Path.GetFileName(path));
			_HandleAsyncResource(res, bundle, path);
			return res;
		}

		public AsyncResource LoadAsync(string path)
		{
			path = _PreprocessAssetPath(path);
			if (!m_assetNameToBundleInfoMap.TryGetValue(path, out BundleInfo bundleInfo) || !m_manager.TryGetOrLoadBundle(bundleInfo, out BundleHolder bundle))
			{
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			AsyncResource res = bundle.LoadAsync(System.IO.Path.GetFileName(path));
			_HandleAsyncResource(res, bundle, path);
			return res;
		}

		public void LoadAsync<T>(string path, Action<bool, T> cb) where T : UnityEngine.Object
		{
			StartCoroutine(_LoadAsync(path, cb));
		}

		public void LoadAsync(string path, Action<bool, UnityEngine.Object> cb)
		{
			StartCoroutine(_LoadAsync(path, cb));
		}

		public T[] LoadAll<T>(string path) where T : UnityEngine.Object
		{
			path = _PreprocessAssetPath(path);
			if (!m_assetNameToBundleInfoMap.TryGetValue(path, out BundleInfo bundleInfo))
			{
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			if (!m_manager.TryGetOrLoadBundle(bundleInfo, out BundleHolder bundle))
			{
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			T[] res = bundle.LoadAll<T>(System.IO.Path.GetFileName(path));
			if (res != null)
			{
				foreach (T item in res)
				{
					_OnAssetLoaded(item, bundle, path);
				}
			}
			else
            {
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			return res;
		}

		public UnityEngine.Object[] LoadAll(string path)
		{
			path = _PreprocessAssetPath(path);
			if (!m_assetNameToBundleInfoMap.TryGetValue(path, out BundleInfo bundleInfo))
			{
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			if (!m_manager.TryGetOrLoadBundle(bundleInfo, out BundleHolder bundle))
			{
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			UnityEngine.Object[] res = bundle.LoadAll(System.IO.Path.GetFileName(path));
			if (res != null)
			{
				foreach (UnityEngine.Object item in res)
				{
					_OnAssetLoaded(item, bundle, path);
				}
			}
			else
			{
				_LogWhenLoadBundleFailed(path, bundleInfo);
				return null;
			}
			return res;
		}

		public bool TryLoad<T>(string path, out T obj) where T : UnityEngine.Object
		{
			path = _PreprocessAssetPath(path);
			if (m_assetNameToBundleInfoMap.ContainsKey(path))
			{
				obj = Load<T>(path);
			}
			else
			{
				obj = null;
			}
			return obj;
		}

		public bool TryLoad(string path, out UnityEngine.Object obj)
		{
			path = _PreprocessAssetPath(path);
			if (m_assetNameToBundleInfoMap.ContainsKey(path))
			{
				obj = Load(path);
			}
			else
			{
				obj = null;
			}
			return obj;
		}

		public void UnloadAsset(UnityEngine.Object obj)
		{
			int instanceId = obj.GetInstanceID();
			if (m_loadedAssetInstanceIdMap.TryGetValue(instanceId, out LoadedAssetEntry entry))
            {
				if (m_assetNameToBundleInfoMap.TryGetValue(entry.path, out BundleInfo bundleInfo))
                {
					m_manager.UnloadAssetAndDecBundleRef(bundleInfo.name, obj);
                }
				if (--entry.refCnt <= 0)
                {
					m_loadedAssetInstanceIdMap.Remove(instanceId);
                }
            }
		}

		public void UnloadAssetByInstanceId(int instanceId)
		{
			if (m_loadedAssetInstanceIdMap.TryGetValue(instanceId, out LoadedAssetEntry entry))
			{
				if (m_assetNameToBundleInfoMap.TryGetValue(entry.path, out BundleInfo bundleInfo))
				{
					m_manager.UnloadAssetAndDecBundleRef(bundleInfo.name, null);
				}
				if (--entry.refCnt <= 0)
				{
					m_loadedAssetInstanceIdMap.Remove(instanceId);
				}
			}
		}

		public bool CheckExists(string path)
		{
			path = _PreprocessAssetPath(path);
			if (!m_assetNameToBundleInfoMap.TryGetValue(path, out _))
			{
				return false;
			}
			//MODIFY
			return true;
		}

		/// <summary>
		/// NOTUSED
		/// </summary>
		public AsyncOperation LoadSceneAsync(string path, LoadSceneMode mode)
		{
			return null;
		}

		/// <summary>
		/// NOTUSED
		/// </summary>
		public bool LoadScene(string path, LoadSceneMode mode)
		{
			return default(bool);
		}

		/// <summary>
		/// NOTUSED
		/// </summary>
		public void UnloadScene(string path, bool forceUnloadEvenUsed)
		{
		}

		public AsyncOperation UnloadUnusedAssets()
		{
			m_manager.UnloadUnusedBundles();
			return UnityEngine.Resources.UnloadUnusedAssets();
		}

		[DebuggerHidden]
		public IEnumerator UnloadAllAssets(bool forceUnloadEvenUsed)
		{
			m_manager.UnloadAll(forceUnloadEvenUsed);
			m_loadedAssetInstanceIdMap.Clear();
			yield return UnityEngine.Resources.UnloadUnusedAssets();
			yield return _WaitForUnfinishedAsyncResources();
		}

		/// <summary>
		/// NOTUSED
		/// </summary>
		[DebuggerHidden]
		public IEnumerator UnloadAllAssetsExcept(string[] excludedPrefixes, bool forceUnloadEvenUsed)
		{
			return null;
		}

		/// <summary>
		/// NOTUSED
		/// </summary>
		public void RegisterListener(IResourceListener listener)
		{
		}

		/// <summary>
		/// NOTUSED
		/// </summary>
		public void UnregisterListener(IResourceListener listener)
		{
		}

		[DebuggerHidden]
		private IEnumerator _LoadAsync(string path, Action<bool, UnityEngine.Object> cb)
		{
			AsyncResource resource = LoadAsync(path);
			yield return resource;
			UnityEngine.Object asset = resource.GetAsset();
			cb.Invoke(asset, asset);
		}

		[DebuggerHidden]
		private IEnumerator _LoadAsync<T>(string path, Action<bool, T> cb) where T : UnityEngine.Object
		{
			AsyncResource resource = LoadAsync<T>(path);
			yield return resource;
			T asset = resource.GetAsset<T>();
			cb.Invoke(asset, asset);
		}

		[DebuggerHidden]
		private IEnumerator _WaitForUnfinishedAsyncResources()
		{
			while (m_unfinishedAsyncResources.Count > 0)
			{
				m_tempAsyncResList.Clear();
				m_tempAsyncResList.AddRange(m_unfinishedAsyncResources);
				m_unfinishedAsyncResources.Clear();
				for (int i = 0; i < m_tempAsyncResList.Count; i++)
				{
					yield return m_tempAsyncResList[i];
				}
			}
		}

		private void _HandleAsyncResource(AsyncResource asyncRes, BundleHolder bundle, string path)
		{
			if (asyncRes != null)
            {
				if (asyncRes.keepWaiting)
                {
					bundle.AddAssetRef();
					m_unfinishedAsyncResources.Add(asyncRes);
					asyncRes.AddLoadedCallback(obj =>
					{
						m_unfinishedAsyncResources.Remove(asyncRes);
						bundle.DecAssetRef(1, true);
						_OnAssetLoaded(asyncRes.GetAsset(), bundle, path);
					});
				}
				else
                {
					_OnAssetLoaded(asyncRes.GetAsset(), bundle, path);
                }
            }
		}

		private void _OnAssetLoaded(UnityEngine.Object asset, BundleHolder bundle, string path)
		{
			if (asset)
			{
				int id = asset.GetInstanceID();
				if (!m_loadedAssetInstanceIdMap.TryGetValue(id, out LoadedAssetEntry instance))
				{
					m_loadedAssetInstanceIdMap.Add(id, new(path));
				}
				bundle.AddAssetRef();
			}
			else
			{
				m_manager.CheckBundleInvalid(bundle);
			}
		}

		private void _DoInitIfNot(bool isFirstInit)
		{
			_InitIndexAndManifest();
			_InitBundleManager();
			m_inited = true;
			// for m_listeners
			DLog.Log(string.Format("[ResourceManager] Inited ABResourceManager. Bundles: {0}, assets: {1}", index.bundles.Count, index.assetToBundleList.Count));
		}

		/*private void _InitResLangFolder(ResourceOptions options)
		{
		}*/

		/// <summary>
		/// 老版本的初始化索引
		/// </summary>
		// [Obsolete]
		private void _InitIndexAndManifest()
		{
			string key = "torappu";
			string fullPath = BundleRouter.GetRawPath(key + ".ab");
			AssetBundle manifestBundle = AssetBundle.LoadFromFile(fullPath);
			manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
			if (manifest == null)
			{
				UnityEngine.Debug.LogError("[ResourceManager] Failed to load manifest from: " + fullPath);
			}
			manifestBundle.Unload(false);
			fullPath = BundleRouter.GetRawPath(key + "_index.ab");
			AssetBundle indexBundle = AssetBundle.LoadFromFile(fullPath);
			index = indexBundle.LoadAsset<ResourceIndex>(key + "_index");
			if (index == null)
			{
				UnityEngine.Debug.LogError("[ResourceManager] Failed to load index from: " + fullPath);
			}
			else
            {
                for (int i = 0; i < index.bundles.Count; i++)
                {
					if (bundlesNeedForceCache.Contains(index.bundles[i].name))
					{
						ResourceIndex.BundleMeta meta = index.bundles[i];
						meta.isCacheable = true;
						index.bundles[i] = meta;
					}
				}
            }
			indexBundle.Unload(false);
		}

		private void _InitBundleManager()
		{
			m_manager = new(this);
			m_assetNameToBundleInfoMap.Clear();
			foreach (ResourceIndex.AssetToBundleMeta meta in index.assetToBundleList)
			{
				if (m_manager.TryGetInfo(meta.bundleName, out BundleInfo info))
				{
					m_assetNameToBundleInfoMap[meta.assetName] = info;
				}
				else
				{
					UnityEngine.Debug.LogError("[ResourceManager] Could not find assetBundle: " + meta.bundleName);
				}
			}
		}

		public static void UnloadABAsset(UnityEngine.Object asset, bool allowDestroyingAssets)
		{
			if (asset)
			{
				UnityEngine.Resources.UnloadAsset(asset);
				if (allowDestroyingAssets)
				{
					DestroyImmediate(asset, true);
				}
			}
		}

		private string _PreprocessAssetPath(string path)
		{
			path = path.ToLower();
			string extension = System.IO.Path.GetExtension(path);
			string noExtension = path[..^extension.Length];
			if (m_resLangFolder != null)
			{
				string res = FileUtil.Combine(m_resLangFolder, noExtension);
				if (m_assetNameToBundleInfoMap.ContainsKey(res))
					return res;
			}
			if (m_commonLangFolder == null)
			{
				return noExtension;
			}
			return FileUtil.Combine(m_commonLangFolder, noExtension);
		}

		private static void _LogWhenLoadBundleFailed(string path, BundleInfo info)
		{
			if (info != null)
			{
				UnityEngine.Debug.LogError("[ResourceManager] Failed to load asset: " + path);
			}
			else
			{
				UnityEngine.Debug.LogError("[ResourceManager] boudle info not found : " + path);

			}
		}

		/// <summary>
		/// NOTUSED
		/// </summary>
		private static bool _IsAuditMode()
		{
			return false;
		}

		private bool m_inited;
		private Dictionary<string, BundleInfo> m_assetNameToBundleInfoMap = new(StringComparer.OrdinalIgnoreCase);
		private Dictionary<int, LoadedAssetEntry> m_loadedAssetInstanceIdMap = new();
		private BundleManager m_manager;
		//private ListSet<IResourceListener> m_listeners = new();
		private HashSet<AsyncResource> m_unfinishedAsyncResources = new();
		private List<AsyncResource> m_tempAsyncResList = new();
		private string m_resLangFolder;
		private string m_commonLangFolder;

		public class BundleInfo
		{
			public string name;
			public bool isCacheable;
			public int sccIndex;
		}

		private class LoadedAssetEntry
		{
			public LoadedAssetEntry(string path)
			{
				this.path = path;
				refCnt = 0;
			}

			public string path;
			public int refCnt;
		}

		private class BundleManager : IEnumerable<KeyValuePair<string, BundleHolder>>, IEnumerable
		{
			public BundleManager(RTResourceManager resManager)
			{
				m_resManager = resManager;
				m_bundleNameToBundleInfoMap.Clear();
				string[] bundleNames = m_resManager.manifest.GetAllAssetBundles();
				List<ResourceIndex.BundleMeta> bundleIndexs = m_resManager.index.bundles;
				if (bundleNames.Length != bundleIndexs.Count - m_resManager.index.rawCount)
				{
					UnityEngine.Debug.LogError("[ResourceManager] Num of AssetBundle in index is not the same as manifest!");
				}
				int maxSccIndex = 0;
				foreach (ResourceIndex.BundleMeta bundleIndex in bundleIndexs)
				{
					BundleInfo bundleInfo = new();
					string bundleName = bundleIndex.name;
					int sccIndex = bundleIndex.sccIndex;
					bundleInfo.name = bundleName;
					bundleInfo.isCacheable = bundleIndex.isCacheable;
					bundleInfo.sccIndex = sccIndex;
					m_bundleNameToBundleInfoMap.Add(bundleName, bundleInfo);
					maxSccIndex = Mathf.Max(maxSccIndex, sccIndex);
				}
				m_activeBundleSCCGroups = new List<BundleHolder>[maxSccIndex];
				for (int i = 0; i < m_activeBundleSCCGroups.Length; i++)
				{
					m_activeBundleSCCGroups[i] = new();
				}
				for (int j = 0; j < bundleIndexs.Count; j++)
				{
					foreach (string dependencie in resManager.manifest.GetAllDependencies(bundleIndexs[j].name))
					{
						if (m_bundleNameToBundleInfoMap.TryGetValue(dependencie, out BundleInfo value))
						{
							value.isCacheable = false;
						}
					}
				}
			}

			public int activeBundleCnt
			{
				get
				{
					return m_activeBundlesMap.Count;
				}
			}

			public Dictionary<string, BundleHolder> activeBundles
			{
				get
				{
					return m_activeBundlesMap;
				}
			}

			private ResourceIndex index
			{
				get
				{
					return m_resManager.index;
				}
			}

			private AssetBundleManifest manifest
			{
				get
				{
					return m_resManager.manifest;
				}
			}

			/*private ResourceOptions options
			{
				get
				{
					return null;
				}
			}*/

			public bool TryGetInfo(string bundleName, out BundleInfo info)
			{
				return m_bundleNameToBundleInfoMap.TryGetValue(bundleName, out info);
			}

			public bool TryGetOrLoadBundle(BundleInfo info, out BundleHolder bundle)
			{
				bundle = GetOrLoadBundle(info);
				return bundle != null;
			}

			public BundleHolder GetOrLoadBundle(BundleInfo info)
			{
				if (m_activeBundlesMap.TryGetValue(info.name, out BundleHolder bundleHolder))
				{
					if (!bundleHolder.isDestroyed)
						return bundleHolder;
				}
				bundleHolder = BundleHolder.Create(info);
				if (bundleHolder != null)
				{
					m_activeBundlesMap[info.name] = bundleHolder;
					m_activeBundleSCCGroups[info.sccIndex].Add(bundleHolder);
					string[] allDependencies = manifest.GetAllDependencies(info.name);
					if (allDependencies != null)
					{
						foreach (string dependency in allDependencies)
						{
							if (m_bundleNameToBundleInfoMap.TryGetValue(dependency, out BundleInfo bundleInfo))
							{
								bundleInfo.isCacheable = false;
								BundleHolder holder = GetOrLoadBundle(bundleInfo);
								if (holder != null)
								{
									holder.AddBundleRef(info.sccIndex == bundleInfo.sccIndex);
									continue;
								}
								UnityEngine.Debug.LogError("[ResourceManager] Couldn't load dependence bundle: " + bundleInfo.name);
							}
							else
							{
								UnityEngine.Debug.LogError(string.Format("[ResourceManager] Couldn't find dependence of [{0}]: {1}", info.name, dependency));
							}
						}
					}
					bundleHolder.LateInit();
					return bundleHolder;
				}
				return null;
			}

			public void UnloadAssetAndDecBundleRef(string name, UnityEngine.Object obj)
			{
				if (m_activeBundlesMap.TryGetValue(name, out BundleHolder bundleHolder))
				{
					if (!bundleHolder.isCached)
					{
						if (obj)
						{
							UnloadABAsset(obj, false);
						}
					}
					_DecRef(bundleHolder, true, false);
				}
			}

			public void DecBundleRef(string bundleName, BundleInfo source)
			{
				if (m_activeBundlesMap.TryGetValue(bundleName, out BundleHolder bundleHolder))
				{
					_DecRef(bundleHolder, false, bundleHolder.info.sccIndex == source.sccIndex);
				}
			}

			private void _DecRef(BundleHolder bundle, bool isAssetRef, bool sameSCC)
			{
				bool suc;
				if (!bundle.isDestroyed && isAssetRef)
				{
					suc = bundle.DecAssetRef();
				}
				else
				{
					suc = bundle.DecBundleRef(sameSCC);
				}
				if (suc)
				{
					m_activeBundlesMap.Remove(bundle.name);
					m_activeBundleSCCGroups[bundle.info.sccIndex].Remove(bundle);
					string[] dependencies = manifest.GetAllDependencies(bundle.name);
					for (int i = 0; i < dependencies.Length; i++)
					{
						DecBundleRef(dependencies[i], bundle.info);
					}
				}
				else if (bundle.isDestroyed)
				{
					m_activeBundlesMap.Remove(bundle.name);
					m_activeBundleSCCGroups[bundle.info.sccIndex].Remove(bundle);
				}
			}

			public bool CheckBundleInvalid(BundleHolder bundle)
			{
				if (!bundle.isDestroyed)
                {
					if (bundle.CheckRefInvalid())
                    {
						m_activeBundlesMap.Remove(bundle.name);
						m_activeBundleSCCGroups[bundle.info.sccIndex].Remove(bundle);
						string[] allDependencies = manifest.GetAllDependencies(bundle.name);
						if (allDependencies != null)
						{
							foreach (string dependency in allDependencies)
							{
								DecBundleRef(dependency, bundle.info);
							}
						}
						return true;
					}
					else
                    {
						return false;
                    }
                }
				return true;
			}

			/// <summary>
			/// NOTUSED
			/// </summary>
			public void Unload(string bundleName, bool forceUnloadEvenUsed)
			{
			}

			public void UnloadAll(bool forceUnloadEvenUsed)
			{
                foreach (KeyValuePair<string, BundleHolder> pair in m_activeBundlesMap)
                {
					pair.Value.Unload(forceUnloadEvenUsed);
				}
				m_activeBundlesMap.Clear();
                for (int i = 0; i < m_activeBundleSCCGroups.Length; i++)
                {
					m_activeBundleSCCGroups[i].Clear();
				}
			}

			/// <summary>
			/// NOTUSED
			/// </summary>
			public void UnloadAllExcept(ICollection<string> excludedBundles, bool forceUnloadEvenUsed)
			{
			}

			public void UnloadUnusedBundles()
			{
				return;
				//TODO
                for (int i = 0; i < m_activeBundleSCCGroups.Length; i++)
                {
					List<BundleHolder> list = m_activeBundleSCCGroups[i];
					if (list.Count > 0)
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
							if (list[j].isDestroyed && list[j].refCounterExceptInnerSCC > 0)
								break;
                        }
						for (int j = 0; j < list.Count; j++)
						{
							m_activeBundlesMap.Remove(list[j].name);
							list[j].Unload(true);
						}
						list.Clear();
					}
				}
			}

			public IEnumerator<KeyValuePair<string, BundleHolder>> GetEnumerator()
			{
				return null;
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}

			private static List<BundleHolder> s_tempBundleList;
			private RTResourceManager m_resManager;
			private Dictionary<string, BundleInfo> m_bundleNameToBundleInfoMap = new();
			private Dictionary<string, BundleHolder> m_activeBundlesMap = new();
			private List<BundleHolder>[] m_activeBundleSCCGroups;
		}
	}
}