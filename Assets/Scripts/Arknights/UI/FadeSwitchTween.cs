// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using UnityEngine;
//using XLua;

namespace RhodeIsland.Arknights.UI
{
	public class FadeSwitchTween : UISwitchTween
	{
		public FadeSwitchTween(CanvasGroup alphaHandler, bool ignoreTimeScale)
		{
		}

		public FadeSwitchTween(CanvasGroup alphaHandler, float duration = 0.16f, bool ignoreTimeScale = true)
		{
		}

		public float duration
		{
			get
			{
				return default(float);
			}
			set
			{
			}
		}

		public CanvasGroup alphaHandler
		{
			get
			{
				return null;
			}
		}

		protected override ITweenHandler GenerateTweenOfHide()
		{
			return null;
		}

		protected override ITweenHandler GenerateTweenOfShow()
		{
			return null;
		}

		protected sealed override void BeforeShowEffect()
		{
		}

		protected sealed override void AfterHideEffect()
		{
		}

		protected sealed override void ResetToState(bool isShow)
		{
		}

		protected virtual void SetObjectActive(CanvasGroup alphaHandler, bool isActive)
		{
		}

		public void Release()
		{
		}

		public const float DEFAULT_TWEEN_DURATION = 0.16f;

		private CanvasGroup m_alphaHandler;

		private bool m_ignoreTimeScale;
	}
}
