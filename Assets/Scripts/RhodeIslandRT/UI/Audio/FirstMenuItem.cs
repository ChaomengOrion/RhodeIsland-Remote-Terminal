// Created by ChaomengOrion
// Create at 2022-08-13 23:06:15
// Last modified on 2022-08-14 16:00:54

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using RhodeIsland.RemoteTerminal.Audio;

namespace RhodeIsland.RemoteTerminal.UI.Audio
{
    [ComponentColor(ComponentType.ELEMENT)]
    public class FirstMenuItem : UIAnimation
    {
        [SerializeField]
        private Button _btn;
        [SerializeField]
        private MusicGroupType _type;
        [Inject]
        private AudioPage m_audioPage;

        private void Awake()
        {
            _btn.onClick.AddListener(_OnClick);
        }

        private void _OnClick()
        {
            m_audioPage.SetToGroup(_type);
        }

        public override void Play(AnimationClip clip, float delay, out float duration)
        {
            base.Play(clip, delay, out duration);
        }
    }
}