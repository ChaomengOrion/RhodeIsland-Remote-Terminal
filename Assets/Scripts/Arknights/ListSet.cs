// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections;
using System.Collections.Generic;

namespace RhodeIsland.Arknights
{
	[Serializable]
	public class ListSet<TItem> : IEnumerable, IEnumerable<TItem>
	{
		public ListSet() { m_items = new(); }

		public ListSet(int capacity)
		{
			m_items = new(capacity);
		}

		public int count
		{
			get
			{
				return m_items.Count;
			}
		}

		public bool isEmpty
		{
			get
			{
				return count == 0;
			}
		}

		public TItem this[int index]
		{
			get
			{
				return m_items[index];
			}
		}

		public void Clear()
		{
			m_items.Clear();
		}

		public bool Contains(TItem item)
		{
			for (int i = 0; i < m_items.Count; i++)
            {
				if (m_items[i].Equals(item))
                {
					return true;
                }
            }
			return false;
		}

		public bool Add(TItem item)
		{
			if (m_items.Contains(item))
            {
				return false;
            }
			m_items.Add(item);
			return true;
		}

		public void AddRange(IList<TItem> items)
		{
			//TODO
		}

		public void RemoveRange(IEnumerable<TItem> items)
		{
			m_items.RemoveRange(items);
		}

		public bool Remove(TItem item)
		{
			return m_items.Remove(item);
		}

		public void RemoveAt(int index)
		{
			m_items.RemoveAt(index);
		}

		public IEnumerator<TItem> GetEnumerator()
		{
			return m_items.GetEnumerator();
		}

		public TItem[] ToArray()
		{
			return m_items.ToArray();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private List<TItem> m_items;
	}
}