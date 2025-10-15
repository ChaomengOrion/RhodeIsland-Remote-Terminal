// Created by ChaomengOrion
// Create at 2022-07-18 22:37:44
// Last modified on 2022-07-20 13:38:03

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace RhodeIsland.RemoteTerminal.UI.ScrollView
{
    public class StoryInfoItem : VariableAngleItemBase, IAcceptFocus
    {
        [SerializeField]
        protected Color _color, _focusColor, _dynamicFocusColor;
        [SerializeField]
        protected float _focusZ = -80f, _focusDuration = 0.3f, _dynamicFocusDuration = 0.1f;

        [SerializeField]
        protected Text _name, _code, _tag;
        private string m_storyTxt, m_storyInfo;
        private bool m_inTrans = false;
        private bool m_isFocus = false;

        public virtual Vector3 GetLocalPos()
        {
            return _focus.localPosition;
        }

        public virtual void OnDisFocus()
        {
            this.DOKill();
            this.DOColor(_color, m_isFocus ? _focusDuration : _dynamicFocusDuration).SetEase(Ease.InSine).SetUpdate(true).Play();
            if (m_isFocus)
                rectTransform.DOLocalMoveZ(0f, _focusDuration).SetEase(Ease.InSine).SetUpdate(true).Play();
            m_isFocus = false;
        }

        public virtual void OnFocus() 
        {
            m_isFocus = true;
            this.DOKill();
            this.DOColor(_focusColor, _focusDuration).SetEase(Ease.OutSine).SetUpdate(true).Play();
            rectTransform.DOLocalMoveZ(_focusZ, _focusDuration).SetEase(Ease.OutSine).SetUpdate(true).Play();
        }

        public virtual void OnDynamicFocus()
        {
            this.DOColor(_dynamicFocusColor, _dynamicFocusDuration).SetEase(Ease.OutSine).SetUpdate(true).Play();
            if (m_isFocus)
                rectTransform.DOLocalMoveZ(0f, _focusDuration).SetEase(Ease.InSine).SetUpdate(true).Play();
            m_isFocus = false;
        }

        public virtual void OnDistanceChange(float x) { }

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
            return (m_storyTxt, m_storyInfo);
        }

        /// <summary>
        /// 渲染
        /// </summary>
        /// <param name="args">string storyTxt, string storyInfo, string storyCode, string storyName, string storyTag</param>
        public virtual void Render(params object[] args)
        {
            m_storyTxt = (string)args[0];
            m_storyInfo = (string)args[1];
            string code = (string)args[2];
            if (string.IsNullOrEmpty(code))
                _code.text = "NONE";
            else
                _code.text = code;
            _name.text = (string)args[3];
            _tag.text = (string)args[4];
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
