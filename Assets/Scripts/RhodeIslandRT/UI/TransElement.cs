// Created by ChaomengOrion
// Create at 2022-08-05 18:08:20
// Last modified on 2022-08-05 22:13:41

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RhodeIsland.RemoteTerminal.UI
{
    public class TransElement : MonoBehaviour
    {
        [SerializeField]
        public GameObject obj;
        public RectTransform rectTransform;
        public Graphic graphic;
        public CanvasGroup canvasGroup;

        protected void OnValidate()
        {
            if (!obj) obj = gameObject;
        }
    }
}