// Created by ChaomengOrion
// Create at 2022-07-05 13:21:03
// Last modified on 2022-07-19 23:59:39

using UnityEngine;

namespace RhodeIsland.RemoteTerminal.UI.ScrollView
{
    public interface IAcceptFocus
    {
        /// <summary>
        /// 获得本地坐标
        /// </summary>
        public Vector3 GetLocalPos();
        /// <summary>
        /// 聚焦距离改变时
        /// </summary>
        public void OnDistanceChange(float x);
        /// <summary>
        /// 当聚焦时
        /// </summary>
        public void OnFocus();
        /// <summary>
        /// 当动态聚焦时
        /// </summary>
        public void OnDynamicFocus(); 
        /// <summary>
        /// 失去聚焦时
        /// </summary>
        public void OnDisFocus();
        public object GetObject();
    }
}