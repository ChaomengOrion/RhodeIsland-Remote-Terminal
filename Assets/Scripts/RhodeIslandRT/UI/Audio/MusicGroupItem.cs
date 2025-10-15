// Created by ChaomengOrion
// Create at 2022-08-14 14:52:59
// Last modified on 2022-08-14 23:59:13

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using VContainer;

namespace RhodeIsland.RemoteTerminal.UI.Audio
{
    [ComponentColor(ComponentType.ELEMENT)]
    public class MusicGroupItem : UIAnimation, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
    {
        [SerializeField]
        private Image _enterPic;
        [SerializeField]
        private Image _backImage;
        [SerializeField]
        private Text _nameCN;
        [SerializeField]
        private Text _nameEN;
        [SerializeField]
        private Color _defaultColor = Color.white;
        [SerializeField]
        private Color _highLightColor = Color.white;
        [SerializeField]
        private Color _downColor = Color.white;

        [Inject]
        private AudioPage m_page;
        private MusicGroupData m_data;
        private bool m_side;

        protected void Start()
        {
            _backImage.color = _defaultColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _backImage.DOKill();
            _backImage.DOColor(_defaultColor, 0.1f);
            m_page.SetToPlayView(m_data);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _backImage.DOKill();
            _backImage.DOColor(_downColor, 0.1f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
#if !UNITY_EDITOR
            if (Application.platform == RuntimePlatform.Android && eventData.pointerId < 0)
                return;
#endif
            _backImage.DOKill();
            _backImage.DOColor(_highLightColor, 0.1f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
#if !UNITY_EDITOR
            if (Application.platform == RuntimePlatform.Android && eventData.pointerId < 0)
                return;
#endif
            _backImage.DOKill();
            _backImage.DOColor(_defaultColor, 0.1f);
        }

        public override bool TryGetAnimationIdentify(string key, out string identify)
        {
            identify = m_side ? "Left" : "Right";
            return true;
        }

        public void Render(MusicGroupData data, bool side)
        {
            m_data = data;
            m_side = side;
            _enterPic.sprite = data.GetEnterPic();
            _nameCN.text = data.GetNameCN();
            _nameEN.text = data.GetNameEN();
        }
    }
}