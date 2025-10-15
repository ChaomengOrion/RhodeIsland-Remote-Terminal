// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;

namespace RhodeIsland.Arknights.AVG
{
	/// <summary>
	/// DONE
	/// </summary>
	public static class Consts
	{
		public const string COMMAND_DIALOG = "dialog";
		public const string COMMAND_HEADER = "header";
		public const string COMMAND_ASIDE = "aside";
		public const string COMMAND_SKIP_TO_THIS = "skiptothis";
		public const string COMMAND_MULTILINE = "multiline";
		public const string PARAM_IS_TUTORIAL = "is_tutorial";
		public const string PARAM_IS_SKIPPABLE = "is_skippable";
		public const string PARAM_IS_AUTOABLE = "is_autoable";
		public const string PARAM_IS_VIDEO_ONLY = "is_video_only";
		public const string PARAM_DENY_AUTO_SWITCH_SCENE = "deny_auto_switch_scene";
		public const string PARAM_DONT_CLEAR_GAMEOBJECTPOOL_ONSTART = "dont_clear_gameobjectpool_onstart";
		public const string PARAM_KEY = "key";
		public const string PARAM_FIT_MODE = "fit_mode";
		public const string PARAM_CHARACTER_SORT_TYPE = "char_sort_type";
		public const string EXECUTOR_TUTORIAL = "tutorial";
		public const string EXECUTOR_GOTOPAGE = "gotopage";
		public const string EXECUTOR_GOTO_CHARINFO = "gotocharinfo";
		public const string SIGNAL_ANY = "any";
		public const string KEY_BTN_FIRST_ZONESTAGE = "pool_btn_first_zonestage";
		public const string KEY_BTN_FIRST_ZONE = "pool_btn_first_zone";
		public const string KEY_BTN_SUBSTAGE_TRAIN_FORMAT = "pool_btn_train_substage#{0}";
		public const string KEY_BTN_ZONETAB_TRAINNING = "pool_btn_zonetab_training";
		public const string KEY_BTN_ZONETAB_WEEKLY_MTL = "pool_btn_zonetab_weekly_mtl";
		public const string KEY_BTN_ZONETAB_WEEKLY_EVOLVE = "pool_btn_zonetab_weekly_evolve";
		public const string KEY_BTN_ZONETAB_CAMPAIGN = "pool_btn_zonetab_campaign";
		public const string KEY_BTN_SQUAD_FIRST_EMPTY_SLOT = "pool_btn_squad_first_empty_slot";
		public const string KEY_BTN_SQUAD_SELECT_FIRST_ITEM = "pool_btn_squad_select_first_item";
		public const string KEY_BTN_RECRUIT_FIRST_EMPTY_SLOT = "pool_btn_recruit_first_empty_slot";
		public const string KEY_BTN_CHAR_REPO_FIRST_ITEM = "pool_btn_char_repo_first_item";
		public const string KEY_BTN_HANDBOOK_DEFAULT_ITEM = "pool_btn_handbook_default_item";
		public const string KEY_BTN_BUILDING_STATIONSELECT_FIRST_ITEM = "pool_btn_building_stationselect_first_item";
		public const string KEY_BTN_BUILDING_ROOM_STATION_FIRST_EMPTY_SLOT = "pool_btn_building_room_station_first_empty_slot";
		public const string KEY_BTN_BUILDING_MANUFACT_FORMULA_FIRST_ITEM = "pool_btn_building_manufact_formula_first_item";
		public const string KEY_BTN_BUILDING_WORKSHOP_FORMULA_FIRST_ITEM = "pool_btn_building_workshop_formula_first_item";
		public const string KEY_BTN_BUILDING_TRADING_FIRST_ORDER = "pool_btn_building_trading_first_order";
		public const string KEY_BTN_BUILDING_ASSIST_REPORT_FIRST_SLOT = "pool_btn_building_assist_report_first_slot";
		public const string KEY_BTN_BUILDING_ASSIST_REPORT_SECOND_SLOT = "pool_btn_building_assist_report_second_slot";
		public const string KEY_BTN_CAMPAIGN_ZONE = "pool_btn_campaign_zone";
		public const string KEY_BTN_CAMPAIGN_FIRST_ZONE_STAGE = "pool_btn_campaign_first_zone_stage";
		public const string KEY_BTN_INTERLOCK_FIRST_DEFEND = "pool_btn_interlock_first_defend";
		public const string KEY_BTN_INTERLOCK_FIRST_FINAL = "pool_btn_interlock_first_final";
		public const string CUSTTRIGGER_BUILDING_CLOSED = "building_closed";
		public const string KEY_ACT12SIDE_FIRST_CHARM_IN_LIST = "pool_act12side_first_charm_in_list";
		public const string KEY_ACT13SIDE_FIRST_DAILY_SLOT = "pool_first_daily_slot";
		public const string KEY_BTN_UNIQEQUIP_DETAIL = "pool_btn_equip_detail";
		public static readonly string[] COMMANDS_ALLOW_NO_EXECUTOR = new string[2] { "header", "skiptothis" };
		public const float DELAY_SINGLE_FRAME = 0.016666668f;
		public const string EXECUTOR_SHOP_SWITCH_TOP_TAB = "shop.switchtoptab";
		public const string SIGNAL_SHOP_TOPTAB_SWITCHED = "shop_toptab_switched";
		public const string EXECUTOR_CAMPAIGN_FOCUS_ZONE = "Campaign.FocusZone";
		public const string EXECUTOR_CAMPAIGN_REGISTER_ZONE_BTN = "Campaign.RegisterZoneBtn";
	}
}
