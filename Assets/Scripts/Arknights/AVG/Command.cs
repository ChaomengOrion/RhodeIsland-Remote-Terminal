// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;

namespace RhodeIsland.Arknights.AVG
{
	public class Command
	{
		public bool TryGetParam(string key, out object value)
		{
			if (param == null || !param.TryGetValue(key, out value))
            {
				value = null;
				return false;
            }
			return true;
		}

		public bool TryGetParam(string key, out float value)
		{
            if (TryGetParam(key, out object v))
            {
                value = Convert.ToSingle(v);
				return true;
            }
            else
            {
                value = 0f;
				return false;
            }
        }

		public bool TryGetParam(string key, out int value)
		{
			if (TryGetParam(key, out object v))
			{
				value = Convert.ToInt32(v);
				return true;
			}
			else
			{
				value = 0;
				return false;
			}
		}

		public bool TryGetParam(string key, out bool value)
		{
			if (TryGetParam(key, out object v))
			{
				value = Convert.ToBoolean(v);
				return true;
			}
			else
			{
				value = false;
				return false;
			}
		}

		public bool TryGetParam(string key, out string value)
		{
			if (TryGetParam(key, out object v))
			{
				value = (string)v;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
		}

		public static bool TryGetParam<T>(IList<string> keys, out T value, TryGetParamDelegate<T> getter)
		{
			if (keys != null)
            {
				foreach (string key in keys)
                {
					if (getter.Invoke(key, out value))
                    {
						return true;
                    }
				}
            }
			value = default;
			return false;
		}

		public static T GetOrDefault<T>(string key, T defaultValue, TryGetParamDelegate<T> getter)
		{
            if (!getter.Invoke(key, out T value))
            {
                return defaultValue;
            }
            return value;
		}

		public static T GetOrDefault<T>(IList<string> keys, T defaultValue, TryGetParamDelegate<T> getter)
		{
			if (keys != null)
			{
				foreach (string key in keys)
				{
					if (getter.Invoke(key, out T value))
					{
						return value;
					}
				}
			}
			return defaultValue;
		}

		public string command;

		public string content;

		public Dictionary<string, object> param = new();

		public int lineNumber;

		public delegate bool TryGetParamDelegate<T>(string key, out T value);
	}
}