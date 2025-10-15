// Created by ChaomengOrion
// Create at 2022-07-26 12:55:26
// Last modified on 2022-07-29 01:14:32

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Torappu.UI;

namespace RhodeIsland.RemoteTerminal.UI.Character
{
    public class CharacterIllustHolder
    {
        private UICharIllustAdditionOffset.Offset? m_additionOffset;
        private Sprite m_characterSprite;
        private Texture m_alphaTex;

        public CharacterIllustHolder(GameObject obj)
        {
            Image img = obj.GetComponent<Image>();
            if (!img)
            {
                Debug.LogError(string.Format("{0} don't has image component", obj));
                return;
            }
            m_characterSprite = img.sprite;
            m_alphaTex = img.material.GetTexture("_AlphaTex");
            m_additionOffset = obj.GetComponent<UICharIllustAdditionOffset>()?.GetOffset();
        }

        public void ApplyTo(Image image, bool withOffset = true)
        {
            if (!image)
                return;
            image.sprite = m_characterSprite;
            image.material.SetTexture("_AlphaTex", m_alphaTex);
            if (withOffset && m_additionOffset.HasValue)
            {
                DLog.Log(string.Format("size: {0}, pos: {1}", m_additionOffset.Value.enableSize, m_additionOffset.Value.enablePos));
                if (m_additionOffset.Value.enableSize)
                {
                    image.rectTransform.sizeDelta = m_additionOffset.Value.size;
                }
                if (m_additionOffset.Value.enablePos)
                {
                    image.rectTransform.anchoredPosition = m_additionOffset.Value.position;
                }
            }
            else if (!m_additionOffset.HasValue)
            {
                DLog.Log("Dont has value");
            }
        }

        public void ApplyTo(Material mat)
        {
            mat.SetTexture("_AlphaTex", m_alphaTex);
        }
    }
}