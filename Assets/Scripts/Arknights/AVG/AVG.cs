// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Runtime.InteropServices;
using RhodeIsland.Arknights.Resource;
using UnityEngine;
//using XLua;
using System.Collections.Generic;
namespace RhodeIsland.Arknights.AVG
{
	public class AVG : SingletonMonoBehaviour<AVG>
	{
		public void StartStory(string storyId, [Optional] Action<Story> onStoryEnd)
		{
			StartStory(storyId, onStoryEnd, new());
		}

		public void StartStory(string storyId, Action<Story> onStoryEnd, Story.StoryParam param)
		{
			if (AVGController.instance.isRunning)
            {
				StopStory();
            }
			_onStoryEndCB = onStoryEnd;
			TextAsset raw = _assetLoader.Load<TextAsset>(ResourceRouter.GetStoryPath(storyId));
			if (!raw)
            {
				Debug.LogError("[AVG] Failed to load story at path " + storyId);
				_OnStoryEnd(null);
            }
			else
            {
				param.overrideId = storyId;
				_StartStoryInternal(raw, param, onStoryEnd);
			}
		}

		public void StartStory(TextAsset storyTextAsset, Action<Story> onStoryEnd, Story.StoryParam param)
		{
			_StartStoryInternal(storyTextAsset, param, onStoryEnd);
		}

		public void StartStoryByString(string storyContent, [Optional] Action<Story> onStoryEnd)
		{
			_StartStoryInternalByString(storyContent, new(), onStoryEnd);
		}

		public void StopStory()
		{
		}

		public void InterruptStory()
		{
		}

		public void ReloadCommonData()
		{
			_variableConfig.TryLoadConfig(_assetLoader);
		}

		public AVGVariableConfig GetVariableConfig()
		{
			return _variableConfig;
		}

		//ADD
		public bool TryGetStory(string storyId, out Story story)
		{
			story = null;
			TextAsset raw = _assetLoader.Load<TextAsset>(ResourceRouter.GetStoryPath(storyId));
			if (!raw)
			{
				Debug.LogError("[AVG] Failed to load story at path " + storyId);
				return false;
			}
			AVGParser parser = new();
			if (_variableConfig.available)
			{
				parser.variableConverter = _variableConfig;
			}
			Story.StoryParam param = new();
			param.overrideId = storyId;
			parser.TryParse(raw.text, param, out story);
			return true;
		}

		private void _StartStoryInternal(TextAsset storyTextAsset, Story.StoryParam param, Action<Story> onStoryEnd)
		{
			_StartStoryInternalByString(storyTextAsset.text, param, onStoryEnd, storyTextAsset.name);
		}

		private void _StartStoryInternalByString(string storyContent, Story.StoryParam param, Action<Story> onStoryEnd, [Optional] string hintName)
		{
			bool isAuditMode = false; //MODIFY
			if (isAuditMode)
            {
				if (onStoryEnd != null)
                {
					onStoryEnd.Invoke(null);
                }
            }
			else
            {
				if (AVGController.instance.isRunning)
				{
					StopStory();
				}
				_onStoryEndCB = onStoryEnd;
				AVGParser parser = new();
				if (_variableConfig.available)
                {
					parser.variableConverter = _variableConfig;
                }
				/*int count = Environment.ProcessorCount;
				DateTime beforDT = DateTime.Now;
				int f = count;
				for (int i = 0; i < count; i++)
				{
					System.Threading.Thread childThread = new(() => 
					{ 
						for (int s = 0; s < 800 / count; s++) parser.TryParse(storyContent, param, out Story q);
						if (--f <= 0) 
							Debug.Log("Run " + count * (800 / count) + "times in " + DateTime.Now.Subtract(beforDT).TotalMilliseconds + "ms");
					});
					childThread.Start();
				}
				return;*/
				if (parser.TryParse(storyContent, param, out Story story))
                {
					foreach (string res in AVGController.instance.GatherStory(story))
                    {
						Debug.Log("<color=#66ccff>" + res + "</color>");
                    }
					AVGController.instance.RunStory(story);
                }
				else
                {
					Debug.LogError(string.Format("[AVG] Failed to load story {0}: {1}", hintName ?? "<UNKNOWN>", parser.GetErrorMessage()));
					_OnStoryEnd(null);
                }
			}
		}

		private void _OnStoryEnd(object arg)
		{
			_onStoryEndCB?.Invoke(arg as Story);
			_onStoryEndCB = null;
		}

		private void _OnStoryFailed(object arg)
		{
		}

		protected override void OnInit()
		{
			GameObject obj = (GameObject)Instantiate(_prefab);
			obj.transform.parent = transform;
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localScale = Vector3.one;
			obj.transform.localRotation = Quaternion.identity;
			_variableConfig.TryLoadConfig(_assetLoader);
			AVGController.instance.eventPool.On(AVGController.Event.ON_END_SUCCEED, _OnStoryEnd);
			AVGController.instance.eventPool.On(AVGController.Event.ON_END_FAILED, _OnStoryFailed);
		}

		[SerializeField]
		private UnityEngine.Object _prefab;

		private Action<Story> _onStoryEndCB;

		private DirectAssetLoader _assetLoader = new();

		private AVGVariableConfig _variableConfig = new();
	}
}
