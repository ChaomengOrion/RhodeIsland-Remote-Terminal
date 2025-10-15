// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using UnityEngine;
using RhodeIsland.Arknights;
using RhodeIsland.Arknights.Resource;

namespace RhodeIsland.RemoteTerminal.Resources
{
	internal class BundleHolder : BundleRef
	{
		public BundleHolder(AssetBundle ab, RTResourceManager.BundleInfo info)
		{
			this.ab = ab;
			this.info = info;
			string[] allScenePaths = ab.GetAllScenePaths();
			isSceneBundle = allScenePaths != null && allScenePaths.Length > 0;
		}

		public AssetBundle ab { get; private set; }

		public RTResourceManager.BundleInfo info { get; private set; }

		public bool isCached
		{
			get
			{
				return ab == null;
			}
		}

		public bool isSceneBundle { get; private set; }

		public bool isDestroyed { get; private set; }

		public string name
		{
			get
			{
				return info.name;
			}
		}

		public static BundleHolder Create(RTResourceManager.BundleInfo info)
		{
			string fullPath = BundleRouter.GetRawPath(info.name);
			AssetBundle bundle = AssetBundle.LoadFromFile(fullPath);
			if (bundle == null)
            {
				Debug.LogError("[ResourceManager] Can not load assetbundle: " + fullPath);
				return null;
            }
			else
            {
				return new BundleHolder(bundle, info);
            }
		}

		public void LateInit(/*ResourceOptions options*/)
		{
			_TryCacheAssets();
		}

		public T Load<T>(string assetName) where T : Object
		{
			//DLog.Log("load: " + assetName + ", isCached: " + isCached + ", hasCached: " + (m_cachedAssets != null));
			if (!typeof(Component).IsAssignableFrom(typeof(T)))
            {
				if (!isCached)
                {
					return _LoadAssetFromAB<T>(assetName);
                }
				if (m_cachedAssets != null)
                {
					return _LoadAssetFromCache<T>(assetName);
				}
				return null;
			}
			GameObject obj = Load<GameObject>(assetName);
			if (!obj)
            {
				return null;
			}
			return obj.GetComponent<T>();
		}

		public Object Load(string assetName)
		{
			if (!isCached)
			{
				return _LoadAssetFromAB(assetName);
			}
			if (m_cachedAssets != null)
			{
				return _LoadAssetFromCache(assetName);
			}
			return null;
		}

		public AsyncResource LoadAsync<T>(string assetName) where T : Object
		{
			AsyncResource res = null;
			if (typeof(Component).IsAssignableFrom(typeof(T)))
			{
				if (isCached)
				{
					res =  new(_LoadAssetFromCache<GameObject>(assetName));
				}
				else
                {
					if (!ab.isStreamedSceneAssetBundle)
						res = new(ab.LoadAssetAsync<GameObject>(assetName));
                }
			}
			else if (isCached)
			{
				res = new(_LoadAssetFromCache<T>(assetName));
			}
			else
			{
				if (!ab.isStreamedSceneAssetBundle)
					res = new(ab.LoadAssetAsync<T>(assetName));
			}
			return res;
		}

		public AsyncResource LoadAsync(string assetName)
		{
			AsyncResource res = null;
			if (isCached)
			{
				res = new(_LoadAssetFromCache(assetName));
			}
			else
			{
				if (!ab.isStreamedSceneAssetBundle)
					res = new(ab.LoadAssetAsync(assetName));
			}
			return res;
		}

		public T[] LoadAll<T>(string assetName) where T : Object
		{
			if (isCached)
			{
				System.Collections.Generic.List<T> list = new();
				for (int i = 0; i < m_cachedAssets.Length; i++)
				{
					if (m_cachedAssets[i].name.ToLower() == assetName)
					{
						if (m_cachedAssetsWithSubAssets[i].IsNullOrEmpty())
						{
							if (typeof(T).IsAssignableFrom(m_cachedAssets[i].GetType()))
							{
								list.Add((T)m_cachedAssets[i]);
							}
						}
						else
						{
                            foreach (Object obj in m_cachedAssetsWithSubAssets[i])
                            {
								if (typeof(T).IsAssignableFrom(obj.GetType()))
								{
									list.Add((T)obj);
								}
							}
						}
					}
				}
				return list.ToArray();
			}
			T[] objects = ab.LoadAssetWithSubAssets<T>(assetName);
			if (objects.IsNullOrEmpty())
			{
				T obj = ab.LoadAsset<T>(assetName);
				if (!obj)
					return new T[0];
				objects = new T[1];
				objects[0] = obj;
			}
			return objects;
		}

