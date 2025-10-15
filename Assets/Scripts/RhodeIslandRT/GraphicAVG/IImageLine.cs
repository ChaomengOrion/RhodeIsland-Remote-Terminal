// Created by ChaomengOrion
// Create at 2022-05-15 08:23:46
// Last modified on 2022-08-01 19:07:49

using UnityEngine;
using RhodeIsland.Arknights.AVG;

namespace RhodeIsland.RemoteTerminal.GraphicAVG
{
    public interface IImageLine
    {
        void Init(Command command, Sprite image, int lineCount);
    }
}