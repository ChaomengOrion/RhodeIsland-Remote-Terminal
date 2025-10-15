// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;

namespace RhodeIsland.Arknights
{
	public static class DateTimeUtil
	{
		public static DateTime TimeStampToDateTime(long timeStamp)
		{
			return default(DateTime);
		}
		public static long DateTimeToTimeStamp(DateTime dateTime)
		{
			return default(long);
		}
		public static void SyncTime(long serverTs)
		{
		}
		public static DateTime currentTime
		{
			get
			{
				return default(DateTime);
			}
		}
		public static long timeStampNow
		{
			get
			{
				return default(long);
			}
		}
		private static DateTime _CurrentTimeDeviceUTC8()
		{
			return default(DateTime);
		}
		public static bool IsSameDay(DateTime d1, DateTime d2)
		{
			return default(bool);
		}
		public static int DeltaDayInSameYear(DateTime d1, DateTime d2)
		{
			return default(int);
		}
		public static int DeltaNatureDays(DateTime lhs, DateTime rhs)
		{
			return default(int);
		}
		public static bool IsSameNatureDay(DateTime d1, DateTime d2)
		{
			return default(bool);
		}
		public static DateTime GetNextCrossDayTime(DateTime fromTime)
		{
			return default(DateTime);
		}
		public static DateTime Min(DateTime d1, DateTime d2)
		{
			return default(DateTime);
		}
		public static readonly DateTime EMPTY_DATETIME;
		private const int UTC_BIAS_HOURS = 8;
		private const long SECS_PER_DAY = 86400L;
		private static readonly DateTime START_TIME;
		private static long s_testTimeBias;
		private static long s_serverTimeBias;
	}
}
