// Created by ChaomengOrion
// Create at 2022-07-23 17:04:44
// Last modified on 2022-07-27 14:46:12

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace RhodeIsland.RemoteTerminal.UI.Character
{
    public class CharacterPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private Image _backImage;
        [SerializeField]
        private Image _rarity;
        [SerializeField]
        private Text _nameCN;
        [SerializeField]
        private Text _nameFL;
        [SerializeField]
        private Color _defaultColor = Color.white;
        [SerializeField]
        private Color _highLightColor = Color.white;
        [SerializeField]
        private Color _downColor = Color.white;

        private string m_id;

        public void OnPointerClick(PointerEventData eventData)
        {
            _backImage.DOKill();
            _backImage.DOColor(_defaultColor, 0.1f);
            CharacterPage.instance.SwitchToCharacter(m_id, _backImage);
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

        public void Render(string id, RarityRank rarityRank, string nameCN, string nameFL)
        {
            m_id = id;
            if (CharacterPage.instance.TryLoadCharacterAvatar(id, out Sprite avatar))
                _image.sprite = avatar;
            _rarity.sprite = CharacterPage.instance.LoadRaritySprite(rarityRank);
            _nameCN.text = nameCN;
            _nameFL.text = nameFL;
        }
    }
}