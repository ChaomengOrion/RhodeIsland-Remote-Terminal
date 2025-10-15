// Created by ChaomengOrion
// Create at 2022-08-08 09:17:19
// Last modified on 2022-08-15 00:23:28

using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

namespace RhodeIsland.RemoteTerminal.UI
{
    [ComponentColor(ComponentType.MANAGER)]
    public class UIAnimationPlayer : SerializedMonoBehaviour
    {
        public List<IUIAnimation> Animations => m_animations;

        [SerializeField, LabelText("启动时自动获得组件")]
        private bool _autoAddComponetsOnAwake = true;
        [SerializeField]
        private Dictionary<string, AnimationClipData> _animationsDict = new();

        private readonly List<IUIAnimation> m_animations = new();

        [Serializable]
        private class AnimationClipData
        {
            [SerializeField]
            private AnimationClip defaultClip;
            [SerializeField]
            private Dictionary<string, AnimationClip> clips = new();

            public AnimationClip GetValue(string key)
            {
                if (key != null && clips != null && clips.TryGetValue(key, out AnimationClip clip))
                    return clip;
                return defaultClip;
            }
        }

        protected void Awake()
        {
            ClearAnimations();
            ResetAnimations(_autoAddComponetsOnAwake);
        }

        public void ClearAnimations()
        {
            m_animations.Clear();
        }

        public void ResetAnimations(bool autoAddComponets = false)
        {
            if (autoAddComponets)
            {
                IUIAnimation item = GetComponent<IUIAnimation>();
                if (item != null) m_animations.Add(item);
                m_animations.AddRange(GetComponentsInChildren<IUIAnimation>(true));
            }
            foreach (IUIAnimation item in m_animations)
            {
                item.Reset();
            }
        }

        /// <summary>
        /// Play animations with a key from <see cref="_animationsDict"/>
        /// </summary>
        /// <param name="key">The key of animation group to play</param>
        /// <param name="eachDelay">The delay between each animation</param>
        /// <returns>A <see cref="UniTask"/> which will finish when all animations have finished</returns>
        [Button("Play", ButtonSizes.Small)]
        public async UniTask Play(string key, float eachDelay = -1f)
        {
            if (!_animationsDict.TryGetValue(key, out AnimationClipData clipData))
            {
                DLog.LogWarning(string.Format("Cannot find key {0} in the animations dict", key));
                return;
            }
            if (eachDelay < 0f)
            {
                eachDelay = 0f;
            }
            float endTime = 0f;
            for (int i = 0; i < m_animations.Count; i++)
            {
                if (m_animations[i].TryGetAnimationIdentify(key, out string identify))
                {
                    m_animations[i].Play(clipData.GetValue(identify), i * eachDelay, out float duration);
                    float et = Time.time + duration;
                    if (et > endTime)
                    {
                        endTime = et;
                    }
                }
                else
                {
                    DLog.LogWarning(string.Format("Cannot find identify {0} in the animation {1}'s data", identify, key));
                }
            }
            float t = Time.time;
            if (endTime > t)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(endTime - t));
            }
        }

        /// <summary>
        /// Play animations with a key from <see cref="_animationsDict"/>
        /// </summary>
        /// <param name="key">The key of animation group to play</param>
        /// <param name="delayGetter">The getter to change the delay between each animation</param>
        /// <returns>A <see cref="UniTask"/> which will finish when all animations have finished</returns>
        public async UniTask Play<T>(string key, Func<T, float> delayGetter) where T : IUIAnimation
        {
            if (!_animationsDict.TryGetValue(key, out AnimationClipData clipData))
            {
                DLog.LogWarning(string.Format("Cannot find key {0} in the animations dict", key));
                return;
            }
            float endTime = 0f;
            foreach (IUIAnimation animation in m_animations)
            {
                if (animation.TryGetAnimationIdentify(key, out string identify))
                {
                    float delay = Mathf.Max(0f, delayGetter.Invoke((T)animation));
                    animation.Play(clipData.GetValue(identify), delay, out float duration);
                    float et = Time.time + duration;
                    if (et > endTime)
                    {
                        endTime = et;
                    }
                }
                else
                {
                    DLog.LogWarning(string.Format("Cannot find identify {0} in the animation {1}'s data", identify, key));
                }
            }
            float t = Time.time;
            if (endTime > t)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(endTime - t));
            }
        }
    }
}