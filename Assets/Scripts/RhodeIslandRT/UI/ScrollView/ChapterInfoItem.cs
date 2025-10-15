// Created by ChaomengOrion
// Create at 2022-07-05 13:19:19
// Last modified on 2022-07-20 13:37:56

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace RhodeIsland.RemoteTerminal.UI.ScrollView
{
    public class ChapterInfoItem : VariableAngleItemBase, IAcceptFocus
    {
        [SerializeField]
        protected Color _color, _focusColor;
        [SerializeField]
        protected float _focusDistence, _focusScale, _transDistence, _transHeight;

        private bool m_inTrans;

        public virtual Vector3 GetLocalPos()
        {
            return _focus.localPosition;
        }

        public virtual void OnDisFocus() { }

        public virtual void OnFocus() { }

        public virtual void OnDynamicFocus() { }

        public virtual void OnDistanceChange(float x)
        {
            if (x >= 0)
            {
                float t = Mathf.Abs(x / _focusDistence);
                color = Color.Lerp(_focusColor, _color, t);
                Vector3 pos = _focus.localPosition;
                pos.z = _DistenceToHeightByCos(x, _transDistence, _transHeight);
                _focus.localPosition = pos;
                _focus.localScale = Vector3.one * Mathf.Lerp(_focusScale, 1f, t);
            }
        }

        public override void UpdatePadding(float contentY, float viewHeight)
        {
            if (!m_inTrans)
                base.UpdatePadding(contentY, viewHeight);
        }

        public virtual void OnEnter(float contentY, float viewHeight, float duration)
        {
            m_inTrans = true;
            (Vector2, Vector2) pos = _CalculateAnchoredAndSizeDelta(contentY, viewHeight);
            m_rectTransform.anchoredPosition = new Vector2(pos.Item1.x - 1200f, 0f);
            m_rectTransform.DOAnchorPos(pos.Item1, duration).SetEase(Ease.OutBack).OnComplete(() => m_inTrans = false).Play();
            m_rectTransform.sizeDelta = pos.Item2;
        }

        public virtual void OnExit(float duration)
        {
            m_inTrans = true;
            m_rectTransform.DOKill();
            m_rectTransform.DOAnchorPos(m_rectTransform.anchoredPosition - new Vector2(1200f, 0f), duration).SetEase(Ease.OutCubic).Play();
        }

        public virtual object GetObject()
        {
            Debug.LogWarning("此函数缺少实现");
            return null;
        }

        public virtual void Render(params object[] args)
        {
            Debug.LogWarning("此函数缺少实现");
        }

        /// <summary>
        /// 通过余弦函数将距离转换至高度
        /// <para/>
        /// 函数算法:
        /// <code>
        /// y = a * [cos(x / k * Π) + 1],
        /// y = 0, (|x| >= k).
        /// </code>
        /// k为变换结束位置到中轴距离, a为高度
        /// </summary>
        private float _DistenceToHeightByCos(float x, float k, float a)
        {
            return Mathf.Abs(x) < k ? a * (Mathf.Cos(x / k * Mathf.PI) + 1f) / 2f : 0f;
        }
    }
}
