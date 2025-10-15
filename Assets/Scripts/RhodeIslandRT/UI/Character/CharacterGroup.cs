// Created by ChaomengOrion
// Create at 2022-07-23 21:18:19
// Last modified on 2022-07-25 00:30:02

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RhodeIsland.RemoteTerminal.Character;
using CharacterInfo = RhodeIsland.RemoteTerminal.Character.CharacterInfo;

namespace RhodeIsland.RemoteTerminal.UI.Character
{
    public class CharacterGroup : MonoBehaviour
    {
        [SerializeField]
        private GameObject _characterPanelPerfab;
        [SerializeField]
        private Text _title;
        [SerializeField]
        private Image _backImage;
        [SerializeField]
        private GridLayoutGroup _contect;
        [SerializeField]
        private float _originHeight;

        private List<CharacterPanel> m_characters = new();

        public void SetUp(GroupInfo info, ref Action onUpdate)
        {
            _title.text = info.key as string;
            //_backImage.sprite = info.sprite;
            foreach (CharacterInfo character in info.characters)
            {
                GameObject obj = Instantiate(_characterPanelPerfab, _contect.transform);
                CharacterPanel characterPanel = obj.GetComponent<CharacterPanel>();
                m_characters.Add(characterPanel);
                characterPanel.Render(character.id, character.rarity, character.nameCN, character.nameFL);
            }
            onUpdate += _UpdateLayout;
        }

        private void _UpdateLayout()
        {
            RectTransform rectTransform = this.rectTransform();
            Vector2 size = rectTransform.sizeDelta;
            size.y = _originHeight + _contect.preferredHeight;
            rectTransform.sizeDelta = size;
        }
    }
}
