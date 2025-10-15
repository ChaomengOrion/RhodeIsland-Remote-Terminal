// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using System.Globalization;

namespace RhodeIsland.Arknights
{
	public static class StringUtil
	{
		public static bool StartsWithOneOf(string str, string[] prefixes, bool ignoreCase = false)
		{
			return default(bool);
		}
		public static int CompareChinese(string a, string b)
		{
			return default(int);
		}
		public static int IndexOfAny(this string str, IList<string> subStrs)
		{
			return default(int);
		}
		public static int IndexOfAny(this string str, IList<string> subStrs, StringComparison comparison)
		{
			return default(int);
		}
		public static int LastIndexOfAny(this string str, IList<string> subStrs)
		{
			return default(int);
		}
		public static bool ContainsAny(this string str, IList<string> subStrs)
		{
			if (subStrs.SafeCount() > 0)
            {
				foreach (string subStr in subStrs)
                {
					if (str.Contains(subStr))
						return true;
                }
            }
			return false;
		}
		public static bool ContainsIgnoreCase(this string str, string substr)
		{
			return default(bool);
		}
		private static CultureInfo CHINESE_CULTURE = new("zh-cn");
	}
}
