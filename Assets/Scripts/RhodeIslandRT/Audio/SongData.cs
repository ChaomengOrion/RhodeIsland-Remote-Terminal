// Created by ChaomengOrion
// Create at 2022-08-14 22:09:56
// Last modified on 2022-08-15 01:07:37

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RhodeIsland.RemoteTerminal.Audio
{
    public class SongData
    {
        public SongData(string id, AudioData.SongData songData)
        {
            m_id = id;
            m_name = songData.name;
            m_author = songData.author;
            m_albumPic = songData.albumPic;
            m_sirenId = songData.sirenId;
        }

        public string GetId() => m_id;
        public string GetName() => m_name;
        public string GetAuthor() => m_author;

        private string m_id;
        private string m_name;
        private string m_author;
        private string m_albumPic;
        private string m_sirenId;
    }
}