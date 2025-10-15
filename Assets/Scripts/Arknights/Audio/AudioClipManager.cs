// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RhodeIsland.Arknights.ObjectPool;
using UnityEngine;

namespace RhodeIsland.Arknights.Audio
{
	public class AudioClipManager
	{
		public AudioClipManager()
		{
			m_clipPool = new(_CreateClip, new ObjectPool<AudioClipResource>.Options { });
			m_loadedClips = new();
		}

		public Dictionary<string, AudioClipResource> loadedClips
		{
			get
			{
				return null;
			}
		}

		public AudioClip LoadClip(string key, [Optional] string persistTag, bool forceLoadData = false)
		{
			if (!m_loadedClips.TryGetValue(key, out AudioClipResource resource))
            {
				AudioClip clip = Resource.ResourceManager.Load<AudioClip>(key);
				if (clip)
				{
					if (forceLoadData)
					{
						clip.LoadAudioDataIfNecessary();
					}
					resource = m_clipPool.Allocate();
					resource.key = key;
					resource.audioClip = clip;
					m_loadedClips.Add(key, resource);
				}
				else
				{
					return null;
				}
			}
			if (!string.IsNullOrEmpty(persistTag))
            {
				resource.persistTag = persistTag;
            }
			return resource.LoadClip(persistTag != null);
		}

		public bool SetPersistTag(string key, string persistTag)
		{
			return default(bool);
		}
		public bool SetMaxRefAllowed(string key, int maxRefAllowed)
		{
			return default(bool);
		}
		public List<AudioClipResource> FindClipsWithPersistTag(string persistTag)
		{
			return null;
		}

		public void UnloadClipByRef(string key)
		{
			if (m_loadedClips.TryGetValue(key, out AudioClipResource resource))
            {
				if (resource.UnloadClip())
                {
					if (string.IsNullOrEmpty(resource.persistTag))
                    {
						_ReleaseClip(resource);
                    }
                }
            }
		}

		public void UnloadClips(List<AudioClipResource> clips)
		{
		}
		public void UnloadAllWithPersistTag(string persistTag)
		{
		}

		public void UnloadAll()
		{
		}

		private void _ReleaseClip(AudioClipResource clip)
		{
			if (clip.audioClip)
            {
				int id = clip.audioClip.GetInstanceID();
				clip.audioClip = null;
				Resource.ResourceManager.UnloadAssetByInstanceId(id);
            }
			m_loadedClips.Remove(clip.key);
			m_clipPool.Recycle(clip);
		}

		private AudioClipResource _CreateClip()
		{
			return new();
		}

		private ObjectPool<AudioClipResource> m_clipPool;
		private Dictionary<string, AudioClipResource> m_loadedClips;

		public class AudioClipResource : IReusable
		{
			public AudioClip LoadClip(bool preload = false)
			{
				if (maxRefAllowed <=0 || refCount != maxRefAllowed)
                {
					if (!preload)
                    {
						++refCount;
                    }
					return audioClip ? audioClip : Resource.ResourceManager.Load<AudioClip>(key);
                }
				return null;
			}

			public bool UnloadClip()
			{
				refCount = Mathf.Max(refCount - 1, 0);
				return refCount == 0;
			}

			public void OnAllocate()
			{
				refCount = 0;
				maxRefAllowed = 0;
			}

			public void OnRecycle()
			{
				audioClip = null;
				key = null;
				persistTag = null;
			}

			public string key;
			public AudioClip audioClip;
			public int refCount;
			public int maxRefAllowed;
			public string persistTag;
		}
	}
}
