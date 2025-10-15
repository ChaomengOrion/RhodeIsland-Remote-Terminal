// Created by ChaomengOrion
// Create at 2022-08-14 14:15:03
// Last modified on 2022-08-14 14:46:14

using System;
using System.Collections.Generic;

namespace RhodeIsland.RemoteTerminal.Audio
{
    [Serializable]
    public class AudioData
    {
        [UnityEngine.Scripting.Preserve]
        public AudioData() { }

        public Dictionary<string, SongData> songs;

        public Dictionary<MusicGroupType, MusicGroupData[]> groupData;

        public struct SongData
        {
            public string name;

            public string author;

            public string albumPic;

            public string sirenId;
        }

        public struct MusicGroupData
        {
            public string nameCN;

            public string nameEN;

            public string enterPic;

            public string backgroundPic;

            public string[] songsList;
        }
    }
}