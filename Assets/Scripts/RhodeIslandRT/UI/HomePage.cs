// Created by ChaomengOrion
// Create at 2022-04-30 12:19:44
// Last modified on 2022-08-14 13:28:44

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace RhodeIsland.RemoteTerminal.UI
{
    [Icon("Assets/StaticAssets/Editor/Free Flat Gear 2 Icon.png"), GUIColor(1f, 0.75f, 0.5f)]
    public class HomePage : SingletonSerializedMonoBehaviour<HomePage>, IPage
    {
        [SerializeField]
        private Dictionary<Button, IPage> _pages;

        protected override void OnInit()
        {
            base.OnInit();
            foreach (KeyValuePair<Button, IPage> pair in _pages)
            {
                pair.Key.onClick.AddListener(() =>
                {
                    PageManager.instance.EnterPage(pair.Value);
                });
            }
        }

        public void OnClose()
        {
            gameObject.SetActive(false);
        }

        public void OnEnter()
        {
            gameObject.SetActive(true);
        }
    }
}