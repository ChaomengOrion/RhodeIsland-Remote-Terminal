// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using RhodeIsland.Arknights.Audio;
using RhodeIsland.Arknights.ObjectPool;
//using RhodeIsland.Torappu.Setting;
using UnityEngine;
using UnityEngine.Audio;
//using XLua;

namespace RhodeIsland.Arknights
{
	public class AudioManager : PersistentSingleton<AudioManager>
	{
		public static float musicVolume
		{
			get
			{
				return default(float);
			}
			set
			{
			}
		}
		public static float fxVolume
		{
			get
			{
				return default(float);
			}
			set
			{
			}
		}
		public static float voiceVolume
		{
			get
			{
				return default(float);
			}
			set
			{
			}
		}
		public static SnapshotParam currentSnapshotParam
		{
			get
			{
				return null;
			}
		}
		public static AudioClipManager audioClipManager
		{
			get
			{
				return null;
			}
		}

		public static void Init() { }

		public static AudioChannel PlaySoundFx(string key, float volume = 1f, float delay = 0f, bool loop = false, FXCategory fxCategory = FXCategory.FX_UI, bool important = false, string channel = null)
		{
			return instance._PlaySoundFx(channel, key, loop, volume, delay, important, fxCategory);
		}

		public static AudioChannel PlayMusic(string key, float volume = 1f, float crossfadeDuration = 0.4f, float delay = 0f, string channel = "MUSIC")
		{
			return PlayMusicWithIntro(null, key, volume, crossfadeDuration, delay, channel);
		}

		public static AudioChannel PlayMusicWithIntro(string introKey, string loopKey, float volume = 1f, float crossfadeDuration = 0.4f, float delay = 0f, string channel = "MUSIC")
		{
			return instance._PlayMusic(channel, introKey, loopKey, volume, crossfadeDuration, delay);
		}

		public static AudioChannel PlayVoice(string key, [Optional] string channel, float volume = 1f, float crossfadeDuration = 0.4f, float delay = 0f, bool loop = false)
		{
			return instance._PlayVoice(channel, key, loop, volume, crossfadeDuration, delay);
		}

		public static AudioChannel PlayAudio(string[] keys, string customGroup, float volume = 1f, float crossfadeDuration = 0.4f, float delay = 0f, bool loop = false, string channel = null, bool forceReplay = true)
		{
			return instance._PlayAudio(customGroup, channel, keys, loop, volume, crossfadeDuration, delay, forceReplay);
		}

		public static AudioChannel GetChannel(string channelName)
		{
			instance.m_channels.TryGetValue(channelName, out AudioChannel channel);
			return channel;
		}

		public static AudioChannel GetMusicChannel()
		{
			return GetChannel("MUSIC");
		}

		public static void StopChannel(string channelName, float fadeDuration = 0f)
		{
			GetChannel(channelName)?.Stop(fadeDuration);
		}

		public static void StopMusic(float fadeDuration = 0f)
		{
			StopChannel("MUSIC", fadeDuration);
		}

		public static void PreloadAudioClip(string key, string persistTag, bool forceLoadData = false)
		{
		}
		public static void UnloadPreloadedAudioClips(string persistTag)
		{
		}
		public static void StopPreloadedAudioClips(string persistTag)
		{
		}
		public static void SetAudioClipMaxInstanceCount(string key, int count)
		{
		}
		public static void TransitionToDefaultSnapshot(float duration, float delay = 0f)
		{
		}
		public static bool TransitionToSnapshot(string snapshot, float duration, float delay = 0f)
		{
			return default(bool);
		}
		public static bool TransitionToSnapshot(string[] snapshots, float[] weights, float duration, float delay = 0f)
		{
			return default(bool);
		}
		public static bool TransitionToSnapshot(AudioManager.SnapshotParam snapshotParam, float duration, float delay = 0f)
		{
			return default(bool);
		}
		public static void SetListenerPosition(Vector3 worldPosition, Quaternion worldRotation)
		{
		}
		public ObjectPool<AudioChannel> channelPool
		{
			get
			{
				return null;
			}
		}
		public Dictionary<string, AudioChannel> channels
		{
			get
			{
				return m_channels;
			}
		}
		private float _musicVolume
		{
			get
			{
				return default(float);
			}
			set
			{
			}
		}
		private float _fxVolume
		{
			get
			{
				return default(float);
			}
			set
			{
			}
		}
		private float _voiceVolume
		{
			get
			{
				return default(float);
			}
			set
			{
			}
		}
		private void _Init()
		{
		}
		private AudioChannel _CreateChannel()
		{
			AudioChannel channel = new();
			channel.Init(m_audioSourcesHolder.transform);
			return channel;
		}
		private float _GetMixerParam(string name)
		{
			return default(float);
		}

