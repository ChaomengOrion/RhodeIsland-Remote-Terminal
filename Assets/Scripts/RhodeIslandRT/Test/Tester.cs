// Created by ChaomengOrion
// Create at 2022-08-01 22:37:40
// Last modified on 2022-09-15 12:35:27

using System;
using System.Collections.Generic;
using UnityEngine;
using RhodeIsland.Arknights;
using RhodeIsland.Arknights.Audio;
using RhodeIsland.Arknights.AVG;
using RhodeIsland.RemoteTerminal.AVG;
using Sirenix.OdinInspector;
using BestHTTP;
using RhodeIsland.RemoteTerminal.UI;
using UnityEngine.UI;

namespace RhodeIsland.RemoteTerminal.TaskSystem
{
    public class Tester : MonoBehaviour
    {
        [SerializeField]
        private Button _btn;
        [SerializeField]
        private InputField _inputField;

        private void Awake()
        {
            _btn.onClick.AddListener(_OnPlayClick);
        }

        [Button("PlayMusic")]
        public void PlayMusic(string key)
        {
            AudioManager.PlayMusic(key);
        }

        [Button("PlayMusicWithIntro")]
        public void PlayMusicWithIntro(string intro, string loop)
        {
            AudioManager.PlayMusicWithIntro(intro, loop);
        }

        [Button("URI")]
        public void Uri()
        {
            HTTPManager.Proxy = new HTTPProxy(new("http://192.168.2.4:11240"), null, true, true, false);
            HTTPRequest req = new(new("https://ak-gs-b.hypergryph.com"), HTTPMethods.Post, (suc, req) => { DLog.Log(req.DataAsText); });
            req.Send();
        }

        [Button("AVG")]
        public void Play(string s)
        {
            StoryPage.instance.RunStory(s);
        }

        private void _OnPlayClick()
        {
            Arknights.AVG.AVG.instance.StartStory(_inputField.text, s =>
            {
                foreach (var channel in AudioManager.instance.channels.Values)
                {
                    channel.Stop(0.2f);
                }
            });
        }
    }
}