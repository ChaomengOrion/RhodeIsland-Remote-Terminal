// Created by ChaomengOrion
// Create at 2022-07-27 15:01:41
// Last modified on 2022-07-27 18:56:19

using System;
using System.Collections.Generic;

namespace RhodeIsland.RemoteTerminal.Resources
{
	[Serializable]
	public class HandBookStoryViewData
	{
		[UnityEngine.Scripting.Preserve]
		public HandBookStoryViewData() { }

		public List<StoryText> stories;

		public string storyTitle;

		public bool unLockorNot;

		[Serializable]
		public class StoryText
		{
			[UnityEngine.Scripting.Preserve]
			public StoryText() { }

			/*public StoryText(string text, DataUnlockType unLockType, string unLockParam, string unLockString)
			{
			}*/
			
			public string storyText;

			//public DataUnlockType unLockType;

			public string unLockParam;

			public string unLockString;
		}
	}
}