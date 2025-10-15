// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using UnityEngine;
//using XLua;

namespace RhodeIsland.Arknights.UI
{
	public abstract class UIRecycleLayoutAdapter// : IHotfixable
	{
		protected UIRecycleLayoutAdapter()
		{
		}

		public void Init(UIRecycleLayoutGroup.IViewHandler handler)
		{
		}

		protected UIRecycleLayoutGroup.IViewHandler viewHandler
		{
			get
			{
				return null;
			}
			private set
			{
			}
		}

		public abstract IList<UIRecycleLayoutAdapter.IVirtualView> GenerateViewsForRebuild();

		public interface IVirtualView// : IHotfixable
		{
			void AttachView(GameObject view);

			void DetachView();

			GameObject GetAttachedView();

			int GetViewID();

			GameObject GetPrefab();

			float GetPreferSize();
		}

		public abstract class VirtualView<TView> : UIRecycleLayoutAdapter.IVirtualView/*, IHotfixable*/ where TView : Component
		{
			protected VirtualView()
			{
			}

			protected TView view
			{
				get
				{
					return null;
				}
				private set
				{
				}
			}

			public void AttachView(GameObject gameObj)
			{
			}

			public void DetachView()
			{
			}

			public GameObject GetAttachedView()
			{
				return null;
			}

			public int GetViewID()
			{
				return default(int);
			}

			protected abstract void OnViewAttached();

			protected abstract void OnViewDetached();

			public abstract GameObject GetPrefab();

			public abstract float GetPreferSize();

			private GameObject m_gameObj;
		}
	}
}
