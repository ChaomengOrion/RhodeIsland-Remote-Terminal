// Created by ChaomengOrion
// Create at 2022-07-23 18:39:37
// Last modified on 2022-07-26 18:51:55

using System;
using System.Collections.Generic;
using UnityEngine;
using NPinyin;

namespace RhodeIsland.RemoteTerminal.Character
{
    public class CharacterSorter
    {
        private CharacterInfo[] m_characters;
        private GroupInfo[] m_cachedGroupInfos;
        private SortMethod m_sortMethod = SortMethod.None;

        private readonly char[][] splits =
            {
                new char[] { 'a', 'b', 'c', 'd' },
                new char[] { 'e', 'f', 'g', 'h' },
                new char[] { 'i', 'j', 'k', 'l' },
                new char[] { 'm', 'n', 'o', 'p'},
                new char[] { 'q', 'r', 's', 't'},
                new char[] { 'u', 'v', 'w'},
                new char[] { 'x', 'y', 'z'},
            };

        private readonly char[][] splitsCN =
            {
                new char[] { 'a', 'b', 'c'},
                new char[] { 'd', 'e', 'f'},
                new char[] { 'g', 'h', 'i', 'j' },
                new char[] { 'k', 'l', 'm'},
                new char[] { 'n', 'o', 'p', 'q'},
                new char[] { 'r', 's', 't' },
                new char[] { 'u', 'v', 'w', 'x' },
                new char[] { 'y', 'z'},
            };

        public void SetCharacters(CharacterInfo[] characters)
        {
            m_characters = characters;
        }

        public GroupInfo[] SortCharacters(SortMethod method)
        {
            if (m_sortMethod == method) 
                return m_cachedGroupInfos;
            GroupInfo[] infos = method switch
            {
                SortMethod.NameCN => SortByNameCN(m_characters),
                SortMethod.NameFL => SortByNameFL(m_characters),
                SortMethod.Rarity => SortByRarity(m_characters),
                SortMethod.Profession => SortByProfession(m_characters),
                SortMethod.Nation => SortByNationId(m_characters),
                SortMethod.Group => SortByGroupId(m_characters),
                SortMethod.Team => SortByTeamId(m_characters),
                _ => new GroupInfo[0],
            };
            m_sortMethod = method;
            m_cachedGroupInfos = infos;
            return infos;
        }


        /// <summary>
        /// 按照中文名称排序
        /// </summary>
        public GroupInfo[] SortByNameCN(CharacterInfo[] infos)
        {
            GroupInfo[] groupInfos = new GroupInfo[splitsCN.Length];
            for (int i = 0; i < groupInfos.Length; i++)
            {
                groupInfos[i].key = (splitsCN[i][0] + "-" + splitsCN[i][^1]).ToUpper();
                groupInfos[i].characters = new();
            }
            foreach (CharacterInfo character in infos)
            {
                int split = _SplitsWithPinyin(character.nameCN, splitsCN);
                if (split >= 0)
                {
                    groupInfos[split].characters.Add(character);
                }
            }

            foreach (GroupInfo info in groupInfos)
            {
                info.characters.Sort((x, y)
                    => Pinyin.GetInitials(x.nameCN).CompareTo(Pinyin.GetInitials(y.nameCN)));
            }

            return groupInfos;
        }

        /// <summary>
        /// 按照外文名称排序
        /// </summary>
        public GroupInfo[] SortByNameFL(CharacterInfo[] infos)
        {
            GroupInfo[] groupInfos = new GroupInfo[splits.Length + 1];
            for (int i = 0; i < groupInfos.Length - 1; i++)
            {
                groupInfos[i].key = (splits[i][0] + "-" + splits[i][^1]).ToUpper();
                groupInfos[i].characters = new();
            }
            groupInfos[^1].key = "#";
            groupInfos[^1].characters = new();
            foreach (CharacterInfo character in infos)
            {
                int split = _Splits(character.nameFL, splits);
                if (split >= 0)
                {
                    groupInfos[split].characters.Add(character);
                }
                else
                {
                    groupInfos[^1].characters.Add(character);
                }
            }

            foreach (GroupInfo info in groupInfos)
            {
                info.characters.Sort((x, y)
                    => x.nameFL.CompareTo(y.nameFL));
            }

            return groupInfos;
        }

