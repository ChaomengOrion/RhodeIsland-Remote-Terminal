// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RhodeIsland.Arknights.ObjectPool
{
	public class ObjectPool<T> where T : class, IReusable
	{
		public ObjectPool(Func<T> constructor, [Optional] Options options)
		{
			m_constructor = constructor;
			m_options = options;
			m_options.Prepare();
			if (m_options.allowPoolAutoReuse)
            {
				m_pendingObjsToAutoReuse = new();
            }
			_LoadToSize(m_options.preloadSize);
		}

		public int availableUnusedCnt
		{
			get
			{
				return m_unusedObjs.Count;
			}
		}

		public int allLoadedCnt
		{
			get
			{
				return m_unusedObjs.Count + m_usingObjs.Count;
			}
		}

		public T Allocate()
		{
			T newItem;
			if (m_unusedObjs.Count <= 0)
            {
				if (allLoadedCnt >= m_options.maxCapacity)
                {
					if (!m_options.allowPoolAutoReuse)
                    {
						return null;
                    }
					newItem = _PickOneAndForceReuse();
				}
				else
                {
					newItem = _CreateNew();
                }
            }
			else
            {
				newItem = m_unusedObjs.Pop();
			}
			if (newItem != null)
            {
				m_usingObjs.Add(newItem);
				if (m_options.allowPoolAutoReuse)
                {
					m_pendingObjsToAutoReuse.Enqueue(newItem);
                }
				_SendNotification(newItem, NotificationEvent.ON_ALLOCATE);
				return newItem;
			}
			return null;
		}

		public bool Recycle(T obj)
		{
			if (obj == null)
            {
				return false;
            }
			if (!m_usingObjs.Remove(obj))
            {
				return false;
            }
			_RecycleInternal(obj);
			return true;
		}

		public void Reset()
		{
			m_usingObjs.Clear();
			_LoadToSize(m_options.preloadSize);
		}

		public void ClearUsingLinksOnly()
		{
			m_usingObjs.Clear();
		}

		private T _PickOneAndForceReuse()
		{
			if (m_usingObjs.Count > 0)
            {
				while (true)
                {
					if (m_pendingObjsToAutoReuse.Count <= 0)
                    {
						break;
                    }
					T obj = m_pendingObjsToAutoReuse.Dequeue();
					if (m_usingObjs.Contains(obj))
                    {
						if (obj != null)
                        {
							m_usingObjs.Remove(obj);
							_SendNotification(obj, NotificationEvent.ON_RECYCLE);
							return obj;
						}
						break;
                    }
                }
				UnityEngine.Debug.Log("[PoolManager] There should be an valid object but not found!");
				return null;
			}
			return null;
		}

		private void _LoadToSize(int size)
		{
			int unCount = m_unusedObjs.Count,
				count = m_usingObjs.Count;
			if (unCount < size - count)
            {
				int i = size - unCount - count;
                do
                {
					T obj = _CreateNew();
					if (obj != null)
                    {
						m_unusedObjs.Push(obj);
                    }
					--i;
                } while (i != 0);
            }
		}

		private void _RecycleInternal(T obj)
		{
			m_unusedObjs.Push(obj);
			_SendNotification(obj, NotificationEvent.ON_RECYCLE);
		}

		private void _SendNotification(T obj, NotificationEvent ev)
		{
			if (ev == NotificationEvent.ON_RECYCLE)
            {
				obj.OnRecycle();
				return;
            }
			else if (ev == NotificationEvent.ON_ALLOCATE)
            {
				obj.OnAllocate();
				return;
			}
			UnityEngine.Debug.LogWarning("[ObjectPool] Invalid notification event: " + ev);
		}

		private T _CreateNew()
		{
			return m_constructor.Invoke();
		}

		private Options m_options;

		private Stack<T> m_unusedObjs = new();

		private HashSet<T> m_usingObjs = new();

		private Func<T> m_constructor;

		private Queue<T> m_pendingObjsToAutoReuse;

		[Serializable]
		public struct Options
		{
			public void Prepare()
			{
				if (maxCapacity <= 0)
				{
					maxCapacity = 0x7FFFFFFF;
				}
			}

			public int preloadSize;

			public int maxCapacity;

			public bool allowPoolAutoReuse;
		}
	}
}
