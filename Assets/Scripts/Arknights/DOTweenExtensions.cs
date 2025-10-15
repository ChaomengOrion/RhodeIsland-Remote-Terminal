// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using DG.Tweening;
//using DG.Tweening.Core.Surrogates;
using UnityEngine;
using UnityEngine.UI;

namespace RhodeIsland.Arknights
{
	public static class DOTweenExtensions
	{
		public static Tweener DOColorWithoutAlpha(this Image target, Color endValue, float duration)
		{
			return null;
		}
		public static T SetIgnoreTimeScale<T>(this T tweener, bool ignoreTimeScale) where T : Tween
		{
			return tweener.SetUpdate(ignoreTimeScale);
		}
		public static Tweener DoScrollHorzTo(this ScrollRect target, float horzPos, float duration)
		{
			return null;
		}
		public static Tweener DoScrollVertTo(this ScrollRect target, float vertPos, float duration)
		{
			return null;
		}
	}
}
