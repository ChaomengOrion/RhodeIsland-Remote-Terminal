// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using UnityEngine;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	public class BackgroundPanel : AVGImagePanel, IContainsResRefs
	{
		public override Dictionary<string, Executor> GetExecutors()
		{
			return new()
            {
				["background"] = _ExecuteImage,
				["backgroundtween"] = _ExecuteImageTween
            };
		}

		public override Dictionary<string, Gather> GetGathers()
		{
			return new()
			{
				["background"] = _GatherImage,
			};
		}

		public override AbstractResRefCollecter DontInvoke_PlzImplInternalResRefCollector()
		{
			throw new NotImplementedException();
		}

		protected override Sprite _LoadSprite(string key)
		{
			string path = ResourceRouter.GetBackgroundPath(key);
			return assetLoader.Load<Sprite>(path);
		}

		private class InternalResRefCollector : AbstractResRefCollecter
		{
			public InternalResRefCollector()
			{
			}
			public override void GatherResRefs(Command command, HashSet<string> references)
			{
			}
		}
	}
}
