// Created by ChaomengOrion
// Create at 2022-04-30 23:39:18
// Last modified on 2022-07-20 15:19:24

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using Sirenix.OdinInspector;
using RhodeIsland.RemoteTerminal.AVG;

namespace RhodeIsland.RemoteTerminal.UI
{
    [RequireComponent(typeof(Toggle))]
    public class StoryTypeToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField]
        private float _focusZ, _focusX;
        [SerializeField, Range(0f, 1f)]
        private float _moveTime = 0.2f;
        [SerializeField]
        private Ease _moveEase = Ease.OutCubic;
        [SerializeField]
        private Color _defaultColor = Color.white;
        [SerializeField]
        private Color _highLightColor = Color.white;
        [SerializeField]
        private Color _downColor = Color.white;
        [SerializeField]
        private Color _selectColor = Color.white;
        [SerializeField]
        private StoryReviewType _reviewType = StoryReviewType.NONE;

        private Toggle m_toggle;
        private float m_ogrinalX;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (m_toggle.isOn)
                return;
            m_toggle.image.DOKill();
            m_toggle.image.DOColor(_downColor, 0.1f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_toggle.isOn)
                return;
            m_toggle.image.DOKill();
            m_toggle.image.DOColor(_highLightColor, 0.1f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_toggle.isOn)
                return;
            m_toggle.image.DOKill();
            m_toggle.image.DOColor(_defaultColor, 0.1f);
        }

        protected void Awake()
        {
            m_toggle = GetComponent<Toggle>();
            m_toggle.onValueChanged.AddListener(_OnValueChange);
            m_ogrinalX = transform.localPosition.x;
        }

        private void _OnValueChange(bool value)
        {
            m_toggle.image.DOKill();
            m_toggle.image.DOColor(value ? _selectColor : _defaultColor, 0.1f);
            transform.DOKill();
            transform.DOLocalMoveZ(value ? _focusZ : 0f, _moveTime).SetEase(_moveEase).Play();
            transform.DOLocalMoveX(value ? _focusX : m_ogrinalX, _moveTime).SetEase(_moveEase).Play();
            if (value)
            {
                StoryPage.instance.SetChapterGroup(_reviewType);
            }
        }
    }
}