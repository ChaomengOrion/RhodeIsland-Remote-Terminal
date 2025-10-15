// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
//using Torappu.Network;
using RhodeIsland.Arknights.Resource;
using RhodeIsland.Arknights.UI;
using UnityEngine;
//using XLua;
using Torappu.UI;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGController : SingletonMonoBehaviour<AVGController>, ISingletonNotAutoCreate
	{
		public UIShaderProfile avgUIShaderProfile;

		public string storyId
		{
			get
			{
				if (m_story != null)
                {
					return m_story.id;
                }
				return string.Empty;
			}
		}

		public AVGStoryCache storyCache
		{
			get
			{
				return m_storyCache;
			}
		}

		public bool toastQuickPlay
		{
			get
			{
				return default(bool);
			}
		}

		public bool isSkippable
		{
			get
			{
				return default(bool);
			}
		}

		[Sirenix.OdinInspector.ShowInInspector]
		public AVGStoryCache.AVGAutoMode autoPlayMode
		{
			get
			{
				return m_storyCache.autoPlayMode;
			}
			set
			{
				if (isRunning)
                {
					m_storyCache.autoPlayMode = value;
					if (value == AVGStoryCache.AVGAutoMode.QUICK_PLAY)
                    {
						eventPool.Emit(Event.ON_CLICK);
						_speedBtn.SetActive(false);
                    }
					else if (value == AVGStoryCache.AVGAutoMode.BUTTON_AUTO)
                    {
						_speedBtn.SetActive(true);
						_quickPlayPanel.SetStatus(false, 0);
						_ShowSpeedImage();
                    }
				}
			}
		}

		public float autoWaitBaseTime
		{
			get
			{
				if (m_storyCache.autoPlayMode == AVGStoryCache.AVGAutoMode.QUICK_PLAY)
                {
					return _quickAutoSpeed.SafeGet(0, _dialogDefaultSpeed).AutoWaitBaseTime;
                }
				else
                {
					if (m_storyCache.autoPlayMode != AVGStoryCache.AVGAutoMode.BUTTON_AUTO)
                    {
						return _dialogDefaultSpeed.AutoWaitBaseTime;
                    }
					return _btnAutoSpeed.SafeGet(0, _dialogDefaultSpeed).AutoWaitBaseTime;
				}
			}
		}

		public float autoWaitTimePerText
		{
			get
			{
				if (m_storyCache.autoPlayMode == AVGStoryCache.AVGAutoMode.QUICK_PLAY)
				{
					return _quickAutoSpeed.SafeGet(0, _dialogDefaultSpeed).AutoWaitTimePerText;
				}
				else
				{
					if (m_storyCache.autoPlayMode != AVGStoryCache.AVGAutoMode.BUTTON_AUTO)
					{
						return _dialogDefaultSpeed.AutoWaitTimePerText;
					}
					return _btnAutoSpeed.SafeGet(0, _dialogDefaultSpeed).AutoWaitTimePerText;
				}
			}
		}

		public float typeWriterDelay
		{
			get
			{
				if (m_storyCache.autoPlayMode == AVGStoryCache.AVGAutoMode.QUICK_PLAY)
				{
					return _quickAutoSpeed.SafeGet(0, _dialogDefaultSpeed).TypeWriterDelay;
				}
				else
				{
					if (m_storyCache.autoPlayMode != AVGStoryCache.AVGAutoMode.BUTTON_AUTO)
					{
						return _dialogDefaultSpeed.TypeWriterDelay;
					}
					return _btnAutoSpeed.SafeGet(0, _dialogDefaultSpeed).TypeWriterDelay;
				}
			}
		}

		public float animateRatio
		{
			get
			{
				if (m_storyCache.autoPlayMode == AVGStoryCache.AVGAutoMode.QUICK_PLAY)
				{
					return _quickAutoSpeed.SafeGet(0, _dialogDefaultSpeed).AnimateRatio;
				}
				else
				{
					if (m_storyCache.autoPlayMode != AVGStoryCache.AVGAutoMode.BUTTON_AUTO)
					{
						return _dialogDefaultSpeed.AnimateRatio;
					}
					return _btnAutoSpeed.SafeGet(0, _dialogDefaultSpeed).AnimateRatio;
				}
			}
		}

		public bool isTheaterMode
		{
			get
			{
				return m_storyCache.isTheaterMode;
			}
		}

		public bool isAutoClickRaised
		{
			get
			{
				return m_autoPlayingCoroutine != null;
			}
		}

		public bool isRunning
		{
			get
			{
				return m_story != null;
			}
		}

		public bool isRunningTutorial
		{
			get
			{
				return m_story != null && m_story.isTutorial;
			}
		}

		public int decisionIndex
		{
			get
			{
				return default(int);
			}
			set
			{
			}
		}

		public Story.StoryParam storyParam
		{
			get
			{
				return default(Story.StoryParam);
			}
		}

		public void SetCommandPredicator(ICommandPredicator pred)
		{
			m_commandPredicator = pred;
		}

		public void SetCommandFlowController(ICommandFlowController controller)
		{
			m_commandFlowController = controller;
		}

		public void SetCommandSkipController(ICommandSkipController controller)
		{
			m_commandSkipController = controller;
		}

		public AbstractAssetLoader assetLoader
		{
			get
			{
				return m_assetLoader;
			}
		}

		public EventPool<Event> eventPool
		{
			get
			{
				return m_eventPool;
			}
		}

		public ResourceRouter router
		{
			get
			{
				return m_router;
			}
		}

		public void RunStory(Story story)
		{
			if (story != null && !isRunning)
            {
				m_story = null;
				m_commandPredicator = null;
				if (m_commandFlowController != null)
                {
					m_commandFlowController.Reset();
				}
				if (m_commandSkipController != null)
				{
					m_commandSkipController.Reset();
				}
				_avgSceneCamera.SetEnabledIfNecessary(false);
				_avgUICamera.SetEnabledIfNecessary(false);
				gameObject.SetActive(true);
				m_story = story;
				m_assetLoader.ClearAll();
				m_executeIndex = 0;
				m_skipToIndex = -1;
				m_decisionValue = -1;
				m_needResumeAuto = false;
				_ResetCache();
				m_storyCache.Init(AVGUtils.CheckFirstRead(storyId));
				_SetTheaterModeStatus();
				OnReset();
				OnStoryBegin(story);
				if (ResPreferenceController.CheckUpdate("video") || !m_story.isVideoOnly)
                {
					StartCoroutine(DoExecuteCommands());
                }
				else
                {
					DoEndStory();
                }
			}
		}

		public HashSet<string> GatherStory(Story story)
        {
			HashSet<string> resources = new();
			foreach (Command command in story.commands)
            {
				HashSet<ICommandExecutor> executors = _GetCommandExecutors(command.command);
				if (executors != null)
                {
					foreach (ICommandExecutor executor in executors)
					{
						string res = executor.Gather(command);
						if (!string.IsNullOrEmpty(res))
						{
							if (command.command.ToLower() == "character" && res.Contains('|'))
							{
								foreach (string s in res.Split('|', StringSplitOptions.RemoveEmptyEntries))
								{
									if (!resources.Contains(s))
									{
										resources.Add(s);
									}
								}
							}
							else if (!resources.Contains(res))
							{
								resources.Add(res);
							}
						}
					}
				}
            }
			return resources;
        }

		public void StopStory(string errorMsg, bool isInterrupt = false)
		{
			if (isRunning)
            {
				if (m_coroutine != null)
                {
					StopCoroutine(m_coroutine);
					m_coroutine = null;
                }
				EndStory(errorMsg, isInterrupt);
            }
		}

		protected void SkipStory()
		{
			if (m_skipToIndex < 0)
            {
				if (m_storyCache.SkipNodeNum <= 0)
                {
					StopStory("Skipped", false);
                }
				else
                {
					//TODO
                }
            }
		}

		protected void EndStory(string errorMsg, bool isInterrupt = false)
		{
			m_coroutine = null;
			m_autoPlayingCoroutine = null;
			m_commandPredicator = null;
			_ResetCache();
			_StopAutoClick();
			//MODIFY
			UnityEngine.Debug.Log("[AVG] Story is about to end with status: " + errorMsg ?? "Succeed");
			if (isRunning)
			{
				m_story = null;
				gameObject.SetActive(false);
				m_assetLoader.ClearAll();
				OnStoryEnd(true, m_story);
			}
			else
            {
				UnityEngine.Debug.LogError("[AVG] About to finish a story which is already finished!");
            }
		}

		//protected void OnStoryCommitted(bool isOk, Story.StoryOutPut outPut)

		public void SetQuickSpeed(int speed)
		{
		}

		public void RaiseAutoClick(int messageLength)
		{
			RaiseAutoClick(messageLength * autoWaitTimePerText + autoWaitBaseTime);
		}

		public void RaiseAutoClick(float delay)
		{
			if (gameObject.activeInHierarchy)
            {
				if (m_autoPlayingCoroutine != null)
                {
					StopCoroutine(m_autoPlayingCoroutine);
                }
				m_autoPlayingCoroutine = StartCoroutine(DoAutoClick(delay));
            }
		}

		public void RaiseSignal(string command, string signal = "any")
		{
		}

		public void RaiseSignal(Command command)
		{
		}

		public void RegisterExecutor(ICommandExecutor executor)
		{
			string key = executor.command.ToLower();
			if (!m_commandExecutorsMap.TryGetValue(key, out HashSet<ICommandExecutor> executors))
            {
				executors = new HashSet<ICommandExecutor>();
				m_commandExecutorsMap.Add(key, executors);
			}
			executors.Add(executor);
		}

		public void UnregisterExecutor(ICommandExecutor executor)
		{
			if (m_commandExecutorsMap.TryGetValue(executor.command.ToLower(), out HashSet<ICommandExecutor> executors))
			{
				executors.Remove(executor);
			}
		}

		public void RegisterComponent(AVGComponent component)
		{
			foreach (ICommandExecutor executor in component.GetCommandExecutors())
            {
				RegisterExecutor(executor);
            }
			component.SetController(this);
		}

		public void UnregisterComponent(AVGComponent component)
		{
		}

		public void RegisterExtraGameObject(string name, GameObject go)
		{
		}

		public void UnregisterExtraGameObject(string name)
		{
		}

		public GameObject GetExtraGameObject(string name)
		{
			return null;
		}

		public TComponent GetExtraGameObject<TComponent>(string name)
		{
			return default;
		}

		public bool TryGetCharSortType(out CharacterSortType charSortType)
		{
			charSortType = CharacterSortType.BY_DEF_UP;
			return default(bool);
		}

		private void _TryShowQuickPlayTip()
		{
		}

		[DebuggerHidden]
		protected IEnumerator DoExecuteCommands()
		{
			Command command = null;
			HashSet<ICommandExecutor> executors = null;
			bool isUIInputCommand = false;
			_TryShowQuickPlayTip();
			_avgSceneCamera.SetEnabledIfNecessary(true);
			_avgUICamera.SetEnabledIfNecessary(true);
			if (m_story.commands != null)
            {
				for (; m_executeIndex < m_story.commands.Count; ++m_executeIndex)
				{
					if (m_commandFlowController != null)
					{
						int index = m_commandFlowController.GotoCommandIndex();
						if (index >= 0)
						{
							m_executeIndex = index;
						}
					}
					command = m_story.commands[m_executeIndex];
					executors = _GetCommandExecutors(command.command);
					if (m_commandPredicator != null && !m_commandPredicator.NeedToExecuteCommand(command))
					{
						continue;
					}

					isUIInputCommand = false;
					m_blockingExecutors.Clear();
					if (executors.IsNullOrEmpty())
					{
						if (!command.command.ContainsAny(Consts.COMMANDS_ALLOW_NO_EXECUTOR))
						{
							UnityEngine.Debug.LogWarning("[AVG] There is NO executors for command: " + command.command);
						}
					}
					else
					{
						m_blockingExecutors.AddRange(executors);
						UnityEngine.Debug.Log(string.Format("[AVG] Run command {0} with {1} executors...", command.command, executors.Count));
						foreach (ICommandExecutor executor in executors)
						{
							if (executor as IUIInputCommand != null)
							{
								isUIInputCommand = true;
							}
							executor.Execute(command, OnCommandExecuted);
						}
						if (isUIInputCommand)
						{
							_OnUIInputCommandEnter();
						}
					}
					while (m_blockingExecutors.Count > 0)
					{
						yield return null;
					}
					if (isUIInputCommand)
					{
						_OnUIInputCommandExit();
					}
				}
				_PreprocessCommands(m_story.commands);
            }
			yield return new WaitForSecondsRealtime(FINISH_STORY_DELAY);
			DoEndStory();
		}

		protected void DoEndStory()
		{
			EndStory(null, false);
			ResourceManager.UnloadUnusedAssets();
		}

		[DebuggerHidden]
		protected IEnumerator DoAutoClick(float delay)
		{
			yield return new WaitUntil(() => autoPlayMode != AVGStoryCache.AVGAutoMode.DEFAULT);
			yield return new WaitForSecondsRealtime(delay);
			yield return new WaitUntil(() => autoPlayMode != AVGStoryCache.AVGAutoMode.DEFAULT);
			eventPool.Emit(Event.ON_CLICK, null);
			m_autoPlayingCoroutine = null;
		}

		private HashSet<ICommandExecutor> _GetCommandExecutors(string command)
		{
			m_commandExecutorsMap.TryGetValue(command.ToLower(), out HashSet<ICommandExecutor> executors);
			return executors;
		}

		private void _PreprocessCommands(IList<Command> commands)
		{
			if (commands.Count > 0)
            {
                for (int i = 0; i < commands.Count; i++)
                {
					if (commands[i].command == "skiptothis")
					{
						m_skipToIndex = i;
						break;
					}
				}
            }
			if (m_commandFlowController != null)
            {
				m_commandFlowController.PreprocessCommands(m_story.commands);
			}
			if (m_commandSkipController != null)
			{
				m_commandSkipController.PreprocessCommands(m_story.commands);
			}
		}

		private void _InitExecutors()
		{
			m_commandExecutorsMap.Clear();
			foreach (AVGComponent component in m_components)
            {
				RegisterComponent(component);
            }
		}

		private void _StopAutoClick()
		{
		}

		private void _ResetCache()
		{
			m_storyCache = AVGStoryCache.RESET;
		}

		private void _CacheOriginSizeDeltaOfFitTargetsIfNot()
		{
		}

		private void _SetupFitMode(FitMode fitMode)
		{
			_CacheOriginSizeDeltaOfFitTargetsIfNot();
			if (fitMode == FitMode.BLACK_MASK)
            {
                for (int i = 0; i < _fitTargets.Length; i++)
                {
					RectTransform rectTransform = _fitTargets[i];
					rectTransform.anchorMin = Vector2.one * 0.5f;
					rectTransform.anchorMax = Vector2.one * 0.5f;
					rectTransform.sizeDelta = m_originSizeDeltaOfFitTargets[i];
                }
				_cullMask.gameObject.SetActiveIfNecessary(true);
            }
			else
            {
				_cullMask.gameObject.SetActiveIfNecessary(false);
				for (int i = 0; i < _fitTargets.Length; i++)
				{
					RectTransform rectTransform = _fitTargets[i];
					rectTransform.anchorMin = Vector2.one;
					rectTransform.anchorMax = Vector2.one;
					rectTransform.sizeDelta = Vector2.zero;
				}
			}
		}

		protected void OnCommandExecuted(ICommandExecutor executor)
		{
			m_blockingExecutors.Remove(executor);
			UnityEngine.Debug.Log("[AVG] Completed command " + executor.command);
		}

		protected void OnStoryBegin(Story story)
		{
			//MODIFY
			/*_skipBtn.SetActiveIfNecessary(story.isSkippable);
			_autoBtn.gameObject.SetActiveIfNecessary(story.isAutoable);
			_playbackBtn.SetActiveIfNecessary(!story.isTutorial);
			_hideuiBtn.SetActiveIfNecessary(!story.isTutorial);*/
			//TODO
			_SetupFitMode(story.fitMode);
			eventPool.Emit(Event.ON_BEGIN);
            for (int i = 0; i < m_components.Length; i++)
            {
				m_components[i].OnStoryBegin(story);
            }
			UnityEngine.Debug.Log("[AVG] Run story: " + m_story.id);
		}

		protected void OnStoryEnd(bool isCommitOk, Story story)
		{
			foreach (AVGComponent component in m_components)
			{
				component.OnStoryEnd(story);
			}
			eventPool.Emit(Event.ON_END_SUCCEED);
			foreach (AVGComponent component in m_components)
			{
				component.OnReset();
			}
			m_gameObjectPool.Clear();
		}

		protected void OnReset()
		{
			_DoCacheOriginActiveStates();
			eventPool.Emit(Event.ON_RESET);
			//_briefPanel.Reset();
			foreach (AVGComponent component in m_components)
            {
				component.OnReset();
            }
			if (m_story == null || !m_story.dontClearGameObjectPoolOnStart)
            {
				m_gameObjectPool.Clear();
            }
		}

		public void OnSkipBtnClicked()
		{
			_PauseAuto();
			//MODIFY
			RemoteTerminal.UI.FrontPanelManager.instance.PopUpBinaryPanel("是否确认跳过本节剧情？", result =>
			{
				if (result)
				{
					SkipStory();
				}
				else
				{
					ResumeAuto();
				}
			});
		}

		public void OnAutoBtnClicked()
		{
		}

		public void OnSpeedBtnClicked()
		{
		}

		private void _ShowSpeedImage()
		{
		}

		public void OnPlaybackBtnClicked()
		{
		}

		public void OnClickPress()
		{
			if (!isTheaterMode)
            {
				//TODO
				//QuickPlay
				eventPool.Emit(Event.ON_CLICK);
            }
		}

		/*private UINotification.Options<QuickPlayKnownNotifyView, QuickPlayKnownNotifyView.Param> _CreateQuickPlayNotifyOptions(float duration, bool isShowBtn)
		{
			return default(UINotification.Options<QuickPlayKnownNotifyView, QuickPlayKnownNotifyView.Param>);
		}*/

		public void OnLongPress(Vector2 pos)
		{
		}

		public void SetTheaterMode(bool value)
		{
		}

		private void _SetTheaterModeStatus()
		{
		}

		private void _SaveAutoStatus()
		{
		}

		private void _OnUIInputCommandEnter()
		{
		}

		private void _OnUIInputCommandExit()
		{
		}

		private void _LoadAutoStatus()
		{
		}

		public void UpdateStorySkipMode(SkipNodeLabel node)
		{
		}

		private void _UpdateSkipStatus()
		{
		}

		private void _ShowBriefPanel(string storyId)
		{
		}

		private void _PauseAuto()
		{
		}

		public void ResumeAuto()
		{
		}

		public void OnHideuiBtnClicked()
		{
		}

		public void OnHideuiResumeClicked()
		{
		}

		private void _DoCacheOriginActiveStates()
		{
		}

		protected override void OnInit()
		{
			base.OnInit();
			_avgSceneCamera.depth = SCENECAMERA_DEPTH;
			_avgUICamera.depth = UICAMERA_DEPTH;
			m_components = GetComponentsInChildren<AVGComponent>(true);
			_InitExecutors();
			gameObject.SetActive(false);
			_clickBtn.onClickAction = OnClickPress;
			_clickBtn.onLongPressAction = OnLongPress;
			_clickBtn.onDragAction = _quickPlayPanel.OnDragAction;
			RectTransform rect = null;
			if (_skipBtn.transform != null)
            {
				rect = (RectTransform)_skipBtn.transform;
            }
			//TODO
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public static void TryFetchAndAddCameras(List<Camera> cameras)
		{
		}

		private const int SCENECAMERA_DEPTH = 50;

		private const int UICAMERA_DEPTH = 51;

		private const float FINISH_STORY_DELAY = 0.2f;

		[SerializeField]
		private Camera _avgSceneCamera;

		[SerializeField]
		private Camera _avgUICamera;

		[SerializeField]
		private GameObject _skipBtn;

		[SerializeField]
		private AVGAutoButton _autoBtn;

		[SerializeField]
		private GameObject _speedBtn;

		[SerializeField]
		private GameObject _playbackBtn;

		[SerializeField]
		private GameObject _hideuiBtn;

		[SerializeField]
		private PlaybackPanel _playbackPanel;

		[SerializeField]
		private GameObject _hideUiMask;

		[SerializeField]
		private GameObject[] _hideObjects;

		[SerializeField]
		private GameObject _dialogPanel;

		[SerializeField]
		private SkipBriefPanel _briefPanel;

		[SerializeField]
		private AVGButton _clickBtn;

		[SerializeField]
		private AutoSpeed _dialogDefaultSpeed;

		[SerializeField]
		private AutoSpeed[] _btnAutoSpeed = new AutoSpeed[0];

		[SerializeField]
		private AutoSpeed[] _quickAutoSpeed = new AutoSpeed[0];

		[SerializeField]
		private RectTransform _cullMask;

		[SerializeField]
		private RectTransform[] _fitTargets = new RectTransform[0];

		[SerializeField]
		private AVGQuickPlay _quickPlayPanel;

		[SerializeField]
		private int _toastHoldOnInterval = 50;

		[SerializeField]
		private int _clickToastHoldOnInterval;

		[SerializeField]
		private int _tipClickTimes;

		[SerializeField]
		private int _tipPerClickInterval;

		[SerializeField]
		private int _tipClickTotalInterval = 10;

		//[SerializeField]
		//private QuickPlayKnownNotifyView _quickPlayKnownNotifyPrefab;

		private Vector2[] m_originSizeDeltaOfFitTargets;

		private int m_skipToIndex = -1;

		private AVGComponent[] m_components;

		private EventPool<Event> m_eventPool = new();

		private ResourceRouter m_router = new();

		[Sirenix.OdinInspector.ShowInInspector]
		private Story m_story;

		[Sirenix.OdinInspector.ShowInInspector]
		private Dictionary<string, HashSet<ICommandExecutor>> m_commandExecutorsMap = new();

		[Sirenix.OdinInspector.ShowInInspector]
		private int m_executeIndex;

		[Sirenix.OdinInspector.ShowInInspector]
		private List<ICommandExecutor> m_blockingExecutors = new();

		private Coroutine m_coroutine;

		private Coroutine m_autoPlayingCoroutine;

		private AVGAssetLoader m_assetLoader = new();

		private int m_decisionValue;

		private bool m_needResumeAuto;

		private bool m_needResumeAutoStatus;

		public AVGStoryCache.AVGAutoMode autoPlayModeCache;

		public int btnAutoModeCache;

		private Dictionary<string, GameObject> m_gameObjectPool = new();

		private bool[] m_prevActiveStates;

		private bool[] m_originActiveStates;

		private AVGStoryCache m_storyCache;

		private ICommandPredicator m_commandPredicator;

		private ICommandFlowController m_commandFlowController;

		private ICommandSkipController m_commandSkipController;

		public enum FitMode
		{
			DEFAULT,
			BLACK_MASK
		}

		public enum Event
		{
			ON_BEGIN,
			ON_END_SUCCEED,
			ON_END_FAILED,
			ON_RESET,
			ON_CLICK,
			ON_SPEED_SET,
			ON_PRE_END
		}

		public interface ICommandPredicator
		{
			bool NeedToExecuteCommand(Command command);
		}

		public interface ICommandFlowController
		{
			void Reset();

			void PreprocessCommands(List<Command> commands);

			int GotoCommandIndex();
		}

		public interface ICommandSkipController
		{
			void Reset();

			void PreprocessCommands(List<Command> commands);
		}
    }
}
