// Created by ChaomengOrion
// Create at 2022-07-20 18:29:38
// Last modified on 2022-07-20 22:14:41

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace RhodeIsland.RemoteTerminal.UI.ScrollView
{
    public class ActivityChapterInfoItem : ChapterInfoItem
    {
        [SerializeField]
        private Text _chapterName, _chapterType, _activityId;

        private string m_id;

        public override object GetObject()
        {
            return m_id;
        }

        /// <summary>
        /// 渲染
        /// </summary>
        /// <param name="args">string id, string chapterName, string chapterType</param>
        public override void Render(params object[] args)
        {
            m_id = (string)args[0];
            _activityId.text = m_id.ToUpper();
            _chapterName.text = _focus.name = (string)args[1];
            _chapterType.text = (string)args[2];
        }
    }
}
