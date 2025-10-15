// Created by ChaomengOrion
// Create at 2022-07-17 00:26:55
// Last modified on 2022-07-26 23:57:47

using System;

namespace RhodeIsland.RemoteTerminal
{
	[Serializable]
	public class ZoneData
	{
		[UnityEngine.Scripting.Preserve]
		public ZoneData() { }

		public string zoneID;

		public int zoneIndex;

		public ZoneType type;

		public string zoneNameFirst;

		public string zoneNameSecond;

		public string zoneNameTitleCurrent;

		public string zoneNameTitleUnCurrent;

		public string zoneNameTitleEx;

		public string zoneNameThird;

		public string lockedText;

		public bool canPreview;
	}
}