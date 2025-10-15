// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RhodeIsland.RemoteTerminal.AVG;
using RhodeIsland.Arknights.AVG;

namespace RhodeIsland.RemoteTerminal.UI
{
    public class ActivityStoryGroupPanel : MonoBehaviour, IStoryGroupPanel
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
        private Image _image;
        [SerializeField]
        private Color _valueColor;

        private StoryReviewGroupClientData storyData = null;

        public void Init(StoryReviewGroupClientData data, int index)
        {
            storyData = data;
            string color = ColorUtility.ToHtmlStringRGBA(_valueColor);
            if (!string.IsNullOrEmpty(storyData.name))
            {
                _nameText.text = storyData.name;
                name = storyData.name;
            }
            if (storyData.infoUnlockDatas != null)
            {
                _infoText.text = $"章节数量：<color=#{color}>{storyData.infoUnlockDatas.Count}节</color>\n剧情总长度：<color=#{color}>{0}字</color>";
                //_btn.onClick.AddListener(() => StoryPage.instance.OnGroupClick(storyData));
            }
            string startTime = "未知", remakeTime = "未知";
            if (storyData.startTime > 0)
            {
                startTime = TimeUtil.GetDateTime(storyData.startTime).ToString("yyyy年MM月dd日");
            }
            if (storyData.remakeStartTime > 0)
            {
                remakeTime = TimeUtil.GetDateTime(storyData.remakeStartTime).ToString("yyyy年MM月dd日");
            }
            _timeText.text = $"活动开始：<color=#{color}>{startTime}</color>\n活动复刻：<color=#{color}>{remakeTime}</color>";
            Sprite entryPic = null;
            if (!string.IsNullOrEmpty(data.storyEntryPicId))
            {
                entryPic = StoryPage.instance.LoadStoryReviewEntryImage(data.storyEntryPicId, data.actType);
            }
            if (entryPic != null)
            {
                _image.sprite = entryPic;
                _image.color = Color.white;
                _image.SetNativeSize();
            }
            else
            {
                _image.enabled = false;
            }
            if (index % 2 == 0)
            {
                _image.rectTransform.localPosition += new Vector3(80f, 0f);
            }
        }
    }
}