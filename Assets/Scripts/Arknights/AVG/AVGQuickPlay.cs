// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:49

using System;
using UnityEngine;
using UnityEngine.UI;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGQuickPlay : MonoBehaviour
	{
		public AVGQuickPlay()
		{
		}
		public AVGQuickPlay.State state
		{
			get
			{
				return AVGQuickPlay.State.NONE;
			}
		}
		public void OnDragAction(Vector2 pos)
		{
		}
		public void SetSpeedBtn(string name)
		{
		}
		private void _SetSpeedBtn()
		{
		}
		public void SetStatus(bool flag, int pos = 0)
		{
		}
		private void Update()
		{
		}
		private const float X_POS = 550f;
		private const float DEFAULT_HEIGHT = 326f;
		private const float SMALL_HEIGHT = 118f;
		[SerializeField]
		private GameObject _slider;
		[SerializeField]
		private GameObject _speedBtn;
		[SerializeField]
		private Image _sliderFill;
		[SerializeField]
		private int _holdTime;
		[SerializeField]
		private AVGQuickPlayBtn[] _speedBtns;
		[SerializeField]
		private RectTransform _speedBtnBG;
		[SerializeField]
		private CanvasGroup _btnsCanvasGroup;
		private DateTime m_startTime;
		private AVGQuickPlay.State m_state;
		private int m_speed;
		public enum State
		{
			NONE,
			COUNT,
			QUICK_PLAY
		}
	}
}
