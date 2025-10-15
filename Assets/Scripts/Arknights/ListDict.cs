// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//using XLua;

namespace RhodeIsland.Arknights
{
	[Serializable]
	public class ListDict<TKey, TValue> : List<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>, IListDict, IEnumerable, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>
	{
		[UnityEngine.Scripting.Preserve]
		public ListDict()
		{
			_SetEquals(null);
		}

		public ListDict(Equality equals)
		{
			_SetEquals(equals);
		}

		public ListDict(ListDict<TKey, TValue> another, [Optional] Equality equals) : base(another)
		{
			_SetEquals(equals);
		}

		public ListDict(IEnumerable<KeyValuePair<TKey, TValue>> collection, [Optional] Equality equals) : base(collection)
		{
			_SetEquals(equals);
		}

		public TValue this[TKey key]
		{
			get
			{
				int index = IndexOf(key);
				if (index >= 0)
				{
					return base[index].Value;
				}
				return default;
			}
			set
			{
				int index = IndexOf(key);
				KeyValuePair<TKey, TValue> pair = _MakePair(key, value);
				if (index < 0)
                {

					base.Add(pair);
				}
				else
                {
					base[index] = pair;
				}
			}
		}

		public KeyValuePair<TKey, TValue> Get(int index)
		{
			return base[index];
		}

		public void Set(int index, KeyValuePair<TKey, TValue> value)
		{
			if (Count > index)
            {
				base[index] = value;
			}
		}

		public ICollection<TKey> Keys
		{
			get
			{
				TKey[] keys = new TKey[Count];
                for (int i = 0; i < Count; i++)
                {
					keys[i] = base[i].Key;
				}
				return keys;
			}
		}

		public ICollection<TValue> Values
		{
			get
			{
				TValue[] values = new TValue[Count];
				for (int i = 0; i < Count; i++)
				{
					values[i] = base[i].Value;
				}
				return values;
			}
		}

		public new void Add(KeyValuePair<TKey, TValue> pair)
		{
			Add(pair.Key, pair.Value);
		}

		public void Add(TKey key, TValue value)
		{
			int index = IndexOf(key);
			KeyValuePair<TKey, TValue> pair = _MakePair(key, value);
			if (index < 0)
			{

				base.Add(pair);
			}
			else
			{
				base[index] = pair;
			}
		}

		public bool ContainsKey(TKey key)
		{
			return IndexOf(key) >= 0;
		}

		public bool Remove(TKey key)
		{
			int index = IndexOf(key);
			if (((index >> 31) & 3) != 0)
            {
				return false;
			}
			else
            {
                RemoveAt(index);
				return true;
            }
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			int index = IndexOf(key);
			if (index < 0)
            {
				value = default;
				return false;
            }
			else
            {
				value = base[index].Value;
				return true;
            }
		}

		public int IndexOf(TKey key)
		{
            for (int i = 0; i < Count; i++)
            {
				if (m_equals.Invoke(key, base[i].Key))
                {
					return i;
                }
            }
			return -1;
		}

		private KeyValuePair<TKey, TValue> _MakePair(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		private void _SetEquals(Equality equals)
		{
			if (equals == null)
            {
				EqualityComparer<TKey> comparer = EqualityComparer<TKey>.Default;
				equals = new Equality(comparer.Equals);
			}
			m_equals = equals;
		}

		private Equality m_equals;

		public delegate bool Equality(TKey v1, TKey v2);
	}
}
