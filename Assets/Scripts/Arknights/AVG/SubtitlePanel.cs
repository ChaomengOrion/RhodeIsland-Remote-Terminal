// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	public class SubtitlePanel : ExecutorComponent
	{
		public override Dictionary<string, Executor> GetExecutors()
		{
			return new()
            {
				["subtitle"] = _ExecuteSubtitle
            };
		}

		public bool isHidden
		{
			get
			{
				return m_hidden;
			}
			private set
			{
				_SetHiddenInternal(value, false);
			}
		}

		public bool isTyping
		{
			get
			{
				return _typeWriter.isTyping;
			}
		}

		private void Awake()
		{
			m_CanvasGroup = GetComponent<CanvasGroup>();
			_message.text = string.Empty;
		}

		protected bool _ExecuteSubtitle(Command command)
		{
			string text = AVGTextManager.instance.Translate(Command.GetOrDefault("text", string.Empty, command.TryGetParam));
			if (string.IsNullOrEmpty(text))
            {
				isHidden = true;
				return false;
            }
			float x = Command.GetOrDefault("x", 0f, command.TryGetParam);
			float y = Command.GetOrDefault("y", 0f, command.TryGetParam);
			float width = Command.GetOrDefault("width", 1280f, command.TryGetParam);
			int size = Command.GetOrDefault("size", 24, command.TryGetParam);
			string alignment = Command.GetOrDefault("alignment", "left", command.TryGetParam);
			if (y > SCREEN_HEIGHT || x < 0f)
            {
				return false;
            }
			if (x <= SCREEN_WIDTH && y >= 0f)
            {
				_textTransform.anchoredPosition = new(x, -y);
				_textTransform.sizeDelta = new(width > SCREEN_WIDTH ? SCREEN_WIDTH - x : width, 0f);
				if (alignment != null)
                {
					if (alignment == "center")
                    {
						_message.alignment = TextAnchor.UpperCenter;
                    }
					if (alignment == "right")
                    {
						_message.alignment = TextAnchor.UpperRight;
                    }
				}
				_message.alignment = TextAnchor.UpperLeft;
				_message.fontSize = size;
				isHidden = false;
				_typeWriter.BeginText(text, _OnTypeWriterEnd);
				controller.eventPool.On(AVGController.Event.ON_CLICK, _OnClicked);
				return true;
			}
			return false;
		}

		private void _OnTypeWriterEnd()
		{
			if (_textTransform.sizeDelta.y > 720f)
            {
				Debug.LogError(string.Format("[AVG.Subtitle] Subtitle Text Overflow: \"{0}\".", _message.text));
            }
			AVGController.instance.RaiseAutoClick(_typeWriter.messageLength);
		}

		protected override void ForceCommandEnd()
		{
			_typeWriter.TryFinish();
		}
         
		protected virtual void _OnClicked(object arg)
		{
			if (isTyping)
			{
				_typeWriter.TryFinish();
			}
			else
			{
				this.InvokeEndOfFrame(FinishCommand);
			}
		}

		public override void OnStoryBegin(Story story)
		{
			base.OnStoryBegin(story);
			_SetTypeWriterDelay(null);
			controller.eventPool.On(AVGController.Event.ON_SPEED_SET, _SetTypeWriterDelay);
		}

		protected override void OnFinish()
		{
			base.OnFinish();
			_typeWriter.TryFinish();
			controller.eventPool.Remove(AVGController.Event.ON_CLICK, _OnClicked);
		}

		public override void OnReset()
		{
			base.OnReset();
			_SetHiddenInternal(true, true);
			_typeWriter.OnReset();
			controller.eventPool.Remove(AVGController.Event.ON_CLICK, _OnClicked);
			controller.eventPool.Remove(AVGController.Event.ON_SPEED_SET, _SetTypeWriterDelay);
		}
         
		private void _SetTypeWriterDelay(object arg)
		{
			_typeWriter.typeWriterDelay = AVGController.instance.typeWriterDelay;
		}
         
		private void _SetHiddenInternal(bool value, bool force)
		{
			if (value != m_hidden || force)
			{
				m_hidden = value;
				if (force)
				{
					m_CanvasGroup.alpha = value ? 0f : 1f;
				}
				else
				{
					m_CanvasGroup.DOKill();
					Tweener tweener;
					if (m_hidden)
					{
						tweener = m_CanvasGroup.DOFade(0f, _hideDuration);
					}
					else
					{
						tweener = m_CanvasGroup.DOFade(1f, _hideDuration);
					}
					tweener.SetEase(_hideEase).SetUpdate(true).SetIgnoreTimeScale(true).Play();
				}
			}
		}
      
		private const float SCREEN_WIDTH = 1280f;
      
		private const float SCREEN_HEIGHT = 720f;

		[SerializeField]
		private AVGTypeWriterText _typeWriter;

		[SerializeField]
		private float _hideDuration = 0.15f;
         
		[SerializeField]
		private float _autoWaitBaseTime = 1f;

		[SerializeField]
		private float _autoWaitTimePerText = 0.05f;
         
		[SerializeField]
		private Ease _hideEase = Ease.Linear;
         
		[SerializeField]
		private Text _message;
         
		[SerializeField]
		private RectTransform _textTransform;

		private bool m_hidden = true;

		private CanvasGroup m_CanvasGroup;
	}
}
