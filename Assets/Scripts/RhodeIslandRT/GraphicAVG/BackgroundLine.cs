// Created by ChaomengOrion
// Create at 2022-05-15 08:53:25
// Last modified on 2022-08-01 19:07:51

using UnityEngine;
using UnityEngine.UI;
using RhodeIsland.Arknights.AVG;

namespace RhodeIsland.RemoteTerminal.GraphicAVG
{
    public class BackgroundLine : MonoBehaviour, IImageLine
    {
        [SerializeField]
        private Image _image;

        public void Init(Command command, Sprite image, int lineCount)
        {
            name = $"Line{lineCount} - Background";
            _image.sprite = image;
        }
    }
}