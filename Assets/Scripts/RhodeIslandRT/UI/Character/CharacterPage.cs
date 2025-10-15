// Created by ChaomengOrion
// Create at 2022-07-23 16:16:02
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using Torappu.UI;
using RhodeIsland.Arknights.Resource;
using RhodeIsland.RemoteTerminal.Resources;
using RhodeIsland.RemoteTerminal.Character;
using CharacterInfo = RhodeIsland.RemoteTerminal.Character.CharacterInfo;

namespace RhodeIsland.RemoteTerminal.UI.Character
{
    [Icon("Assets/StaticAssets/Editor/Free Flat Gear 2 Icon.png"), GUIColor(1f, 0.75f, 0.5f)]
    public class CharacterPage : SingletonSerializedMonoBehaviour<CharacterPage>, IPage
    {
        public CharacterInfoView InfoView => _characterInfoView;
        public CircleTrans Trans => _trans;

        [SerializeField]
        private Dictionary<RarityRank, Sprite> _raritySpriteMap;
        [SerializeField]
        private Transform _content;
        [SerializeField]
        private GameObject _groupPerfab;
        [SerializeField]
        private CircleTrans _trans;
        [SerializeField]
        private GameObject _menu;
        [SerializeField]
        private CharacterInfoView _characterInfoView;
        [SerializeField]
        private Button _btnHome;

        private CharacterSorter m_sorter = new();
        private Dictionary<string, CharacterData> m_characterDatas;


        private AutoPackSpriteHub m_charAvatarHub;

        [ShowInInspector]
        private GroupInfo[] m_groupInfo;

        protected override void OnInit()
        {
            base.OnInit();
            _btnHome.onClick.AddListener(PageManager.instance.OnHomeBtnClick);
            m_characterDatas = TableManager.instance.CharacterDatas;
            List<CharacterInfo> list = new();
            foreach (KeyValuePair<string, CharacterData> pair in m_characterDatas)
            {
                CharacterData data = pair.Value;
                if (data.profession == ProfessionCategory.TOKEN || data.profession == ProfessionCategory.TRAP || data.profession == ProfessionCategory.NONE)
                    continue;
                list.Add(new()
                {
                    id = pair.Key,
                    nameCN = data.name,
                    nameFL = data.appellation,
                    rarity = data.rarity,
                    profession = data.profession,
                    nationId = data.nationId,
                    groupId = data.groupId,
                    teamId = data.teamId,
                });
            }
            m_sorter.SetCharacters(list.ToArray());
            m_charAvatarHub = ResourceManager.Load<AutoPackSpriteHub>("Arts/Charavatars/Avatar_Hub");
        }

        public void OnClose()
        {
            gameObject.SetActive(false);
        }

        public void OnEnter()
        {
            PageManager.instance.ClearCurrents();
            PageManager.instance.AddCurrent(() => { _menu.SetActive(true); _btnHome.gameObject.SetActive(true); }, null);
            gameObject.SetActive(true);
        }

        public bool TryLoadCharacterAvatar(string id, out Sprite sprite)
        {
            return m_charAvatarHub.TryLoadSprite(id, out sprite);
        }

        public Sprite LoadRaritySprite(RarityRank rarity)
        {
            return _raritySpriteMap[rarity];
        }

        [Button("TEST")]
        public void Test()
        {
            var obj = ResourceManager.LoadRaw<Material>("building/vault/characters/build_char_290_vigna", "SkeletonAnimation");
            //Instantiate(obj);
            DLog.Log(obj);
        }

        public void SetSortMethods(SortMethod method)
        {
            m_groupInfo = m_sorter.SortCharacters(method);
            _content.ClearAllChildren();
            Action onUpdate = null;
            foreach (GroupInfo info in m_groupInfo)
            {
                GameObject obj = Instantiate(_groupPerfab, _content);
                CharacterGroup group = obj.GetComponent<CharacterGroup>();
                group.SetUp(info, ref onUpdate);
            }
            Canvas.ForceUpdateCanvases();
            onUpdate?.Invoke();
        }

        public void SwitchToCharacter(string charId, Image source)
        {
            _trans.gameObject.SetActive(true);
            _characterInfoView.SetCharData(charId);
            _trans.DoTrans(source, _characterInfoView.GetIllustMainColor(), 1.5f, () => _menu.SetActive(false));
            _characterInfoView.DoTrans();
            _btnHome.gameObject.SetActive(false);
            PageManager.instance.AddCurrent(null, () => _trans.TransOff(1f));
        }
    }
}
