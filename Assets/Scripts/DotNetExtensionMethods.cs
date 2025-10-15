// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-03-30 18:48:06

using System;
using System.Collections.Generic;
using UnityEngine;

public static class DotNetExtensionMethods
{
	public static float GetFloat(this Dictionary<string, object> param, string key, float defaultValue = 0f)
	{
		if (!param.ContainsKey(key))
		{
			return defaultValue;
		}
		return Convert.ToSingle(param[key]);
	}
	public static string GetString(this Dictionary<string, object> param, string key, string defaultValue = "")
	{
		if (!param.ContainsKey(key))
        {
			return defaultValue;
        }
		return param[key].ToString();
	}
	public static int GetInt(this Dictionary<string, object> param, string key, int defaultValue = 0)
	{
		if (!param.ContainsKey(key))
		{
			return defaultValue;
		}
		return Convert.ToInt32(param[key]);
	}
	public static bool GetBool(this Dictionary<string, object> param, string key, bool defaultValue = false)
	{
		if (!param.ContainsKey(key))
		{
			return defaultValue;
		}
		return Convert.ToBoolean(param[key]);
	}
	public static Vector2 GetVector2(this Dictionary<string, object> param, string key)
	{
		return default(Vector2);
	}
	public static Vector3 GetVector3(this Dictionary<string, object> param, string key)
	{
		return default(Vector3);
	}
	public static Vector4 GetVector4(this Dictionary<string, object> param, string key)
	{
		return default(Vector4);
	}
	public static List<float> GetFloatList(this Dictionary<string, object> param, string key)
	{
		return null;
	}
	public static List<string> GetStringList(this Dictionary<string, object> param, string key)
	{
		return null;
	}
	public static List<object> GetObjectList(this Dictionary<string, object> param, string key)
	{
		return null;
	}
	public static T GetEnum<T>(this Dictionary<string, object> param, string key, T defaultEnum, bool ignoreCase = false)
	{
		if (!typeof(T).IsEnum)
        {
			return defaultEnum;
		}
		string value = param.GetString(key, string.Empty);
		if (string.IsNullOrEmpty(value))
        {
			return defaultEnum;
        }
		return (T)Enum.Parse(typeof(T), value, ignoreCase);
	}
	public static bool TryGetEnum<T>(this Dictionary<string, object> param, string key, out T value, bool ignoreCase = false)
	{
		value = default;
		return default;
	}
	public static List<T> GetEnumList<T>(this Dictionary<string, object> param, string key, T defaultEnum)
	{
		return null;
	}
	public static void ChangeKey<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey oldKey, TKey newKey)
	{
	}
	public static void Insert<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey keyIndex, bool above, TKey newKey, TValue newValue)
	{
	}
	public static bool ValueEquals<T>(this List<T> list, List<T> other)
	{
		return default(bool);
	}
	public static T Pop<T>(this List<T> list)
	{
		return default;
	}
	public static string Replace(this string str, string filterStr, string replaceStr, bool ignoreCase)
	{
		return null;
	}
	public static int ToInt32(this string str)
	{
		return default(int);
	}
	public static int LoopMod(this int num, int count)
	{
		return default(int);
	}
	public static bool IsFlagSet<T>(this int flags, T flag)
	{
		return default(bool);
	}
	public static int FlagSet<T>(this int flags, T flag)
	{
		return default(int);
	}
	public static int FlagUnset<T>(this int flags, T flag)
	{
		return default(int);
	}
	public static bool IsPOT(this int x)
	{
		return default(bool);
	}
}