		private bool _SetMixerParam(string name, float value)
		{
			return default(bool);
		}

		private AudioChannel _PlaySoundFx(string channel, string key, bool loop, float volume, float delay, bool important, FXCategory fxCategory)
		{
			AudioMixerGroup group = null;
			if (fxCategory == FXCategory.FX_BATTLE)
            {
				group = important ? AudioOptions.instance.importantBattleFxGroup : AudioOptions.instance.battleFxGroup;
			}
			if (fxCategory == FXCategory.FX_UI)
            {
				group = important ? AudioOptions.instance.importantUIFxGroup : AudioOptions.instance.uiFxGroup;
			}
			return _PlayAudio(group, channel, new string[1] { key }, loop, volume, 0f, delay, true);
		}

		private AudioChannel _PlayMusic(string channel, string introKey, string loopKey, float volume, float crossfadeDuration, float delay)
		{
			string[] keys;
			if (string.IsNullOrEmpty(introKey))
            {
				keys = new string[1] { loopKey };
            }
			else
            {
				keys = new string[2] { introKey, loopKey };
			}
			return _PlayAudio(AudioOptions.instance.musicGroup, channel, keys, true, volume, crossfadeDuration, delay, false);
		}

		private AudioChannel _PlayVoice(string channel, string key, bool loop, float volume, float crossfadeDuration, float delay)
		{
			return null;
		}
		private AudioChannel _PlayAudio(string customGroup, string channel, string[] keys, bool loop, float volume, float crossfadeDuration, float delay, bool forceReplay)
		{
			return null;
		}
		private AudioChannel _PlayAudio(AudioMixerGroup targetGroup, string channelName, string[] keys, bool loop, float volume, float crossfadeDuration, float delay, bool forceReplay)
		{
			AudioClip[] clips = new AudioClip[keys.Length];
			int i = 0;
			for (; ; i++)
            {
				if (i >= keys.Length)
                {
					if (string.IsNullOrEmpty(channelName))
                    {
						m_allocatedChannelID++;
						channelName = CHANNEL_AUTO + m_allocatedChannelID;
					}
					bool suc;
					if (m_channels.TryGetValue(channelName, out AudioChannel channel))
                    {
						if (!forceReplay)
                        {
							if (channel.loadedClipCount == clips.Length)
                            {
								for (int j = 0; j < clips.Length; j++)
                                {
									AudioChannel.ChannelAudioSource source = channel.GetAudioSource(j);
									if (source.audioSource.clip != clips[j])
                                    {
										goto REPLAY;
                                    }
								}
								for (int j = 0; j < i; j++)
								{
									m_audioClipManager.UnloadClipByRef(keys[j]);
								}
								channel.volume = volume;
								channel.loop = loop;
								return channel;
							}
                        }
					REPLAY:
						m_channels.Remove(channel.name);
						m_allocatedChannelID++;
						channel.name += "_CROSSFADING_" + m_allocatedChannelID;
						m_channels[channel.name] = channel;
						channel.Stop(crossfadeDuration);
						suc = true;
					}
					else
                    {
						suc = false;
                    }
					channel = m_channelPool.Allocate();
					channel.name = channelName;
					if (suc && crossfadeDuration > 0.01f)
                    {
						float time = delay + crossfadeDuration;
						channel.PlayAudio(clips, keys, loop, time);
						channel.volume = 0f;
						channel.TweenVolume(volume, crossfadeDuration, crossfadeDuration + delay);
					}
					else
                    {
						channel.PlayAudio(clips, keys, loop, delay);
						channel.volume = volume;
					}
					channel.outputAudioMixerGroup = targetGroup;
					m_channels[channelName] = channel;
					return channel;
				}
				AudioClip clip = m_audioClipManager.LoadClip(keys[i]);
				clips[i] = clip;
				if (!clip)
                {
					break;
				}
			}
			for (int j = 0; j < i; j++)
			{
				m_audioClipManager.UnloadClipByRef(keys[j]);
			}
			return null;
		}

