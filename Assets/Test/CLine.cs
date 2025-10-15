using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]//��������Ҫ LineRenderer���
public class CLine : MonoBehaviour
{
    public int segments;//���õ�����������Խ�࣬��������Բ��Բ��
    public float xradius;//X�� �뾶
    public float yradius;
    public float zradius;
    public LineRenderer line;

    [Sirenix.OdinInspector.Button("Create")]
    void CreatePoints()//����Բ
    {
        line.positionCount = (segments + 1);//���� LineRenderer ����Ļ�Բ����������
        line.useWorldSpace = false;//��ʹ����������
        float x;
        float y = 0;
        float z;
        float angle = 0;
        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * zradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
            line.SetPosition(i, new Vector3(x, y, z));//����ÿ���������

            angle += (360f / segments);
        }//end for
    }//end create points
}//end class