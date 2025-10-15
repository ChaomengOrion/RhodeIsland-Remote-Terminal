// Created by ChaomengOrion
// Create at 2022-07-26 12:49:07
// Last modified on 2022-07-26 13:07:43

using System;
using UnityEngine;

namespace Torappu.UI
{
	public class UICharIllustAdditionOffset : MonoBehaviour
	{
		/*public void ApplySkinOffset()
		{
		}*/

		public Offset GetOffset() => _skinOffset;

		[SerializeField]
		[HideInInspector]
		private Offset _skinOffset;

		[Serializable]
		public struct Offset
		{
			public bool enablePos;

			public Vector2 position;

			public bool enableSize;

			public Vector2 size;
		}
	}
}
