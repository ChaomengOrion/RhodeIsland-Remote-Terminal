// Created by ChaomengOrion
// Create at 2022-08-13 14:22:03
// Last modified on 2022-08-13 14:23:06

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Torappu.Fx
{
	public class FxDelay : MonoBehaviour//, IHotfixable
	{
		public FxDelay()
		{
		}

		public float playbackSpeed
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		private void OnEnable()
		{
		}

		public void ForceToEnd()
		{
		}

		public void OnRecycle()
		{
		}

		private void _DelayFunc()
		{
		}

		private const string DELAY_FUNC = "_DelayFunc";

		[SerializeField]
		private float _delayTime;

		private float m_playbackSpeed;

		private bool m_isWaiting;
	}
}