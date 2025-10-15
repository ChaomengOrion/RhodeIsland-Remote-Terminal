// Created by ChaomengOrion
// Create at 2022-05-01 11:20:00
// Last modified on 2022-07-28 01:05:20

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RhodeIsland.RemoteTerminal.AVG;
using RhodeIsland.RemoteTerminal.Resources;

namespace RhodeIsland.RemoteTerminal.UI
{
    public class CharacterStoryGroupPanel : MonoBehaviour, ICharacterStoryGroupPanel
    {
        [SerializeField]
        private Button _btn;
        [SerializeField]
        private Text _nameText;
        [SerializeField]
        private Text _infoText;
        [SerializeField]
        private Text _timeText;
        [SerializeField]
        private Color _valueColor, _nameColor;

        private HandbookAvgGroupData storyData = null;

        public void Init(HandbookAvgGroupData data, int index)
        {
            storyData = data;
            string valueColor = ColorUtility.ToHtmlStringRGBA(_valueColor);
            string nameColor = ColorUtility.ToHtmlStringRGBA(_nameColor);
            if (!string.IsNullOrEmpty(storyData.storySetName))
            {
                _nameText.text =  $"<color=#{nameColor}>{DataConverter.GetCharacterName(data.charId) ?? "NONE"}·</color><i>{storyData.storySetName}</i>";
                name = _nameText.text;
            }
            if (storyData.avgList != null)
            {
                _infoText.text = $"章节数量：<color=#{valueColor}>{storyData.avgList.Count}节</color>\n剧情总长度：<color=#{valueColor}>{0}字</color>";
                //_btn.onClick.AddListener(() => StoryPage.instance.OnGroupClick(storyData));
            }
            string startShowTime = "未知";
            if (storyData.storyGetTime > 0)
            {
                startShowTime = TimeUtil.GetDateTime(storyData.storyGetTime).ToString("yyyy年MM月dd日");
            }
            _timeText.text = $"剧情开放：<color=#{valueColor}>{startShowTime}</color>";
        }
    }
}