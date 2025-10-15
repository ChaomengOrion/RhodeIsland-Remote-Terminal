// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-16 00:19:52

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace RhodeIsland.Arknights.Audio
{
	public class AudioSchedule : MonoBehaviour
	{
		public bool isScheduling
		{
			get
			{
				return m_scheduling;
			}
		}

		public float length
        {
			get
            {
				float l = 0f;
				foreach (var s in m_sources)
                {
					l += s.audioSource.clip.length;
                }
				return l;
            }
        }

		public float pos
        {
			get
            {
				return m_cacheLastTime + m_sources[m_index].audioSource.time;
			}
        }

		private AudioChannel.ChannelAudioSource[] m_sources;
		private int m_count, m_index;
        private bool m_playing = false;
		private bool m_dynamic = true;
		private float m_cacheLastTime = 0f;

		public void StartAudioSchedule(AudioChannel.ChannelAudioSource[] sources, int count, float delay = 0f)
		{
			if (m_coroutine != null)
            {
				StopCoroutine(m_coroutine);
            }
			m_scheduling = true;
            m_sources = sources; // Fixed
            m_coroutine = StartCoroutine(_AudioScheduleCoroutine(sources, count, delay));
        }

		public void StopAudioSchedule()
		{
			if (m_coroutine != null)
			{
				StopCoroutine(m_coroutine);
			}
			m_scheduling = false;
			m_playing = false;
		}

		public void SetPos(float t)
        {
			if (t > length) return;
			if (m_dynamic)
			{
				if (m_coroutine != null)
				{
					StopCoroutine(m_coroutine);
				}
				m_scheduling = true;
				m_coroutine = StartCoroutine(_DynamicScheduleCoroutine());
			}
			float sum = 0f;
			for (int i = 0; i < m_count; i++)
            {
				float lastSum = sum;
				sum += m_sources[i].audioSource.clip.length;
				if (t < sum || i + 1 >= m_count)
				{
					m_sources[m_index].audioSource.Stop();
					if (m_index + 1 < m_count) m_sources[m_index + 1].audioSource.Stop();

					m_index = i;
					CalCacheTime();
					m_sources[i].audioSource.time = Mathf.Clamp(t - lastSum, 0f, m_sources[i].audioSource.clip.length);
					if (m_playing)
                    {
						m_sources[i].audioSource.Play();
						if (m_index + 1 < m_count)
						{
							float restSamples = m_sources[m_index].audioSource.clip.samples - m_sources[m_index].audioSource.timeSamples;
							float restTime = restSamples * m_sources[m_index].audioSource.clip.length / m_sources[m_index].audioSource.clip.samples;
							double loopPlayTime = restTime + AudioSettings.dspTime;
							m_sources[m_index + 1].audioSource.PlayScheduled(loopPlayTime);
						}
					}
					return; // fixed
				}
            }
        }

		public void Pause()
        {
			m_sources[m_index].audioSource.Pause();
			if (m_index + 1 < m_count) m_sources[m_index + 1].audioSource.Stop();
			m_playing = false;
		}

		public void Resume()
		{
			m_sources[m_index].audioSource.Play();
			if (m_index + 1 < m_count)
            {
				float restSamples = m_sources[m_index].audioSource.clip.samples - m_sources[m_index].audioSource.timeSamples;
				float restTime = restSamples * m_sources[m_index].audioSource.clip.length / m_sources[m_index].audioSource.clip.samples;
				double loopPlayTime = restTime + AudioSettings.dspTime;
				m_sources[m_index + 1].audioSource.PlayScheduled(loopPlayTime);
			}
			m_playing = true;
		}

		private void CalCacheTime()
		{
			float sum = 0f;
			for (int i = 0; i < m_index; i++)
			{
				sum += m_sources[i].audioSource.clip.length;
			}
			m_cacheLastTime = sum;
		}

		private IEnumerator _DynamicScheduleCoroutine()
		{
			while (true)
			{
				if (m_index + 1 < m_count && m_sources[m_index + 1].audioSource.timeSamples != 0)
				{
					m_index++;
					CalCacheTime();
                    if (m_index + 1 < m_count)
                    {
                        float restSamples = m_sources[m_index].audioSource.clip.samples - m_sources[m_index].audioSource.timeSamples;
                        float restTime = restSamples * m_sources[m_index].audioSource.clip.length / m_sources[m_index].audioSource.clip.samples;
                        double loopPlayTime = restTime + AudioSettings.dspTime;
                        m_sources[m_index + 1].audioSource.PlayScheduled(loopPlayTime);
						m_sources[m_index + 1].audioSource.clip.LoadAudioDataIfNecessary();
                    }
				}
				else
				{
					yield return null;
				}
			}
		}

		[DebuggerHidden]
		private IEnumerator _AudioScheduleCoroutine(AudioChannel.ChannelAudioSource[] sources, int count, float delay = 0f)
		{
			m_count = Mathf.Min(sources.Length, count);
			int restSamples;
			float restTime;
			double loopPlayTime;
			m_index = 0;

            AudioSource source = sources[0].audioSource;
            if (delay <= 0f)
            {
                source.Play();
            }
            else
            {
                source.PlayDelayed(delay);
            }
            if (m_index + 1 < m_count)
            {
                restSamples = m_sources[0].audioSource.clip.samples - m_sources[0].audioSource.timeSamples;
                restTime = restSamples * m_sources[0].audioSource.clip.length / m_sources[0].audioSource.clip.samples;
                loopPlayTime = restTime + AudioSettings.dspTime + Mathf.Max(0f, delay);
                m_sources[m_index + 1].audioSource.PlayScheduled(loopPlayTime);
            }
            m_playing = true;

            while (m_index + 1 < m_count)
            {
				if (sources[m_index + 1].audioSource.timeSamples != 0)
				{
                    m_index++;
                    CalCacheTime();
                    if (m_index + 1 < m_count)
                    {
                        restSamples = m_sources[m_index].audioSource.clip.samples - m_sources[m_index].audioSource.timeSamples;
                        restTime = restSamples * m_sources[m_index].audioSource.clip.length / m_sources[m_index].audioSource.clip.samples;
                        loopPlayTime = restTime + AudioSettings.dspTime;
                        m_sources[m_index + 1].audioSource.PlayScheduled(loopPlayTime);
                        m_sources[m_index + 1].audioSource.clip.LoadAudioDataIfNecessary();
                    }
                }
				else
                {
					yield return null;
				}
			}
			m_scheduling = false;
		}

		private Coroutine m_coroutine;
		private bool m_scheduling;
	}
}