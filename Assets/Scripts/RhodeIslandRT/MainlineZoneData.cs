// Created by ChaomengOrion
// Create at 2022-07-17 13:52:16
// Last modified on 2022-07-26 23:58:30

using System;
//using System.Collections.Generic;

namespace RhodeIsland.RemoteTerminal
{
	[Serializable]
	public class MainlineZoneData
	{
		[UnityEngine.Scripting.Preserve]
		public MainlineZoneData() { }

		public string zoneId;

		public string chapterId;

		public string preposedZoneId;

		public int zoneIndex;

		public string startStageId;

		public string endStageId;

		public string mainlneBgName;

		public string recapId;

		public string recapPreStageId;

		public string buttonName;

		public ZoneReplayBtnType buttonStyle;

		public bool spoilAlert;

		public long zoneOpenTime;

		//public List<StageDiffGroup> diffGroup;

		public enum ZoneReplayBtnType
		{
			NONE,
			RECAP,
			REPLAY
		}
	}
}