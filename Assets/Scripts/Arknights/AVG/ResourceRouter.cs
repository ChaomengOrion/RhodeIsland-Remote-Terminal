// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-12 23:18:32

using System;

namespace RhodeIsland.Arknights.AVG
{
	public class ResourceRouter
	{
		public static string GetBackgroundPath(string key)
		{
			return FileUtil.Combine(BACKGROUND_FOLDER, key);
		}

		public static string GetImagePath(string key)
		{
			return FileUtil.Combine(IMAGE_FOLDER, key);
		}

		public static string GetCharacterPath(string key)
		{
			return FileUtil.Combine(CHARACTER_FOLDER, key);
		}

		public static string GetItemPath(string key)
		{
			return FileUtil.Combine(ITEM_FOLDER, key);
		}

		public static string GetAudioPath(string key)
		{
			return FileUtil.Combine(SOUND_FOLDER, key);
		}

		public static string GetMusicPath(string key)
		{
			return FileUtil.Combine(MUSIC_FOLDER, key);
		}

		public static string GetStoryPath(string key)
		{
			return FileUtil.Combine(STORY_FOLDER, key);
		}

		public static string GetStoryBriefPath(string key)
		{
			return FileUtil.Combine(STORY_FOLDER, string.Format("[UC]{0}", key));
		}

		public static string GetVariableFilePath()
		{
			return FileUtil.Combine(STORY_FOLDER, "story_variables");
		}

		public static string GetBattleEffectPath(string key)
        {
			return FileUtil.Combine(BATTLE_EFFECT_FOLDER, key);
		}

		private const string BACKGROUND_FOLDER = "AVG/Backgrounds";

		private const string IMAGE_FOLDER = "AVG/Images";

		private const string CHARACTER_FOLDER = "AVG/Characters";

		private const string ITEM_FOLDER = "AVG/Items";

		private const string SOUND_FOLDER = "Audio";

		private const string MUSIC_FOLDER = "Audio";

		private const string BATTLE_EFFECT_FOLDER = "AVG/Effects/Battle";

		public const string STORY_FOLDER = "GameData/Story";
	}
}
