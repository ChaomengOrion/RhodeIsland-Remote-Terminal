using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
//using XLua;

namespace RhodeIsland.Torappu.AVG
{
	public class AVGVideoPanel : ExecutorComponent
	{
		private void Awake()
		{
			m_canvasGroup = GetComponent<CanvasGroup>();
			m_canvasGroup.alpha = 0f;
			m_hidden = true;
		}

		public override Dictionary<string, Executor> GetExecutors()
		{
			return new()
            {
				["video"] = _ExecuteVideo
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
				_mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, url, true);
				_mediaPlayer.Events.AddListener(_HandlePlayEvent);
				if (gameObject.activeInHierarchy)
                {
					m_startPlayCoroutine = StartCoroutine(_StartPlayCoroutine());
                }
				else
                {
					_mediaPlayer.Play();
					_SetHiddenInternal(false, true);
					m_startPlayCoroutine = null;
				}
				return true;
            }
			if (command.TryGetParam("res", out string res))
            {
				string fullPath = Resource.RawResManager.GetABFullPath(res);
				if (Resource.RawResManager.CheckExist(res))
                {
					_mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, fullPath, true);
					_mediaPlayer.Events.AddListener(_HandlePlayEvent);
					if (gameObject.activeInHierarchy)
					{
						m_startPlayCoroutine = StartCoroutine(_StartPlayCoroutine());
					}
					else
					{
						_mediaPlayer.Play();
						_SetHiddenInternal(false, true);
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
         
		private void _HandlePlayEvent(MediaPlayer player, MediaPlayerEvent.EventType evt, ErrorCode error)
		{
			//MODIFY
			if (evt == MediaPlayerEvent.EventType.FinishedPlaying)
            {
				FinishCommand();
			}
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
					m_canvasGroup.alpha = value ? 0f : 1f;
				}
				else
				{
					m_canvasGroup.DOKill();
					Tweener tweener;
					if (m_hidden)
					{
						tweener = m_canvasGroup.DOFade(0f, FADE_DURATION).OnComplete(() => gameObject.SetActive(false));
					}
					else
					{
						gameObject.SetActive(true);
						tweener = m_canvasGroup.DOFade(1f, FADE_DURATION);
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
				_mediaPlayer.CloseVideo();
            }
			else
            {
				m_disposeVideoTween = DOTween.Sequence().SetDelay(closeDelay).SetIgnoreTimeScale(true).AppendCallback(() =>
                {
					_mediaPlayer.CloseVideo();
					m_disposeVideoTween = null;
				}).SetAutoKill(true);
            }
		}
         
		[DebuggerHidden]
		private IEnumerator _StartPlayCoroutine()
		{
			_mediaPlayer.Play();
			IMediaControl control = _mediaPlayer.Control;
			while (!control.IsFinished())
            {
				yield return null;
            }
			_SetHiddenInternal(false, true);
			m_startPlayCoroutine = null;
		}
      
		private const float FADE_DURATION = 0.23f;
         
		[SerializeField]
		private MediaPlayer _mediaPlayer;

		[SerializeField]
		private Ease _hideEase = Ease.Linear;
         
		private Coroutine m_startPlayCoroutine;

		private Tween m_disposeVideoTween;

		private CanvasGroup m_canvasGroup;

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