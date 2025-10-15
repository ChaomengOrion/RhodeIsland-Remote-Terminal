// Created by ChaomengOrion
// Create at 2022-07-23 18:37:15
// Last modified on 2022-07-23 20:45:16

using System;
using System.Collections.Generic;
using UnityEngine;

namespace RhodeIsland.RemoteTerminal.Character
{
    [Serializable]
    public struct GroupInfo
    {
        public object key;
        public Sprite sprite;
        public List<CharacterInfo> characters;
    }
}