// Created by ChaomengOrion
// Create at 2022-08-02 21:02:09
// Last modified on 2022-08-15 23:26:28

using System;
using System.Collections.Generic;
using UnityEngine;

namespace RhodeIsland.RemoteTerminal.Resources
{
	[Serializable]
	public class AudioData
	{
		[UnityEngine.Scripting.Preserve]
		public AudioData() { }

		public BGMBank[] bgmBanks;

		//public SoundFXBank[] soundFXBanks;

		//public SoundFXCtrlBank[] soundFXCtrlBanks;

		//public SnapshotBank[] snapshotBanks;

		//public BattleVoiceData battleVoice;

		public MusicData[] musics;

		//public Dictionary<string, SoundFXVoiceLangData> soundFxVoiceLang;

		public Dictionary<string, string> bankAlias;

		public bool TryGetBGMBank(string id, out BGMBank bank)
        {
			foreach (BGMBank mbank in bgmBanks)
            {
				if (mbank.name == id)
                {
					bank = mbank;
					return true;
                }
            }
			bank = null;
			return false;
        }
	}


	[Serializable]
	public class BGMBank
	{
		[UnityEngine.Scripting.Preserve]
		public BGMBank() { }

		public string intro;

		public string loop;

		public float volume;

		public float crossfade;

		public float delay;

		public string name;
	}

	[Serializable]
	public class MusicData
	{
		[UnityEngine.Scripting.Preserve]
		public MusicData() { }

		public string id;

		public string name;

		public string bank;
	}
}