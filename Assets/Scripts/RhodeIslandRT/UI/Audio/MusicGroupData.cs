// Created by ChaomengOrion
// Create at 2022-08-14 15:02:14
// Last modified on 2022-08-14 22:21:25

using System;
using UnityEngine;
using RhodeIsland.RemoteTerminal.Audio;
using RhodeIsland.Arknights.Resource;

namespace RhodeIsland.RemoteTerminal.UI.Audio
{
    public class MusicGroupData
    {
        public MusicGroupData(AudioData.MusicGroupData data, Func<string, SongData> songGetter)
        {
            m_nameCN = data.nameCN;
            m_nameEN = data.nameEN;
            m_enterPic = data.enterPic;
            m_backgroundPic = data.backgroundPic;
            m_songsList = data.songsList;
            m_songGetter = songGetter;
        }

        public string GetNameCN() => m_nameCN;
        public string GetNameEN() => m_nameEN;
        public Sprite GetEnterPic() => string.IsNullOrEmpty(m_enterPic) ? null : ResourceManager.Load<Sprite>(m_enterPic);
        public Sprite GetBackgroundPic() => string.IsNullOrEmpty(m_backgroundPic) ? null : ResourceManager.Load<Sprite>(m_backgroundPic);
        
        public SongData[] GetSongsList()
        {
            SongData[] datas = new SongData[m_songsList.Length];
            for (int i = 0; i < m_songsList.Length; i++)
                datas[i] = m_songGetter.Invoke(m_songsList[i]);
            return datas;
        }

        private string m_nameCN;

        private string m_nameEN;

        private string m_enterPic;

        private string m_backgroundPic;

        private string[] m_songsList;

        private Func<string, SongData> m_songGetter;
    }
}