// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-04-23 09:07:24

using System;

public class TimeUtil
{
    /// <summary>  
    /// 日期转换成获取时间戳
    /// </summary>  
    /// <param name="dt"></param>  
    /// <returns></returns>  
    public static int GetTimeStamp(DateTime dt)
    {
        DateTime dateStart = new DateTime(1970, 1, 1).ToLocalTime();
        int timeStamp = Convert.ToInt32((dt - dateStart).TotalSeconds);
        return timeStamp;
    }

    /// <summary>  
    /// 时间戳转换成日期  
    /// </summary>  
    /// <param name="timeStamp">时间戳</param>  
    /// <returns>日期  </returns>  
    public static DateTime GetDateTime(long timeStamp)
    {
        return new DateTime(1970, 1, 1).ToLocalTime().AddSeconds(timeStamp);
    }  
}