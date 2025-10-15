// Created by ChaomengOrion
// Create at 2022-07-25 19:00:41
// Last modified on 2022-07-26 17:05:38

using System;
using System.Runtime.InteropServices;

namespace RhodeIsland.RemoteTerminal.Resources
{
	public static class ResourceUrls
	{
		private static string _GetVoiceJPPath(string path)
		{
			return null;
		}

		private static string _GetVoiceCNPath(string path)
		{
			return null;
		}

		private static string _GetVoiceENPath(string path)
		{
			return null;
		}

		private static string _GetVoiceKRPath(string path)
		{
			return null;
		}

		private static string _GetVoiceCustomPath(string path)
		{
			return null;
		}

		private static string _GetChrIllustResPath(string charId, string illustId)
		{
			return string.Format("{0}/{1}/{2}", "Arts/Characters", charId, illustId);
			/*
			if (string.IsNullOrEmpty(charQuery.tmplId) || charQuery.charId == charQuery.tmplId)
				return string.Format("{0}/{1}/{2}", "Arts/Characters", charQuery.charId, illustId);
			else
				return string.Format("{0}/{1}/{2}", "Arts/Characters", charQuery.tmplId, illustId);*/
		}

		private static string _GetNpcIllustResPath(string npcId)
		{
			return null;
		}

		public static string BattleOnlyChrIllustResPath(CharQuery charQuery, string illustId)
		{
			return null;
		}

		public static string BattleFinishOnlyChrIllustResPath(CharQuery charQuery, string illustId)
		{
			return null;
		}

		public static string GachaOnlyChrIllustResPath(CharQuery charQuery, string illustId)
		{
			return null;
		}

		//MODIFY
		public static string IllustLoaderOnlyChrIllustResPath(string charId, string prefabName)
		{
			return _GetChrIllustResPath(charId, prefabName);
		}

		public static string IllustLoaderOnlyNpcIllustPath(string npcId)
		{
			return null;
		}

		public static string DynIllustResPath(CharQuery charQuery, string dynIllustId)
		{
			return null;
		}

		public static string DynPortraitResPath(CharQuery charQuery, string dynPortraitId)
		{
			return null;
		}

		public static string DynEntranceResPath(CharQuery charQuery, string dynPortraitId)
		{
			return null;
		}

		public static string GetVoiceJPPath(string path)
		{
			return null;
		}

		public static string GetVoiceCNPath(string path)
		{
			return null;
		}

		public static string GetVoiceENPath(string path)
		{
			return null;
		}

		public static string GetVoiceKRPath(string path)
		{
			return null;
		}

		public static string GetVoiceCustomPath(string path)
		{
			return null;
		}

		public static string GetSkillIconHubPath()
		{
			return null;
		}

		public static string ItemUtilOnlyItemIconHubPath()
		{
			return null;
		}

		public static string GetBrandIconHubPath()
		{
			return null;
		}

		public static string GetSkinKvHubPath()
		{
			return null;
		}

		public static string GetVoucherItemBackHubPath()
		{
			return null;
		}

		public static string GetOptionalVoucherBgDecHubPath()
		{
			return null;
		}

		public static string GetItemIconNoTinyPath()
		{
			return null;
		}

		public static string GetItemIconStackPath()
		{
			return null;
		}

		public static string GetStageStartBattleButtonStylePath()
		{
			return null;
		}

		public static string ItemUtilOnlyStageETHubPath()
		{
			return null;
		}

		public static string BattleMenuOnlyStageETHubPath()
		{
			return null;
		}

		public static string GetEnemyIconHubPath()
		{
			return null;
		}

		public static string GetEnemyBossHpIconHubPath()
		{
			return null;
		}

		public static string CharUtilOnlyCharPortraitHubPath()
		{
			return null;
		}

		public static string GetProfessionIconHubPath()
		{
			return null;
		}

		public static string GetProfessionIconV2HubPath()
		{
			return null;
		}

		public static string GetRarityHubPath()
		{
			return null;
		}

		public static string GetCashHubPath()
		{
			return null;
		}

		public static string GetTeamIconHubPath()
		{
			return null;
		}

		public static string GetSpecializedHubPath()
		{
			return null;
		}

		public static string GetStageZoneMapPath(string zoneId)
		{
			return null;
		}

		public static string GetRetroMapPath(string retroId, string zoneId)
		{
			return null;
		}

		public static string GetDiffGroupHolderViewPath()
		{
			return null;
		}

		public static string GetNoramlPreviewPath()
		{
			return null;
		}

		public static string GetHardPreviewPath()
		{
			return null;
		}

		public static string GetZoneDiffHolderPath()
		{
			return null;
		}

		public static string GetFriendAssistPrefabHolder()
		{
			return null;
		}

		public static string GetWeeklyGroupZoneSpritePath(string zoneId)
		{
			return null;
		}

		public static string GetStageZonePageImage(string zoneId)
		{
			return null;
		}

		public static string GetStageZoneBackImage(string zoneId)
		{
			return null;
		}

		public static string GetBuyDiamondShardPanel()
		{
			return null;
		}

		public static string GetStageRoguelikeZoneGroupPrefabPath()
		{
			return null;
		}

		public static string GetClimbTowerIconHubPath()
		{
			return null;
		}

		public static string GetClimbTowerBkgHubPath()
		{
			return null;
		}

		public static string GetClimbTowerStageButtonIconHubPath()
		{
			return null;
		}

		public static string GetClimbTowerCharMarkHubPath()
		{
			return null;
		}

		public static string GetClimbTowerBattleFinishViewPath()
		{
			return null;
		}

		public static string GetStageCampaginZonePrefabPath(string zoneId)
		{
			return null;
		}

		public static string GetStageCampaginZoneStageHubPath()
		{
			return null;
		}

		public static string GetCampaginStageBtnPath(string stageId)
		{
			return null;
		}

		public static string GetCampaignSweepRulePath()
		{
			return null;
		}

		public static string GetCampaigeFastFinishBkgPath()
		{
			return null;
		}

		public static string GetRetroTitlePath(string retroId)
		{
			return null;
		}

		public static string GetRetroMapDecroPath(string retroId)
		{
			return null;
		}

