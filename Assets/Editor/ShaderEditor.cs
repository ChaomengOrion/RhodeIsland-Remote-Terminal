using System;
using UnityEditor;
using UnityEngine;

public class ShaderEditor : MonoBehaviour
{

    [UnityEditor.Callbacks.OnOpenAsset(0)]
    public static bool OnOpen(int instanceID, int line)
    {
        string strFilePath = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(instanceID));
        string strFileName = System.IO.Directory.GetParent(Application.dataPath) + "/" + strFilePath;

        if (strFileName.EndsWith(".shader"))
        {
            string editorPath = "D:/Microsoft VS Code"; //Environment.GetEnvironmentVariable("VSCode_Path");
            if (editorPath != null && editorPath.Length > 0)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = editorPath + (editorPath.EndsWith("/") ? "" : "/") + "Code.exe";
                startInfo.Arguments = "\"" + strFileName + "\"";
                process.StartInfo = startInfo;
                process.Start();
                return true;
            }
            else
            {
                Debug.Log("null environment �� VSCode_Path");
                return false;
            }
        }
        return false;
    }
}