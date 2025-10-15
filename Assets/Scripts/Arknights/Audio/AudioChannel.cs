// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-16 00:20:34

using System;
using RhodeIsland.Arknights.ObjectPool;
using UnityEngine;
using UnityEngine.Audio;

namespace RhodeIsland.Arknights.Audio
{
	public class AudioChannel : IReusable
	{
		public string name
		{
			get
			{
				return m_gameObject.name;
			}
			set
			{
				m_gameObject.name = value;
			}
		}

		public float volume
		{
			get
			{
				return m_currentVolume;
			}
			set
			{
				m_tweenTargetVolume = -1f;
				m_currentVolume = value;
				_UpdateVolumes();
			}
		}

		public float pitch
		{
			get
			{
				return m_audioSources[0].audioSource.pitch;
			}
			set
			{
				for (int i = 0; i < m_audioSources.Length; i++)
				{
					m_audioSources[i].audioSource.pitch = value;
				}
			}
		}

		public bool loop
		{
			get
			{
				return m_audioSources[loadedClipCount - 1].audioSource.loop;
			}
			set
			{
				m_audioSources[loadedClipCount - 1].audioSource.loop = value;
			}
		}

		public float spatialBlend
		{
			get
			{
				return m_audioSources[0].audioSource.spatialBlend;
			}
			set
			{
				for (int i = 0; i < m_audioSources.Length; i++)
				{
					m_audioSources[i].audioSource.spatialBlend = value;
				}
			}
		}

		public Vector3 position
		{
			get
			{
				return m_gameObject.transform.position;
			}
			set
			{
				m_gameObject.transform.position = value;
			}
		}

		public AudioMixerGroup outputAudioMixerGroup
		{
			get
			{
				return m_audioSources[0].audioSource.outputAudioMixerGroup;
			}
			set
			{
				for (int i = 0; i < m_audioSources.Length; i++)
				{
					m_audioSources[i].audioSource.outputAudioMixerGroup = value;
				}
			}
		}

		public bool isPlaying
		{
			get
			{
				if (!m_audioSchedule || !m_audioSchedule.isScheduling)
                {
                    for (int i = 0; i < m_audioSources.Length; i++)
                    {
						if (m_audioSources[i].audioSource.isPlaying)
                        {
							return true;
                        }
                    }
					return false;
                }
				return true;
			}
		}

		public float length
		{
			get
			{
				return default(float);
			}
		}

		public AudioSchedule audioSchedule
		{
			get
			{
				if (m_audioSchedule)
                {
					return m_audioSchedule;
				}
				AudioSchedule audioSchedule = m_gameObject.AddComponent<AudioSchedule>();
				m_audioSchedule = audioSchedule;
				return audioSchedule;
			}
		}

		public void Init(Transform parent)
		{
			GameObject channel = m_gameObject = new();
			channel.transform.parent = parent;
			channel.transform.localPosition = Vector3.zero;
			channel.transform.localScale = Vector3.one;
			channel.transform.localRotation = Quaternion.identity;
			OnRecycle();
		}

		public ChannelAudioSource GetAudioSource(int index)
		{
			if (m_audioSources != null && m_audioSources.Length > index)
            {
				return m_audioSources[index];
			}
			return null;
		}

		public void TweenVolume(float tagetVolume, float duration, float delay = 0f)
		{
			m_stopWhenTweenEnd = false;
			m_tweenStartVolume = m_currentVolume;
			m_tweenTargetVolume = tagetVolume;
			m_tweenStartTime = Time.unscaledTime + delay;
			m_tweenEndTime = Time.unscaledTime + duration + delay;
		}

		public void StopTweenVolume()
		{
			m_tweenTargetVolume = -1f;
		}

		public void PlayAudio(AudioClip[] clips, string[] clipKeys, bool loop, float delay)
		{
			m_stopWhenTweenEnd = false;
			m_tweenTargetVolume = -1f;
			loadedClipCount = clips.Length;
			_PrepareAudioSources(loadedClipCount);
            for (int i = 0; i < clips.Length; i++)
            {
				m_audioSources[i].loadedKey = clipKeys[i];
				m_audioSources[i].audioSource.clip = clips[i];
				m_audioSources[i].audioSource.loop = loop && i == clips.Length - 1;
            }
			if (loadedClipCount == 1)
            {
				AudioSource source = m_audioSources[0].audioSource;
				if (Math.Abs(delay) >= 0.01f)
                {
					source.PlayDelayed(delay);
                }
				else
                {
					source.Play();
                }
            }
			else
            {
				audioSchedule.StartAudioSchedule(m_audioSources, loadedClipCount, delay > 0f ? delay : 0f);
            }
		}

