// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-04-30 08:19:53

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RhodeIsland
{
	public static class EventPool
	{
		public delegate void EventCallbackDelegate(object arg);
	}

	public class EventPool<TEnum> where TEnum : struct
	{
		public void On(TEnum ev, EventPool.EventCallbackDelegate cb)
		{
			_EnsureEvents(m_eventsMap, ev).Add(cb);
		}

		public void Once(TEnum ev, EventPool.EventCallbackDelegate cb)
		{
			_EnsureEvents(m_onceEventsMap, ev).Add(cb);
		}

		public void Remove(EventPool.EventCallbackDelegate cb)
		{
			foreach (KeyValuePair<TEnum, AddOrRemoveSafeCallbackSet> pair in m_eventsMap)
            {
				pair.Value.Remove(cb);
            }
			m_eventsMap.TrimExcess();
			foreach (KeyValuePair<TEnum, AddOrRemoveSafeCallbackSet> pair in m_onceEventsMap)
			{
				pair.Value.Remove(cb);
			}
			m_onceEventsMap.TrimExcess();
		}

		public void Remove(TEnum ev, EventPool.EventCallbackDelegate cb)
		{
			_EnsureEvents(m_eventsMap, ev).Remove(cb);
			_EnsureEvents(m_onceEventsMap, ev).Remove(cb);
		}

		public void Emit(TEnum ev, [Optional] object arg)
		{
			_EnsureEvents(m_eventsMap, ev).Emit(arg);
			_EnsureEvents(m_onceEventsMap, ev).Emit(arg);
			m_onceEventsMap.Remove(ev);
		}

		public void EmitWithArgs(TEnum ev, params object[] args)
		{
			Emit(ev, args);
		}

		public void Clear()
		{
			m_eventsMap.Clear();
			m_onceEventsMap.Clear();
		}

		private AddOrRemoveSafeCallbackSet _EnsureEvents(Dictionary<TEnum, AddOrRemoveSafeCallbackSet> events, TEnum ev)
		{
			if (!events.ContainsKey(ev))
            {
				events[ev] = new AddOrRemoveSafeCallbackSet();
            }
			return events[ev];
		}

		private Dictionary<TEnum, AddOrRemoveSafeCallbackSet> m_eventsMap = new();
		private Dictionary<TEnum, AddOrRemoveSafeCallbackSet> m_onceEventsMap = new();

		private class AddOrRemoveSafeCallbackSet
		{
			public void Add(EventPool.EventCallbackDelegate cb)
			{
				if (m_iterCounter != 0)
                {
					m_pendingAddOrRemove.Add(new(true, cb));
				}
				else
                {
					m_internalSet.Add(cb);
				}
			}

			public void Remove(EventPool.EventCallbackDelegate cb)
			{
				if (m_iterCounter != 0)
				{
					m_pendingAddOrRemove.Remove(new(true, cb));
				}
				else
				{
					m_internalSet.Remove(cb);
				}
			}

			public void Emit(object arg)
			{
				++m_iterCounter;
				foreach (EventPool.EventCallbackDelegate cb in m_internalSet)
                {
					cb.Invoke(arg);
                }
				if (m_iterCounter-- == 1)
                {
                    for (int i = 0; i < m_pendingAddOrRemove.Count; i++)
                    {
						KeyValuePair<bool, EventPool.EventCallbackDelegate> pair = m_pendingAddOrRemove[i];
                        if (pair.Key)
                        {
                            m_internalSet.Add(pair.Value);
                        }
						else
                        {
							m_internalSet.Remove(pair.Value);
						}
                    }
					m_pendingAddOrRemove.Clear();
				}
			}

			private ushort m_iterCounter = 0;
			private HashSet<EventPool.EventCallbackDelegate> m_internalSet = new();
			private List<KeyValuePair<bool, EventPool.EventCallbackDelegate>> m_pendingAddOrRemove = new();
		}
	}
}