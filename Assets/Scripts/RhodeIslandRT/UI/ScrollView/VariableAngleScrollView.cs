// Created by ChaomengOrion
// Create at 2022-07-05 01:18:28
// Last modified on 2022-08-14 19:07:40

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
    [RequireComponent(typeof(ScrollRect))]
    public class VariableAngleScrollView : AutoFoucsScrollView
    {
        public float Angle
        {
            get => m_angle;
        }

        private float m_angle;
        [SerializeField]
        private VariableAngleBase[] _variableAngles = new VariableAngleBase[0];
        private readonly List<VariableAngleItemBase> m_variableAngleItems = new();
        private Tweener m_angleTweener = null;

        public Tweener DOAngle(float toAngle, float duration)
        {
            return _DOAngle(toAngle, duration).SetEase(Ease.InOutCirc);
        }

        public override void ClearItem()
        {
            m_variableAngleItems.Clear();
            base.ClearItem();
        }

        public void SetAngle(float angle)
        {
            m_angle = angle;
            _UpdateAngle(m_angle);
        }

        protected override void OnReset()
        {
            _UpdateAnglePadding();
            base.OnReset();
        }

        protected override GameObject OnAddItem(GameObject perfab)
        {
            GameObject obj = base.OnAddItem(perfab);
            VariableAngleItemBase item = obj.GetComponentInChildren<VariableAngleItemBase>();
            if (item != null)
            {
                item.Angle = m_angle;
                m_variableAngleItems.Add(item);
            }
            return obj;
        }

        private void _UpdateAnglePadding()
        {
            float y = ScrollRect.content.localPosition.y, height = ScrollRect.viewport.rect.height;
            foreach (VariableAngleItemBase item in m_variableAngleItems)
            {
                item.UpdatePadding(y, height);
            }
        }

        private void _UpdateAngle(float angle)
        {
            float y = ScrollRect.content.localPosition.y, height = ScrollRect.viewport.rect.height;
            foreach (VariableAngleBase item in _variableAngles)
            {
                item.Angle = angle;
                item.SetVerticesDirty();
            }
            foreach (VariableAngleItemBase item in m_variableAngleItems)
            {
                item.Angle = angle;
                item.SetVerticesDirty();
                item.UpdatePadding(y, height);
            }
        }

        private Tweener _DOAngle(float toAngle, float duration)
        {
            m_angleTweener?.Kill();
            m_angleTweener = DOTween.To(() => m_angle, x => { 
                m_angle = x;
                _UpdateAngle(x);
            }, toAngle, duration).SetUpdate(true).Play();
            return m_angleTweener;
        }

        /// <summary>
        /// 滑动时调用
        /// </summary>
        protected override void OnScoll(Vector2 pos)
        {
            _UpdateAnglePadding();
            base.OnScoll(pos);
        }

        /// <summary>
        /// 结束拖动时调用
        /// </summary>
        public override void OnEndDrag(PointerEventData eventData)
        {
            _UpdateAnglePadding();
            base.OnEndDrag(eventData);
        }
    }
}
