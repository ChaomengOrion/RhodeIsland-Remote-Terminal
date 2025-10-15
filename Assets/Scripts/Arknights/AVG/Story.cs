// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections.Generic;
using RhodeIsland.Arknights.UI;

namespace RhodeIsland.Arknights.AVG
{
	[Serializable]
	public class Story
	{
		public Story()
		{
		}

		public StoryOutPut Output
		{
			get
			{
				return default(StoryOutPut);
			}
			set
			{
			}
		}

		public string TryGetStrParam(string paramName)
		{
			return null;
		}

		public string OverrideId
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public const string STORY_PARAM_GOTO_CHARINFOID = "gotoCharInfoId";

		public string id;

		public string title;

		public bool isTutorial;

		public bool isSkippable;

		public bool isAutoable;

		public bool isVideoOnly;

		public bool denyAutoSwitchScene;

		public bool dontClearGameObjectPoolOnStart;

		public AVGController.FitMode fitMode;

		public CharacterSortType characterSortType;

		public StoryParam param;

		public List<Command> commands;

		private StoryOutPut m_outPut;

		public struct StoryParam
		{
			public bool isEmpty
			{
				get
				{
					return default(bool);
				}
			}

			public string overrideId;

			public Blackboard pool;
		}

		public struct StoryOutPut
		{
			public bool IsEmpty
			{
				get
				{
					return default(bool);
				}
			}

			public ItemBundle[] items;
		}
	}
}
