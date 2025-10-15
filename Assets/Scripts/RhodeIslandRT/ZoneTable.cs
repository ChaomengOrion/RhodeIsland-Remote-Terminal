// Created by ChaomengOrion
// Create at 2022-07-17 13:50:13
// Last modified on 2022-07-26 23:57:38

using System;
using System.Collections.Generic;

namespace RhodeIsland.RemoteTerminal
{
	[Serializable]
	public class ZoneTable
	{
		[UnityEngine.Scripting.Preserve]
		public ZoneTable() { }

		public Dictionary<string, ZoneData> zones;

		//public ListDict<string, WeeklyZoneData> weeklyAdditionInfo;

		//public Dictionary<string, ZoneValidInfo> zoneValidInfo;

		public Dictionary<string, MainlineZoneData> mainlineAdditionInfo;

		//public Dictionary<string, ZoneRecordGroupData> zoneRecordGroupedData;

		//public Dictionary<string, List<string>> zoneRecordRewardData;
	}
}