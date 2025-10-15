// Created by ChaomengOrion
// Create at 2022-07-30 00:18:25
// Last modified on 2022-07-31 02:08:11

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace RhodeIsland.RemoteTerminal.UI.ScrollView
{
    [RequireComponent(typeof(ScrollRect)), RequireComponent(typeof(CanvasGroup))]
    public class ShrinkableScrollView : MonoBehaviour
    {
        [SerializeField]
        private HorizontalLayoutGroup _horizontalLayoutGroup;

        [SerializeField]
        private float _spacing;
        [SerializeField]
        private float _itemWidth;
        [SerializeField]
        private float _shrinkDuration, _unfoldDuration;
        private ScrollRect m_scrollRect;
        private CanvasGroup m_canvasGroup;
        private HashSet<ShrinkableScrollViewItem> m_items = new();
        private ShrinkableScrollViewItem m_activeItem = null;
        protected State m_state;

        public List<ShrinkableScrollViewItem> s;

        private Coroutine m_waitClickCoroutine = null;

        protected void Awake()
        {
            m_scrollRect = GetComponent<ScrollRect>();
            m_canvasGroup = GetComponent<CanvasGroup>();
            _horizontalLayoutGroup.reverseArrangement = true;
        }

        public void SetUpState(State state)
        {
            if (m_state == State.InTrans) return;
            if (m_state == State.Shrink)
            {
                foreach (ShrinkableScrollViewItem item in m_items)
                {
                    if (item != m_activeItem)
                    {
                        item.gameObject.SetActiveIfNecessary(false);
                    }
                }
                m_activeItem.OnShrinkUpper();
                _horizontalLayoutGroup.spacing = -_itemWidth;
            }
            else
            {
                foreach (ShrinkableScrollViewItem item in m_items)
                {
                    item.gameObject.SetActiveIfNecessary(true);
                }
                _horizontalLayoutGroup.spacing = _spacing;
            }
            m_state = state;
        }

        public void SetState(State state)
        {
            if (m_state == State.InTrans || state == m_state) return;
            m_state = state;
            if (m_state == State.Shrink)
            {
                _Shrink();
            }
            else
            {
                _Unfold();
            }
        }

        public void Clear()
        {
            m_scrollRect.content.ClearAllChildren();
            m_items.Clear();
        }

        public ShrinkableScrollViewItem Add(ShrinkableScrollViewItem perfab)
        {
            ShrinkableScrollViewItem obj = Instantiate(perfab, m_scrollRect.content);
            m_items.Add(obj);
            obj.transform.SetAsFirstSibling();
            obj.Init(this);
            return obj;
        }

        public void OnSetOn(ShrinkableScrollViewItem item)
        {
            int index = item.transform.GetSiblingIndex();
            item.transform.SetAsLastSibling();
            if (m_activeItem && item != m_activeItem)
            {
                m_activeItem.OnDeselect();
                m_activeItem.transform.SetSiblingIndex(index);
            }
            m_activeItem = item;
        }

        public void IfSetOff(ShrinkableScrollViewItem item)
        {
            m_waitClickCoroutine = this.InvokeEndOfFrame(() =>
            {
                item.OnShrinkUpper();
                SetState(State.Shrink);
            });
        }

        public void OnRaiseClick()
        {
            if (m_waitClickCoroutine != null)
                StopCoroutine(m_waitClickCoroutine);
        }

        public void OnRaiseResume()
        {
            EventSystem.current.SetSelectedGameObject(m_activeItem.gameObject);
        }

        private void _Shrink()
        {
            m_state = State.InTrans;
            m_canvasGroup.blocksRaycasts = false;
            DOTween.To(() => _horizontalLayoutGroup.spacing, space => _horizontalLayoutGroup.spacing = space, -_itemWidth, _shrinkDuration)
                .OnComplete(() =>
                {
                    m_state = State.Shrink;
                    m_canvasGroup.blocksRaycasts = true;
                    foreach (ShrinkableScrollViewItem item in m_items)
                    {
                        if (item != m_activeItem)
                        {
                            item.gameObject.SetActiveIfNecessary(false);
                        }
                    }
                }).Play();
        }

        private void _Unfold()
        {
            m_state = State.InTrans;
            m_canvasGroup.blocksRaycasts = false;
            DOTween.To(() => _horizontalLayoutGroup.spacing, space => _horizontalLayoutGroup.spacing = space, _spacing, _unfoldDuration).OnComplete(() =>
            { 
                m_canvasGroup.blocksRaycasts = true;
                m_state = State.Unfold;
            }).Play();
            foreach (ShrinkableScrollViewItem item in m_items)
            {
                item.gameObject.SetActiveIfNecessary(true);
            }
            m_activeItem.OnDisShrinkUpper();
        }

        public enum State
        {
            Shrink = 0,
            InTrans = 1,
            Unfold = 2
        }
    }
}