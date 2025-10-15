// Created by ChaomengOrion
// Create at 2022-08-14 18:46:33
// Last modified on 2022-08-14 22:22:05

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace RhodeIsland.RemoteTerminal.UI.ScrollView
{
    public delegate void OnFocusHandler(object obj);

    [RequireComponent(typeof(ScrollRect))]
    public class AutoFoucsScrollView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        public ScrollRect ScrollRect
        {
            get => m_scrollRect;
        }

        public IAcceptFocus Foucs
        {
            get => m_acceptFocus[m_focusIndex];
        }

        public List<GameObject> Items
        {
            get => m_items;
        }

        public int FoucsIndex
        {
            get => m_focusIndex;
        }

        /// <summary>
        /// 当结束拖动聚焦到对象时调用
        /// </summary>
        public event OnFocusHandler OnFocus;
        /// <summary>
        /// 当拖动时聚焦对象改变时调用
        /// </summary>
        public event OnFocusHandler OnFocusChange;
        /// <summary>
        /// 当开始拖动时调用
        /// </summary>
        public Action OnDisFocus;

        [SerializeField]
        private float _itemHeight;
        [SerializeField]
        private float _autoFoucsDuration;
        [SerializeField]
        private float _autoFoucsDelay;
        [SerializeField]
        private Ease _autoFoucsEase = Ease.OutCubic;
        [SerializeField]
        private VerticalLayoutGroup _verticalLayoutGroup;
        [SerializeField]
        private RectTransform _focusCenterRect;
        private readonly List<IAcceptFocus> m_acceptFocus = new();
        private readonly List<GameObject> m_items = new();
        private ScrollRect m_scrollRect;
        private Tweener m_scrollTweener = null;
        private RectTransform m_rectTransform;

        private IAcceptFocus m_dynamicFocus = null;
        private float m_focusCenter;
        private int m_focusIndex = -1;
        private bool m_isFocused = false;

        protected virtual void Awake()
        {
            m_rectTransform = GetComponent<RectTransform>();
            m_scrollRect = GetComponent<ScrollRect>();
            m_scrollRect.onValueChanged.AddListener(OnScoll);
            m_scrollRect.inertia = true;
            m_scrollRect.decelerationRate = 1f;
            OnReset();
        }

        public List<GameObject> AddItems(List<GameObject> perfabs)
        {
            List<GameObject> res = new();
            foreach (GameObject item in perfabs)
            {
                res.Add(OnAddItem(item));
            }
            OnReset();
            if (m_acceptFocus.Count > 0)
                this.InvokeEndOfFrame(() => { if (m_focusIndex == -1) SetFocusForce(0); });
            return res;
        }

        public List<GameObject> AddItems(GameObject perfab, int times)
        {
            List<GameObject> res = new();
            for (int i = 0; i < times; i++)
            {
                res.Add(OnAddItem(perfab));
            }
            OnReset();
            if (m_acceptFocus.Count > 0)
                this.InvokeEndOfFrame(() => { if (m_focusIndex == -1) SetFocusForce(0); });
            return res;
        }

        public GameObject AddItem(GameObject perfab)
        {
            GameObject obj = OnAddItem(perfab);
            OnReset();
            if (m_acceptFocus.Count > 0)
                this.InvokeEndOfFrame(() => { if (m_focusIndex == -1) SetFocusForce(0); });
            return obj;
        }

        public virtual void ClearItem()
        {
            m_focusIndex = -1;
            m_acceptFocus.Clear();
            foreach (GameObject item in m_items)
            {
                item.transform.SetParent(null); // 避免1帧内Layout异常
                Destroy(item);
            }
            m_items.Clear();
            OnReset();
        }

        public void SetFocusForce(int index)
        {
            if (index < 0 || index >= m_acceptFocus.Count)
                return;
            m_scrollRect.StopMovement();
            m_scrollTweener?.Kill();
            m_scrollRect.DOKill();
            m_scrollTweener = null;
            m_focusIndex = index;
            m_scrollRect.normalizedPosition = new(0f, 1f - (index / (m_acceptFocus.Count - 1f)));
            _UpdateFocusCenter();
            foreach (IAcceptFocus item in m_acceptFocus)
            {
                item.OnDistanceChange(Mathf.Abs(m_focusCenter - item.GetLocalPos().y));
            }
            m_acceptFocus[index].OnFocus();
            OnFocus?.Invoke(m_acceptFocus[index].GetObject());
        }

        protected virtual void OnReset()
        {
            m_scrollTweener?.Kill();
            _UpdateLayoutPadding();
            m_scrollRect.normalizedPosition = new(0f, 1f);
            _UpdateFocusCenter();
        }

        protected virtual GameObject OnAddItem(GameObject perfab)
        {
            GameObject obj = Instantiate(perfab, m_scrollRect.content);
            m_items.Add(obj);
            IAcceptFocus focus = obj.GetComponentInChildren<IAcceptFocus>();
            if (focus != null)
            {
                m_acceptFocus.Add(focus);
            }
            return obj;
        }

        /// <summary>
        /// 更新聚焦
        /// </summary>
        private void _UpdateFoucs()
        {
            _UpdateFocusCenter();
            foreach (IAcceptFocus item in m_acceptFocus)
            {
                item.OnDistanceChange(Mathf.Abs(m_focusCenter - item.GetLocalPos().y));
            }
            int index = _CalculateNearestIndex(m_acceptFocus, m_focusCenter);
            if (index != m_focusIndex && !m_isFocused)
            {
                m_dynamicFocus?.OnDisFocus();
                m_dynamicFocus = m_acceptFocus[index];
                m_dynamicFocus.OnDynamicFocus();
                OnFocusChange?.Invoke(m_acceptFocus[index].GetObject());
                m_focusIndex = index;
            }
        }

        /// <summary>
        /// 更新布局填充
        /// </summary>
        private void _UpdateLayoutPadding()
        {
            int dis = Mathf.RoundToInt(m_rectTransform.rect.height / 2f - _itemHeight);
            _verticalLayoutGroup.padding.top = dis;
            _verticalLayoutGroup.padding.bottom = dis;
            Canvas.ForceUpdateCanvases();
        }

        /// <summary>
        /// 更新聚焦中心点
        /// </summary>
        private void _UpdateFocusCenter()
        {
            m_focusCenter = -m_scrollRect.content.localPosition.y - m_rectTransform.rect.height / 2f + _focusCenterRect.localPosition.y;
        }

        /// <summary>
        /// 聚焦到索引
        /// </summary>
        /// <param name="index">索引</param>
        private void _FoucsTo(int index)
        {
            if (index < 0 || index >= m_acceptFocus.Count)
                return;
            m_scrollRect.StopMovement();
            m_scrollTweener?.Kill();
            m_scrollRect.DOKill();
            m_scrollTweener = m_scrollRect.DOVerticalNormalizedPos(1f - (index / (m_acceptFocus.Count - 1f)), _autoFoucsDuration)
                .SetEase(_autoFoucsEase)
                .SetUpdate(true)
                .Play();
        }

        /// <summary>
        /// 惯性滑动并聚焦到索引
        /// </summary>
        /// <param name="index"></param>
        private void _StopAndFoucsTo(int index)
        {
            if (index < 0 || index >= m_acceptFocus.Count)
                return;
            if (m_scrollRect.normalizedPosition.y <= 0f || m_scrollRect.normalizedPosition.y >= 1f) //超出边界直接回弹
                _FoucsTo(index);
            else //继续惯性滑动
            {
                m_scrollTweener?.Kill();
                m_scrollTweener = DOTween.To(() => m_scrollRect.velocity.y, y => m_scrollRect.velocity = new Vector2(0f, y), 0f,
                    Mathf.Sqrt(Mathf.Abs(m_scrollRect.velocity.y / m_rectTransform.rect.height / 50f)))
                    .SetEase(Ease.OutSine)
                    .SetUpdate(true)
                    .OnUpdate(() =>
                    {
                        if (m_scrollRect.normalizedPosition.y <= 0f || m_scrollRect.normalizedPosition.y >= 1f) //超出边界直接回弹
                            _FoucsTo(index);
                    })
                    .OnComplete(() => _FoucsTo(index))
                    .Play();
            }
        }

        /// <summary>
        /// 计算距离坐标最近的对象
        /// </summary>
        /// <param name="acceptFocus">有序列表（从上往下）</param>
        /// <param name="posY">Y坐标</param>
        /// <returns>返回索引</returns>
        private int _CalculateNearestIndex(List<IAcceptFocus> acceptFocus, float posY)
        {
            if (acceptFocus.Count <= 0)
                return -1;
            if (acceptFocus.Count == 1)
                return 0;
            float min = Mathf.Infinity;
            for (int i = 0; i < acceptFocus.Count; i++)
            {
                float dis = Mathf.Abs(posY - acceptFocus[i].GetLocalPos().y);
                if (dis > min)
                {
                    return i - 1;
                }
                min = dis;
            }
            return acceptFocus.Count - 1;
        }

        /// <summary>
        /// 开始拖动时调用
        /// </summary>
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            m_isFocused = false;
            if (m_focusIndex > 0 && m_focusIndex < m_acceptFocus.Count)
            {
                m_dynamicFocus = m_acceptFocus[m_focusIndex];
                m_dynamicFocus.OnDynamicFocus();
            }
            m_focusIndex = -1;
            m_scrollTweener.Kill();
        }

        /// <summary>
        /// 滑动时调用
        /// </summary>
        protected virtual void OnScoll(Vector2 pos)
        {
            _UpdateFoucs();
        }

        /// <summary>
        /// 结束拖动时调用
        /// </summary>
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            m_isFocused = true;
            _UpdateFoucs();
            int index = _CalculateNearestIndex(m_acceptFocus, m_focusCenter);
            m_focusIndex = index;
            IAcceptFocus focus = m_acceptFocus[index];
            if (m_dynamicFocus != focus)
            {
                m_dynamicFocus?.OnDisFocus();
            }
            focus.OnFocus();
            OnFocus?.Invoke(m_acceptFocus[index].GetObject());
            _StopAndFoucsTo(index);
        }
    }
}