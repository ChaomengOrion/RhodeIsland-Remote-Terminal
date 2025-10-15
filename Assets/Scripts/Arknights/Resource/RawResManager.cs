// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
//using XLua;

namespace RhodeIsland.Arknights.Resource
{
	public static class RawResManager
	{
		public static string GetABFullPath(string resPathWithExtension)
		{
			//MODIFY
			string abPath = FileUtil.Combine("raw", resPathWithExtension.ToLower());
			return RemoteTerminal.Resources.BundleRouter.GetRawPath(abPath);
		}

		public static bool CheckExist(string resPathWithExtension)
		{
			//MODIFY
			return ResourceManager.CheckExists(resPathWithExtension.ToLower());
		}

		public const string ROOT_DIR_PATH = "Assets/Torappu/RawAssets";

		public const string AB_DIR_NAME = "raw";

		public const string TAG_FILE_NAME = "[x]raw";
	}
}