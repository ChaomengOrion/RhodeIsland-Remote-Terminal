// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGAutoButton : MonoBehaviour
	{
		public AVGAutoButton()
		{
		}

		public void SetPos(bool hasSkipBtn)
		{
		}

		public void SetIsAutoPlaying(AVGStoryCache.AVGAutoMode autoMode)
		{
		}

		private void _StopTween()
		{
		}

		[SerializeField]
		private RectTransform _image;

		[SerializeField]
		private GameObject _text;

		[SerializeField]
		private float _noPlayWidth;

		[SerializeField]
		private float _defaultWidth;

		[SerializeField]
		private float _stepWidth;

		[SerializeField]
		private float _stepCount;

		[SerializeField]
		private float _stepTime;

		[SerializeField]
		private Vector2 _defaultPos;

		[SerializeField]
		private Vector2 _posWithoutSkipBtn;

		private Tweener m_tweener;
	}
}
