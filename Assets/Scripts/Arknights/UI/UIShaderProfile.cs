// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-04-16 21:49:56

using System;
using UnityEngine;
//using XLua;

namespace Torappu.UI
{
	public class UIShaderProfile : MonoBehaviour//, IHotfixable
	{
		public Shader avgCharSplitShader
		{
			get
			{
				return _avgCharSplitShader;
			}
		}

		[SerializeField]
		private Shader _avgCharSplitShader;
	}
}