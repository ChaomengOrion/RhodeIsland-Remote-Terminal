// Created by ChaomengOrion
// Create at 2022-07-21 00:15:18
// Last modified on 2022-07-21 12:34:46

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RhodeIsland.RemoteTerminal.UI
{
    public class UIRotationElement : MonoBehaviour
    {
        [SerializeField]
        private float _xScale = 1f, _yScale = 1f;
        
        private float x, y, z;

        private void Awake()
        {
            Vector3 angle = transform.eulerAngles;
            x = angle.x;
            y = angle.y;
            z = angle.z;
        }

        public void UpdateRotation(float inX, float inY)
        {
            Vector3 angle = transform.eulerAngles;
            transform.eulerAngles = new(x + inX * _xScale, y + inY * _yScale, z);
        }
    }
}