// Created by ChaomengOrion
// Create at 2022-05-01 10:56:39
// Last modified on 2022-11-13 17:24:22

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using RhodeIsland.Arknights.Resource;
using RhodeIsland.RemoteTerminal.Resources;
using RhodeIsland.RemoteTerminal.AVG;

namespace RhodeIsland.RemoteTerminal
{
    [Icon("Assets/StaticAssets/Editor/Free Flat Gear 2 Icon.png"), GUIColor(1f, 0.75f, 0.5f)]

    public class TableManager : PersistentSingleton<TableManager>
    {
        public Dictionary<string, HandbookInfoData> HandbookDatas => m_handbookDatas;

        public Dictionary<string, StoryReviewGroupClientData> StoryReviewGroupDatas => m_storyReviewGroupDatas;

        public ZoneTable ZoneTable => m_zoneTable;

        public SkinTable SkinTable => m_skinTable;

        public AudioData AudioTable => m_audioTable;

        public Dictionary<string, CharacterData> CharacterDatas => m_characterDatas;

        public RTConfig Config => m_RTConfig;

        [ShowInInspector, GUIColor(1f, 1f, 1f)]
        private Dictionary<string, CharacterData> m_characterDatas;
        [ShowInInspector, GUIColor(1f, 1f, 1f)]
        private Dictionary<string, HandbookInfoData> m_handbookDatas = new();
        [ShowInInspector, GUIColor(1f, 1f, 1f)]
        private Dictionary<string, StoryReviewGroupClientData> m_storyReviewGroupDatas;
        [ShowInInspector, GUIColor(1f, 1f, 1f)]
        private ZoneTable m_zoneTable;
        [ShowInInspector, GUIColor(1f, 1f, 1f)]
        private SkinTable m_skinTable;
        [ShowInInspector, GUIColor(1f, 1f, 1f)]
        private AudioData m_audioTable;
        [ShowInInspector, GUIColor(1f, 1f, 1f)]
        private RTConfig m_RTConfig;

        public IEnumerator Init()
        {
            base.OnInit();
            try
            {
                m_RTConfig = JsonConvert.DeserializeObject<RTConfig>(UnityEngine.Resources.Load<TextAsset>("RTConfig").text);
            }
            catch
            {
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    m_RTConfig = JsonConvert.DeserializeObject<RTConfig>(File.ReadAllText("D:/RTConfig.json"));
                }
                else
                {
                    m_RTConfig = JsonConvert.DeserializeObject<RTConfig>(File.ReadAllText(Application.persistentDataPath + "/RTConfig.json"));
                }
            }

            AsyncResource resource = ResourceManager.LoadAsync<TextAsset>("Gamedata/Excel/Character_Table");
            yield return resource;
            m_characterDatas = JsonConvert.DeserializeObject<Dictionary<string, CharacterData>>(
                Crypto.TableTextAssetDecrypt(resource.GetAsset<TextAsset>().bytes));
            
            resource = ResourceManager.LoadAsync<TextAsset>("Gamedata/Excel/Skin_Table");
            yield return resource;
            m_skinTable = JsonConvert.DeserializeObject<SkinTable>(Crypto.TableTextAssetDecrypt(resource.GetAsset<TextAsset>().bytes));

            resource = ResourceManager.LoadAsync<TextAsset>("Gamedata/Excel/Story_Review_Table");
            yield return resource;
            m_storyReviewGroupDatas = JsonConvert.DeserializeObject<Dictionary<string, StoryReviewGroupClientData>>(
                Crypto.TableTextAssetDecrypt(resource.GetAsset<TextAsset>().bytes));

            resource = ResourceManager.LoadAsync<TextAsset>("Gamedata/Excel/Handbook_Info_Table");
            yield return resource;
            JObject handbookInfo = JsonConvert.DeserializeObject<JObject>(
                Crypto.TableTextAssetDecrypt(resource.GetAsset<TextAsset>().bytes));
            m_handbookDatas = handbookInfo["handbookDict"].ToObject<Dictionary<string, HandbookInfoData>>();

            resource = ResourceManager.LoadAsync<TextAsset>("Gamedata/Excel/Audio_Data");
            yield return resource;
            m_audioTable = JsonConvert.DeserializeObject<AudioData>(Crypto.TableTextAssetDecrypt(resource.GetAsset<TextAsset>().bytes));

            resource = ResourceManager.LoadAsync<TextAsset>("Gamedata/Excel/Zone_Table");
            yield return resource;
            m_zoneTable = JsonConvert.DeserializeObject<ZoneTable>(Crypto.TableTextAssetDecrypt(resource.GetAsset<TextAsset>().bytes));
        }
    }
}