        /// <summary>
        /// 按照星级排序
        /// </summary>
        public GroupInfo[] SortByRarity(CharacterInfo[] infos)
        {
            GroupInfo[] groupInfos = new GroupInfo[6];
            for (int i = 0; i < groupInfos.Length; i++)
            {
                groupInfos[i].characters = new();
            }
            groupInfos[0].key = "1星干员";
            groupInfos[1].key = "2星干员";
            groupInfos[2].key = "3星干员";
            groupInfos[3].key = "4星干员";
            groupInfos[4].key = "5星干员";
            groupInfos[5].key = "6星干员";

            foreach (CharacterInfo character in infos)
            {
                switch (character.rarity)
                {
                    case RarityRank.TIER_1:
                        groupInfos[0].characters.Add(character);
                        break;
                    case RarityRank.TIER_2:
                        groupInfos[1].characters.Add(character);
                        break;
                    case RarityRank.TIER_3:
                        groupInfos[2].characters.Add(character);
                        break;
                    case RarityRank.TIER_4:
                        groupInfos[3].characters.Add(character);
                        break;
                    case RarityRank.TIER_5:
                        groupInfos[4].characters.Add(character);
                        break;
                    case RarityRank.TIER_6:
                        groupInfos[5].characters.Add(character);
                        break;
                }
            }

            foreach (GroupInfo info in groupInfos)
            {
                info.characters.Sort((x, y)
                    => Pinyin.GetInitials(x.nameCN).CompareTo(Pinyin.GetInitials(y.nameCN)));
            }

            return groupInfos;
        }

        /// <summary>
        /// 按照职业排序
        /// </summary>
        public GroupInfo[] SortByProfession(CharacterInfo[] infos)
        {
            GroupInfo[] groupInfos = new GroupInfo[8];
            for (int i = 0; i < groupInfos.Length; i++)
            {
                groupInfos[i].characters = new();
            }
            groupInfos[0].key = ProfessionCategory.WARRIOR;
            groupInfos[1].key = ProfessionCategory.SNIPER;
            groupInfos[2].key = ProfessionCategory.TANK;
            groupInfos[3].key = ProfessionCategory.MEDIC;
            groupInfos[4].key = ProfessionCategory.SUPPORT;
            groupInfos[5].key = ProfessionCategory.CASTER;
            groupInfos[6].key = ProfessionCategory.SPECIAL;
            groupInfos[7].key = ProfessionCategory.PIONEER;

            foreach (CharacterInfo character in infos)
            {
                switch (character.profession)
                {
                    case ProfessionCategory.WARRIOR:
                        groupInfos[0].characters.Add(character);
                        break;
                    case ProfessionCategory.SNIPER:
                        groupInfos[1].characters.Add(character);
                        break;
                    case ProfessionCategory.TANK:
                        groupInfos[2].characters.Add(character);
                        break;
                    case ProfessionCategory.MEDIC:
                        groupInfos[3].characters.Add(character);
                        break;
                    case ProfessionCategory.SUPPORT:
                        groupInfos[4].characters.Add(character);
                        break;
                    case ProfessionCategory.CASTER:
                        groupInfos[5].characters.Add(character);
                        break;
                    case ProfessionCategory.SPECIAL:
                        groupInfos[6].characters.Add(character);
                        break;
                    case ProfessionCategory.PIONEER:
                        groupInfos[7].characters.Add(character);
                        break;
                }
            }

            foreach (GroupInfo info in groupInfos)
            {
                info.characters.Sort((x, y)
                    => Pinyin.GetInitials(x.nameCN).CompareTo(Pinyin.GetInitials(y.nameCN)));
            }

            return groupInfos;
        }

