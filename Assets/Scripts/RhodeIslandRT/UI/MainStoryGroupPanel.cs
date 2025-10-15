// Created by ChaomengOrion
// Create at 2022-04-30 21:22:52
// Last modified on 2025-02-27 21:57:20

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RhodeIsland.RemoteTerminal.AVG;

namespace RhodeIsland.RemoteTerminal.UI
{

    public class MainStoryGroupPanel : MonoBehaviour, IStoryGroupPanel
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
                _nameText.text = "EP" + index + '·' + storyData.name;
                name = _nameText.text;
            }
            if (storyData.infoUnlockDatas != null)
            {
                _infoText.text = $"章节数量：<color=#{color}>{storyData.infoUnlockDatas.Count}节</color>\n剧情总长度：<color=#{color}>{0}字</color>";
                //_btn.onClick.AddListener(() => StoryPage.instance.OnGroupClick(storyData));
            }
            string startShowTime = "未知";
            if (storyData.startShowTime > 0)
            {
                startShowTime = TimeUtil.GetDateTime(storyData.startShowTime).ToString("yyyy年MM月dd日");
            }
            _timeText.text = $"章节开放：<color=#{color}>{startShowTime}</color>";
            if (StoryPage.instance.TryLoadMainLineImage(storyData.id, out Sprite sprite))
            {
                _image.sprite = sprite;
                _image.color = Color.white;
            }
            else
            {
                _image.enabled = false;
            }
            if (index % 2 == 0)
            {
                _image.rectTransform.localPosition += new Vector3(50f, 0f);
            }
        }
    }
}