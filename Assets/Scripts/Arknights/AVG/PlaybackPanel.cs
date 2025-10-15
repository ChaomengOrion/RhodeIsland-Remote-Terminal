// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using System.Text;
using RhodeIsland.Arknights.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	public class PlaybackPanel : ExecutorComponent, IPointerClickHandler, IEventSystemHandler
	{
		public PlaybackPanel()
		{
		}

		protected Adapter adapter
		{
			get
			{
				return null;
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
		}

		public override Dictionary<string, Executor> GetExecutors()
		{
			return new()
			{
				["dialog"] = _ExecuteDialog,
				["decision"] = _ExecuteDecision,
				["predicate"] = _ExecutePredicate,
				["subtitle"] = _ExecuteSubtitle,
				["aside"] = _ExecuteAside,
				["multiline"] = _ExecuteMultiline,
				["sticker"] = _ExecuteSticker
			};
		}

		public override void OnReset()
		{
		}

		private bool _ExecuteDialog(Command command)
		{
			return false;
		}

		private bool _ExecuteDecision(Command command)
		{
			return default(bool);
		}

		private bool _ExecutePredicate(Command command)
		{
			return default(bool);
		}

		private bool _ExecuteSubtitle(Command command)
		{
			return default(bool);
		}

		private bool _ExecuteAside(Command command)
		{
			return default(bool);
		}

		private bool _ExecuteMultiline(Command command)
		{
			return default(bool);
		}

		private void _TryEndMultilineMode()
		{
		}

		private bool _ExecuteSticker(Command command)
		{
			return default(bool);
		}

		private void _ResetLastCurrentIcon()
		{
		}

		private CanvasGroup canvasGroup
		{
			get
			{
				return null;
			}
		}

		private UISwitchTween fadeSwitchTween
		{
			get
			{
				return null;
			}
		}

		private void _UpdateShown(bool value, bool force)
		{
		}

		private void _ResetScrollSlide()
		{
		}

		public bool isShown
		{
			get
			{
				return default(bool);
			}
			set
			{
			}
		}

		public void OnCloseBtnClicked()
		{
		}

		protected override void ForceCommandEnd()
		{
		}

		private const string PARAM_NAME_OPTIONS = "options";

		private const string COMMAND_NAME_DECISION = "decision";

		private const string COMMAND_NAME_PREDICATE = "predicate";

		private const string COMMAND_SUBTITLE = "subtitle";

		private const string COMMAND_STICKER = "sticker";

		[SerializeField]
		private AVGPlaybackTextView _avgPlaybackTextView;

		[SerializeField]
		private ScrollRect _scrollView;

		[SerializeField]
		private UIRecycleLayoutGroup _content;

		[SerializeField]
		private ContentSizeFitterHelper _fitterHelper;

		[SerializeField]
		private GameObject _closeBtn;

		private CanvasGroup m_canvasGroup;

		private UISwitchTween m_playbackTween;

		private PlaybackPanel.Adapter m_innerAdapter;

		private bool m_isProcessingMultiline;

		private StringBuilder m_cachedStrBuilder;

		private AVGPlaybackTextView.Options m_multilineOption;

		private class SwitchTween : UISwitchTween
		{
			public SwitchTween(CanvasGroup alphaHandler, float duration = 0.16f)
			{
			}

			protected override UISwitchTween.ITweenHandler GenerateTweenOfHide()
			{
				return null;
			}

			protected override UISwitchTween.ITweenHandler GenerateTweenOfShow()
			{
				return null;
			}

			protected override void BeforeShowEffect()
			{
			}

			protected override void AfterHideEffect()
			{
			}

			private CanvasGroup m_alphaHandler;

			private float m_duration;
		}

		protected class Adapter : UIRecycleLayoutAdapter
		{
			public Adapter(PlaybackPanel closure)
			{
			}

			public override IList<UIRecycleLayoutAdapter.IVirtualView> GenerateViewsForRebuild()
			{
				return null;
			}

			public AVGPlaybackTextView.VirtualView GetLastCell()
			{
				return null;
			}

			public void AddCell(AVGPlaybackTextView.VirtualView view)
			{
			}

			public void NotifyViewChanged(UIRecycleLayoutAdapter.IVirtualView view)
			{
			}

			public PlaybackPanel m_closure;

			private List<AVGPlaybackTextView.VirtualView> m_cells;
		}
	}
}
