// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using XLua;
using DG.Tweening;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGBlockerPanel : ExecutorComponent, IFadeTimeRatio
	{
		public override Dictionary<string, Executor> GetExecutors()
		{
			return new()
            {
				["blocker"] = _ExecuteBlocker
            };
		}

		public override void OnReset()
		{
			base.OnReset();
			_blocker.color = new(0f, 0f, 0f, 0f);
			gameObject.SetActive(false);
		}

		private bool _ExecuteBlocker(Command command)
		{
			Color color = _blocker.color;
			Color start = new(
				command.param.GetFloat("rfrom", color.r),
				command.param.GetFloat("gfrom", color.g),
				command.param.GetFloat("bfrom", color.b),
				command.param.GetFloat("afrom", color.a)
				);
			float fadetime = CalculateFadetime(command.param.GetFloat("fadetime", _defaultFadetime));
			Color end = new(
				command.param.GetFloat("r", 0f),
				command.param.GetFloat("g", 0f),
				command.param.GetFloat("b", 0f),
				command.param.GetFloat("a", 1f)
				);
			_blocker.color = start;
			gameObject.SetActive(true);
			_blocker.DOKill();
			if (MathUtil.IsZero(fadetime))
            {
				_blocker.color = end;
				return false;
            }
			else
            {
				_blocker.DOColor(end, fadetime).OnComplete(FinishCommand).SetIgnoreTimeScale(true).Play();
				return command.param.GetBool("block", false);

			}
		}

		//EMPTY
		protected override void ForceCommandEnd() { }

		protected override void OnFinish()
		{
			base.OnFinish();
			float a = _blocker.color.a;
			if (MathUtil.IsZero(a))
            {
				gameObject.SetActive(false);
            }
		}

		public float CalculateFadetime(float initialFadetime)
		{
			return initialFadetime * AVGController.instance.animateRatio;
		}

		public bool NeedSkipAnimation(float fadetime)
		{
			return default(bool);
		}

		[SerializeField]
		private Image _blocker;
		[SerializeField]
		private float _defaultFadetime = 0.4f;
	}
}
