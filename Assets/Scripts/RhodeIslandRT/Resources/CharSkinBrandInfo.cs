// Created by ChaomengOrion
// Create at 2022-07-26 16:27:31
// Last modified on 2022-07-27 00:00:38

using System;
using System.Collections.Generic;

namespace RhodeIsland.RemoteTerminal.Resources
{
	[Serializable]
	public class CharSkinBrandInfo
	{
		[UnityEngine.Scripting.Preserve]
		public CharSkinBrandInfo() { }

		public string brandId;

		public List<string> groupList;

		public List<string> kvImgIdList;

		public string brandName;

		public string brandCapitalName;

		public string description;

		public int sortId;
	}
}
