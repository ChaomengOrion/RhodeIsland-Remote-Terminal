// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using RhodeIsland.Arknights.AVG;
//using XLua;

namespace RhodeIsland.Arknights
{
	//MODIFY
	public class AVGTextManager : Singleton<AVGTextManager>
	{
		[UnityEngine.Scripting.Preserve]
		public AVGTextManager() { }

		public string Translate(string content)
		{
			return content;
		}

		private IAVGTextTranslater m_translater;
	}
}
