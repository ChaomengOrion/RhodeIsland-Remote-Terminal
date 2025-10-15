// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-07-24 14:42:07

using UnityEngine;

namespace RhodeIsland.RemoteTerminal.Resources
{
	public abstract class BundleRef
	{
		public int refCounter
		{
			get
			{
				return m_assetRef + m_outerSCCBundleRef + m_innerSCCBundleRef;
			}
		}
		public int refCounterExceptInnerSCC
		{
			get
			{
				return m_outerSCCBundleRef + m_assetRef;
			}
		}
		public void AddBundleRef(bool sameSCC, int delta = 1)
		{
			if (sameSCC)
            {
				m_innerSCCBundleRef += delta;
            }
			else
            {
				m_outerSCCBundleRef += delta;
            }
		}

		public void AddAssetRef(int delta = 1)
		{
			++m_assetRef;
		}

		public bool DecBundleRef(bool sameSCC, int delta = 1, bool dontCheck = false)
		{
			if (sameSCC)
            {
				m_innerSCCBundleRef = Mathf.Max(0, m_innerSCCBundleRef - delta);
            }
			else
            {
				m_outerSCCBundleRef = Mathf.Max(0, m_outerSCCBundleRef - delta);
			}
			if (dontCheck)
				return false;
			return CheckRefInvalid();
		}

		public bool DecAssetRef(int delta = 1, bool dontCheck = false)
		{
			m_assetRef = Mathf.Max(0, m_assetRef - delta);
			if (dontCheck)
				return false;
			return CheckRefInvalid();
		}

		public bool CheckRefInvalid()
		{
			if (m_assetRef + m_outerSCCBundleRef + m_innerSCCBundleRef > 0)
				return false;
			OnDestroy();
			return true;
		}

		public void ForceDestroy()
		{
			m_outerSCCBundleRef = 0;
			m_innerSCCBundleRef = 0;
			m_assetRef = 0;
			OnDestroy();
		}

		protected abstract void OnDestroy();
		private int m_outerSCCBundleRef = 0;
		private int m_innerSCCBundleRef = 0;
		private int m_assetRef = 0;
	}
}