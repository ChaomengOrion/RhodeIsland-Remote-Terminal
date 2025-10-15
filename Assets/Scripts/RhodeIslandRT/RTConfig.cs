// Created by ChaomengOrion
// Create at 2022-07-20 22:16:04
// Last modified on 2022-08-14 14:15:04

using System;
using System.Collections.Generic;
using RhodeIsland.RemoteTerminal.Audio;

namespace RhodeIsland.RemoteTerminal
{
    [Serializable]
    public class RTConfig
    {
        [UnityEngine.Scripting.Preserve]
        public RTConfig() { }

        public Dictionary<string, string> activityBG;

        public AudioData audioData;
    }
}
