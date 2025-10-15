// Created by ChaomengOrion
// Create at 2022-07-28 00:07:51
// Last modified on 2022-07-28 01:22:09

using System.Text;
using UnityEngine;
using UnityEngine.UI;
using RhodeIsland.RemoteTerminal.Resources;

namespace RhodeIsland.RemoteTerminal.UI.Character
{
    public class InfoTextGroup : MonoBehaviour
    {
        #region SerializeField
        [SerializeField]
        private Text _title;
        [SerializeField]
        private Text _text;
        [SerializeField]
        private float _baseHeight;
        #endregion

        #region PublicMethods
        public void Render(HandBookStoryViewData data)
        {
            StringBuilder sb = new();
            for (int i = 0; i < data.stories.Count; i++)
            {
                if (i > 0)
                    sb.Append('\n');
                sb.Append(data.stories[i].storyText);
            }
            _text.text = sb.ToString();
            _title.text = data.storyTitle;
            float height = _CalculateTextHeight(_text, _text.text);
            Vector2 size = _text.rectTransform.sizeDelta;
            size.y = height;
            _text.rectTransform.sizeDelta = size;
            size = this.rectTransform().sizeDelta;
            size.y = _baseHeight + height;
            this.rectTransform().sizeDelta = size;
        }
        #endregion

        #region PrivateMethods
        private static float _CalculateTextHeight(Text textComponent, string text)
        {
            TextGenerationSettings settings = textComponent.GetGenerationSettings(textComponent.GetPixelAdjustedRect().size);
            return textComponent.cachedTextGeneratorForLayout.GetPreferredHeight(text, settings) / textComponent.pixelsPerUnit;
        }
        #endregion
    }
}