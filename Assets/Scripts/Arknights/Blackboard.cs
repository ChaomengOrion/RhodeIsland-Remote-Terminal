// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RhodeIsland.Arknights
{
	[Serializable]
	public class Blackboard : List<Blackboard.DataPair>
	{
		public Blackboard()
		{
		}

		public Blackboard(IList<Blackboard.DataPair> another)
		{
		}

		public Blackboard(IList<Blackboard.DataPair> another, float scale, IList<string> exceptKeys, bool isScaleDeltaToOne)
		{
		}

		public Blackboard(IList<Blackboard.DataPair> another, float scale, IList<string> certainKeys, bool isCertain, bool isScaleDeltaToOne)
		{
		}

		public void Reset(IList<Blackboard.DataPair> another)
		{
		}

		public void Assign(IList<Blackboard.DataPair> another)
		{
		}

		public void Assign(string key, float value)
		{
		}

		/*public void Assign(string key, FP value)
		{
		}*/

		public void Assign(string key, string value)
		{
		}

		public bool ContainsKey(string key)
		{
			return default(bool);
		}

		/*private void _AssignInternal(Blackboard.DataPair item)
		{
		}

		public static Blackboard Of(IList<Blackboard.DataPair> data)
		{
			return null;
		}

		public static void MergeTo(ref Blackboard to, Blackboard from)
		{
		}

		public static void MergeTo(ref List<Blackboard.DataPair> to, List<Blackboard.DataPair> from)
		{
		}*/

		public static Blackboard Union(Blackboard lhs, Blackboard rhs)
		{
			return null;
		}

		public float GetFloat(string key)
		{
			return default(float);
		}

		/*public FP GetFP(string key)
		{
			return default(FP);
		}*/

		public int GetInt(string key)
		{
			return default(int);
		}

		public bool GetBool(string key)
		{
			return default(bool);
		}

		public string GetString(string key)
		{
			return null;
		}

		/*public bool TryGetFloat(string key, out float value)
		{
			return default(bool);
		}

		public bool TryGetFP(string key, out FP value)
		{
			return default(bool);
		}

		public bool TryGetInt(string key, out int value)
		{
			return default(bool);
		}

		public bool TryGetBool(string key, out bool value)
		{
			return default(bool);
		}

		public bool TryGetString(string key, out string value)
		{
			return default(bool);
		}

		public bool TryGetInternalData(string key, out Blackboard.DataPair data)
		{
			return default(bool);
		}

		public float GetFloatOrDefault(string key, float defaultValue, bool showWarning = true)
		{
			return default(float);
		}

		public FP GetFpOrDefault(string key, FP defaultValue, bool showWarning = true)
		{
			return default(FP);
		}*/

		public bool GetBoolOrDefault(string key, bool defaultValue, bool showWarning = true)
		{
			return default(bool);
		}

		public int GetIntOrDefault(string key, int defaultValue, bool showWarning = true)
		{
			return default(int);
		}

		public string GetStringOrDefault(string key, string defaultValue, bool showWarning = true)
		{
			return null;
		}

		public float EnsureFloat(string key)
		{
			return default(float);
		}

		public bool EnsureBool(string key)
		{
			return default(bool);
		}

		public int EnsureInt(string key)
		{
			return default(int);
		}

		public string EnsureString(string key)
		{
			return null;
		}

		public void GenerateBlackboardWithPrefix(ref Blackboard blackboard, string prefix)
		{
		}

		public Blackboard GenerateBlackboardWithPrefix(string prefix)
		{
			return null;
		}

		public void AssignByPrefix(IList<Blackboard.DataPair> another, string prefixToStrip)
		{
		}

		public void AssignValueStrByPrefix(IList<Blackboard.DataPair> another, string prefixToStrip)
		{
		}

		public void AddBlackboardStrictly(Blackboard other)
		{
		}

		public void AddBlackboardBaseOneStrictly(Blackboard other)
		{
		}

		/*private bool _TryGetNumber(string key, out float value)
		{
			return default(bool);
		}

		private bool _TryToStripPrefix(string key, string prefixToStrip, out string outKey)
		{
			return default(bool);
		}*/

		[Serializable]
		public struct DataPair
		{
			public DataPair(string key, float value)
			{
				this.key = key;
				this.value = value;
				this.valueStr = null;
			}

			public DataPair(string key, string value)
			{
				this.key = key;
				this.valueStr = value;
				this.value = 0;
			}

			[JsonIgnore]
			public bool isNumericValue
			{
				get
				{
					return default(bool);
				}
				set
				{
				}
			}

			[JsonIgnore]
			public bool isStringValue
			{
				get
				{
					return default(bool);
				}
			}

			public override string ToString()
			{
				return null;
			}

			public string key;

			public float value;

			public string valueStr;
		}
	}
}