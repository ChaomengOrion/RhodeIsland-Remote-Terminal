// Created by ChaomengOrion
// Create at 2022-07-29 22:57:05
// Last modified on 2022-07-29 22:59:19

using System.Diagnostics;
using UnityEngine;

public static class StackTracer
{
    public static void Trace()
    {
        DLog.Log(StackTraceUtility.ExtractStackTrace());
    }
}
