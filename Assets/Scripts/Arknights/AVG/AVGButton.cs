// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using UnityEngine;
using UnityEngine.EventSystems;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGButton : MonoBehaviour, /*IHotfixable,*/ IPointerDownHandler, IEventSystemHandler
	{
		private void _OnPointerExit()
		{
			m_state = State.NONE;
			m_context = DragContext.EMPTY;
			onClickAction.Invoke();
		}

		private void Update()
		{
			if (m_state == State.CLICK)
			{
				if (!InputUtil.GetCursorScreenPos(m_context.pointerId, out Vector2 point))
                {
					//TODO
					_OnPointerExit();
                }
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (m_state != State.CLICK)
            {
				m_state = State.CLICK;
				m_pressStartTime = DateTimeUtil.currentTime;
				m_context = new(eventData.pointerId, eventData.position);
			}
		}

		[SerializeField]
		private int _longPressThreshold = 300;
		[SerializeField]
		private int _dragDiatance = 50;
		public Action onClickAction;
		public Action<Vector2> onLongPressAction;
		public Action<Vector2> onDragAction;
		private DragContext m_context = DragContext.EMPTY;
		private State m_state = State.NONE;
		private DateTime m_pressStartTime = DateTimeUtil.EMPTY_DATETIME;

		private enum State
		{
			NONE,
			CLICK,
			LONG_PRESS
		}

		private struct DragContext
		{
			public DragContext(int pointerID, Vector2 startPos)
			{
				pointerId = pointerID;
				this.startPos = startPos;
				isEmpty = startPos == null;
			}
			public int pointerId { get; private set; }
			public bool isEmpty { get; private set; }
			public Vector2 startPos { get; private set; }
			public static readonly DragContext EMPTY = new(0, Vector2.zero);
		}
	}
}