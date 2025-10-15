// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
//using XLua;

namespace RhodeIsland.Arknights
{
	[Serializable]
	public class StoryData
	{
		public StoryData()
		{
		}
		public bool CheckNeedCommit()
		{
			return default(bool);
		}
		public bool CheckCommittedOrDontNeedCommit()
		{
			return default(bool);
		}
		public bool NeedTrig(StoryData.Trigger.TriggerType type, string key)
		{
			return default(bool);
		}
		public bool NeedTrigWithoutCheckTrigger(bool forceRepeatableAndIgnoreStageCond = false)
		{
			return default(bool);
		}
		public bool NeedTrigCanIgnoreStageCond(bool IgnoreStageCond = false)
		{
			return default(bool);
		}
		public string id;
		public bool needCommit;
		public bool repeatable;
		public bool disabled;
		public bool videoResource;
		public StoryData.Trigger trigger;
		public StoryData.Condition condition;
		public int setProgress;
		public string[] setFlags;
		public ItemBundle[] completedRewards;
		[JsonIgnore]
		public bool forceOmitCommit;
		[Serializable]
		public struct Trigger
		{
			public bool Check(StoryData.Trigger.TriggerType type, string key)
			{
				return default(bool);
			}
			public void Normalize()
			{
			}
			[JsonIgnore]
			public const int TRIGGER_TYPE_NUM = 11;
			public StoryData.Trigger.TriggerType type;
			public string key;
			public bool useRegex;
			[JsonIgnore]
			private Regex m_regex;
			public enum TriggerType
			{
				GAME_START,
				BEFORE_BATTLE,
				AFTER_BATTLE,
				SWITCH_TO_SCENE,
				PAGE_LOADED,
				STORY_FINISH,
				CUSTOM_OPERATION,
				STORY_FINISH_OR_PAGE_LOADED,
				ACTIVITY_LOADED,
				ACTIVITY_ANNOUNCE,
				CRISIS_SEASON_LOADED,
				E_NUM
			}
		}
		[Serializable]
		public class Condition
		{
			public Condition()
			{
			}
			//public bool Check(PlayerDataModel model, bool ignoreStageCond = false)
			//{
			//	return default(bool);
			//}
			public int minProgress;
			public int maxProgress;
			public int minPlayerLevel;
			public string[] requiredFlags;
			public string[] excludedFlags;
			public StoryData.Condition.StageCondition[] requiredStages;
			[Serializable]
			public class StageCondition
			{
				public StageCondition()
				{
				}
				//public StageCondition(string stageId, PlayerStageState minState, PlayerStageState maxState = PlayerStageState.COMPLETE)
				//{
				//}
				public string stageId;
				//public PlayerStageState minState;
				//public PlayerStageState maxState;
			}
		}
	}
}
