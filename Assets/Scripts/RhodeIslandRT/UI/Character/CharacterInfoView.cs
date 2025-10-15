// Created by ChaomengOrion
// Create at 2022-07-26 00:22:52
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using RhodeIsland.RemoteTerminal.Resources;
using RhodeIsland.RemoteTerminal.UI.ScrollView;
using RhodeIsland.Arknights.Resource;
using Torappu.Building.Vault;

namespace RhodeIsland.RemoteTerminal.UI.Character
{
    public class CharacterInfoView : MonoBehaviour
    {
        [SerializeField]
        private SkinChoseItem _skinChoseItemPerfab;
        [SerializeField]
        private ShrinkableScrollView _scrollView;
        [SerializeField]
        private ChangeableImage _illust, _illustBG;
        [SerializeField]
        private Material _splitMat;
        [SerializeField]
        private Text _nameCN, _nameFL, _code;
        [SerializeField]
        private Transform _infoContent;
        [SerializeField]
        private GameObject _infoGroupPerfab;
        [SerializeField]
        private SpineCharacter _spineCharacter;
        [SerializeField]
        private AnimationData[] animationDatas;

        private List<GameObject> m_infos = new();
        private string m_charId;
        private string m_skinId;

        public void DoTrans()
        {
            foreach (AnimationData data in animationDatas)
            {
                data.rectTransform.DOKill();
                data.rectTransform.anchoredPosition = data.fromPos;
                data.rectTransform.DOAnchorPos(data.toPos, data.t).SetEase(data.ease).Play();
            }
        }

        public void SetCharData(string charId)
        {
            m_charId = charId;
            _code.text = charId.ToUpper();
            CharacterData charData = DataConverter.GetCharacterData(charId);
            _nameCN.text = charData.name;
            _nameFL.text = charData.appellation;
            HandbookInfoData data = TableManager.instance.HandbookDatas[charId];
            foreach (GameObject obj in m_infos)
            {
                Destroy(obj);
                obj.transform.SetParent(null);
            }
            m_infos.Clear();
            foreach (HandBookStoryViewData item in data.storyTextAudio)
            {
                GameObject obj = Instantiate(_infoGroupPerfab, _infoContent);
                m_infos.Add(obj);
                obj.GetComponent<InfoTextGroup>().Render(item);
            }
            HashSet<CharSkinData> skinDatas = DataConverter.GetCharAllSkinDatas(charId);
            if (!DataConverter.TryGetCharLastEvolveSkinId(charId, out string m_skinId))
            {
                CharSkinData[] datas = new CharSkinData[skinDatas.Count];
                skinDatas.CopyTo(datas, 0);
                m_skinId = datas[0].skinId;
            }
            _spineCharacter.ResetPos();
            SetSkin(m_skinId, false);
            _scrollView.Clear();
            SkinChoseItem upperItem = null;
            foreach (CharSkinData skin in skinDatas)
            {
                SkinChoseItem item = (SkinChoseItem)_scrollView.Add(_skinChoseItemPerfab);
                item.Init(skin.skinId, _GetSkinName(skin.displaySkin));
                if (skin.skinId == m_skinId)
                    upperItem = item;
            }
            upperItem.SetUpper();
            _scrollView.SetUpState(ShrinkableScrollView.State.Shrink);
        }

        public Color GetIllustMainColor()
        {
            return _illust.sprite.texture.CalculateMainColor(_splitMat);
        }

        public void SetSkin(string skinId, bool withColor = true)
        {
            if (skinId == m_skinId) return;
            m_skinId = skinId;
            CharSkinData skinData = DataConverter.GetCharSkinData(skinId);
            string path = ResourceUrls.IllustLoaderOnlyChrIllustResPath(m_charId, skinData.GetIllustId());
            CharacterIllustHolder holder = new(ResourceManager.Load<GameObject>(path));
            holder.ApplyTo(_illust);
            holder.ApplyTo(_illust.ModifiedMaterial);
            holder.ApplyTo(_illustBG, false);
            holder.ApplyTo(_illust.ModifiedMaterial);
            holder.ApplyTo(_splitMat);
            if (withColor) CharacterPage.instance.Trans.DoColor(GetIllustMainColor(), 0.4f);
            _spineCharacter.SetUp(new(ResourceManager.Load<VCharacter>(DataConverter.GetCharacterVaultPath(skinData.GetBuildingId()))));
        }

        private static string _GetSkinName(CharSkinData.DisplaySkin skin)
        {
            string group = skin.skinGroupId;
            string prefix = group switch
            {
                "ILLUST_0" => "无精英·",
                "ILLUST_1" => "精英一·",
                "ILLUST_2" => "精英二·",
                _ => string.Empty
            };
            return prefix + skin.skinGroupName;
        }

        [Serializable]
        private struct AnimationData
        {
            public RectTransform rectTransform;
            public Vector2 fromPos;
            public Vector2 toPos;
            public float t;
            public Ease ease;
        }
    }
}