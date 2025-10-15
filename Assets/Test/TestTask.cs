using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Cysharp.Threading.Tasks;

public class TestTask : MonoBehaviour
{
    private UniTask m_task;

    private void Start()
    {
        Texture2D a;
        m_task = UniTask.RunOnThreadPool(Task);
        UniTask.Void(A);
        UniTask.Void(B);
    }


    private void Task()
    {
        Thread.Sleep(1000);
    }

    private async UniTaskVoid A()
    {
        await m_task;
        Debug.Log(Environment.TickCount);
    }

    private async UniTaskVoid B()
    {
        await m_task;
        Debug.Log(Environment.TickCount);
    }
}
