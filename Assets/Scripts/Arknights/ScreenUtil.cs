// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
//using XLua;

namespace RhodeIsland.Arknights
{
	public class ScreenUtil : Singleton<ScreenUtil>//, IHotfixable
	{
		[UnityEngine.Scripting.Preserve]
		public ScreenUtil() { }

		public static UISleepBlocker BlockSleep()
		{
			return default(UISleepBlocker);
		}

		private UISleepBlocker _RequestSleepBlocker()
		{
			return default(UISleepBlocker);
		}

		private void _ReleaseSleepBlocker(long id)
		{
		}

		private static void _BlockSleep()
		{
			UnityEngine.Screen.sleepTimeout = -1;
		}

		private static void _UnblockSleep()
		{
			UnityEngine.Screen.sleepTimeout = -2;
		}

		private const long UISLEEPBLOCK_INVALID_ID = 0L;

		private readonly ListSet<long> m_activeBlockers = new();

		private long m_activeBlockerIndex;

		public struct UISleepBlocker
		{
			public static ScreenUtil.UISleepBlocker ScreenUtil_Create(long id)
			{
				return new UISleepBlocker { m_id = id };
			}

			public void Release()
			{
			}

			public bool IsEmpty()
			{
				return default(bool);
			}

			private long m_id;

			public static readonly ScreenUtil.UISleepBlocker EMPTY;
		}
	}
}
