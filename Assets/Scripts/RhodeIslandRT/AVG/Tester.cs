// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using RhodeIsland.Arknights.AVG;
using RhodeIsland.Arknights.Resource;
using System.Reflection;

namespace RhodeIsland.RemoteTerminal.AVG.Test
{
    public class Tester : MonoBehaviour
    {
        public string id = "obt/main/level_main_02-09_end";
        public string audio, intro, loop;
        public UnityEngine.UI.Image a;
        [Button("Init")]
        public void Init()
        {
            UI.FrontPanelManager.instance.PopUpBinaryPanel("是否确定要跳过剧情？", res =>
            {
                DLog.Log(res);
            });
        }
        [Button("Test")]
        public void Test()
        {
            RhodeIsland.Arknights.AVG.AVG.instance.StartStory(id);
        }
        [Button("Values")]
        public void V()
        {
            foreach (var v in GetFields(a))
            {
                Debug.Log(v);
            }
        }
        [Button("FX")]
        public void FX()
        {
            Arknights.AudioManager.PlaySoundFx(audio);
        }

        [Button("Music")]
        public void Music()
        {
            Arknights.AudioManager.PlayMusicWithIntro(intro, loop);
        }
        public List<string> GetFields<T>(T t)
        {
            List<string> ListStr = new List<string>();
            if (t == null)
            {
                return ListStr;
            }
            System.Reflection.FieldInfo[] fields = t.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (fields.Length <= 0)
            {
                return ListStr;
            }
            foreach (System.Reflection.FieldInfo item in fields)
            {
                string name = item.Name; //名称
                object value = item.GetValue(t);  //值
                ListStr.Add(name + ' ' + value?.ToString());
            }
            return ListStr;
        }
    }
}
