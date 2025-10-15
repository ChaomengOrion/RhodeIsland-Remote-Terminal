// Created by ChaomengOrion
// Create at 2022-05-08 13:18:39
// Last modified on 2022-05-08 13:26:14

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RhodeIsland.RemoteTerminal.Resources
{
	public class HotUpdateInfo
	{
		public ABInfo fullPack;

		public string versionId;

		public List<ABInfo> abInfos;

		public int countOfTypedRes;

		public List<ABInfo> packInfos;

		public struct ABInfo
		{
			[JsonIgnore]
			public bool isValid
			{
				get
				{
					return default(bool);
				}
			}

			public bool ShouldSerializepackId()
			{
				return default(bool);
			}

			[JsonIgnore]
			public static readonly ABInfo EMPTY;

			public string name;

			public string hash;

			public string md5;

			public long totalSize;

			public long abSize;

			public string type;

			public string packId;

			public int code;
		}
	}
}