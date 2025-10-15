// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System.Collections.Generic;
using RhodeIsland.Arknights.Resource;

namespace RhodeIsland.Arknights.AVG
{
    public class AVGVariableConfig : IAVGVariableConverter
    {
        public void TryLoadConfig(DirectAssetLoader loader)
        {
            UnityEngine.TextAsset raw = loader.Load<UnityEngine.TextAsset>(ResourceRouter.GetVariableFilePath());
            if (raw)
            {
                m_variableMap = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(raw.text);
            }
        }

        public bool available
        {
            get
            {
                return m_variableMap != null;
            }
        }

        public object Convert(object src)
        {
            if (m_variableMap != null)
            {
                if (src == null)
                    return null;
                if (src is string str && str.StartsWith(VARIABLE_PROMPT))
                {
                    string fixedStr = str[VARIABLE_PROMPT.Length..];
                    if (m_variableMap.TryGetValue(fixedStr, out object value))
                    {
                        return value;
                    }
                }
            }
            return src;
        }

        public const string VARIABLE_PROMPT = "$";
        private Dictionary<string, object> m_variableMap;
    }
}
