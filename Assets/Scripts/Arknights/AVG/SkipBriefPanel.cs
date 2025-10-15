// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:49

using System;
using RhodeIsland.Arknights.UI;
using UnityEngine;
using UnityEngine.UI;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	public class SkipBriefPanel : MonoBehaviour//, IHotfixable
	{
		public SkipBriefPanel()
		{
		}

		public void Reset()
		{
		}

		public void RenderBriefSkip(string chapterName, string title, string avgTag, string content)
		{
		}

		public void RenderNonBriefSkip()
		{
		}

		private CanvasGroup canvasGroup
		{
			get
			{
				return null;
			}
		}

		private FadeSwitchTween fadeSwitchTween
		{
			get
			{
				return null;
			}
		}

		private void _UpdateShown(bool value, bool force)
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

		public void OnConfirmBtnClicked()
		{
		}

		[SerializeField]
		private Text _chapterName;

		[SerializeField]
		private Text _title;

		[SerializeField]
		private Text _avgTag;

		[SerializeField]
		private Text _content;

		[SerializeField]
		private GameObject _nonBriefPanel;

		[SerializeField]
		private GameObject _briefPanel;

		[NonSerialized]
		public Action _onConfirm;

		private CanvasGroup m_canvasGroup;

		private FadeSwitchTween m_skipBriefTween;
	}
}
