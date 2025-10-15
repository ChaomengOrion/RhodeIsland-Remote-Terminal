// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using DG.Tweening;
//using XLua;

namespace RhodeIsland.Arknights.UI
{
	public abstract class UISwitchTween// : IHotfixable, ILuaCallCSharp
	{
		protected UISwitchTween()
		{
		}

		protected abstract ITweenHandler GenerateTweenOfShow();

		protected abstract ITweenHandler GenerateTweenOfHide();

		protected virtual void BeforeShowEffect()
		{
		}

		protected virtual void BeforeHideEffect()
		{
		}

		protected virtual void AfterShowEffect()
		{
		}

		protected virtual void AfterHideEffect()
		{
		}

		protected virtual void ResetToState(bool isShow)
		{
		}

		public void Show()
		{
		}

		public void Hide()
		{
		}

		public void SetOptions(UISwitchTween.Options options)
		{
		}

		public bool isTweening
		{
			get
			{
				return default(bool);
			}
		}

		public bool isShow
		{
			get
			{
				return default(bool);
			}
			set
			{
			}
		}

		public void Reset(bool isShow)
		{
		}

		protected void ClearTween()
		{
		}

		private Options m_options;

		private ITweenHandler m_tween;

		private bool m_isInited;

		private bool m_isShowing;

		public struct Options
		{
			public Action onShown
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			public Action onHiden
			{
				get
				{
					return null;
				}
				set
				{
				}
			}
		}

		public interface ITweenHandler// : IHotfixable
		{
			ITweenHandler SetAutoKill(bool autoKill);

			ITweenHandler OnComplete(TweenCallback callback);

			bool IsPlaying();

			void KillIfNecessary();
		}

		public class TweenWrapper : ITweenHandler//, IHotfixable
		{
			public TweenWrapper(Tween tween)
			{
			}

			public bool IsActive()
			{
				return default(bool);
			}

			public bool IsPlaying()
			{
				return default(bool);
			}

			public void KillIfNecessary()
			{
			}

			public ITweenHandler OnComplete(TweenCallback callback)
			{
				return null;
			}

			public ITweenHandler SetAutoKill(bool autoKill)
			{
				return null;
			}

			private Tween m_tween;
		}
	}
}
