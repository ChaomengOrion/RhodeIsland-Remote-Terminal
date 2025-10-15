// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;
//using XLua;
using UnityEngine.Video;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGVideoUnityPanel : ExecutorComponent
	{
		private void Awake()
		{
			_mediaPlayer.targetCameraAlpha = 0f;
			m_hidden = true;
			//_mediaPlayer.Prepare();
		}

		public override Dictionary<string, Executor> GetExecutors()
		{
			return new()
            {
				["video"] = _ExecuteVideo
            };
		}

		public override Dictionary<string, Gather> GetGathers()
		{
			return new()
			{
				["video"] = _GatherVideo
			};
		}

		public AbstractResRefCollecter DontInvoke_PlzImplInternalResRefCollector()
		{
			throw new NotImplementedException();
		}
         
		protected override void ForceCommandEnd()
		{
			_DisposeVideo(FADE_DURATION);
			_SetHiddenInternal(true, true);
		}
         
		private bool _ExecuteVideo(Command command)
		{
			_DisposeVideo(0f);
			if (command.TryGetParam("url", out string url))
            {
				_mediaPlayer.source = VideoSource.Url;
				_mediaPlayer.url = url;
				_mediaPlayer.loopPointReached += source => FinishCommand();
				if (gameObject.activeInHierarchy)
                {
					m_startPlayCoroutine = StartCoroutine(_StartPlayCoroutine());
                }
				else
				{
					_SetHiddenInternal(false, true);
					_mediaPlayer.Play();
					m_startPlayCoroutine = null;
				}
				return true;
            }
			if (command.TryGetParam("res", out string res))
            {
				string fullPath = Resource.RawResManager.GetABFullPath(res);
				if (Resource.RawResManager.CheckExist(res))
				{
					_mediaPlayer.source = VideoSource.Url;
					_mediaPlayer.url = fullPath;
					_mediaPlayer.loopPointReached += source => FinishCommand();
					if (gameObject.activeInHierarchy)
					{
						m_startPlayCoroutine = StartCoroutine(_StartPlayCoroutine());
					}
					else
					{
						_SetHiddenInternal(false, true);
						_mediaPlayer.Play();
						m_startPlayCoroutine = null;
					}
					return true;
				}
				//MODIFY-Rasie windows
				UnityEngine.Debug.Log("[AVG.Video] No Video File.");
				return false;
            }
			else
            {
				UnityEngine.Debug.LogError("[AVG.Video] No url or res!");
				return false;
            }
		}
		
		private string _GatherVideo(Command command)
		{
			if (command.TryGetParam("res", out string res))
			{
				return res;
			}
			return null;
		}

		protected override void OnFinish()
		{
			_DisposeVideo();
			_SetHiddenInternal(true, true);
		}
        
		private void _SetHiddenInternal(bool value, bool force)
		{
			if (value != m_hidden || force)
			{
				m_hidden = value;
				if (force)
				{
					gameObject.SetActive(!value);
					_mediaPlayer.targetCameraAlpha = value ? 0f : 1f;
				}
				else
				{
					tweener?.Kill();
					if (m_hidden)
					{
						tweener = DOTween.To(() => _mediaPlayer.targetCameraAlpha, x => _mediaPlayer.targetCameraAlpha = x , 0f, FADE_DURATION).OnComplete(() => gameObject.SetActive(false));
					}
					else
					{
						gameObject.SetActive(true);
						tweener = DOTween.To(() => _mediaPlayer.targetCameraAlpha, x => _mediaPlayer.targetCameraAlpha = x, 1f, FADE_DURATION);
					}
					tweener.SetEase(_hideEase).SetUpdate(true).SetIgnoreTimeScale(true).Play();
				}
			}
		}
         
		private void _DisposeVideo(float closeDelay = 0f)
		{
			if (m_startPlayCoroutine != null)
            {
				StopCoroutine(m_startPlayCoroutine);
				m_startPlayCoroutine = null;
            }
			if (m_disposeVideoTween != null)
            {
				m_disposeVideoTween.Kill();
				m_disposeVideoTween = null;
            }
			if (MathUtil.LE(closeDelay, 0f))
            {
				_mediaPlayer.Stop();
			}
			else
            {
				m_disposeVideoTween = DOTween.Sequence().SetDelay(closeDelay).SetIgnoreTimeScale(true).AppendCallback(() =>
				{
					_mediaPlayer.Stop();
					m_disposeVideoTween = null;
				}).SetAutoKill(true);
            }
		}
         
		[DebuggerHidden]
		private IEnumerator _StartPlayCoroutine()
		{
			_mediaPlayer.Play();
			while (_mediaPlayer.isPlaying)
            {
				yield return null;
            }
			_SetHiddenInternal(false, true);
			m_startPlayCoroutine = null;
		}
      
		private const float FADE_DURATION = 0.23f;
         
		[SerializeField]
		private VideoPlayer _mediaPlayer;

		[SerializeField]
		private Ease _hideEase = Ease.Linear;
         
		private Coroutine m_startPlayCoroutine;

		private Tween m_disposeVideoTween;

		private Tweener tweener = null;

		private bool m_hidden;

		private class InternalResRefCollector : AbstractResRefCollecter
		{
			public InternalResRefCollector()
			{
			}
			public override void GatherResRefs(Command command, HashSet<string> references)
			{
			}
		}
    }
}