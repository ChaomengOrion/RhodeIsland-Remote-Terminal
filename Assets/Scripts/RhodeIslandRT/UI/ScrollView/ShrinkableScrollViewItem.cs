// Created by ChaomengOrion
// Create at 2022-07-30 00:23:14
// Last modified on 2022-07-31 16:22:44

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace RhodeIsland.RemoteTerminal.UI.ScrollView
{
    [RequireComponent(typeof(Graphic))]
    public class ShrinkableScrollViewItem : MonoBehaviour,
        ISelectHandler, IDeselectHandler, IPointerEnterHandler,
        IPointerExitHandler, IPointerClickHandler, IPointerDownHandler,
        IPointerUpHandler
    {
        [SerializeField]
        private Color _defaultColor = Color.white;
        [SerializeField]
        private Color _highLightColor = Color.white;
        [SerializeField]
        private Color _selectColor = Color.white;
        [SerializeField]
        private Color _upperColor = Color.white;

        protected Graphic m_graphic;
        private ShrinkableScrollView m_view;
        private bool m_select = false;
        private bool m_upper = false;
        private Coroutine m_waitClickCoroutine = null;

        protected virtual void Awake()
        {
            m_graphic = GetComponent<Graphic>();
        }

        public void Init(ShrinkableScrollView view)
        {
            m_view = view;
        }

        public void SetUpper()
        {
            m_upper = true;
            m_select = true;
            m_graphic.DOKill();
            m_graphic.color = _upperColor;
            m_view.OnSetOn(this);
            OnSelect();
        }

        public void OnShrinkUpper()
        {
            m_upper = true;
            m_select = true;
            m_graphic.color = _selectColor;
            m_graphic.DOKill();
            m_graphic.DOColor(_upperColor, 0.2f);
        }

        public void OnDisShrinkUpper()
        {
            m_upper = false;
            EventSystem.current.SetSelectedGameObject(gameObject);
            m_graphic.DOKill();
            m_graphic.DOColor(_selectColor, 0.2f);
            m_view.OnSetOn(this);
        }

        /// <summary>
        /// 当选中时调用
        /// </summary>
        protected virtual void OnSelect() { }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_select || m_upper) return;
            m_graphic.DOKill();
            m_graphic.DOColor(_highLightColor, 0.1f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_select || m_upper) return;
            m_graphic.DOKill();
            m_graphic.DOColor(_defaultColor, 0.1f);
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (m_select) return;
            m_graphic.DOKill();
            m_graphic.color = _selectColor;
            m_view.OnSetOn(this);
            m_select = true;
            OnSelect();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            m_view.IfSetOff(this);
        }

        public void OnDeselect()
        {
            m_graphic.DOKill();
            m_graphic.color = _defaultColor;
            m_select = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (m_waitClickCoroutine != null)
                StopCoroutine(m_waitClickCoroutine);
            if (m_upper)
            {
                m_view.SetState(ShrinkableScrollView.State.Unfold);
            }
            else if (!m_select)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!m_select)
            {
                m_view.OnRaiseClick();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_waitClickCoroutine = this.InvokeEndOfFrame(() =>
            {
                m_view.OnRaiseResume();
            });
        }
    }
}