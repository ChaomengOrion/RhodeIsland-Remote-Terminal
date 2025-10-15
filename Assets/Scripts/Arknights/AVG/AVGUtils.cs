// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Runtime.InteropServices;
using UnityEngine;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	public static class AVGUtils
	{
		public static void SwapUnderSameParent(Transform lhs, Transform rhs)
		{
			int lhsIndex = lhs.transform.GetSiblingIndex();
			int rhsIndex = rhs.transform.GetSiblingIndex();
			if (lhsIndex <= rhsIndex)
            {
				rhs.transform.SetSiblingIndex(lhsIndex);
				lhs.transform.SetSiblingIndex(rhsIndex);
            }
			else
            {
				rhs.transform.SetSiblingIndex(rhsIndex);
				lhs.transform.SetSiblingIndex(lhsIndex);
			}
		}

		public static bool TrigCustomOperation(string operation)
		{
			return default(bool);
		}
		public static bool TrigCustomOperation(string operation, Action<Story> onCompleted)
		{
			return default(bool);
		}
		public static bool TriggerCustomOperation(string operation, Action<Story> onCompleted, Story.StoryParam param)
		{
			return default(bool);
		}
		/*public static bool FetchCustomOperationStory(string operation, out StoryData storyData)
		{
			return default(bool);
		}*/
		/*public static bool TrigFirstStoryOnPageLoaded(AVGPageKey avgPage, bool forceRepeatableAndOmitCommit = false)
		{
			return default(bool);
		}*/
		public static bool TrigFirstStoryOnActivityLoaded(string activityId, [Optional] Action<Story> onCompleted)
		{
			return default(bool);
		}
		public static bool TrigFirstVideoStoryOnActivityLoaded(string activityId, [Optional] Action<Story> onCompleted, bool forceRepeatable = false)
		{
			return default(bool);
		}
		/*public static bool TrigHandBookAvg(string storyId, GameFlowController.Options returnOptions)
		{
			return default(bool);
		}*/
		public static bool TrigCrisisSeasonLoaded(string seasonId, [Optional] Action<Story> onCompleted)
		{
			return default(bool);
		}
		public static bool TrigRoguelikeTopicLoaded(string trigger, [Optional] Action<Story> onCompleted)
		{
			return default(bool);
		}
		/*public static bool TrigStoryInStandaloneScene(StoryData storyToTrig, GameFlowController.Options returnOptions)
		{
			return default(bool);
		}*/
		/*public static void FetchBattleStory(string stageId, out StoryData before, out StoryData after, bool forceRepeatableAndOmitCommitAndStageCond = true)
		{
			before = default;
			after = default;
		}*/
		public static void FetchActivityAnnounceStory(string activityId, out string storyId)
		{
			storyId = default;
		}

		//MODIFY
		public static bool CheckFirstRead(string storyID)
		{
			return true;
		}
		public static bool CheckMarkStoryAcceKnown()
		{
			return default(bool);
		}
		public static bool CanQuickPlay(Story story)
		{
			return default(bool);
		}
		public static string GetStoryIDFromPath(string path)
		{
			return null;
		}
	}
}