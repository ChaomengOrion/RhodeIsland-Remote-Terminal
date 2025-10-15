// Created by ChaomengOrion
// Create at 2022-07-05 18:02:00
// Last modified on 2022-08-01 19:07:50


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RhodeIsland.Arknights.UI
{

    [ExecuteInEditMode]
    public class UIPerspectiveFx : BaseMeshEffect
    {
        [Header("Only Image")]
        public int subdivision = 2;//有的材质细分2次，有的需3-4次。
        public float perspectiveScale = 1.0f;
        public bool alwaysRefresh = true;


        private void Update()
        {
            if (alwaysRefresh)
                graphic.SetVerticesDirty();
        }

        private void CalcPerspectiveScale(ref Vector3 point)
        {
            Vector3 wPos = transform.localToWorldMatrix.MultiplyPoint(point);
            float fixValue = wPos.z * perspectiveScale;
            point *= 1f + fixValue;
        }

        public override void ModifyMesh(VertexHelper vh)
        {
            RectTransform rectTransform = transform as RectTransform;

            if (graphic is Image)
            {
                vh.Clear();

                Vector2 begin = -rectTransform.sizeDelta * 0.5f;
                Vector2 cell = rectTransform.sizeDelta / subdivision;
                float uvCell = 1f / subdivision;
                for (int x = 0; x < subdivision; x++)//TODO:可进一步做缓存优化
                {
                    for (int y = 0; y < subdivision; y++)
                    {
                        Vector3 p0 = new Vector3(begin.x + cell.x * x, begin.y + cell.y * y);
                        Vector3 p1 = new Vector3(begin.x + cell.x * x, begin.y + cell.y * (y + 1));
                        Vector3 p2 = new Vector3(begin.x + cell.x * (x + 1), begin.y + cell.y * (y + 1));
                        Vector3 p3 = new Vector3(begin.x + cell.x * (x + 1), begin.y + cell.y * y);

                        Vector3 uv0 = new Vector3(x * uvCell, y * uvCell);
                        Vector3 uv1 = new Vector3(x * uvCell, (y + 1) * uvCell);
                        Vector3 uv2 = new Vector3((x + 1) * uvCell, (y + 1) * uvCell);
                        Vector3 uv3 = new Vector3((x + 1) * uvCell, y * uvCell);

                        CalcPerspectiveScale(ref p0);
                        CalcPerspectiveScale(ref p1);
                        CalcPerspectiveScale(ref p2);
                        CalcPerspectiveScale(ref p3);

                        vh.AddUIVertexQuad(new UIVertex[]
                        {
                        new UIVertex(){position=p0, color=graphic.color, uv0=uv0},
                        new UIVertex(){position=p1, color=graphic.color, uv0=uv1},
                        new UIVertex(){position=p2, color=graphic.color, uv0=uv2},
                        new UIVertex(){position=p3, color=graphic.color, uv0=uv3}
                        });
                    }
                }
            }
            else if (graphic is Text)
            {
                for (int i = 0, iMax = vh.currentVertCount; i < iMax; i++)
                {
                    UIVertex vertex = default;
                    vh.PopulateUIVertex(ref vertex, i);
                    CalcPerspectiveScale(ref vertex.position);
                    vh.SetUIVertex(vertex, i);
                }
            }
        }
    }
}