// Created by ChaomengOrion
// Create at 2022-05-14 21:29:27
// Last modified on 2022-08-01 19:07:50

using UnityEngine;
using UnityEngine.UI;
using RhodeIsland.Arknights.AVG;

namespace RhodeIsland.RemoteTerminal.GraphicAVG
{
    public class DialogLine : MonoBehaviour, IDialogLine
    {
        [SerializeField]
        private Text _nameText;
        [SerializeField]
        private Text _messageText;

        public void Init(Command command, int lineCount)
        {
            this.name = $"Line{lineCount} - Dialog";
            if (command.TryGetParam("name", out string name))
            {
                _nameText.text = name;
            }
            else
            {
                Destroy(_nameText.gameObject);
            }
            RectTransform rectTransform = transform.rectTransform();
            rectTransform.sizeDelta = new(rectTransform.sizeDelta.x, _CalculateTextHeight(_messageText, rectTransform.parent.rectTransform(), command.content));
            _messageText.text = command.content;
        }

        private float _CalculateTextHeight(Text textComponent, RectTransform rect, string text)
        {
            TextGenerationSettings settings = textComponent.GetGenerationSettings(rect.rect.size + textComponent.GetPixelAdjustedRect().size);
            return textComponent.cachedTextGeneratorForLayout.GetPreferredHeight(text, settings) / textComponent.pixelsPerUnit;
        }
    }
}