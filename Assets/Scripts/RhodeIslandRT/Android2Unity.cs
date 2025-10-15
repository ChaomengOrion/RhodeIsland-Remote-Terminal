// Created by ChaomengOrion
// Create at 2022-05-08 12:20:07
// Last modified on 2022-05-08 13:24:16

using UnityEngine;

namespace RhodeIsland.RemoteTerminal
{
    public class Android2Unity
    {
        public static AndroidJavaObject m_instance;

        public static AndroidJavaObject Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new AndroidJavaObject("com.rhodeisland.remoteterminal.android2unity.Android2Unity");
                }
                return m_instance;
            }
        }

        public static string GetPackageStreamDataPath(string name)
        {
            return Instance.Call<string>("getPackageStreamDataPath", name);
        }
    }
}