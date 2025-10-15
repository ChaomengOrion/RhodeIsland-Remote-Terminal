// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-05-15 11:38:31

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RhodeIsland.RemoteTerminal.AVG;
using RhodeIsland.RemoteTerminal.GraphicAVG;

namespace RhodeIsland.RemoteTerminal.UI
{
    public class StoryInfoPanel : MonoBehaviour, IStoryInfoPanel
    {
        [SerializeField]
        private Button _btnPlay;
        [SerializeField]
        private Button _btnTextMode;
        [SerializeField]
        private Text _nameText;
        [SerializeField]
        private Text _codeText;
        [SerializeField]
        private Text _tagText;
        [SerializeField]
        private Text _textCountText;
        [SerializeField]
        private Text _IDText;
        [SerializeField]
        private Color _valueColor;

        private StoryReviewInfoClientData storyData = null;

        public void Init(StoryReviewInfoClientData data, int index)
        {
            storyData = data;
            string color = ColorUtility.ToHtmlStringRGBA(_valueColor);
            if (!string.IsNullOrEmpty(storyData.storyName))
            {
                _nameText.text = storyData.storyName;
                name = storyData.storyName;
            }
            if (!string.IsNullOrEmpty(storyData.storyCode))
            {
                _codeText.text = storyData.storyCode;
            }
            if (!string.IsNullOrEmpty(storyData.avgTag))
            {
                _tagText.text = "/ " + storyData.avgTag;
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