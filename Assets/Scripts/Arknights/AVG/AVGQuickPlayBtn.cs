// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using UnityEngine;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGQuickPlayBtn : MonoBehaviour
	{
		public AVGQuickPlayBtn()
		{
		}
		public bool isSelected
		{
			get
			{
				return default(bool);
			}
			set
			{
			}
		}
		public void OnClick()
		{
		}
		[SerializeField]
		private GameObject _selected;
		[SerializeField]
		private AVGQuickPlay _quickPlayPanel;
		private bool m_isSelected;
	}
}