		public void Stop(float duration = 0f)
		{
			if (Mathf.Abs(duration) <= 0.01f)
            {
				_Stop();
            }
			else
            {
				TweenVolume(0f, duration);
				m_stopWhenTweenEnd = true;
			}
			m_audioSchedule?.StopAudioSchedule();
		}

		public bool ContainsAudioClipKey(string key)
		{
			return default(bool);
		}

		private void _UpdateVolumes()
		{
            for (int i = 0; i < m_audioSources.Length; i++)
            {
				m_audioSources[i].audioSource.volume = m_currentVolume;
            }
		}

		private void _Stop()
		{
			if (loadedClipCount > 0)
            {
                for (int i = 0; i < m_audioSources.Length; i++)
                {
					m_audioSources[i].audioSource.Stop();
                }
            }
		}

		private void _PrepareAudioSources(int count)
		{
			if (m_audioSources.Length < count)
            {
				Array.Resize(ref m_audioSources, count);
				for (int i = 0; i < m_audioSources.Length; i++)
                {
					ChannelAudioSource source = _CreateAudioSource();
					m_audioSources[i] = source;
                }
            }
			if (count > 0)
            {
                for (int i = 0; i != count; i++)
                {
					m_audioSources[i].OnAllocate();
                }
            }
		}

		private ChannelAudioSource _CreateAudioSource()
		{
			ChannelAudioSource audioSource = new();
			AudioSource soruce = m_gameObject.AddComponent<AudioSource>();
			audioSource.audioSource = soruce;
			soruce.playOnAwake = false;
			return audioSource;
		}

		public void OnAllocate()
		{
			m_currentVolume = 0f;
			m_tweenTargetVolume = -1f;
			m_gameObject.SetActive(true);
		}

		public void OnRecycle()
		{
			onChannelRecycled?.Invoke();
			m_gameObject.name = null;
			m_gameObject.SetActive(false);
            for (int i = 0; i < m_audioSources.Length; i++)
            {
				m_audioSources[i].OnRecycle();
            }
			loadedClipCount = 0;
			m_gameObject.transform.localPosition = Vector3.zero;
		}

		public void Update()
		{
			if (m_tweenTargetVolume >= 0f)
            {
				if (m_tweenStartTime >= Time.unscaledTime)
                {
					m_currentVolume = m_tweenStartVolume;
                }
				else
                {
					if (Time.unscaledTime < m_tweenEndTime)
                    {
						m_currentVolume = Mathf.Lerp(m_tweenStartVolume, m_tweenTargetVolume, (Time.unscaledTime - m_tweenStartTime) / (m_tweenEndTime - m_tweenStartTime));
                    }
					else
                    {
						m_currentVolume = m_tweenTargetVolume;
						m_tweenTargetVolume = -1f;
						if (m_stopWhenTweenEnd)
                        {
							m_stopWhenTweenEnd = false;
							_Stop();
                        }
                    }
                }
				_UpdateVolumes();
            }
		}

		public int loadedClipCount;
		public Action onChannelRecycled;
		private GameObject m_gameObject;
		private ChannelAudioSource[] m_audioSources = new ChannelAudioSource[0];
		private float m_currentVolume;
		private float m_tweenStartVolume;
		private float m_tweenTargetVolume;
		private float m_tweenStartTime;
		private float m_tweenEndTime;
		private bool m_stopWhenTweenEnd;
		private AudioSchedule m_audioSchedule;

		public class ChannelAudioSource : IReusable
		{
			public void OnAllocate()
			{
				audioSource.pitch = 1f;
				audioSource.rolloffMode = AudioRolloffMode.Linear;
				audioSource.volume = 1f;
				audioSource.enabled = true;
				audioSource.spatialBlend = 0f;
				audioSource.loop = false;
			}

			public void OnRecycle()
			{
				audioSource.Stop();
				audioSource.clip = null;
				audioSource.outputAudioMixerGroup = null;
				audioSource.enabled = false;
			}

			public AudioSource audioSource;
			public string loadedKey;
		}
	}
}