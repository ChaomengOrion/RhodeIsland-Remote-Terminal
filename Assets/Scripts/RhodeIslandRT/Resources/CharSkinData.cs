// Created by ChaomengOrion
// Create at 2022-07-26 16:18:15
// Last modified on 2022-07-26 23:47:00

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RhodeIsland.RemoteTerminal.Resources
{
	[Serializable]
	public class CharSkinData
	{
		[UnityEngine.Scripting.Preserve]
		public CharSkinData() { }

		public CharSkinData(string skinId, string charId, string illustId, string dynIllustId, string dynPortraitId, string dynEntranceId, string avatarId, string portraitId, bool isBuySkin, string buildingId, DisplaySkin displaySkin)
			: this(skinId, charId, illustId, dynIllustId, dynPortraitId, dynEntranceId, avatarId, portraitId, isBuySkin, buildingId, null, BattleSkin.EMPTY, displaySkin) { }

		public CharSkinData(string skinId, string charId, string illustId, string dynIllustId, string dynPortraitId, string dynEntranceId, string avatarId, string portraitId, bool isBuySkin, string buildingId, List<TokenSkinInfo> tokenSkinMap, BattleSkin battleSkin, DisplaySkin displaySkin)
		{
			this.skinId = skinId;
			this.charId = charId;
			m_illustId = illustId;
			m_dynIllustId = dynIllustId;
			m_dynPortraitId = dynPortraitId;
			m_dynEntranceId = dynEntranceId;
			m_avatarId = avatarId;
			m_portraitId = portraitId;
			m_isBuyAble = isBuySkin;
			m_buildingId = buildingId;
			this.tokenSkinMap = tokenSkinMap;
			m_battleSkin = battleSkin;
			this.displaySkin = displaySkin;
		}

		[JsonIgnore]
		public bool isEmpty
		{
			get
			{
				return charId == null;
			}
		}

		public string GetIllustId()
		{
			return m_illustId;
		}

		public string GetDynIllustId()
		{
			return m_dynIllustId;
		}

		public string GetDynPortraitId()
		{
			return m_dynPortraitId;
		}

		public string GetDynEntranceId()
		{
			return m_dynEntranceId;
		}

		public string GetIllustIdForBattle()
		{
			return string.Format("{0}b", m_illustId);
		}

		public string GetAvatarId()
		{
			if (string.IsNullOrEmpty(m_avatarId))
				m_avatarId = charId;
			return m_avatarId;
		}

		public string GetPortraitId()
		{
			return m_portraitId;
		}

		public string GetBuildingId()
		{
			if (string.IsNullOrEmpty(m_buildingId))
				m_buildingId = charId;
			return m_buildingId;
		}

		public string GetBattlePrefabId()
		{
			if (m_battleSkin.isValid && m_battleSkin.overwritePrefab)
				return m_battleSkin.skinOrPrefabId;
			return charId;
		}

		public bool IsBuyAble()
		{
			return m_isBuyAble;
		}

		public bool TryGetBattleSkin(out BattleSkin skin)
		{
			if (m_battleSkin.isValid)
            {
				skin = m_battleSkin;
				return true;
			}
			skin = BattleSkin.EMPTY;
			return false;
		}

		[JsonIgnore]
		[NonSerialized]
		public static readonly CharSkinData EMPTY;

		public string skinId;

		public string charId;

		public List<TokenSkinInfo> tokenSkinMap;

		[JsonProperty("illustId")]
		private string m_illustId;

		[JsonProperty("dynIllustId")]
		private string m_dynIllustId;

		[JsonProperty("avatarId")]
		private string m_avatarId;

		[JsonProperty("portraitId")]
		private string m_portraitId;

		[JsonProperty("dynPortraitId")]
		private string m_dynPortraitId;

		[JsonProperty("dynEntranceId")]
		private string m_dynEntranceId;

		[JsonProperty("buildingId")]
		private string m_buildingId;

		[JsonProperty("battleSkin")]
		private BattleSkin m_battleSkin;

		[JsonProperty("isBuySkin")]
		private bool m_isBuyAble;

		public string tmplId;

		public string voiceId;

		public SkinVoiceType voiceType;

		public DisplaySkin displaySkin = new();

		[Serializable]
		public class DisplaySkin
		{
			public string skinName;

			public List<string> colorList;

			public List<string> titleList;

			public string modelName;

			public string drawerName;

			public string skinGroupId;

			public string skinGroupName;

			public int skinGroupSortIndex;

			public string content;

			public string dialog;

			public string usage;

			public string description;

			public string obtainApproach;

			public int sortId;

			public string displayTagId;

			public long getTime;

			public int onYear;

			public int onPeriod;
		}

		[Serializable]
		public class TokenSkinInfo
		{
			public string tokenId;

			public string tokenSkinId;
		}

		[Serializable]
		public struct BattleSkin
		{
			[JsonIgnore]
			public bool isValid
			{
				get
				{
					return skinOrPrefabId != null;
				}
			}

			[JsonIgnore]
			[NonSerialized]
			public static readonly BattleSkin EMPTY = new() { skinOrPrefabId = null, overwritePrefab = false };

			public bool overwritePrefab;

			public string skinOrPrefabId;
		}
	}

	public enum SkinVoiceType
	{
		NONE,
		ILLUST,
		ALL
	}
}