		public static string GetRetroSquadHomePluginPath(string retroId)
		{
			return null;
		}

		public static string GetRetroBackHubPath()
		{
			return null;
		}

		public static string GetRetroBlurBackHubPath()
		{
			return null;
		}

		public static string GetRetroTitleHubPath()
		{
			return null;
		}

		public static string GetSubProfessionHubPath()
		{
			return null;
		}

		public static string GetUniEquipImgHubPath()
		{
			return null;
		}

		public static string GetUniEquipTypeHubPath()
		{
			return null;
		}

		public static string GetUniEquipExtraTypeHubPath()
		{
			return null;
		}

		public static string GetUniEquipTypeShiningHubPath()
		{
			return null;
		}

		public static string GetUniEquipTypeDirectionHubPath()
		{
			return null;
		}

		public static string GetEffectLightPrefabPath()
		{
			return null;
		}

		public static string GetCommonDotBkgPath()
		{
			return null;
		}

		public static string GetStageCampaginRule(string campaginId)
		{
			return null;
		}

		public static string GetNumberPath()
		{
			return null;
		}

		public static string GetCharAvatarHubPath()
		{
			return null;
		}

		public static string GetClueHubPath()
		{
			return null;
		}

		public static string GetFurnitureGroupDetailHubPath(string path)
		{
			return null;
		}

		public static string GetFurnitureIconHubPath()
		{
			return null;
		}

		public static string GetMailUIBackHub()
		{
			return null;
		}

		public static string GetStartBattleButton()
		{
			return null;
		}

		public static string GetFurnitureThemeHubPath()
		{
			return null;
		}

		public static string GetMailSenderConfigPath()
		{
			return null;
		}

		public static string GetStageMapPreviewPath()
		{
			return null;
		}

		public static string GetLogoHubPath()
		{
			return LOGO_HUB_PATH;
		}

		public static string GetChainLoginPath()
		{
			return null;
		}

		public static string GetProfessionTextPath()
		{
			return null;
		}

		public static string GetEliteHubPath()
		{
			return null;
		}

		public static string GetPotentialHubPath()
		{
			return null;
		}

		public static string GetProfessionLargeHubPath()
		{
			return null;
		}

		public static string GetFriendAssistProfessionHubPath()
		{
			return null;
		}

		public static string GetProfessionNoShadowHubPath()
		{
			return null;
		}

		public static string GetSortTypeIconHubPath()
		{
			return null;
		}

		public static string GetGachaDetailHubPath()
		{
			return null;
		}

		public static string GetMapCommonResource()
		{
			return null;
		}

		public static string GetHandBookBattleMapHub()
		{
			return null;
		}

		public static string GetHandBookGroupView(string groupId)
		{
			return null;
		}

		public static string GetGachaPath(string poolId)
		{
			return null;
		}

		public static string GetGachaCostAdditionPath()
		{
			return null;
		}

		public static string GetShopRecommendHubPath([Optional] string recommendId)
		{
			return null;
		}

		public static string GetGiftPackageHubPath()
		{
			return null;
		}

		public static string GetLMTGSResPath(string resName)
		{
			return null;
		}

		public static string GetShopRecommendPath(string typeId)
		{
			return null;
		}

		public static string GetAnnouncePath(string announceId)
		{
			return null;
		}

		public static string GetSkinGroupPath(string skinGroupId)
		{
			return null;
		}

		public static string GetZoneWeeklyItemImage(string zoneId)
		{
			return null;
		}

		public static string GetLoadingIllustPath(string illustId)
		{
			return null;
		}

		public static string GetWorldTipsPath(string tipId)
		{
			return null;
		}

		public static string GetGuidebookPagePath(string pageId)
		{
			return null;
		}

		public static string GetGuidebookTriggerPath(string configPath)
		{
			return null;
		}

		public static string GetBuildingBuffIconPath()
		{
			return null;
		}

		public static string GetBuildingBuffImageConfigPath()
		{
			return null;
		}

		public static string GetBuildingBuffSkillIconPath()
		{
			return null;
		}

		public static string GetBuildingArchIconHubPath()
		{
			return null;
		}

		public static string GetBuildingReflectConfigPath()
		{
			return null;
		}

		public static string GetHGReflectionShaderProfilePath()
		{
			return null;
		}

		public static string GetGraphicCPULadderPath()
		{
			return null;
		}

		public static string GetBuildingStationRoomIconBkgHubPath()
		{
			return null;
		}

		public static string GetBuildingWSBonusHubPath()
		{
			return null;
		}

		public static string GetBuildingDIYShopPath()
		{
			return null;
		}

		public static string GetActivityStagePath(string activityId)
		{
			return null;
		}

		public static string GetActivityResHolderPath(string activityId)
		{
			return null;
		}

		public static string GetActivityPath(string activityId, string item)
		{
			return null;
		}

		public static string GetActivityResMissionPrefab(string activityId)
		{
			return null;
		}

		public static string GetActivityItemIconHub()
		{
			return null;
		}

		public static string GetTimelyDropPrefab(string dropId)
		{
			return null;
		}

		public static string GetTimelyDropHolderPath(string dropId)
		{
			return null;
		}

		public static string GetShopKeeperGraphicPath(string shopKeeperId)
		{
			return null;
		}

		public static string GetHomeBannerZoneHubPath()
		{
			return null;
		}

		public static string GetHomeBannerGachaHubPath()
		{
			return null;
		}

		public static string GetHomeBannerShopHubPath()
		{
			return null;
		}

		public static string GetMissionDailyTabPath(string missionTagID)
		{
			return null;
		}

		public static string GetUIEffectPath(string effectName)
		{
			return null;
		}

		public static string GetMedalIconPicHubPath()
		{
			return null;
		}

		public static string GetMedalTitlePicHubPath()
		{
			return null;
		}

		public static string GetCrisisStageEntryPicHubPath()
		{
			return null;
		}

		public static string GetCrisisLevelBackHubPath()
		{
			return null;
		}

		public static string GetCrisisRuneIconHubPath()
		{
			return null;
		}

		public static string GetCrisisAppraiseIconHubPath()
		{
			return null;
		}

		public static string GetRuneDetailBgHubPath()
		{
			return null;
		}

