// Created by ChaomengOrion
// Create at 2022-07-16 23:51:06
// Last modified on 2022-07-19 23:18:05

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace RhodeIsland.RemoteTerminal.UI.ScrollView
{
    public class MainLineChapterInfoItem : ChapterInfoItem
    {
        [SerializeField]
        private Text _chapterName, _chapterIndex, _chapterEnglishIndex;

        private string m_id;

        public override object GetObject()
        {
            return m_id;
        }

        /// <summary>
        /// 渲染
        /// </summary>
        /// <param name="args">string id, string chapterName, string chapterIndex, string chapterEnglishIndex</param>
        public override void Render(params object[] args)
        {
            m_id = (string)args[0];
            _chapterName.text = _focus.name = (string)args[1];
            _chapterIndex.text = (string)args[2];
            _chapterEnglishIndex.text = (string)args[3];
        }
    }
}
