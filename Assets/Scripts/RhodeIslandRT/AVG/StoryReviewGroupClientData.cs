// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-07-26 23:59:58

using System.Collections.Generic;

namespace RhodeIsland.RemoteTerminal.AVG
{
	public class StoryReviewGroupClientData
	{
		[UnityEngine.Scripting.Preserve]
		public StoryReviewGroupClientData() { }

		public string id;

		public string name;

		public StoryReviewEntryType entryType;

		public StoryReviewType actType;

		public long startTime;

		public long endTime;

		public long startShowTime;

		public long endShowTime;

		public long remakeStartTime;

		public long remakeEndTime;

		public string storyEntryPicId;

		public string storyPicId;

		public string storyMainColor;

		//public string storyCompleteMedalId;

		//public ItemBundle[] rewards;

		public List<StoryReviewInfoClientData> infoUnlockDatas;
	}
}