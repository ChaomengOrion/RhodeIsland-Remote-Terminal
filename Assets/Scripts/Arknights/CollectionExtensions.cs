// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RhodeIsland.Arknights
{
	public static class CollectionExtensions
	{
		public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
		{
			if (collection != null)
            {
				return collection.Count == 0;
            }
			return true;
		}
		public static T[] ToArrayE<T>(this ICollection<T> collection)
		{
			return null;
		}
		public static List<T> ToListE<T>(this ICollection<T> collection)
		{
			return null;
		}
		public static T FirstE<T>(this IEnumerable<T> list)
		{
			return default;
		}
		public static T First<T>(this IList<T> list)
		{
			return default;
		}
		public static T FirstOfDefault<T>(this IList<T> list)
		{
			return default;
		}
		public static T Last<T>(this IList<T> list)
		{
			return default;
		}
		public static T LastOrNull<T>(this IList<T> list)
		{
			return default;
		}
		public static bool ContainsItem<T>(this IList<T> list, T item)
		{
			return default(bool);
		}
		public static bool ContainsItem(this IList<string> list, string item, bool ignoreCase)
		{
			return default(bool);
		}
		public static void Resize<T>(this List<T> list, int size)
		{
		}
		public static void Shrink<T>(this List<T> list, int size)
		{
		}

		public static int IndexOf<T>(this T[] list, T item) where T : class
		{
			for (int i = 0; i < list.Length; i++)
				if (list[i] == item)
					return i;
			return -1;
		}

		public static int IndexOf(this IList<string> list, string item, bool ignoreCase)
		{
			for (int i = 0; i < list.Count; i++)
				if (list[i] == item)
					return i;
			return -1;
		}
		public static int SafeCount<T>(this IList<T> list)
		{
			if (list == null)
            {
				return 0;
            }
			return list.Count;
		}
		public static T SafeGet<T>(this IList<T> list, int index, [Optional] T defaultVal)
		{
			if (list.Count <= index)
            {
				return defaultVal;
            }
			return list[index];
		}
		public static T SafeClampGet<T>(this IList<T> list, int index, [Optional] T defaultVal)
		{
			return default;
		}
		private static T _ClampInternal<T>(T k, T min, T max) where T : IComparable
		{
			return default;
		}
		public static bool TrySafeGet<T>(this IList<T> list, int index, out T result)
		{
			result = default;
			return default(bool);
		}
		public static Value GetValueOrDefault<Key, Value>(this IDictionary<Key, Value> dict, Key key, [Optional] Value defVal)
		{
			return default;
		}
		public static bool CheckValidIndex<T>(this IList<T> list, int index)
		{
			return default(bool);
		}
		public static List<T> DistinctE<T>(this IList<T> list)
		{
			return null;
		}
		public static void RemoveDuplicationsInplace<T>(this IList<T> list)
		{
		}
		public static void ResetAllElementAsDefault<T>(this IList<T> list)
		{
		}
		public static void AddRangeE<T>(this ICollection<T> list, IList<T> add)
		{
		}
		public static void RemoveRange<T>(this IList<T> left, IEnumerable<T> right)
		{
		}
		public static bool ContainsAny<T>(this IList<T> list, IEnumerable<T> right)
		{
			return default(bool);
		}
		public static void StableBubbleSort<T>(this IList<T> list, Comparison<T> compare)
		{
		}
	}
}
