// Created by ChaomengOrion
// Create at 2022-07-24 23:51:52
// Last modified on 2022-07-26 18:51:22

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using Sirenix.OdinInspector;
using RhodeIsland.RemoteTerminal.Character;

namespace RhodeIsland.RemoteTerminal.UI.Character
{
    [RequireComponent(typeof(Toggle))]
    public class SortTypeToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
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
        private SortMethod _sortMethod = SortMethod.None;

        private Toggle m_toggle;
        private float m_ogrinalX;
        private RectTransform m_rectTransform;

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
            m_rectTransform = GetComponent<RectTransform>();
            m_ogrinalX = m_rectTransform.anchoredPosition.x;
        }

        protected void Start()
        {
            if (m_toggle.isOn)
            {
                m_toggle.image.color = _selectColor;
                Vector3 pos = m_rectTransform.anchoredPosition3D;
                pos.x = m_ogrinalX + _focusX;
                pos.z = _focusZ;
                m_rectTransform.anchoredPosition3D = pos;
                CharacterPage.instance.SetSortMethods(_sortMethod);
            }
        }

        private void _OnValueChange(bool value)
        {
            m_toggle.image.DOKill();
            m_toggle.image.DOColor(value ? _selectColor : _defaultColor, 0.1f);
            m_rectTransform.DOKill();
            m_rectTransform.DOAnchorPos3DZ(value ? _focusZ : 0f, _moveTime).SetEase(_moveEase).Play();
            m_rectTransform.DOAnchorPos3DX(value ? m_ogrinalX + _focusX : m_ogrinalX, _moveTime).SetEase(_moveEase).Play();
            if (value)
            {
                CharacterPage.instance.SetSortMethods(_sortMethod);
            }
        }
    }
}