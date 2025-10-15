// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using UnityEngine;

namespace RhodeIsland.Arknights.Resource
{
	public abstract class AbstractAssetLoader : IDisposable
	{
		protected AbstractAssetLoader()
		{
		}

		public virtual T Load<T>(string path) where T : UnityEngine.Object
		{
			if (!TryGetAsset(path, out T asset))
            {
				asset = ResourceManager.Load<T>(path);
            }
			return asset;
		}

		public UnityEngine.Object Load(string path)
		{
			return null;
		}

		public virtual T[] LoadAll<T>(string path) where T : UnityEngine.Object
		{
			return null;
		}

		public UnityEngine.Object[] LoadAll(string path)
		{
			return null;
		}

		public AsyncResource LoadAsync<T>(string path) where T : UnityEngine.Object
		{
			return null;
		}

		public AsyncResource LoadAsync(string path)
		{
			return null;
		}

		public void LoadAsync<T>(string path, Action<bool, T> cb) where T : UnityEngine.Object
		{
		}

		public void LoadAsync(string path, Action<bool, UnityEngine.Object> cb)
		{
		}

		public bool TryLoad<T>(string path, out T obj) where T : UnityEngine.Object
        {
            if (!TryGetAsset(path, out obj))
            {
				if (ResourceManager.TryLoadAsset(path, out obj))
                {
					OnAssetLoaded(path, obj);
					return true;
                }
				else
                {
					return false;
                }
            }
			return true;
        }

        public bool TryLoad(string path, out UnityEngine.Object obj)
		{
			obj = default;
			return default(bool);
		}

		public void Unload(UnityEngine.Object asset)
		{
		}

		public void UnloadByInstanceId(int instanceId)
		{
		}

		public abstract void ClearAll();

		protected abstract void OnAssetLoaded(string path, UnityEngine.Object asset);

		protected abstract void OnAssetUnloading(UnityEngine.Object asset);

		protected virtual bool TryGetAsset<T>(string path, out T asset) where T : UnityEngine.Object
		{
			asset = null;
			return false;
		}

		protected virtual bool TryGetAsset(string path, out UnityEngine.Object asset)
		{
			asset = null;
			return false;
		}

		protected virtual bool TryGetAssets<T>(string path, out T[] assets) where T : UnityEngine.Object
		{
			assets = default;
			return default(bool);
		}

		protected virtual bool TryGetAssets(string path, out UnityEngine.Object[] assets)
		{
			assets = default;
			return default(bool);
		}

		public virtual void Dispose()
		{
		}
	}
}
