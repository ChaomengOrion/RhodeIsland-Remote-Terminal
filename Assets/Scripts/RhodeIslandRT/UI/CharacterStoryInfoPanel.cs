// Created by ChaomengOrion
// Create at 2022-05-01 11:54:22
// Last modified on 2022-07-27 15:01:02

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RhodeIsland.RemoteTerminal.AVG;
using RhodeIsland.RemoteTerminal.GraphicAVG;
using RhodeIsland.RemoteTerminal.Resources;

namespace RhodeIsland.RemoteTerminal.UI
{
    public class CharacterStoryInfoPanel : MonoBehaviour, ICharacterStoryInfoPanel
    {
        [SerializeField]
        private Button _btnPlay;
        [SerializeField]
        private Button _btnTextMode;
        [SerializeField]
        private Text _codeText;
        [SerializeField]
        private Text _introText;
        [SerializeField]
        private Text _textCountText;
        [SerializeField]
        private Text _IDText;
        [SerializeField]
        private Color _valueColor;

        private HandbookAvgData storyData = null;

        public void Init(HandbookAvgData data, int index)
        {
            storyData = data;
            string color = ColorUtility.ToHtmlStringRGBA(_valueColor);
            _codeText.text = $"STORY-{index}";
            if (!string.IsNullOrEmpty(storyData.storyIntro))
            {
                _introText.text = storyData.storyIntro;
            }
            if (!string.IsNullOrEmpty(storyData.storyTxt))
            {
                _IDText.text = "ID: " + storyData.storyTxt;
                _btnPlay.onClick.AddListener(() => StoryPage.instance.RunStory(storyData.storyTxt));
                _btnTextMode.onClick.AddListener(() => StoryPage.instance.RunGraphicStory(storyData.storyTxt));
            }
        }
    }
}