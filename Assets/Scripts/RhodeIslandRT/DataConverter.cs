// Created by ChaomengOrion
// Create at 2022-07-25 18:46:45
// Last modified on 2022-08-01 19:07:51

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RhodeIsland.RemoteTerminal.Resources;
using RhodeIsland.Arknights.Resource;
using Torappu.UI;

namespace RhodeIsland.RemoteTerminal
{
    public static class DataConverter 
    {
        public static string GetCharacterName(string charId)
        {
            if (TableManager.instance.CharacterDatas.TryGetValue(charId, out CharacterData characterData))
            {
                return characterData.name;
            }
            return null;
        }

        public static CharacterData GetCharacterData(string charId)
        {
            if (TableManager.instance.CharacterDatas.TryGetValue(charId, out CharacterData characterData))
            {
                return characterData;
            }
            return null;
        }

        public static HashSet<CharSkinData> GetCharAllSkinDatas(string charId)
        {
            HashSet<CharSkinData> res = new();
            foreach (CharSkinData data in TableManager.instance.SkinTable.charSkins.Values)
            {
                if (data.charId == charId)
                {
                    res.Add(data);
                }
            }
            return res;
        }

        public static CharSkinData GetCharSkinData(string skinId)
        {
            return TableManager.instance.SkinTable.charSkins[skinId];
        }

        public static string GetCharacterVaultPath(string charId)
        {
            return string.Format("building/vault/characters/build_{0}", charId);
        }

        public static bool TryGetCharLastEvolveSkinId(string charId, out string skinId)
        {
            if (TableManager.instance.SkinTable.buildinEvolveMap.TryGetValue(charId, out var list) && list.Count > 0)
            {
                skinId = list[^1].Value;
                return true;
            }
            else
            {
                skinId = null;
                return false;
            }
        }

        public static Sprite LoadLogo(string powerId, bool isOverride = false)
        {
            Sprite sprite = null;
            if (powerId != null && powerId != "none")
            {
                sprite = LoadSpriteFromAutoPackHub(GetPowerLogoId(powerId, isOverride), ResourceUrls.GetLogoHubPath());
                if (!sprite)
                {
                    DLog.LogError(string.Format("Logo not found, power_id [{0}]", powerId));
                }
            }
            return sprite;
        }

        public static string GetPowerLogoId(string powerId, bool isOverride = false)
        {
            if (isOverride && powerId == "rhodes")
            {
                return string.Format("logo_{0}_override", powerId);
            }
            else
            {
                return string.Format("logo_{0}", powerId);
            }
        }

        public static Sprite LoadSpriteFromAutoPackHub(string spriteId, string hubPath)
        {
            AutoPackSpriteHub spriteHub = ResourceManager.Load<AutoPackSpriteHub>(hubPath);
            if (spriteHub && spriteHub.TryGetValue(spriteId, out string path))
            {
                return ResourceManager.Load<Sprite>(path);
            }
            return null;
        }
    }
}