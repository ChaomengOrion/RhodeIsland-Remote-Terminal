// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//using XLua;

namespace RhodeIsland.Arknights.Resource
{
	public static class ResPreferenceController
	{
		public static bool CheckUpdate(string resType)
		{
			return true;
		}
		public static void TryToSetUpdateEnable(string resType)
		{
		}
		public static void SetUpdateListEnable(IList<string> resTypeList)
		{
		}
		public static bool HaveShowPrefDialog()
		{
			return default(bool);
		}
		public static bool IsFullHotupdatePreference()
		{
			return default(bool);
		}
		public static bool IsTypeVersionUpgraded()
		{
			return default(bool);
		}
		public static void MarkHotupdatePreference(bool isFull)
		{
		}
		public static void StartHotUpdate(string resType)
		{
		}
		public static void StartHotUpdate()
		{
		}
		public static string GetVoiceTypeVersion()
		{
			return null;
		}
		public static void UpdateVoiceTypeVersion(string version)
		{
		}
		public static bool CheckIfResReadyForStartStage(string stageId, [Optional] Action nextStep)
		{
			return default(bool);
		}
		public static bool CheckIfResReadyForStartStory(IList<StoryData> storysToTrig, ResPreferenceController.CheckResOptions options)
		{
			return default(bool);
		}
		public static bool CheckIfResReadyForStartStory(IList<StoryData> storysToTrig, [Optional] Action nextStep)
		{
			return default(bool);
		}
		public static bool CheckIfResReadyForVoice(Action nextStep)
		{
			return default(bool);
		}
		public static bool CheckIfResReadyForSkinShop(Action nextStep)
		{
			return default(bool);
		}
		private static void _TryShowResUpdateAlert(IList<string> sharedTypes, ResPreferenceController.StepHandler stepHandler)
		{
		}
		private static void _OnResTypeAlertConfirmed(IList<string> resTypeList)
		{
		}
		/*private static bool _CheckIfShowResAlert(IList<string> types, bool ignoreAlertVersion, out IList<string> neededTypes)
		{
			return default(bool);
		}*/
		private static bool _CheckIfResAlertVersionDirty()
		{
			return default(bool);
		}
		private static void _ConfirmTypesAlertInfo(IList<string> types)
		{
		}
		private static string _GenerateTypeVersion()
		{
			return null;
		}
		private static string _GenerateTypeNameStr(IList<string> types)
		{
			return null;
		}
		private static ResPreferenceController.HotupdatePreferenceData _GetHotupdatePreference()
		{
			return null;
		}
		private static void _SetHotupdatePreference(ResPreferenceController.HotupdatePreferenceData preference)
		{
		}
		public const string TYPE_VIDEO = "video";
		public const string TYPE_DYN_ILLUST = "dyn_illust";
		public const string TYPE_VOICE_BASIC = "voice_basic";
		public static List<string> ALL_EXTRA_RES_TYPES;
		public static Dictionary<string, string> TYPE_NAME_DIC;
		private static List<string> s_sharedTypeList;
		private static List<StoryData> s_sharedStoryList;
		[Serializable]
		public class HotupdatePreferenceData
		{
			public HotupdatePreferenceData()
			{
			}
			public bool haveShowDialog;
			public bool isFull;
			public string typeVersion;
			public string voiceTypeVersion;
		}
		private class StepHandler
		{
			public StepHandler()
			{
			}
			public Action skipStep;
			public Action negativeStep;
			public bool ignoreAlertVersion;
		}
		public struct CheckResOptions
		{
			public Action nextStep;
			public bool ignoreAlertVersion;
		}
	}
}
