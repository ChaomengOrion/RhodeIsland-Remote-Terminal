// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using UnityEngine;

namespace RhodeIsland.Arknights
{
	public static class InputUtil
	{
		public static bool GetCursorScreenPos(int pointerId, out Vector2 screenPoint)
		{
			return _GetTouchPoint(pointerId, out screenPoint) || _GetMousePoint(pointerId, out screenPoint);
		}

		private static bool _GetTouchPoint(int pointerId, out Vector2 screenPoint)
		{
			screenPoint = Vector2.zero;
			Touch[] touches = Input.touches;
			for (int i = 0; i < touches.Length; i++)
            {
				if (touches[i].fingerId == pointerId)
                {
					screenPoint = touches[i].position;
					return true;
                }
            }
			return false;
		}

		private static bool _GetMousePoint(int pointerId, out Vector2 screenPoint)
		{
			screenPoint = Vector2.zero;
			if (pointerId < 0 && Input.GetMouseButton(-pointerId - 1))
            {
				screenPoint = Input.mousePosition;
				return true;
            }
			return false;
		}
	}
}