		public static string GetCrisisShopBackHubPath()
		{
			return null;
		}

		public static string GetCrisisShopSeasonBackHubPath()
		{
			return null;
		}

		public static string GetCrisisStageLogoHubPath()
		{
			return null;
		}

		public static string GetCrisisStageSeasonResHolderPath(string seasonId)
		{
			return null;
		}

		public static string GetCrisisEntrySeasonResHolderPath(string seasonId)
		{
			return null;
		}

		public static string GetCrisisSeasonMiscResHolderPath(string seasonId)
		{
			return null;
		}

		public static string GetActivityAssetMapPath()
		{
			return null;
		}

		public static string GetCrisisMapRuleImgPath()
		{
			return null;
		}

		public static string UIPageControllerGetDynamicPageHubPath()
		{
			return null;
		}

		public static string StateEngineGetDynStateHubPath()
		{
			return null;
		}

		public static string GetDataVersionFilePath()
		{
			return null;
		}

		public static string GetLMTGShopDetailPrefabPath()
		{
			return null;
		}

		public static string GetLMTGSShopButtonHub()
		{
			return null;
		}

		public static string GetTemplateShopRareBgHubPath()
		{
			return null;
		}

		public static string GetItemRepoPanelObtainPrefabPath()
		{
			return null;
		}

		public static string GetStoryReviewActivityImageHubPath()
		{
			return null;
		}

		public static string GetStoryReviewMiniActivityImageHubPath()
		{
			return null;
		}

		public static string GetStoryReviewMiniStoryDetailImageHubPath()
		{
			return null;
		}

		public static string GetStoryReviewMiniStoryCharImageHubPath()
		{
			return null;
		}

		public static string GetMedalDIYFramePath(string frameId)
		{
			return null;
		}

		public static string GetMedalGroupFramePath(string groupId)
		{
			return null;
		}

		public static string GetMedalSuitBkgHubPath()
		{
			return null;
		}

		public static string GetMedalGroupViewPath()
		{
			return null;
		}

		public static string GetRoguelikeChoiceHubPath()
		{
			return null;
		}

		public static string GetRoguelikeInitChoiceHubPath()
		{
			return null;
		}

		public static string GetRoguelikeEndingIconAtlasPath(string topicId)
		{
			return null;
		}

		public static string GetRoguelikeRarityBarHubPath()
		{
			return null;
		}

		public static string GetRoguelikeInitRelicIconHubPath()
		{
			return null;
		}

		public static string GetRoguelikeLevelBGHubPath()
		{
			return null;
		}

		public static string GetCampaignWorldMapPieceHubPath()
		{
			return null;
		}

		public static string GetCampaignZoneIconHubPath()
		{
			return null;
		}

		public static string GetActivityNewsContentPath(string activityId)
		{
			return null;
		}

		public static string GetActivityNewsTitlePath(string activityId)
		{
			return null;
		}

		public static string GetActivityNewsLogoPath(string activityId)
		{
			return null;
		}

		public static string GetActivityNewsLogoLargePath(string activityId)
		{
			return null;
		}

		public static string GetActivityKVPath(string activityId)
		{
			return null;
		}

		public static string GetHandBookBattleFinishViewPath()
		{
			return null;
		}

		public static string GetPlayerAvatarPicHubPath()
		{
			return null;
		}

		public static string GetPlayerAvatarViewPath()
		{
			return null;
		}

		public static string GetUICommentedTextPrefabResPath()
		{
			return null;
		}

		public static string GetStageZoneHomeEntryHub()
		{
			return null;
		}

		public static string GetStageZoneHomeThemeHub()
		{
			return null;
		}

		public static string GetStageZoneWeeklyHub()
		{
			return null;
		}

		public static string GetStageZoneMainlineItemBgHub()
		{
			return null;
		}

		public static string GetStageZoneMainlineBgHub()
		{
			return null;
		}

		public static string GetStageZoneGroupHolder()
		{
			return null;
		}

		public static string GetStageChapterTitleBgHub()
		{
			return null;
		}

		public static string GetStageSpoilerDialog()
		{
			return null;
		}

		public static string GetStageDiffIconHub()
		{
			return null;
		}

		public static string GetRoguelikeRelicConfirmDialog(string topicId)
		{
			return null;
		}

		public static string GetRoguelikeEndingDialog(string topicId)
		{
			return null;
		}

		public static string GetRogueTopicReportPath(string topicId)
		{
			return null;
		}

		public static string GetRoguelikeTopicEndingPath(string topicId)
		{
			return null;
		}

		public static string GetRoguelikeTopicEndBkgHubPath(string topicId)
		{
			return null;
		}

		public static string GetStageItemUseConfirmPanel()
		{
			return null;
		}

		public static string GetTermDesciptionPrefabResPath()
		{
			return null;
		}

		public static string GetCharmIconHubPath()
		{
			return null;
		}

		public static string GetUiShaderProfilePath()
		{
			return null;
		}

		public static string GetActArchiveComponentPath(string componentType)
		{
			return null;
		}

		public static string GetActArchiveImageHubPath()
		{
			return null;
		}

		public static string GetActArchiveIlluImageHubPath()
		{
			return null;
		}

		public static string GetActArchiveItemTitleIconHubPath()
		{
			return null;
		}

		public static string GetActArchiveFileIlluImageHubPath()
		{
			return null;
		}

		public static string GetActArchiveResHolderPath(string archiveId)
		{
			return null;
		}

		public static string GetDeepSeaZoneMapPath(string actId, string zoneId)
		{
			return null;
		}

		public static string GetDeepSeaZoneMapBarPath(bool isRetro, string groupId)
		{
			return null;
		}

		public static string GetDeepSeaRPPicHubPath()
		{
			return null;
		}

		public static string GetDeepSeaRPSpecialPicHubPath()
		{
			return null;
		}

		public static string GetHomeBackgroundPreviewHubPath()
		{
			return null;
		}

		public static string GetHomeBackgroundItemIconHubPath()
		{
			return null;
		}

		public static string GetHomeBackgroundAssetsWrapperPath(string bgId)
		{
			return null;
		}

