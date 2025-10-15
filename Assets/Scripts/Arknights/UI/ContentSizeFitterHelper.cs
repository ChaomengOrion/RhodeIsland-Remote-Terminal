// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using UnityEngine.EventSystems;

namespace RhodeIsland.Arknights.UI
{
	internal class ContentSizeFitterHelper : UIBehaviour
	{
		public ContentSizeFitterHelper()
		{
		}
		protected override void OnRectTransformDimensionsChange()
		{
		}
		[NonSerialized]
		public Action eSizeChanged;
	}
}