        /// <summary>
        /// 按照国籍排序
        /// </summary>
        public GroupInfo[] SortByNationId(CharacterInfo[] infos)
        {
            Dictionary<string, GroupInfo> groupInfoDict = new();

            foreach (CharacterInfo character in infos)
            {
                if (!string.IsNullOrEmpty(character.nationId))
                {
                    if (!groupInfoDict.ContainsKey(character.nationId))
                    {
                        GroupInfo info = new();
                        info.characters = new();
                        groupInfoDict[character.nationId] = info;
                    }
                    groupInfoDict[character.nationId].characters.Add(character);
                }
            }

            GroupInfo[] groupInfos = new GroupInfo[groupInfoDict.Count];
            groupInfoDict.Values.CopyTo(groupInfos, 0);
            string[] keys = new string[groupInfoDict.Count];
            groupInfoDict.Keys.CopyTo(keys, 0);
            for (int i = 0; i < groupInfos.Length; i++)
            {
                groupInfos[i].key = keys[i];
            }

            foreach (GroupInfo info in groupInfos)
            {
                info.characters.Sort((x, y)
                    => Pinyin.GetInitials(x.nameCN).CompareTo(Pinyin.GetInitials(y.nameCN)));
            }

            return groupInfos;
        }

        /// <summary>
        /// 按照集团排序
        /// </summary>
        public GroupInfo[] SortByGroupId(CharacterInfo[] infos)
        {
            Dictionary<string, GroupInfo> groupInfoDict = new();

            foreach (CharacterInfo character in infos)
            {
                if (!string.IsNullOrEmpty(character.groupId))
                {
                    if (!groupInfoDict.ContainsKey(character.groupId))
                    {
                        GroupInfo info = new();
                        info.characters = new();
                        groupInfoDict[character.groupId] = info;
                    }
                    groupInfoDict[character.groupId].characters.Add(character);
                }
            }

            GroupInfo[] groupInfos = new GroupInfo[groupInfoDict.Count];
            groupInfoDict.Values.CopyTo(groupInfos, 0);
            string[] keys = new string[groupInfoDict.Count];
            groupInfoDict.Keys.CopyTo(keys, 0);
            for (int i = 0; i < groupInfos.Length; i++)
            {
                groupInfos[i].key = keys[i];
            }

            foreach (GroupInfo info in groupInfos)
            {
                info.characters.Sort((x, y)
                    => Pinyin.GetInitials(x.nameCN).CompareTo(Pinyin.GetInitials(y.nameCN)));
            }

            return groupInfos;
        }

        /// <summary>
        /// 按照阵营排序
        /// </summary>
        public GroupInfo[] SortByTeamId(CharacterInfo[] infos)
        {
            Dictionary<string, GroupInfo> groupInfoDict = new();

            foreach (CharacterInfo character in infos)
            {
                if (!string.IsNullOrEmpty(character.teamId))
                {
                    if (!groupInfoDict.ContainsKey(character.teamId))
                    {
                        GroupInfo info = new();
                        info.characters = new();
                        groupInfoDict[character.teamId] = info;
                    }
                    groupInfoDict[character.teamId].characters.Add(character);
                }
            }

            GroupInfo[] groupInfos = new GroupInfo[groupInfoDict.Count];
            groupInfoDict.Values.CopyTo(groupInfos, 0);
            string[] keys = new string[groupInfoDict.Count];
            groupInfoDict.Keys.CopyTo(keys, 0);
            for (int i = 0; i < groupInfos.Length; i++)
            {
                groupInfos[i].key = keys[i];
            }

            foreach (GroupInfo info in groupInfos)
            {
                info.characters.Sort((x, y)
                    => Pinyin.GetInitials(x.nameCN).CompareTo(Pinyin.GetInitials(y.nameCN)));
            }

            return groupInfos;
        }

        private int _Splits(string s, char[][] splits)
        {
            for (int i = 0; i < splits.Length; i++)
            {
                for (int j = 0; j < splits[i].Length; j++)
                {
                    if (s.ToLower().StartsWith(splits[i][j]))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private int _SplitsWithPinyin(string s, char[][] splits)
        {
            for (int i = 0; i < splits.Length; i++)
            {
                for (int j = 0; j < splits[i].Length; j++)
                { 
                    if (Pinyin.GetInitials(s).ToLower().StartsWith(splits[i][j]))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    }
}