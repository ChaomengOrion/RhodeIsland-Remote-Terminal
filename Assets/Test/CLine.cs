using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]//该物体需要 LineRenderer组件
public class CLine : MonoBehaviour
{
    public int segments;//所用的线条（线条越多，画出来的圆更圆）
    public float xradius;//X轴 半径
    public float yradius;
    public float zradius;
    public LineRenderer line;

    [Sirenix.OdinInspector.Button("Create")]
    void CreatePoints()//创建圆
    {
        line.positionCount = (segments + 1);//设置 LineRenderer 组件的花圆线条的数量
        line.useWorldSpace = false;//不使用世界坐标
        float x;
        float y = 0;
        float z;
        float angle = 0;
        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * zradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
            line.SetPosition(i, new Vector3(x, y, z));//设置每个点的坐标

            angle += (360f / segments);
        }//end for
    }//end create points
}//end class