		public static string GetHiddenStageDecodePath(string actId, bool isRetro)
		{
			return null;
		}

		public static string GetZoneRecordNotePath(string textPath)
		{
			return null;
		}

		public static string GetZoneRecordPicHubPath()
		{
			return null;
		}

		public static string GetZoneRecordToughPicPath(string zoneId, string picName)
		{
			return null;
		}

		public static string GetRoguelikeTopicPath(string topicId, string relativePath)
		{
			return null;
		}

		public static string GetRoguelikeTopicKVPath(string topicId, string kvId)
		{
			return null;
		}

		public static string GetRoguelikeTopicOuterBuffViewPath(string topicId)
		{
			return null;
		}

		public static string GetRoguelikeTopicOuterBuffIconHubPath()
		{
			return null;
		}

		public static string GetRoguelikeTopicCapsuleIconHubPath(string topicId)
		{
			return null;
		}

		public static string GetRoguelikeTopicChallengeThumbnailHubPath(string topicId)
		{
			return null;
		}

		public static string GetRoguelikeTopicBpGrandPrizeHubPath(string topicId)
		{
			return null;
		}

		public static string GetRoguelikeTopicEntryPath(string topicId)
		{
			return null;
		}

		public static string GetRoguelikeTopicItemHubPath()
		{
			return null;
		}

		public static string GetRoguelikeUnderTex(string topicId)
		{
			return null;
		}

		public static string GetRecruitGrpHubPath()
		{
			return null;
		}

		public static string GetOuterRoguelikeEntryResHolder(string topicId, string displayId)
		{
			return null;
		}

		public static string GetRoguelikeTopicMiscResHolderPath(string topicId)
		{
			return null;
		}

		private const string CHR_ILLUST_PATH = "Arts/Characters";

		private const string CHR_DYN_ILLUST_PATH = "Arts/DynChars";

		private const string CHR_DYN_PORTRAIT_PATH = "Arts/DynPortraits";

		private const string CHR_DYN_ENTRANCE_PATH = "Arts/DynCharStart";

		private const string NPC_ILLUST_PATH = "Arts/NPCs";

		private const string SKILL_ICON_PATH = "Arts/Skills/hub_skillicon";

		private const string ITEM_ICON_HUB_PATH = "Arts/Items/Icons/icon_hub";

		private const string ITEM_ICON_NO_TINY_HUB = "Arts/Items/item_icons_no_tiny_hub";

		private const string ITEM_ICON_STACK_PATH = "Arts/Items/item_icons_stack_hub";

		private const string VOUCHER_ITEM_BACK_HUB = "Arts/Items/VoucherItemIcons/pic_hub";

		private const string OPTIONAL_VOUCHER_BG_DEC_HUB = "Arts/Items/OptionalVoucherItemBgDecs/pic_hub";

		private const string ENEMY_ICON_HUB_PATH = "Arts/Enemies/ahub_enemy_icons";

		private const string ENEMY_BOSS_HPICON_HUB = "Arts/Enemies/enemy_boss_hpicon_hub";

		private const string PROFESSION_ICON_PATH = "Arts/Misc/profession_icons_hub";

		private const string PROFESSION_V2_ICON_PATH = "Arts/Misc/profession_icons_v2_hub";

		private const string SORTTYPE_ICON_PATH = "Arts/UI/CharacterSortArrtIcons/icon_hub";

		private const string TEAM_ICON_PATH = "Arts/TeamIcon/team_icon_hub";

		private const string BRAND_ICON_PATH = "Arts/UI/BrandImage/pic_hub";

		private const string SKIN_KV_HUB_PATH = "UI/KVImg/pic_hub";

		private const string PROFESSION_TEXT_PATH = "Arts/profession_hub";

		private const string CHAR_AVATAR_HUB_PATH = "Arts/CharAvatars/avatar_hub";

		private const string CHAR_PORTRAIT_HUB_PATH = "Arts/CharPortraits/portrait_hub";

		private const string RARITY_HUB_PATH = "Arts/rarity_hub";

		private const string CLUE_HUB_PATH = "Arts/clue_hub";

		private const string FURNITURE_ICON_HUB_PATH = "Arts/UI/FurnitureIcons/furni_icon_hub";

		private const string FURNITURE_THEME_HUB_PATH = "Arts/UI/FurniThemes/furni_theme_hub";

		private const string CASH_HUB_PATH = "Arts/Shop/cash_hub";

		private const string ZONE_MAP_PATH = "UI/[UC]Stage";

		private const string ZONE_BACK_MAP_PATH = "Arts/UI/Stage/[PACK]MainlineZoneItems/zone_back_{0}";

		private const string ZONE_PAGE_MAP_PATH = "Arts/UI/Stage/[PACK]MainlineZoneItems/zone_page_{0}";

		private const string ZONE_WEEKLY_ITEM_IMAGE = "Arts/UI/Stage/[PACK]WeeklyZoneItems/zone_item_{0}";

		public const string ACTIVITY_ROOT = "Activity";

		private const string ACTIVITY_MISSION_PATH = "Activity/[UC]{0}/{0}";

		private const string ACTIVITY_ITEM_ICON_HUB_PATH = "Activity/CommonAssets/[UC]Items/act_item_hub";

		private const string TIMELY_DROP_PATH = "UI/TimelyDrop/{0}";

		private const string TIMELY_DROP_HOLDER = "UI/TimelyDrop/{0}/timely_drop_holder";

		private const string MAP_COMMON_REOUSRCE_PATH = "Arts/Maps/CommonResource";

		private const string HANDBOOK_BATTLE_PATH = "Arts/UI/HandBookBattle/pic_hub";

		private const string HANDBOOK_GROUP_VIEW = "UI/HandBookV2/Group/{0}_group";

		private const string CAMPAIGN_STAGE_BTN = "UI/Campaign/StageButton/btn_{0}";

		private const string RETRO_TITLE_PREFAB = "UI/Retro/Title/retro_{0}";

		private const string RETRO_VIEW_PREFAB = "UI/Retro/View/retro_{0}";

		private const string RETRO_MAP_DECRO_PREFAB = "UI/Retro/MapDecro/retro_{0}";

		private const string RETRO_BACK_HUB = "Arts/UI/Stage/[UC]RetroBack/pic_hub";

