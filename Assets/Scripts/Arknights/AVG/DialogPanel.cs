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
	public class DialogPanel : ExecutorComponent
	{		
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
		
		public override Dictionary<string, Executor> GetExecutors()
		{
			return new()
            {
				["dialog"] = _ExecuteDialog,
				["aside"] = _ExecuteAside,
				["multiline"] = _ExecuteMultiline
            };
		}
		
		public override void OnStoryBegin(Story story)
		{
			base.OnStoryBegin(story);
			_SetTypeWriterDelay(null);
			controller.eventPool.On(AVGController.Event.ON_SPEED_SET, _SetTypeWriterDelay);
		}
		
		public override void OnReset()
		{
			base.OnReset();
			_SetHiddenInternal(true, true);
			_name.text = string.Empty;
			_typeWriter.OnReset();
			controller.eventPool.Remove(AVGController.Event.ON_CLICK, _OnClicked);
			controller.eventPool.Remove(AVGController.Event.ON_SPEED_SET, _SetTypeWriterDelay);
		}
		
		private void _SetTypeWriterDelay(object arg)
		{
			_typeWriter.typeWriterDelay = AVGController.instance.typeWriterDelay;
		}
		
		private void _AdjustMessagePosition(string content)
		{
		}
		
		private bool _ExecuteAside(Command command)
		{
			return default(bool);
		}
		
		private bool _ExecuteDialog(Command command)
		{
			string content = command.content;
			if (string.IsNullOrEmpty(content))
            {
				isHidden = true;
				return false;
            }
			else
            {
				isHidden = false;
				_ResetMultiline();
				content = AVGTextManager.instance.Translate(content);
				string name = AVGTextManager.instance.Translate(Command.GetOrDefault("name", string.Empty, command.TryGetParam));
				float textOver = _CalculateTextHeight(_message, content) - _messageTextMaxHeight,
					nameOver = _CalculateTextHeight(_name, name) - _nameTextMaxHeight;
				_message.rectTransform().anchoredPosition = new(m_messageOriginXPos, textOver > 0 ? m_messageOriginYPos + textOver : m_messageOriginYPos);
				RectTransform rect = _name.rectTransform();
				_name.rectTransform().anchoredPosition = new(rect.anchoredPosition.x, nameOver > 0 ? m_nameOriginYPos + nameOver : m_nameOriginYPos);
				_name.text = name;
				_typeWriter.BeginText(content, _OnTypeWriterEnd);
				controller.eventPool.On(AVGController.Event.ON_CLICK, _OnClicked);
				return true;
			}
		}
		
		private bool _ExecuteMultiline(Command command)                                                   
		{
			return default(bool);
		}
		
		private void _ResetMultiline()
		{
		}
		
		private float _CalculateTextHeight(Text textComponent, string text)
		{
			TextGenerationSettings settings = textComponent.GetGenerationSettings(textComponent.GetPixelAdjustedRect().size);
			return textComponent.cachedTextGeneratorForLayout.GetPreferredHeight(text, settings) / textComponent.pixelsPerUnit;
		}
		
		protected override void OnFinish()
		{
			base.OnFinish();
			_typeWriter.TryFinish();
			controller.eventPool.Remove(AVGController.Event.ON_CLICK, _OnClicked);
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

		private void _OnTypeWriterEnd()
		{
			AVGController.instance.RaiseAutoClick(_typeWriter.messageLength);
		}

		private void _SetHiddenInternal(bool value, bool force)
		{
			if (value != m_hidden || force)
            {
				m_hidden = value;
				if (force)
                {
					m_canvasGroup.alpha = value ? 0f : 1f;
                }
				else
                {
					m_canvasGroup.DOKill();
					Tweener tweener;
                    if (m_hidden)
                    {
                        tweener = m_canvasGroup.DOFade(0f, _hideDuration);
                    }
					else
                    {
						tweener = m_canvasGroup.DOFade(1f, _hideDuration);
                    }
					tweener.SetEase(_hideEase).SetUpdate(true).SetIgnoreTimeScale(true).Play();
                }
            }
		}

		private void Awake()
		{
			m_canvasGroup = GetComponent<CanvasGroup>();
			m_messageOriginXPos = _message.rectTransform().anchoredPosition.x;
			m_messageOriginYPos = _message.rectTransform().anchoredPosition.y;
			m_nameOriginYPos = _name.rectTransform().anchoredPosition.y;
		}

		private const float MESSAGE_ASIDE_X_POS = -86f;
		[SerializeField]
		private Text _name;
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
		private float _messageBottomPadding = 5f;
		[SerializeField]
		private float _messageTextMaxHeight = 89f;
		[SerializeField]
		private float _nameTextMaxHeight = 65f;
		private bool m_hidden = true;
		private float m_messageOriginYPos;
		private float m_messageOriginXPos;
		private float m_nameOriginYPos;
		private CanvasGroup m_canvasGroup;
		private bool m_ismultiline;
		private string m_cachedMultiline = string.Empty;
	}
}