		public Object[] LoadAll(string assetName)
		{
			if (isCached)
            {
				System.Collections.Generic.List<Object> list = new();
				for (int i = 0; i < m_cachedAssets.Length; i++)
                {
					if (m_cachedAssets[i].name.ToLower() == assetName)
                    {
						if (m_cachedAssetsWithSubAssets[i].IsNullOrEmpty())
                        {
							list.Add(m_cachedAssets[i]);
                        }
						else
                        {
							list.AddRange(m_cachedAssetsWithSubAssets[i]);
						}
					}
                }
				return list.ToArray();
            }
			Object[] objects = ab.LoadAssetWithSubAssets(assetName);
			if (objects.IsNullOrEmpty())
            {
				Object obj = ab.LoadAsset(assetName);
				if (!obj)
					return new Object[0];
				objects = new Object[1];
				objects[0] = obj;
            }
			return objects;
		}

		public void Unload(bool unloadAllLoadedAssets)
		{
			if (!isDestroyed)
            {
				if (isCached)
                {
					if (m_cachedAssets != null)
                    {
						if (unloadAllLoadedAssets)
                        {
                            for (int i = 0; i < m_cachedAssets.Length; i++)
                            {
								RTResourceManager.UnloadABAsset(m_cachedAssets[i], unloadAllLoadedAssets);
                            }
                        }
                    }
                }
				else
                {
					ab.Unload(unloadAllLoadedAssets);
                }
				m_cachedAssets = null;
				ab = null;
				isDestroyed = true;
            }
		}

		protected override void OnDestroy()
		{
			Unload(true);
		}

		private bool _TryCacheAssets(/*ResourceOptions options*/)
		{
			if (!info.isCacheable)
            {
				return false;
            }
			if (isSceneBundle)
            {
				return false;
            }
			m_cachedAssets = ab.LoadAllAssets();
            for (int i = 0; i < m_cachedAssets.Length; i++)
            {
				if (_CheckStreamingAsset(m_cachedAssets[i]))
                {
					m_cachedAssets = null;
					m_cachedAssetsWithSubAssets = null;
					return false;
                }
            }
			m_cachedAssetsWithSubAssets = new Object[m_cachedAssets.Length][];
			for (int i = 0; i < m_cachedAssets.Length; i++)
            {
				if (m_cachedAssets[i])
                {
					m_cachedAssetsWithSubAssets[i] = ab.LoadAssetWithSubAssets(m_cachedAssets[i].name);
				}
            }
			ab.Unload(false);
			ab = null;
			return true;
		}

		private bool _CheckStreamingAsset(Object obj)
		{
			if (obj is AudioClip clip)
            {
				if (clip.loadType == AudioClipLoadType.Streaming)
					return true;
				return false;
            }
			//TODO
			return false;
		}

		private T _LoadAssetFromAB<T>(string name) where T : Object
		{
			if (ab.isStreamedSceneAssetBundle)
            {
				return null;
			}
			T asset = ab.LoadAsset<T>(name);
			if (!asset)
            {
				T[] assets = ab.LoadAssetWithSubAssets<T>(name);
				if (assets.IsNullOrEmpty())
                {
					return null;
                }
				return assets[0];
			}
			return asset;
		}

		private Object _LoadAssetFromAB(string name)
		{
			if (ab.isStreamedSceneAssetBundle)
			{
				return null;
			}
			return ab.LoadAsset(name);
		}

		private T _LoadAssetFromCache<T>(string name) where T : Object
		{
			for (int i = 0; i < m_cachedAssets.Length; i++)
			{
				if (m_cachedAssets[i].name.ToLower() == name)
				{
					if (!typeof(T).IsAssignableFrom(m_cachedAssets[i].GetType()))
                    {
						if (m_cachedAssetsWithSubAssets[i] != null)
                        {
							foreach (Object item in m_cachedAssetsWithSubAssets[i])
							{
								if (typeof(T).IsAssignableFrom(item.GetType()))
								{
									return (T)item;
								}
							}
                        }
						return null;
                    }
					return (T)m_cachedAssets[i];
				}
			}
			return null;
		}

		private Object _LoadAssetFromCache(string name)
		{
            for (int i = 0; i < m_cachedAssets.Length; i++)
            {
				if (m_cachedAssets[i].name.ToLower() == name)
				{
					return m_cachedAssets[i];
				}
			}
			return null;
		}

		private Object[] m_cachedAssets;
		private Object[][] m_cachedAssetsWithSubAssets;
	}
}