		private const string RETRO_BLUR_BACK_HUB = "Arts/UI/Stage/[UC]RetroBlurBack/pic_hub";

		private const string RETRO_TITLE_HUB = "Arts/UI/Stage/RetroTitle/pic_hub";

		private const string RETRO_SQUAD_HOME_PLUGIN_PREFAB = "UI/Retro/SquadHomePlugin/retro_squad_plugin_{0}";

		private const string SUB_PROF_ICON_HUB = "Arts/UI/SubProfessionIcon/icon_hub";

		private const string UNI_EQUIP_PIC = "Arts/UI/UniEquipImg/pic_hub";

		private const string UNI_EQUIP_TYPE_PIC = "Arts/UI/UniEquipType/icon_hub";

		private const string UNI_EQUIP_EXTRA_TYPE_PIC = "Arts/UI/UniEquipExtraType/icon_hub";

		private const string UNI_EQUIP_TYPE_SHINING_PIC = "Arts/UI/UniEquipColorShining/pic_hub";

		private const string UNI_EQUIP_TYPE_DIRECTION_PIC = "Arts/UI/UniEquipDirection/pic_hub";

		private const string EFFECT_LIGHT = "UI/[UC]Common/effect_lights";

		private const string UI_COMMON_DOT_BKG = "Arts/UI/[UC]Common/Textures/NoPack/dot_bkg";

		private const string MAIL_SENDER_CONFIG = "UI/Mail/mail_sender_config";

		private const string MAIL_UI_BACK_HUB = "Arts/UI/MailStyle/pic_hub";

		private const string START_BATTLE_BUTTON_HUB = "Arts/UI/StartBattleButton/pic_hub";

		private const string STAGE_MAINLINE_ZONE_BG_PATH = "Arts/UI/Stage/[PACK]MainlineZoneBg/{0}";

		private const string STAGE_WEEKLY_GROUP_ZONE_PATH = "Arts/UI/Stage/[PACK]WeeklyGroupZoneItems/{0}";

		private const string STAGE_MAP_PREVIEW_PATH = "Arts/UI/Stage/MapPreviews";

		private const string STAGE_CAMPAIGNRULE_PATH = "Arts/UI/Stage/[PACK]CampaignRules/rule_{0}";

		private const string LOGO_HUB_PATH = "Arts/CampLogo/logo_hub";

		private const string OPEN_SERVER_CHAIN_LOGIN_PATH = "Arts/openserver_chain_login_hub";

		private const string ELITE_HUB_PATH = "Arts/elite_hub";

		private const string POTENTIAL_HUB_PATH = "Arts/potential_hub";

		private const string PROFESSION_LARGE_HUB_PATH = "Arts/profession_large_hub";

		private const string PROFESSION_NO_SHADOW_HUB_PATH = "Arts/profession_no_shadow_hub";

		private const string NUMBER_HUB_PATH = "Arts/number_hub";

		private const string SPECIALIZED_HUB_PATH = "Arts/specialized_hub";

		private const string GACHA_POOL_PATH = "UI/Gacha/{0}";

		private const string GACHA_COST_ADD_PATH = "Arts/[PACK]CmGachaPool/Prefabs/item_cost_add";

		private const string ANNOUNCE_POOL_PATH = "UI/[UC]Announce/{0}";

		private const string SKIN_PREFAB_PATH = "UI/Skin/{0}";

		private const string SHOP_RECOMMEND_PATH = "UI/RecommendShop/{0}";

		private const string SHOP_IMAGE_HUB_PATH = "Arts/Shop/RecommendShopGroup/{0}";

		private const string SHOP_FURN_HUB_PATH = "Arts/Shop/FurnGroup/{0}";

		private const string SHOP_LMTGS_RES_PATH = "Arts/Shop/[UC]LmtgsShop/{0}";

		private const string SHOP_LMTGS_SHOP_BTNS_PATH = "Arts/Shop/[UC]LmtgsShop/Btns/lmtgs_btn_hub";

		private const string TEMPLATE_SHOP_RARITY_HUB_PATH = "Arts/Shop/[UC]TemplateShop/rairity_bg/{0}";

		private const string ITEM_REPO_PANEL_OBTAIN_PATH = "UI/[UC]ItemRepo/panel_obtain";

		private const string GIFT_PACKAGE_HUB_PATH = "Arts/Shop/GPImages/gp_hub";

		private const string GACHA_DETAIL_HUB_PATH = "Arts/gacha_detail_hub";

		private const string LOADING_ILLUSTS_PATH = "Arts/[UC]LoadingIllusts";

		private const string WORLD_TIPS_PATH = "HotUpdate/[UC]WorldTips";

		private const string GUIDEBOOK_PAGES_PATH = "Arts/GuideBookPages";

		private const string GUIDEBOOK_TRIGGERS_PATH = "Config/GuideBookTriggers";

		private const string VOICE_JP_PATH = "Audio/Sound_Beta_2/Voice/{0}";

		private const string VOICE_CN_PATH = "Audio/Sound_Beta_2/Voice_CN/{0}";

		private const string VOICE_EN_PATH = "Audio/Sound_Beta_2/Voice_EN/{0}";

		private const string VOICE_KR_PATH = "Audio/Sound_Beta_2/Voice_KR/{0}";

		private const string VOICE_CUSTOM_PATH = "Audio/Sound_Beta_2/Voice_Custom/{0}";

		private const string BUILDING_BUFF_ICON_PATH = "Arts/Building/Buffs/buff_icon_hub";

		private const string BUILDING_BUFF_IMAGE_CONFIG_PATH = "Arts/Building/Buffs/buff_image_config";

		private const string BUILDING_BUFF_SKILL_ICON_PATH = "Arts/Building/Skills/icon_hub";

		private const string BUILDING_ARCH_ICON_HUB_PATH = "Arts/Building/Architect/room_icon_sprite_hub";

		private const string BUILDING_STATION_ICON_BKG_HUB_PATH = "Arts/Building/StationRoomBkgs/building_station_icon_bkg";

		private const string BUILDING_WS_BONUS_HUB_PATH = "Arts/Building/[UC]WSBonus/ws_bonus";

