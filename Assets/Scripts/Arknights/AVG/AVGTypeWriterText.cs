// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace RhodeIsland.Arknights.AVG
{
	/// <summary>
	/// AVG对话框逐字效果
	/// </summary>
	public class AVGTypeWriterText : MonoBehaviour
	{
		public bool isTyping
		{
			get
			{
				return m_typing;
			}
		}

		public string message
		{
			get
			{
				return m_message;
			}
		}

		public int messageLength
		{
			get
			{
				if (string.IsNullOrEmpty(m_message))
                {
					return 0;
                }
				return m_message.Length;
			}
		}

		protected Text text
		{
			get
			{
				if (m_text)
                {
					return m_text;
                }
				Text text = GetComponent<Text>();
				m_text = text;
				return text;
			}
		}

		public float typeWriterDelay
		{
			get
			{
				return m_typeWriterDelay;
			}
			set
			{
				m_typeWriterDelay = value;
			}
		}

		public void OnReset()
		{
			_ClearMessage();
			m_onTypeEnd = null;
		}

		public void BeginText(string message, [Optional] Action onTypeEnd)
		{
			_ClearMessage();
			m_onTypeEnd = onTypeEnd;
			m_message = message ?? string.Empty;
			m_typerTime = 0f;
			m_typing = true;
			m_onMiddle = _onMiddle;
			m_textMessageIterator = _GetTextMessageGenerator().GetEnumerator();
			m_sb.Length = 0;
			_AppendHiddenString(m_sb, message);
			m_text.text = m_sb.ToString();
		}

		public void AppendText(string message, [Optional] Action onTypeEnd)
		{
			if (!m_isMultiline)
				_ClearMessage();
			m_isMultiline = true;
			m_typing = false;
			m_onTypeEnd = onTypeEnd;
			m_message += message;
			m_typerTime = 0f;
			m_typing = true;
			m_onMiddle = _onMiddle;
			m_textMessageIterator = _GetMultilineTextMessageGenerator().GetEnumerator();
			_AppendHiddenString(m_sb, message);
			m_text.text = m_sb.ToString();
		}

		public void TryFinish()
		{
			if (m_typing)
            {
				_FinishTyping();
			}
		}

		private void _FinishTyping()
		{
			m_typing = false;
			text.text = m_message;
			if (m_isMultiline)
            {
				string str = text.text;
				m_cachedMultilineTextLenth = str.Length;
				m_sb.Length = 0;
				m_sb.Append(m_message[..m_cachedMultilineTextLenth]);
            }
			if (m_onMiddle)
            {
				if (m_rect)
                {
					LayoutRebuilder.ForceRebuildLayoutImmediate(m_rect);
					_UpdateMaxWidth();
                }
            }
			m_onTypeEnd?.Invoke();
			m_onTypeEnd = null;
		}

		private void _ClearMessage()
		{
			m_message = string.Empty;
			text.text = string.Empty;
			m_typing = false;
			m_typerTime = 0f;
			m_cachedMultilineTextLenth = 0;
			m_isMultiline = false;
		}

		[DebuggerHidden]
		private IEnumerable<string> _GetTextMessageGenerator()
		{
			int textLength = 0;
			int markStart = 0;
			List<string> labelStack = new();
			StringBuilder sb = new();
			bool readyToClose;
			bool parseLabelName = false;
			bool hasNewChar;
			string hiddenString;
			while(true)
			{
				if (textLength >= m_message.Length)
				{
					yield return m_message;
					yield break;
				}
				readyToClose = false;
				hasNewChar = false;
				do
				{
					textLength++;
					if (m_message[textLength - 1] == '<')
					{
						++markStart;
						sb.Length = 0;
						parseLabelName = true;
					}
					else
					{
						if (m_message[textLength - 1] == '>')
						{
							--markStart;
							if (readyToClose)
							{
								if (labelStack.Count <= 0)
								{
									UnityEngine.Debug.LogWarning(string.Format("[AVG] TypeWriter rich text format error\n{0}", m_message));
								}
								if (labelStack[^1] == sb.ToString())
								{
									labelStack.RemoveAt(labelStack.Count - 1);
								}
								else
								{
									UnityEngine.Debug.LogWarning(string.Format("[AVG] TypeWriter rich text format error\n{0}", m_message));
								}
							}
							else
							{
								labelStack.Add(sb.ToString());
							}
							readyToClose = false;
						}
						else
						{
							if (m_message[textLength - 1] == '/')
							{
								readyToClose = true;
							}
							else
							{
								if (m_message[textLength - 1] == '=')
								{
									parseLabelName = false;
								}
								else if (markStart <= 0)
								{
									hasNewChar = true;
								}
								else if (parseLabelName)
								{
									sb.Append(m_message[textLength - 1]);
								}
							}
						}
					}
					if (markStart <= 0 || hasNewChar)
					{
						break;
					}
				} while (textLength <= message.Length);
				m_sb.Length = 0;
				m_sb.Append(m_message[..textLength]);
				for (int i = labelStack.Count - 1; i >= 0; i--)
				{
					m_sb.Append(string.Format("</{0}>", labelStack[i]));
				}
				hiddenString = m_message[textLength..];
				_AppendHiddenString(m_sb, hiddenString);
				yield return m_sb.ToString();
			}
		}

		[DebuggerHidden]
		private IEnumerable<string> _GetMultilineTextMessageGenerator()
		{
			int markStart = 0;
			List<string> labelStack = new();
			StringBuilder sb = new();
			bool readyToClose = false;
			bool parseLabelName = false;
			bool hasNewChar = false;
			string hiddenString;

			while (m_cachedMultilineTextLenth < message.Length)
			{
				if (m_cachedMultilineTextLenth >= m_message.Length)
				{
					yield return m_message;
					yield break;
				}
				do
				{
					m_cachedMultilineTextLenth++;
					if (m_message[m_cachedMultilineTextLenth - 1] == '<')
					{
						++markStart;
						sb.Length = 0;
						parseLabelName = true;
					}
					else
					{
						if (m_message[m_cachedMultilineTextLenth - 1] == '>')
						{
							--markStart;
							if (readyToClose)
							{
								if (labelStack.Count <= 0)
								{
									UnityEngine.Debug.LogWarning(string.Format("[AVG] TypeWriter rich text format error\n{0}", m_message));
								}
								if (labelStack[^1] == sb.ToString())
								{
									labelStack.RemoveAt(labelStack.Count - 1);
								}
								else
								{
									UnityEngine.Debug.LogWarning(string.Format("[AVG] TypeWriter rich text format error\n{0}", m_message));
								}
							}
							else
							{
								labelStack.Add(sb.ToString());
							}
							readyToClose = false;
						}
						else
						{
							if (m_message[m_cachedMultilineTextLenth - 1] == '/')
							{
								readyToClose = true;
							}
							else
							{
								if (m_message[m_cachedMultilineTextLenth - 1] == '=')
								{
									parseLabelName = false;
								}
								else if (markStart <= 0)
								{
									hasNewChar = true;
								}
								else if (parseLabelName)
								{
									sb.Append(m_message[m_cachedMultilineTextLenth - 1]);
								}
							}
						}
					}
					if (markStart <= 0 || hasNewChar)
					{
						break;
					}
				} while (m_cachedMultilineTextLenth <= message.Length);
				sb.Length = 0;
				sb.Append(m_message[..m_cachedMultilineTextLenth]);
				for (int i = labelStack.Count - 1; i >= 0; i--)
				{
					sb.Append(string.Format("</{0}>", labelStack[i]));
				}
				hiddenString = m_message[m_cachedMultilineTextLenth..];
				_AppendHiddenString(m_sb, hiddenString);
				yield return m_sb.ToString();
			}
		}

		private void _UpdateMaxWidth()
		{
			if (m_onMiddle)
			{
				if (m_rect.sizeDelta.x > _maxWidth)
                {
					m_sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
					m_sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
					m_rect.sizeDelta = new(_maxWidth, m_rect.sizeDelta.y);
					m_onMiddle = false;
				}
			}
		}

		private static void _AppendHiddenString(StringBuilder sb, string hiddenString)
		{
			sb.Append("<color=#00000000>");
            bool strLock = true;
			for (int i = 0; i < hiddenString.Length; i++)
            {
				if (hiddenString[i] == '<')
                {
					strLock = false;
                }
				else
                {
					char c = hiddenString[i];
					if (c != '>')
                    {
						if (strLock)
                        {
							sb.Append(c);
                        }
                    }
					else
                    {
						strLock = true;
                    }
                }
            }
			sb.Append("</color>");
		}

		private void Awake()
		{
			m_rect = GetComponent<RectTransform>();
			m_sizeFitter = GetComponent<ContentSizeFitter>();
		}

		private void Update()
		{
			if (m_typing)
            {
				m_typerTime += _ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
			}
			_UpdateMaxWidth();
			if (m_typeWriterDelay < 0f)
            {
				m_typeWriterDelay = _typeWriterDelay;
            }
			if (m_typerTime > m_typeWriterDelay)
            {
				string cur = null;
				bool finish;
				while (true)
                {
					m_typerTime -= m_typeWriterDelay;
					if (!m_textMessageIterator.MoveNext())
					{
						finish = true;
						break;
                    }
					cur = m_textMessageIterator.Current;
					if (m_typerTime <= m_typeWriterDelay)
                    {
						finish = false;
						break;
                    }
				}
				if (cur != null)
                {
					text.text = cur;
                }
				if (finish)
                {
					_FinishTyping();
                }
            }
		}

		[SerializeField]
		private float _typeWriterDelay = 0.04f;
		[SerializeField]
		private bool _ignoreTimeScale;
		[SerializeField]
		private bool _onMiddle;
		[SerializeField]
		private float _maxWidth;
		private Text m_text;
		private string m_message = string.Empty;
		private Action m_onTypeEnd;
		private float m_typerTime;
		private bool m_typing;
		private bool m_onMiddle;
		private StringBuilder m_sb = new();
		private RectTransform m_rect;
		private ContentSizeFitter m_sizeFitter;
		private float m_typeWriterDelay = -1f;
		private IEnumerator<string> m_textMessageIterator;
		private int m_cachedMultilineTextLenth;
		private bool m_isMultiline;
	}
}