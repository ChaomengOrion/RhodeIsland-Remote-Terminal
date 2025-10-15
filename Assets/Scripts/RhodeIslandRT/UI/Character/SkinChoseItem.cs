// Created by ChaomengOrion
// Create at 2022-07-31 00:37:22
// Last modified on 2022-07-31 09:33:07

using UnityEngine;
using UnityEngine.UI;
using RhodeIsland.RemoteTerminal.UI.ScrollView;

namespace RhodeIsland.RemoteTerminal.UI.Character
{
    public class SkinChoseItem : ShrinkableScrollViewItem
    {
        [SerializeField]
        private Text _text;

        private string m_skinId;

        public void Init(string skinId, string text)
        {
            m_skinId = skinId;
            _text.text = text;
        }

        protected override void OnSelect()
        {
            CharacterPage.instance.InfoView.SetSkin(m_skinId);
        }
    }
}