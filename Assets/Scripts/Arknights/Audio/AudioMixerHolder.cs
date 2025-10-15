// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using UnityEngine;
using UnityEngine.Audio;
//using XLua;

namespace RhodeIsland.Arknights.Audio
{
	[CreateAssetMenu(fileName = "AudioMixerHolder", menuName = "Torappu/Creat AudioMixerHolder")]
	public class AudioMixerHolder : ScriptableObject//, IHotfixable
	{
		public AudioMixer mainMixer;
		public AudioMixerGroup musicGroup;
		public AudioMixerGroup voiceGroup;
		public AudioMixerGroup fxGroup;
		public AudioMixerGroup uiFxGroup;
		public AudioMixerGroup importantUIFxGroup;
		public AudioMixerGroup battleFxGroup;
		public AudioMixerGroup importantBattleFxGroup;
	}
}