		private const string BUILDING_DIY_SHOP_PATH = "Building/UI/[UC]DIY/DynUse/diy_shop_panel";

		private const string SHOP_KEEPER_GRAPHIC_PATH = "Prefabs/Shop/ShopKeeper";

		private const string MISSION_TAB_PATH = "UI/Mission/MissionTab";

		private const string STAGE_START_BATTLE_BUTTON_STYLE = "Arts/UI/Stage/StartBattleButton/buildin_styles";

		private const string STAGE_ET_ITEM_ICON_HUB_PATH = "Arts/Items/stage_et_item_icon_hub";

		private const string BUILDING_REFLECT_CONFIG_PATH = "Building/Vault/ReflectConfig";

		private const string STAGE_ZONE_HOME_ENTRY_HUB_PATH = "Arts/UI/Stage/[UC]HomeEntry/entry_hub";

		private const string STAGE_ZONE_HOME_THEME_HUB_PATH = "Arts/UI/Stage/HomeTheme/theme_hub";

		private const string STAGE_ZONE_WEEKLY_HUB_PATH = "Arts/UI/Stage/[UC]StageZoneWeekly/stage_zone_weekly_hub";

		private const string STAGE_ZONE_MAINLINE_BG_HUB_PATH = "Arts/UI/Stage/[UC]StageZoneMainlineBg/mainline_bg_hub";

		private const string STAGE_ZONE_MAINLINE_ITEM_BG_HUB_PATH = "Arts/UI/Stage/[UC]StageZoneMainlineItemBg/zone_item_bg_hub";

		private const string STAGE_CHAPTER_TITLE_BG_HUB_PATH = "Arts/UI/Stage/StageChapterTitleBg/chapter_title_bg_hub";

		private const string STAGE_SPOILER_DIALOG_PATH = "UI/Stage/DynRes/popup_spoiler_confirm_dialog";

		private const string STAGE_DIFF_ICON_HUB_PATH = "Arts/UI/Stage/StageDiffGroup/icon_hub";

		private const string HG_REFLECTION_SHADER_PROFILE_PATH = "Graphics/HGReflectionShaderProfile";

		private const string HG_GRAPHIC_CPU_LADDER_PATH = "Graphics/CpuLadder";

		private const string BUY_DIAMOND_SHARD_PANEL = "UI/Stage/DynRes/panel_buy_diamond_shard";

		private const string STAGE_ZONE_GROUP_HOLDER = "UI/Stage/DynRes/zone_group_holder";

		private const string STAGE_ITEM_USE_CONFIRM_PANEL = "UI/Stage/DynRes/item_use_confirm_float";

		private const string DIFF_GROUP_HOLDER_PANEL = "UI/Stage/DynRes/panel_diff_group_detail_view";

		private const string STAGE_PREVIEW_NORMAL = "UI/Stage/DynRes/Preview/preview_normal_part";

		private const string STAGE_PREVIEW_HARD = "UI/Stage/DynRes/Preview/preview_hard_part";

		private const string STAGE_DIFF_HOLDER = "UI/Stage/DynRes/Preview/diff_container";

		private const string FRIEND_ASSIST_PREFAB_HOLDER = "UI/FriendAssist/friend_assist_tab";

		private const string CRISIS_ENTRY_PICK_HUB_PATH = "Arts/UI/Crisis/EntryPic/pic_hub";

		private const string CRISIS_RUNE_ICON_HUB_PATH = "Arts/UI/Crisis/RuneIcon/icon_hub";

		private const string CRISIS_SHOP_BACK_HUB_PATH = "Arts/UI/Crisis/ShopBack/pic_hub";

		private const string CRISIS_SHOP_SEASON_BACK_HUB_PATH = "Arts/UI/Crisis/ShopSeasonBack/pic_hub";

		private const string CRISIS_APPRAISE_ICON_HUB_PATH = "Arts/UI/Crisis/CrisisAppraiseBack/pic_hub";

		private const string CRISIS_LEVEL_BACK_HUB_PATH = "Arts/UI/Crisis/CrisisLevelBack/pic_hub";

		private const string CRISIS_RUNE_DETAIL_BG_HUB_PATH = "Arts/UI/Crisis/RuneDetailBg/pic_hub";

		private const string CRISIS_STAGE_LOGO_HUB_PATH = "Arts/UI/Crisis/StageLogo/logo_hub";

		private const string CRISIS_SEASON_STAGE_RES_PATH = "CrisisSeasons/[UC]{0}/stage_res_holder";

		private const string CRISIS_SEASON_ENTRY_RES_PATH = "CrisisSeasons/[UC]{0}/entry_res_holder";

		private const string CRISIS_SEASON_MISC_RES_PATH = "CrisisSeasons/[UC]{0}/misc_res";

		private const string CRISIS_MAP_RULE_IMG_PATH = "Arts/UI/Crisis/rune_unlock_rule";

		private const string STORY_REVIEW_ACTIVITY_IMG_HUB_PATH = "Arts/UI/StoryReview/Hubs/Activity/pic_hub";

		private const string STORY_REVIEW_MINI_ACTIVITY_IMG_HUB_PATH = "Arts/UI/StoryReview/Hubs/Mini/pic_hub";

		private const string STORY_REVIEW_MINI_STORY_DETAIL_IMG_HUB_PATH = "Arts/UI/StoryReview/Hubs/MiniDetail/pic_hub";

		private const string STORY_REVIEW_MINI_STORY_CHAR_IMG_HUB_PATH = "Arts/UI/StoryReview/Hubs/MiniChar/pic_hub";

		private const string HOME_BANNER_ZONE_HUB = "Arts/UI/HomeBanners/Zone/banner_hub";

		private const string HOME_BANNER_SHOP_HUB = "Arts/UI/HomeBanners/Shop/banner_hub";

		private const string HOME_BANNER_GACHA_HUB = "Arts/UI/HomeBanners/Gacha/banner_hub";

		private const string MEDAL_ICON_HUB_PATH = "Arts/UI/MedalIcon/pic_hub";

		private const string MEDAL_TITLE_HUB_PATH = "Arts/UI/MedalTitle/pic_hub";

