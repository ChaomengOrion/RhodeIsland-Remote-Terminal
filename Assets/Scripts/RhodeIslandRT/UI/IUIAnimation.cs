// Created by ChaomengOrion
// Create at 2022-08-13 16:18:59
// Last modified on 2022-08-13 19:01:56

using UnityEngine;

namespace RhodeIsland.RemoteTerminal.UI
{
    public interface IUIAnimation
    {
        public bool TryGetAnimationIdentify(string key, out string identify);
        public void Reset();
        public void Play(AnimationClip clip, float delay, out float duration);
    }
}