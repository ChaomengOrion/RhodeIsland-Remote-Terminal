// Created by ChaomengOrion
// Create at 2022-08-13 18:37:40
// Last modified on 2022-08-14 17:08:57

using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace RhodeIsland.RemoteTerminal.UI
{
    public class UIAnimation : MonoBehaviour, IUIAnimation
    {
        [SerializeField]
        private Animation _animation;

        private CancellationTokenSource m_source = null;

        protected virtual void OnValidate()
        {
            _animation = GetComponent<Animation>();
            Reset();
        }

        public virtual void Play(AnimationClip clip, float delay, out float duration)
        {
            m_source?.Cancel();
            _animation.Stop();
            if (clip == null)
            {
                duration = 0f;
                return;
            }
            clip.SampleAnimation(gameObject, 0f);
            _animation.AddClip(clip, clip.name);
            _animation.clip = clip;
            duration = delay + clip.length;
            m_source = new();
            UniTask.Create(async () =>
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
                if (!m_source.Token.IsCancellationRequested)
                    _animation.Play();
            }).Forget();
        }

        public virtual void Reset()
        {
            if (_animation)
            {
                _animation.clip = null;
                _animation.playAutomatically = false;
            }
        }

        public virtual bool TryGetAnimationIdentify(string key, out string identify)
        {
            identify = null;
            return true;
        }
    }
}