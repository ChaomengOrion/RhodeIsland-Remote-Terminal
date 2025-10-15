// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections.Generic;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	public struct AVGStoryCache : IDisposable//, IHotfixable
	{
		public void Init(bool firstRead = true, bool theaterMode = false)
		{
			m_skipNodeLabel = new();
			isTheaterMode = theaterMode;
			m_isFirstRead = firstRead;
			m_screenBlocker = ScreenUtil.UISleepBlocker.EMPTY;
			m_hasSkipNode = false;
			m_autoPlayMode = AVGAutoMode.DEFAULT;
			firstClickTime = DateTimeUtil.EMPTY_DATETIME;
		}

		public AVGAutoMode autoPlayMode
		{
			get
			{
				return m_autoPlayMode;
			}
			set
			{
				m_autoPlayMode = value;
			}
		}

		public bool isWaitForInput
		{
			get
			{
				return default(bool);
			}
			set
			{
			}
		}

		public int AVGBtnAutoMode
		{
			get
			{
				return default(int);
			}
			set
			{
			}
		}

		public int AVGQuickAutoMode
		{
			get
			{
				return default(int);
			}
			set
			{
			}
		}

		public SkipNodeLabel GetNextSkipNode()
		{
			return default(SkipNodeLabel);
		}

		public void AddSkipNode(List<Command> commands)
		{
		}

		public int SkipNodeNum
		{
			get
			{
				return default(int);
			}
		}

		public bool isFirstRead
		{
			get
			{
				return default(bool);
			}
		}

		public bool isShowBrief
		{
			get
			{
				return default(bool);
			}
		}

		private AVGSkipMode CalSkipMode(string mode)
		{
			return AVGSkipMode.CAN_SKIP;
		}

		public AVGSkipMode curSkipMode
		{
			get
			{
				return AVGSkipMode.CAN_SKIP;
			}
			set
			{
			}
		}

		public bool CheckIfSkippableInCurMode()
		{
			return default(bool);
		}

		public void ResetHasSkipNode()
		{
		}

		public void SetClickRecord(DateTime time, int times = 0)
		{
		}

		public void Dispose()
		{
		}

		private void _UpdateBlockStatus()
		{
		}

		public static readonly AVGStoryCache RESET = new()
		{
			m_screenBlocker = ScreenUtil.UISleepBlocker.EMPTY,
			firstClickTime = DateTimeUtil.EMPTY_DATETIME,
			lastClickTime = DateTimeUtil.EMPTY_DATETIME
		};

		private Queue<SkipNodeLabel> m_skipNodeLabel;

		private bool m_isFirstRead;

		private bool m_hasSkipNode;

		private ScreenUtil.UISleepBlocker m_screenBlocker;

		public DateTime firstClickTime;

		public DateTime lastClickTime;

		public int clickTimes;

		public bool isTheaterMode;

		private AVGAutoMode m_autoPlayMode;

		private bool m_isWaitForInput;

		public enum AVGAutoMode
		{
			DEFAULT,
			BUTTON_AUTO,
			QUICK_PLAY
		}
	}
}
