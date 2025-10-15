// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using UnityEngine;

namespace RhodeIsland.Arknights
{
	public static class AudioExtensions
	{
		public static void LoadAudioDataIfNecessary(this AudioClip clip)
		{
			if (clip)
            {
				if (clip.preloadAudioData || clip.loadState != AudioDataLoadState.Loaded)
                {
					clip.LoadAudioData();
                }
            }
		}
	}
}