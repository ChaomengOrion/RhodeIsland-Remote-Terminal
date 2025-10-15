// Created by ChaomengOrion
// Create at 2022-05-01 10:32:06
// Last modified on 2022-07-27 18:56:25

using System;
using System.Collections.Generic;

namespace RhodeIsland.RemoteTerminal.Resources
{
	[Serializable]
	public class HandbookInfoData
	{
		[UnityEngine.Scripting.Preserve]
		public HandbookInfoData() { }

		/*public bool ShouldSerializeisLimited()
		{
			return default(bool);
		}*/

		public string charID;

		public string drawName;

		public string infoName;

		public bool isLimited;

		public HandBookStoryViewData[] storyTextAudio;

		public List<HandbookAvgGroupData> handbookAvgList;
	}
}