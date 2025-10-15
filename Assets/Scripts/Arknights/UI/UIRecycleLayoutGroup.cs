// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using XLua;

namespace RhodeIsland.Arknights.UI
{
	public abstract class UIRecycleLayoutGroup : MonoBehaviour, ILayoutElement//, IHotfixable
	{
		protected UIRecycleLayoutGroup()
		{
		}

		protected UIRecycleLayoutAdapter adapter
		{
			get
			{
				return null;
			}
		}

		protected UIRecycleLayoutGroup.ViewMgr viewMgr
		{
			get
			{
				return null;
			}
		}

		protected float sizeOnAxis
		{
			get
			{
				return default(float);
			}
			private set
			{
			}
		}

		protected float spacing
		{
			get
			{
				return default(float);
			}
		}

		protected Padding padding
		{
			get
			{
				return default(UIRecycleLayoutGroup.Padding);
			}
		}

		public float minWidth
		{
			get
			{
				return default(float);
			}
		}

		public float minHeight
		{
			get
			{
				return default(float);
			}
		}

		public float flexibleWidth
		{
			get
			{
				return default(float);
			}
		}

		public float flexibleHeight
		{
			get
			{
				return default(float);
			}
		}

		public int layoutPriority
		{
			get
			{
				return default(int);
			}
		}

		public void CalculateLayoutInputHorizontal()
		{
		}

		public void CalculateLayoutInputVertical()
		{
		}

		public abstract float preferredWidth { get; }

		public abstract float preferredHeight { get; }

		protected abstract void ApplyLayoutMeta(UIRecycleLayoutAdapter.IVirtualView view, UIRecycleLayoutGroup.LayoutMeta meta);

		protected abstract Vector2 GetVisibleRange(Bounds viewBound);

		public void SetAdapter(UIRecycleLayoutAdapter adapter)
		{
		}

		protected virtual void LateUpdate()
		{
		}

		private void _RebuildAllViews()
		{
		}

		private void _UpdateViews(int fromIndex)
		{
		}

		private bool _InsertView(int index, UIRecycleLayoutAdapter.IVirtualView view)
		{
			return default(bool);
		}

		private bool _RemoveView(UIRecycleLayoutAdapter.IVirtualView view)
		{
			return default(bool);
		}

		private void _UpdateViewsFrom(UIRecycleLayoutAdapter.IVirtualView view)
		{
		}

		private float _GetElementPosByIndex(int index)
		{
			return default(float);
		}

		protected RectTransform content
		{
			get
			{
				return null;
			}
		}

		[SerializeField]
		private RectTransform _viewport;

		[SerializeField]
		private RectTransform _content;

		[SerializeField]
		private int _layoutPriority;

		[SerializeField]
		private UIRecycleLayoutGroup.Padding _padding;

		[SerializeField]
		private float _spacing;

		private Dictionary<int, UIRecycleLayoutGroup.LayoutMeta> m_layoutMetaMap;

		private UIRecycleLayoutAdapter m_adapter;

		private UIRecycleLayoutGroup.ViewMgr m_viewMgr;

		public interface IViewHandler
		{
			UIRecycleLayoutAdapter.IVirtualView GetView(int index);

			int GetViewCount();

			bool InsertView(int index, UIRecycleLayoutAdapter.IVirtualView view);

			bool AddView(UIRecycleLayoutAdapter.IVirtualView view);

			void NotifyViewSizeChanged(UIRecycleLayoutAdapter.IVirtualView view);

			void NotifyAllViewSizeChanged();

			void NotifyRebuild();

			bool RemoveView(UIRecycleLayoutAdapter.IVirtualView view);

			float GetElementPosByIndex(int index);
		}

		protected struct LayoutMeta
		{
			public float pos;

			public float size;

			public int index;

			public float curTotalSize;
		}

		private class ViewPool
		{
			public ViewPool(int viewType, GameObject prefab, Transform container)
			{
			}

			public int viewType
			{
				get
				{
					return default(int);
				}
				private set
				{
				}
			}

			public GameObject Alloc(out bool isNewlyCreated)
			{
				isNewlyCreated = false;
				return null;
			}

			public bool Recycle(GameObject obj)
			{
				return default(bool);
			}

			public void RecycleAll()
			{
			}

			private GameObject m_prefab;

			private Transform m_container;

			private List<GameObject> m_activeObjs;

			private List<GameObject> m_pooledObjs;
		}

		private class EmptyAdapter : UIRecycleLayoutAdapter
		{
			public EmptyAdapter()
			{
			}

			public override IList<UIRecycleLayoutAdapter.IVirtualView> GenerateViewsForRebuild()
			{
				return null;
			}
		}

		protected class ViewMgr : /*IHotfixable,*/ IViewHandler
		{
			public ViewMgr(UIRecycleLayoutGroup closure)
			{
			}

			public void RebuildAll(UIRecycleLayoutAdapter adapter)
			{
			}

			public IList<UIRecycleLayoutAdapter.IVirtualView> GetViews()
			{
				return null;
			}

			public void DetachView(UIRecycleLayoutAdapter.IVirtualView view, GameObject curView)
			{
			}

			public void AttachView(UIRecycleLayoutAdapter.IVirtualView view)
			{
			}

			public UIRecycleLayoutAdapter.IVirtualView GetView(int index)
			{
				return null;
			}

			public int GetViewCount()
			{
				return default(int);
			}

			public bool InsertView(int index, UIRecycleLayoutAdapter.IVirtualView view)
			{
				return default(bool);
			}

			public bool AddView(UIRecycleLayoutAdapter.IVirtualView view)
			{
				return default(bool);
			}

			public bool RemoveView(UIRecycleLayoutAdapter.IVirtualView view)
			{
				return default(bool);
			}

			public void NotifyViewSizeChanged(UIRecycleLayoutAdapter.IVirtualView view)
			{
			}

			public void NotifyAllViewSizeChanged()
			{
			}

			public void NotifyRebuild()
			{
			}

			public float GetElementPosByIndex(int index)
			{
				return default(float);
			}

			private UIRecycleLayoutGroup.ViewPool _EnsureViewPool(UIRecycleLayoutAdapter.IVirtualView view)
			{
				return null;
			}

			private void _NotifyLayoutChanged(int fromIndex)
			{
			}

			private UIRecycleLayoutGroup m_closure;

			private ListDict<int, ViewPool> m_viewPools;

			private List<UIRecycleLayoutAdapter.IVirtualView> m_views;
		}

		[Serializable]
		protected struct Padding
		{
			public int top;

			public int left;

			public int bottom;

			public int right;
		}
	}
}
