// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using UnityEngine;
using UnityEngine.Audio;
//using XLua;

namespace RhodeIsland.Arknights.Audio
{
	[CreateAssetMenu(fileName = "AudioOption", menuName = "Torappu/Creat AudioOption")]
	public class AudioOptions : SingletonScriptableObject<AudioOptions>//, IHotfixable
	{
		public event Action onOptionsChanged { add { } remove { } }

		protected AudioMixerHolder activeMixerHolder
		{
			get
			{
				return _staticLinkHolder;
			}
		}

		public AudioMixer mainMixer
		{
			get
			{
				return _staticLinkHolder.mainMixer;
			}
		}

		public AudioMixerGroup musicGroup
		{
			get
			{
				return _staticLinkHolder.musicGroup;
			}
		}

		public AudioMixerGroup voiceGroup
		{
			get
			{
				return _staticLinkHolder.voiceGroup;
			}
		}

		public AudioMixerGroup fxGroup
		{
			get
			{
				return _staticLinkHolder.fxGroup;
			}
		}

		public AudioMixerGroup uiFxGroup
		{
			get
			{
				return _staticLinkHolder.uiFxGroup;
			}
		}

		public AudioMixerGroup importantUIFxGroup
		{
			get
			{
				return _staticLinkHolder.importantUIFxGroup;
			}
		}

		public AudioMixerGroup battleFxGroup
		{
			get
			{
				return _staticLinkHolder.battleFxGroup;
			}
		}

		public AudioMixerGroup importantBattleFxGroup
		{
			get
			{
				return _staticLinkHolder.importantBattleFxGroup;
			}
		}

		private const string CONFIG_RES_PATH = "Audio/Sound_Beta_2/[X]Configs/main_mixer_holder";

		public string[] musicVolumeParams;

		public string[] voiceVolumeParams;

		public string[] fxVolumeParams;

		public int channelPreloadSize;

		[SerializeField]
		private AudioMixerHolder _staticLinkHolder;
	}
}
