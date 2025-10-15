// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Runtime.InteropServices;

namespace RhodeIsland.Arknights
{
	public static class FileUtil
	{
		public static string FullPathToAssetPath(string path)
		{
			return null;
		}

		public static string AssetPathToFullPath(string path)
		{
			return null;
		}

		public static string AssetPathToRuntimeResourcePath(string path, string fileExt = ".json")
		{
			return null;
		}

		public static string StripExtension(string path)
		{
			return null;
		}

		public static string StripPathPrefix(string path, string prefix)
		{
			return null;
		}
		public static string ReplaceExtension(string path, string newExt)
		{
			return null;
		}

		public static string ReplacePrefixWithExtension(string path, string prefix)
		{
			return null;
		}

		public static string ReplacePrefixButKeepExtension(string path, string prefix)
		{
			return null;
		}

		public static string OverwritePathPrefix(string path, string oldPrefix, string newPrefix)
		{
			return null;
		}

		public static void CreateFolderOrCleanupIfExists(string folderPath)
		{
		}

		public static bool CreateFolderIfNotExists(string folderPath)
		{
			return default(bool);
		}

		public static string Combine(string path1, string path2)
		{
			string combine = System.IO.Path.Combine(path1, path2);
			if (combine == null)
				return null;
			combine = combine.Replace('\\', '/');
			return combine.TrimEnd(new char[] { '/' });
		}

		public static bool CheckPathEqual(string path1, string path2)
		{
			return default(bool);
		}

		public static string Trim(string path)
		{
			return null;
		}

		public static void DeleteIfExists(string path)
		{
		}

		public static void DeleteWithMeta(string path)
		{
		}

		public static void DeleteFolderWithMeta(string path, bool recursive)
		{
		}

		public static void MoveWithMeta(string originPath, string newPath)
		{
		}

		public static void MoveAdvanced(string sourceFileName, string destFileName, bool overwrite)
		{
		}

		public static string GetPrettySizeString(long size)
		{
			return null;
		}

		public static string GetPrettySizeString(long size, string formatStr)
		{
			return null;
		}

		public static string GetPrettySizeStringUpToMB(long size, string formatStr)
		{
			return null;
		}

		public static string GetPrettySizeStringUpToMB(long size)
		{
			return null;
		}

		private static string _GetPrettySizeString(long size, string formatStr, int untilSuffixIndex)
		{
			return null;
		}

		public static string ReadFileContentOrNull(string path)
		{
			return null;
		}

		public static void TranverseDirectory(string path, Action<string> onFile, [Optional] Func<string, bool> onDir)
		{
		}

		public static void WriteToFile(string content, string path, bool useRetry = false)
		{
		}

		private static void _WriteToFileAction(string content, string path)
		{
		}

		private static void _RetryIO(Action opt)
		{
		}

		public static long GetFileInfoLength(string fullPath)
		{
			return default(long);
		}

		private static readonly string[] SIZE_SUFFIXS;
		private const string DEFAULT_PRETTY_SIZE_FORMAT_STR = "F2";
		private const int IO_RETRY_COUNT = 3;
	}
}
