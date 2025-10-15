// Created by ChaomengOrion
// Create at 2022-05-01 10:33:22
// Last modified on 2022-07-27 15:00:07

using System.Collections.Generic;

namespace RhodeIsland.RemoteTerminal.Resources
{
	public class HandbookAvgGroupData
	{
		[UnityEngine.Scripting.Preserve]
		public HandbookAvgGroupData() { }

		public string storySetId;

		public string storySetName;

		public int sortId;

		public long storyGetTime;

		//public List<ItemBundle> rewardItem;

		//public List<HandbookUnlockParam> unlockParam;

		public List<HandbookAvgData> avgList;

		public string charId;
	}
}