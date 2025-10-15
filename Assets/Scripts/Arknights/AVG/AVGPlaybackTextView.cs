// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using RhodeIsland.Arknights.UI;
using UnityEngine;
using UnityEngine.UI;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGPlaybackTextView : MonoBehaviour//, IHotfixable
	{
		public AVGPlaybackTextView()
		{
		}

		private void _Render(AVGPlaybackTextView.Options options)
		{
		}

		private void _RenderDecision(string content, int optionIdx)
		{
		}

		private SizeCalculator _GetSizeCalculator()
		{
			return null;
		}

		private static string _ConvertDialogToDecision(string dialogContent, int optIndex)
		{
			return null;
		}

		[SerializeField]
		private Text _name;

		[SerializeField]
		private Text _content;

		[SerializeField]
		private GameObject _current;

		[SerializeField]
		private GameObject _options;

		[SerializeField]
		private Image[] _optionsChosen;

		[SerializeField]
		private float _contentPadding;

		private SizeCalculator m_sizeCalculator;

		public struct Options
		{
			public AVGPlaybackTextView prefab;

			public string dialogName;

			public string dialogContent;

			public bool isDecision;

			public int decisionIndex;

			public string decisionContent;

			public bool isCurrent;
		}

		public class VirtualView : UIRecycleLayoutAdapter.VirtualView<AVGPlaybackTextView>
		{
			public VirtualView(AVGPlaybackTextView.Options options)
			{
			}

			public override GameObject GetPrefab()
			{
				return null;
			}

			public override float GetPreferSize()
			{
				return default(float);
			}

			protected override void OnViewAttached()
			{
			}

			protected override void OnViewDetached()
			{
			}

			public void SetPlaybackOption(int optIndex)
			{
			}

			public void SetPlaybackCurrent(bool isCurrent)
			{
			}

			private Options m_options;
		}

		private class SizeCalculator// : IHotfixable
		{
			public SizeCalculator(AVGPlaybackTextView prefab)
			{
			}

			public float CalcSize(string name, string msg)
			{
				return default(float);
			}

			private TextGenerator m_textGenerator;

			private TextGenerationSettings m_nameSettings;

			private TextGenerationSettings m_msgSettings;

			private AVGPlaybackTextView m_prefab;
		}
	}
}