		private void _SetListenerPosition(Vector3 worldPosition, Quaternion worldRotation)
		{
		}
		private void _UnloadPreloadedAudioClips(string persistTag)
		{
		}
		private void _StopPreloadedAudioClips(string persistTag)
		{
		}
		private void _StopChannelsWithClips(IList<AudioClipManager.AudioClipResource> clips)
		{
		}
		private bool _TransitionSnapshots(AudioManager.SnapshotParam snapshotParam, float duration, float delay)
		{
			return default(bool);
		}
		[DebuggerHidden]
		private IEnumerator _DoTransitionSnapshots(AudioMixerSnapshot[] snapshots, float[] weights, float duration, float delay)
		{
			return null;
		}
		private void _RecycleChannel(AudioChannel channel)
		{
            for (int i = 0; i < channel.loadedClipCount; i++)
            {
				m_audioClipManager.UnloadClipByRef(channel.GetAudioSource(i).loadedKey);
            }
			m_channelPool.Recycle(channel);
		}

		private void _OnAudioOptionsChanged()
		{
		}
		private void _OnAudioConfigChanged(bool isDeviceChanged)
		{
		}
		/*private void _OnSettingChange(SettingConstVars.SettingType type)
		{
		}*/

		protected override void OnInit()
		{
			base.OnInit();
			//MODIFY - Option
			m_audioSourcesHolder = new("AudioSources");
			m_audioSourcesHolder.transform.parent = transform;
			m_audioSourcesHolder.transform.localPosition = Vector3.zero;
			m_channelPool = new(_CreateChannel, new ObjectPool<AudioChannel>.Options { allowPoolAutoReuse = false, preloadSize = 10 });
			m_audioClipManager = new();
			m_channels = new();
			m_tempChannelsToRemove = new();
			if (!m_listener)
            {
				GameObject listener = new("_listener");
				m_listener = listener.AddComponent<AudioListener>();
				listener.transform.parent = transform;
				listener.transform.localPosition = Vector3.zero;
				listener.transform.localScale = Vector3.one;
				listener.transform.localRotation = Quaternion.identity;
            }
			//_OnSettingChange
			//MODIFY
		}

		private void Update()
		{
			foreach (KeyValuePair<string, AudioChannel> channel in m_channels)
            {
				channel.Value.Update();
				if (!channel.Value.isPlaying)
                {
					m_tempChannelsToRemove.Add(channel.Value);
				}
			}
            for (int i = 0; i < m_tempChannelsToRemove.Count; i++)
            {
				m_channels.Remove(m_tempChannelsToRemove[i].name);
				_RecycleChannel(m_tempChannelsToRemove[i]);
			}
			m_tempChannelsToRemove.Clear();
		}

		public const float TIMEEPS = 0.01f;
		public const string SNAPSHOT_DEFAULT = "Default";
		public const string CHANNEL_MUSIC = "MUSIC";
		private const string CHANNEL_AUTO = "AUTO_";
		//private AudioOptions m_audioOptions;
		private AudioClipManager m_audioClipManager;
		private GameObject m_audioSourcesHolder;
		private AudioListener m_listener;
		private ObjectPool<AudioChannel> m_channelPool;
		private Dictionary<string, AudioChannel> m_channels;
		private int m_allocatedChannelID;
		private List<AudioChannel> m_tempChannelsToRemove;
		private float m_musicVolume;
		private float m_fxVolume;
		private float m_voiceVolume;
		private SnapshotParam m_currentSnapshotParam;

		public enum FXCategory
		{
			FX_UI,
			FX_BATTLE
		}

		public class SnapshotParam
		{
			public SnapshotParam()
			{
			}

			public bool IsValid
			{
				get
				{
					return default(bool);
				}
			}

			public string[] snapshots;

			public float[] weights;
		}
	}
}