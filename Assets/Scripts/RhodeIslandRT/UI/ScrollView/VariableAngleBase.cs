// Created by ChaomengOrion
// Create at 2022-07-18 17:34:16
// Last modified on 2022-07-18 17:34:34

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RhodeIsland.RemoteTerminal.UI.ScrollView
{

    public class VariableAngleBase : Graphic
    {
        [SerializeField]
        protected float m_angel;

        public float Angle
        {
            set => m_angel = value;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            Vector2 rect = GetPixelAdjustedRect().size / 2f;
            float delta = Mathf.Tan(m_angel * Mathf.Deg2Rad) * rect.y * 2f;
            vh.Clear();
            vh.AddVert(-rect, color, Vector4.zero);
            vh.AddVert(new Vector2(rect.x - delta, -rect.y), color, Vector4.zero);
            vh.AddVert(rect, color, Vector4.zero);
            vh.AddVert(new Vector2(delta - rect.x, rect.y), color, Vector4.zero);
            vh.AddTriangle(0, 1, 2);
            vh.AddTriangle(2, 3, 0);
        }
    }
}
