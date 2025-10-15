// Created by ChaomengOrion
// Create at 2022-08-09 23:56:13
// Last modified on 2022-08-09 23:59:22

using UnityEngine;

namespace RhodeIsland.RemoteTerminal.UI
{
    public class AnimationKey : MonoBehaviour
    {
        public string Key
        {
            get => _key;
            set { _key = value; }
        }

        [SerializeField]
        private string _key;
    }
}