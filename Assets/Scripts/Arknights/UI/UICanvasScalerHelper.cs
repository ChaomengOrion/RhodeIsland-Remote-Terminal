// Created by ChaomengOrion
// Create at 2022-06-02 19:02:52
// Last modified on 2022-08-01 19:07:50

using System;
using UnityEngine;
using UnityEngine.UI;
//using XLua;

namespace RhodeIsland.Arknights.UI
{
	public class UICanvasScalerHelper : MonoBehaviour, ISafeAreaListener//, IHotfixable
	{

		public event Action<CanvasScaler> onScalerChanged
		{
			add
			{
				if (value != null)
                {
					m_onScalerChanged -= value;
					m_onScalerChanged += value;
					if (m_isInited)
                    {
						if (scaler)
                        {
							value.Invoke(scaler);
                        }
                    }
				}
			}
			remove
			{
					m_onScalerChanged -= value;
			}
		}

		public CanvasScaler scaler
		{
            get
            {
				if (m_scaler)
                {
					return m_scaler;
				}
				CanvasScaler scaler = GetComponent<CanvasScaler>();
				m_scaler = scaler;
				return scaler;
			}
		}

		public RectTransform rectTrans
		{
			get
			{
				if (m_rectTrans)
				{
					return m_rectTrans;
				}
				RectTransform rectTrans = this.rectTransform();
				m_rectTrans = rectTrans;
				return rectTrans;
			}
		}

		public Vector2 size
		{
			get
			{
				return rectTrans.rect.size;
			}
		}

		public void OnSafeRectUpdated(SafeRect rect)
		{
			_UpdateMatchMethod();
		}

		private void Start()
		{
			
		}

		private void OnDestroy()
		{
		}

		private void _UpdateMatchMethod()
		{
		}

		public static void UpdateScalerFitMode(CanvasScaler scaler)
		{
		}

		private CanvasScaler m_scaler;
		private RectTransform m_rectTrans;
		private bool m_isInited;
		private Action<CanvasScaler> m_onScalerChanged;
	}
}
