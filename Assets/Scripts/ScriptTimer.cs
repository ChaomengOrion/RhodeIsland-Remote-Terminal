// Created by ChaomengOrion
// Create at 2022-07-24 20:17:56
// Last modified on 2022-07-24 20:19:53

using System.Diagnostics;

public class ScriptTimer
{

    Stopwatch swatch = new Stopwatch();

    public void Start()
    {
        Stopwatch swatch = new Stopwatch();
        swatch.Start();
    }

    public void StopAndLog()
    {
        swatch.Stop();
        string time = swatch.ElapsedMilliseconds.ToString();
        DLog.Log(time + "ms");
        swatch.Reset();
    }
}
