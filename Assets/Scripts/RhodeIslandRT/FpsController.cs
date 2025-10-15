// Created by ChaomengOrion
// Create at 2022-07-27 00:26:25
// Last modified on 2022-07-27 00:33:21

using UnityEngine;

namespace RhodeIsland.RemoteTerminal
{
    public class FpsController : MonoBehaviour
    {
        protected void Awake()
        {
            Application.targetFrameRate = Screen.currentResolution.refreshRate;
        }
    }
}