		private const string MEDAL_DIY_FRAME_PATH = "UI/Medal/[UC]DIYFrame/{0}";

		private const string MEDAl_GROUP_FRAME_PATH = "UI/Medal/[UC]GroupFrame/{0}";

		private const string MEDAL_SUIT_BKG_HUB = "Arts/UI/Medal/SuitBkg/suit_hub";

		private const string MEDAL_GROUP_VIEW_OBJ = "UI/Medal/[PACK]Group/medal_group_view";

		private const string ROGUELIKE_ZONE_GROUP_PANEL = "UI/Stage/Roguelike/panel_zone_group_roguelike";

		private const string ROGUELIKE_UNDER_TEX_HUB_PATH = "UI/RoguelikeTopic/Topics/{0}/UnderTexPic/under_tex_hub";

		private const string ROGUELIKE_RECRUIT_GRP_HUB_PATH = "Arts/UI/RoguelikeTopic/RecruitGrpPic/recruit_gr_hub";

		private const string ROGUELIKE_CHOICE_HUB_PATH = "Arts/UI/RoguelikeTopic/ChoicePic/choice_pic";

		private const string ROGUELIKE_INIT_CHOICE_HUB_PATH = "Arts/UI/RoguelikeTopic/InitChoicePic/init_choice";

		private const string ROGUELIKE_RARITY_BAR_HUB_PATH = "Arts/UI/RoguelikeTopic/Rarity/rglk_rarity_hub";

		private const string ROGUELIKE_INIT_RELIC_ICON_HUB_PATH = "Arts/UI/RoguelikeTopic/InitRelicIconPic/init_relic_hub";

		private const string ROGUELIKE_LEVEL_BG_HUB_PATH = "Arts/UI/RoguelikeTopic/LevelBGPic/level_bg_pic";

		private const string ROGUELIKE_RELIC_CONFIRM_DIALOG_PATH = "UI/RoguelikeTopic/Topics/{0}/Dialog/panel_relic_confirm_dialog";

		private const string ROGUELIKE_ENDING_DIALOG_PATH = "UI/RoguelikeTopic/Topics/{0}/Dialog/panel_ending_dialog";

		private const string ROGUELIKE_TOPIC_REPORT_PATH = "UI/RoguelikeTopic/Topics/{0}/Ending/ending_report";

		private const string ROGUELIKE_ENDING_ICON_ATLAS_PATH = "UI/RoguelikeTopic/Topics/{0}/Ending/ending_icon";

		private const string ROGUELIKE_TOPIC_ENDING_PATH = "UI/RoguelikeTopic/Topics/{0}/Ending/ending_view";

		private const string ROGUELIKE_TOPIC_END_BKG_HUB_PATH = "UI/RoguelikeTopic/Topics/{0}/Ending/GameEndBg/game_end_bg";

		private const string ROGUELIKE_TOPICS_DIR = "UI/RoguelikeTopic/Topics";

		private const string ROGUELIKE_TOPIC_ITEM_HUB_PATH = "Arts/UI/RoguelikeTopic/ItemPic/relic_icons";

		private const string ROGUELIKE_TOPIC_OUTER_BUFF_ICON_HUB_PATH = "Arts/UI/RoguelikeTopic/OuterBuffIcon/outer_buff_icon_hub";

		private const string ROGUELIKE_TOPIC_CAPSULE_ICON_HUB_PATH = "UI/RoguelikeTopic/Topics/{0}/Capsule/capsule_hub";

		private const string ROGUELIKE_TOPIC_CHALLENGE_THUMBNAIL_HUB_PATH = "UI/RoguelikeTopic/Topics/{0}/ChallengeModePic/challenge_mode_pic_hub";

		private const string ROGUELIKE_TOPIC_BP_GRAND_PRIZE_ICON_HUB_PATH = "UI/RoguelikeTopic/Topics/{0}/BPGrandPrize/bg_prize_hub";

		private const string ROGUELIKE_TOPIC_KV_PATH = "UI/RoguelikeTopic/Topics/{0}/EntryKeyVisuals/{1}";

		private const string CAMPAIGN_WORLD_MAP_PIECE_HUB_PATH = "Arts/UI/Campaign/WorldMapPiece/pic_hub";

		private const string CAMPAIGN_ZONE_ICON_HUB_PATH = "Arts/UI/Campaign/ZoneIcon/pic_hub";

		private const string CAMPAIGN_STAGE_ZONE_PATH = "UI/Campaign/Zone/ZoneMap/{0}";

		private const string CAMPAIGN_STAGE_ZONE_STAGE_HUB_PATH = "Arts/UI/Campaign/CampaignStageBtn/pic_hub";

		private const string CLIMB_TOWER_ICON_HUB_PATH = "Arts/UI/ClimbTower/TowerIcon/pic_hub";

		private const string CLIMB_TOWER_BKG_HUB_PATH = "Arts/UI/ClimbTower/TowerBkg/pic_hub";

		private const string CLIMB_TOWER_STAGE_BUTTON_ICON_HUB_PATH = "Arts/UI/ClimbTower/ClimbTowerStageBtn/pic_hub";

		private const string CLIMB_TOWER_SQUAD_CHAR_MARK_HUB_PATH = "Arts/UI/ClimbTower/SquadCharMark/pic_hub";

		private const string CLIMB_TOWER_BATTLE_FINISH_VIEW_PATH = "UI/[UC]ClimbTower/BattleFinish/tower_battle_finish_view";

		private const string CAMPAIGN_SWEEP_RULE_PATH = "UI/Campaign/[UC]DynLoad/FastCamp/fast_camp_rule";

		private const string CAMPAIGN_FAST_FINISH_BKG_PATH = "UI/Campaign/[UC]DynLoad/FastCamp/fast_camp_finish_bkg";

		private const string ACTIVITY_ASSET_MAP_PATH = "Config/act_asset_map";

		private const string BATTLE_CHARACTER_PATH = "Battle/Prefabs/Characters/{0}";

		private const string BATTLE_SKIN_CHARACTER_PATH = "Battle/Prefabs/Skins/Character/{0}/{1}";

		private const string BUILDING_CHARACTER_PATH = "Building/Vault/Characters/{0}";

