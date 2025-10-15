// Created by ChaomengOrion
// Create at 2022-06-03 10:49:01
// Last modified on 2022-08-01 22:07:04

using System.Diagnostics;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Debug = UnityEngine.Debug;

public static class DLog
{
    [Conditional("ENABLE_LOG")]
    public static void Log(object message)
    {
        Debug.Log(message);
    }

    [Conditional("ENABLE_LOG")]
    public static void Log(object message, Object context)
    {
        Debug.Log(message, context);
    }

    [Conditional("ENABLE_LOG")]
    public static void LogWarning(object message)
    {
        Debug.LogWarning(message);
    }

    [Conditional("ENABLE_LOG")]
    public static void LogWarning(object message, Object context)
    {
        Debug.LogWarning(message, context);
    }

    [Conditional("ENABLE_LOG")]
    public static void LogError(object message)
    {
        Debug.LogError(message);
    }

    [Conditional("ENABLE_LOG")]
    public static void LogError(object message, Object context)
    {
        Debug.LogError(message, context);
    }

    public static async UniTask LogAsync(object message)
    {
        await UniTask.SwitchToMainThread();
        Debug.Log(message);
        await UniTask.SwitchToThreadPool();
    }

    public static async UniTask LogAsync(object message, Object context)
    {
        await UniTask.SwitchToMainThread();
        Debug.Log(message, context);
        await UniTask.SwitchToThreadPool();
    }

    public static async UniTask LogWarningAsync(object message)
    {
        await UniTask.SwitchToMainThread();
        Debug.LogWarning(message);
        await UniTask.SwitchToThreadPool();
    }

    public static async UniTask LogWarningAsync(object message, Object context)
    {
        await UniTask.SwitchToMainThread();
        Debug.LogWarning(message, context);
        await UniTask.SwitchToThreadPool();
    }

    public static async UniTask LogErrorAsync(object message)
    {
        await UniTask.SwitchToMainThread();
        Debug.LogError(message);
        await UniTask.SwitchToThreadPool();
    }

    public static async UniTask LogErrorAsync(object message, Object context)
    {
        await UniTask.SwitchToMainThread();
        Debug.LogError(message, context);
        await UniTask.SwitchToThreadPool();
    }
}