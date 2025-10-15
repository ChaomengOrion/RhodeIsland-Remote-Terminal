// Created by ChaomengOrion
// Create at 2022-05-15 08:35:05
// Last modified on 2022-08-01 19:07:50

using UnityEngine;
using UnityEngine.UI;
using RhodeIsland.Arknights.AVG;

namespace RhodeIsland.RemoteTerminal.GraphicAVG
{
    public class ImageLine : MonoBehaviour, IImageLine
    {
        [SerializeField]
        private Image _image;

        public void Init(Command command, Sprite image, int lineCount)
        {
            name = $"Line{lineCount} - Image";
            _image.sprite = image;
        }
    }
}