		private const string DYNAMIC_PAGE_HUB_PATH = "UI/Configs/dynamic_pages";

		private const string DYNAMIC_STATE_HUB_PATH = "UI/Configs/dyn_states";

		private const string UI_EFFECT_ROOT_PATH = "UI/[UC]Effect";

		private const string ACTIVITY_NEWS_CONTENT_PATH = "Activity/[UC]{0}/NewsImg/content/content";

		private const string ACTIVITY_NEWS_TITLE_PATH = "Activity/[UC]{0}/NewsImg/title/title";

		private const string ACTIVITY_NEWS_LOGO_PATH = "Activity/[UC]{0}/NewsImg/logo/small/newsLogo";

		private const string ACTIVITY_NEWS_LOGO_LARGE_PATH = "Activity/[UC]{0}/NewsImg/logo/large/newsLogoLarge";

		private const string ACTIVITY_ENTRY_KV_PATH = "Activity/[UC]{0}/KVs/kv_hub";

		private const string HANDBOOK_BATTLE_FINISH_VIEW_PATH = "UI/HandBook/hand_book_char_story_battle_finish";

		private const string DATA_VERSION_FILE = "GameData/Excel/data_version";

		public const string HANDBOOK_POS_DB = "GameData/Art/handbookpos_table";

		public const string HANDBOOK_FORCE_MAP_DB = "GameData/Art/handbook_force_map_table";

		private const string PLAYER_AVATAR_HUB_PATH = "Arts/UI/PlayerAvatar/pic_hub";

		private const string PlAYER_AVATAR_VIEW_PATH = "UI/PlayerAvatar/player_avatar_item";

		private const string FRIEND_ASSIST_PROFESSION_HUB_PATH = "Arts/friend_assist_profession_hub";

		private const string UI_COMMENTED_TEXT_PREFAB = "Prefabs/Texts/item_commentedTextBkg";

		private const string UI_TERM_DESCRIPTION_VIEW_PREFAB = "Prefabs/TermDescription/ui_term_description_panel";

		private const string CHARM_ICON_HUB_PATH = "Arts/UI/CharmIcon/charm_hub";

		private const string UI_SHADER_PROFILE_PATH = "Arts/ui_shader_profile";

		private const string HOME_BACKGROUND_PREVIEW_ICON_HUB_PATH = "Arts/UI/HomeBackground/PreviewItem/home_background_preview_hub";

		private const string HOME_BACKGROUND_ITEM_ICON_HUB_PATH = "Arts/UI/HomeBackground/ItemIcon/home_background_icon_hub";

		private const string HOME_BACKGROUND_ASSETS_WRAPPER_PATH = "Arts/UI/HomeBackground/Wrapper/{0}";

		private const string DEEP_SEA_ZONE_MAP_PATH = "Activity/[UC]{0}/ZoneMaps/zone_map_{1}";

		private const string DEEP_SEA_ZONE_MAP_BAR_PATH = "Activity/[UC]{0}/Prefabs/{0}_map_bar";

		private const string DEEP_SEA_ZONE_RETRO_MAP__BAR_PATH = "UI/[UC]DeepSea/Retro/retro_deep_sea_rp_map_bar";

		private const string ACT_ARCHIVE_COMPONENT_PATH = "UI/[UC]ActArchive/Component/{0}/{0}_panel";

		private const string ACT_ARCHIVE_IMAGE_HUB_PATH = "Arts/UI/ActArchive/act_archive_hub";

		private const string ACT_ARCHIVE_ILLU_IMAGE_HUB_PATH = "Arts/UI/ActArchiveIllustration/act_archive_illustration_hub";

		private const string ACT_ARCHIVE_FILE_ILLU_IMAGE_HUB_PATH = "Arts/UI/ActArchiveFileIllustration/act_archive_file_illust_hub";

		private const string ACT_ARCHIVE_RES_HOLDER_PATH = "Arts/UI/ActArchiveSkin/[UC]{0}/{0}_res_holder";

		private const string ACT_ARCHIVE_TITLE_ICON_HUB_PATH = "Arts/UI/ActArchiveItemTitleIcon/icon_hub";

		private const string DEEP_SEA_RP_PIC_HUB_PATH = "Arts/UI/DeepSea/NormalPic/pic_hub";

		private const string DEEP_SEA_RP_SPECIAL_PIC_HUB_PATH = "Arts/UI/DeepSea/SpecialPic/pic_hub";

		private const string ZONE_RECORD_TEXT_PATH = "GameData/Story/{0}";

		private const string ZONE_RECORD_TOUGH_PIC_PATH = "UI/Stage/ZoneRecordToughNote/{0}/{1}";

		private const string ZONE_RECORD_PIC_PATH = "Arts/UI/Stage/NoteSprite/note_sprite";

		private const string HIDDEN_STAGE_DECODE_VIEW_PATH = "Activity/[UC]{0}/Prefabs/{0}_hidden_stage_decode_view";

		private const string HIDDEN_STAGE_DECODE_VIEW_RETRO_PATH = "UI/[UC]HiddenStage/Retro/retro_hidden_stage_decode_view";
	}

	[Serializable]
	public struct CharQuery
	{
		public bool IsEmpty()
		{
			return charId == null;
		}

		public static CharQuery SimpleChar(string charId)
		{
			return new CharQuery
            {
				charId = charId,
				tmplId = null,
				isToken = false
            };
		}

		/*public static CharQuery FromPlayer(PlayerCharacter playerChar)
		{
			return default(CharQuery);
		}

		public static CharQuery FromSharedChar(SharedCharData sharedData)
		{
			return default(CharQuery);
		}

		public static CharQuery FromSkin(CharSkinData skinData)
		{
			return default(CharQuery);
		}*/

		public static CharQuery FromSkinId(string skinId)
		{
			if (string.IsNullOrEmpty(skinId) || !true)
            {
				//TODO
				return EMPTY;
			}
			else
            {
				return new CharQuery();
            }
		}

		public override string ToString()
		{
			return charId;
		}

		public static readonly CharQuery EMPTY = new()
        {
			charId = null,
			isToken = false,
			tmplId = null
        };

		public string charId;

		public bool isToken;

		public string tmplId;
	}
}
