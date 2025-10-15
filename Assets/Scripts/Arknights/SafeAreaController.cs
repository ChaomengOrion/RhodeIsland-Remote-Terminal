// Created by ChaomengOrion
// Create at 2022-06-02 19:21:56
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using RhodeIsland.Arknights.SafeArea.Core;
using UnityEngine;
using UnityEngine.UI;

namespace RhodeIsland.Arknights
{
	public static class SafeAreaController
	{
		private static SafeAreaImpl impl
		{
			get
			{
				return null;
			}
		}
		public static bool isInited
		{
			get
			{
				return default(bool);
			}
		}
		public static void InitSafeRect(MonoBehaviour behavior, [Optional] Action onSuc)
		{
		}
		public static SafeRect safeRect
		{
			get
			{
				return default(SafeRect);
			}
		}
		public static int width
		{
			get
			{
				return 0;
			}
		}
		public static int height
		{
			get
			{
				return 0;
			}
		}
		public static float safeRectRatio
		{
			get
			{
				return 0f;
			}
		}
		public static Vector2 GetMaskPadding(CanvasScaler scaler)
		{
			return default(Vector2);
		}
		public static void ChangeSafePaddingConfig(int newSafePadding)
		{
		}
		public static void RegisterSafeAreaListener(ISafeAreaListener listener)
		{
		}
		public static void UnregisterSafeAreaListener(ISafeAreaListener listener)
		{
		}
		public static void UpdateScreenSize()
		{
		}
		[DebuggerHidden]
		private static IEnumerator _InitCorouine(Action onSuc)
		{
			return null;
		}
		private static bool _TryGetSafePaddingFromConfig(out int padding)
		{
			padding = 0;
			return default(bool);
		}
		private static void _NotifySafeRectUpdated(SafeRect safeRect)
		{
		}
		public const int MAX_NOTCH_PADDING = 130;
		private const string SAFE_PADDING_PREF_KEY = "SafeAreaController_safe_padding";
		private static SafeAreaImpl s_impl;
		private static SafeRect s_safeRect;
		private static bool s_isInited;
		private static bool s_isIniting;
		private static List<ISafeAreaListener> s_safeRectListeners;
		private static int s_cachedScreenWidth;
		private static int s_cachedScreenHeight;
	}
}