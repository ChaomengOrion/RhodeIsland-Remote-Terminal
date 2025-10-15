// Created by ChaomengOrion
// Create at 2022-05-15 12:56:09
// Last modified on 2022-08-01 19:07:51

using System.Collections.Generic;
using RhodeIsland.Arknights.AVG;

namespace RhodeIsland.RemoteTerminal.Search
{
    public struct StoryGroupInfo
    {
        public StoryGroupType Type;
        public string Name;

        public Dictionary<string, string> Groups;
    }
}