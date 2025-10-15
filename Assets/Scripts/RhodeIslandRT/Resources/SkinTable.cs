// Created by ChaomengOrion
// Create at 2022-07-26 16:13:23
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using RhodeIsland.Arknights;

namespace RhodeIsland.RemoteTerminal.Resources
{
    [Serializable]
    public class SkinTable
	{
		[UnityEngine.Scripting.Preserve]
		public SkinTable() { }

		public Dictionary<string, CharSkinData> charSkins;

		public Dictionary<string, ListDict<int, string>> buildinEvolveMap;

		public Dictionary<string, ListDict<string, string>> buildinPatchMap;

		public Dictionary<string, CharSkinBrandInfo> brandList;

		//public HashSet<SpecialSkinInfo> specialSkinInfoList;
	}
}