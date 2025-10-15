// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using UnityEngine;
using UnityEngine.UI;

namespace RhodeIsland.Arknights
{
	public static class GUIUtils
	{
		public static void SetAlpha(this Graphic image, float alpha)
		{
			Color color = image.color;
			image.color = new(color.r, color.g, color.b, alpha);
		}

		public static void SetColorWithoutAlpha(this Graphic image, Color color)
		{
			float alpha = image.color.a;
			image.color = new(color.r, color.g, color.b, alpha);
		}

		public static void AssignLocalSettings(this RectTransform target, RectTransform source)
		{
			target.sizeDelta = source.sizeDelta;
			target.pivot = source.pivot;
			target.localScale = source.localScale;
			target.localRotation = source.localRotation;
			target.localPosition = source.localPosition;
		}

		public static Toggle GetActive(this ToggleGroup group)
		{
			return null;
		}

		public static void ForceUpdate(this ScrollRect scrollRect)
		{
		}
	}
}
