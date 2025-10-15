// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using UnityEngine;

namespace RhodeIsland.Arknights.Resource
{
	public class AsyncResource : CustomYieldInstruction
	{
		public AsyncResource(ResourceRequest resRequest)
		{
			m_resRequest = resRequest;
		}

		public AsyncResource(AssetBundleRequest abRequest)
		{
			m_abRequest = abRequest;
		}

		public AsyncResource(UnityEngine.Object asset)
		{
			m_asset = asset;
		}

		public override bool keepWaiting
		{
			get
			{
				if (m_resRequest != null)
                {
					if (!m_resRequest.isDone)
						return true;
					_OnLoadFinished();
					return false;
                }
				if (m_abRequest != null)
				{
					if (!m_abRequest.isDone)
						return true;
					_OnLoadFinished();
					return false;
				}
				_OnLoadFinished();
				return false;
			}
		}

		public bool isDone
		{
			get
			{
				return !keepWaiting;
			}
		}

		public void AddLoadedCallback(Action<UnityEngine.Object> onLoaded)
		{
			if (m_isLoaded)
            {
				onLoaded.Invoke(GetAsset());
            }
			else
            {
				m_onLoaded += onLoaded;
            }
		}

		public virtual UnityEngine.Object GetAsset()
		{
			if (!keepWaiting)
            {
				if (m_resRequest != null)
                {
					return m_resRequest.asset;
                }
				else
                {
					if (m_abRequest != null)
						return m_abRequest.asset;
					else
						return m_asset;
                }
            }
			return null;
		}

		public T GetAsset<T>() where T : UnityEngine.Object
		{
			return (T)GetAsset();
		}

		public TComp GetComponent<TComp>() where TComp : Component
		{
			UnityEngine.Object asset = GetAsset();
			GameObject obj = asset as GameObject;
			if (!obj)
            {
				return (TComp)asset;
            }
			// TODO (?)
			return obj.GetComponent<TComp>();
		}

		private void _OnLoadFinished()
		{
			if (!m_isLoaded)
            {
				m_isLoaded = true;
				if (m_onLoaded != null)
                {
					m_onLoaded.Invoke(GetAsset());
					m_onLoaded = null;
                }
            }
		}

		private Action<UnityEngine.Object> m_onLoaded;

		private UnityEngine.Object m_asset;

		private ResourceRequest m_resRequest;

		private AssetBundleRequest m_abRequest;

		private bool m_isLoaded;
	}
}
