// Created by ChaomengOrion
// Create at 2022-08-14 14:12:14
// Last modified on 2022-08-14 23:48:19

using RhodeIsland.RemoteTerminal.Audio;

namespace RhodeIsland.RemoteTerminal.UI.Audio
{
    public class AudioDataHolder
    {
        private readonly AudioData m_audioData;

        public AudioDataHolder(AudioData data)
        {
            m_audioData = data;
        }

        public bool TryGetGroupData(MusicGroupType key, out MusicGroupData[] groupDatas)
        {
            if (m_audioData.groupData.TryGetValue(key, out var datas))
            {
                groupDatas = new MusicGroupData[datas.Length];
                for (int i = 0; i < datas.Length; i++)
                {
                    groupDatas[i] = new(datas[i], GetSongData);
                }
                return true;
            }
            groupDatas = null;
            return false;
        }

        public SongData GetSongData(string id)
        {
            return new(id, m_audioData.songs[id]);
        }
    }
}