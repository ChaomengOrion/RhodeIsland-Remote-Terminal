// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-04-26 12:43:48

using System;
using UnityEngine;

public static class ComponentExtensions
{
	public static RectTransform rectTransform(this Component cp)
	{
		Transform transform = cp.transform;
		if (transform != null)
        {
			if (transform is RectTransform rectTransform)
            {
				return rectTransform;
            }				
        }
		return null;
	}

	public static float Remap(this float value, float from1, float to1, float from2, float to2)
	{
		return 1f;
	}
}
