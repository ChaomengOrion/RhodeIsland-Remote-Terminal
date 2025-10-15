// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
//using Torappu.UI;
using UnityEngine;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	public class CommonExecutors : AVGComponent, /*IHotfixable,*/ IFadeTimeRatio, IContainsResRefs
	{
		public override IList<ICommandExecutor> GetCommandExecutors()
		{
			return new List<ICommandExecutor>
			{
				new CommandExecutorWrapper("Delay", _ExecuteDelayCommand, null, _ForceEndDelayCommand, null),
				//new CommandExecutorWrapper("Click", _ExecuteClickCommand, _ForceEndClickCommand, null),
				new CommandExecutorWrapper("PlaySound", _ExecutePlaySoundCommand, _GatherSoundCommand, null, null),
				new CommandExecutorWrapper("StopSound", _ExecuteStopSoundCommand, null, null, null),
				new CommandExecutorWrapper("SoundVolume", _ExecuteSoundVolumeCommand, null, null, null),
				new CommandExecutorWrapper("PlayMusic", _ExecutePlayMusicCommand, _GatherMusicCommand, null, null),
				new CommandExecutorWrapper("StopMusic", _ExecuteStopMusicCommand, null, null, null),
				new CommandExecutorWrapper("MusicVolume", _ExecuteMusicVolumeCommand, null, null, null),
				//new CommandExecutorWrapper("ConsumeGuideOnStoryEnd", _ExecuteConsumeGuideOnStoryEndCommand, null, null),
				//new CommandExecutorWrapper("StartBattle", _ExecuteStartBattleCommand, null, null),
				//new CommandExecutorWrapper("gotopage", _ExecuteGotoPageCommand, null, _SignalGotoPageReceiver),
				//new CommandExecutorWrapper("gotocharinfo", _ExecuteGotoCharInfoCommand, null, _SignalGotoCharInfoReceiver)
			};
		}

		public override void OnReset()
		{
			base.OnReset();
			_ResetAudio();
			m_onStoryEnd = null;
		}

		public override void OnStoryEnd(Story story)
		{
			m_onStoryEnd?.Invoke();
		}

		public float CalculateFadetime(float initialFadetime)
		{
			return initialFadetime * AVGController.instance.animateRatio;
		}

		public bool NeedSkipAnimation(float fadetime)
		{
			return default(bool);
		}

		private void _ExecuteDelayCommand(Command command, Action finishCb)
		{
			float delay = CalculateFadetime(Command.GetOrDefault("time", 0, command.TryGetParam));
			m_delayCoroutine = GameObjectUtil.InvokeAsync(this, finishCb, delay, true);
		}

		private void _ForceEndDelayCommand()
		{
			if (m_delayCoroutine != null)
            {
				StopCoroutine(m_delayCoroutine);
				m_delayCoroutine = null;
            }
		}

		private void _ExecuteClickCommand(Command command, Action finishCb)
		{
		}

		private void _ForceEndClickCommand()
		{
		}

		private void _ResetAudio()
		{
            for (int i = 0; i < m_loopSoundChannels.count; i++)
            {
				AudioManager.StopChannel(m_loopSoundChannels[i], _soundDefaultFadeTime);
            }
		}

		private void _ExecutePlaySoundCommand(Command command, Action finishCb)
		{
			string key = command.param.GetString("key", string.Empty);
			string channel = command.param.GetString("channel", key);
			float volume = command.param.GetFloat("volume", 1f);
			float delay = command.param.GetFloat("delay", 0f);
			bool loop = command.param.GetBool("loop", false);
			key = ResourceRouter.GetAudioPath(key);
			AudioManager.PlaySoundFx(key, volume, delay, loop, AudioManager.FXCategory.FX_UI, false, channel);
			finishCb.Invoke();
			if (loop)
            {
				if (!string.IsNullOrEmpty(channel))
                {
					m_loopSoundChannels.Add(channel);
                }
            }
		}

		private string _GatherSoundCommand(Command command)
		{
			string key = command.param.GetString("key", string.Empty);
			return ResourceRouter.GetAudioPath(key);
		}

		private void _ExecuteStopSoundCommand(Command command, Action finishCb)
		{
			string key = command.param.GetString("key", null);
			string channel = command.param.GetString("channel", key);
			float fadetime = command.param.GetFloat("fadetime", 0f);
			AudioManager.StopChannel(channel, fadetime);
			finishCb.Invoke();
			if (channel != null)
            {
				if (m_loopSoundChannels.Contains(channel))
                {
					m_loopSoundChannels.Remove(channel);
                }
            }
		}

		private void _ExecuteSoundVolumeCommand(Command command, Action finishCb)
		{
			string channel = command.param.GetString("channel", null);
			float fadetime = command.param.GetFloat("fadetime", 0f);
			float volume = command.param.GetFloat("volume", 1f);
			if (channel != null)
            {
				AudioManager.GetChannel(channel).TweenVolume(volume, fadetime, 0f);
            }
			finishCb.Invoke();
		}

		private void _ExecutePlayMusicCommand(Command command, Action finishCb)
		{
			string key = command.param.GetString("key", string.Empty);
			float volume = command.param.GetFloat("volume", 1f);
			float delay = command.param.GetFloat("delay", 0f);
			string intro = command.param.GetString("intro", string.Empty);
			float crossfade = command.param.GetFloat("crossfade", 0.4f);
			key = ResourceRouter.GetMusicPath(key);
			if (string.IsNullOrEmpty(intro))
            {
				AudioManager.PlayMusic(key, volume, crossfade, delay, "MUSIC");
            }
			else
            {
				intro = ResourceRouter.GetMusicPath(intro);
				AudioManager.PlayMusicWithIntro(intro, key, volume, crossfade, delay, "MUSIC");
            }
			finishCb.Invoke();
		}

		private string _GatherMusicCommand(Command command)
		{
			string key = command.param.GetString("key", string.Empty);
			return ResourceRouter.GetMusicPath(key);
		}

		private void _ExecuteStopMusicCommand(Command command, Action finishCb)
		{
			float fadetime = command.param.GetFloat("fadetime", 0f);
			AudioManager.StopMusic(fadetime);
			finishCb.Invoke();
		}

		private void _ExecuteMusicVolumeCommand(Command command, Action finishCb)
		{
			float fadetime = command.param.GetFloat("fadetime", 0f);
			float volume = command.param.GetFloat("volume", 1f);
			AudioManager.GetMusicChannel()?.TweenVolume(volume, fadetime, 0f);
			finishCb.Invoke();
		}

		private void _ExecuteConsumeGuideOnStoryEndCommand(Command command, Action finishCb)
		{
		}

		private static string _ExtractStrFromCommand(Command command, string paramName)
		{
			return null;
		}

		private void _ExecuteGotoPageCommand(Command command, Action finishCb)
		{
		}

		/*private bool _RouteToTarget(UIRouteTarget routeTarget, Command command)
		{
			return default(bool);
		}*/
		private void _SignalGotoPageReceiver(Command command)
		{
		}

		private void _ExecuteStartBattleCommand(Command command, Action finishCb)
		{
		}

		private void _InvokeStartBattle(string stageId, bool isPractice, Action onProceed, Action onBlock)
		{
		}

		/*private bool _TryGetRouteTargetFromGotoDest(AVGGotoPageDest destPage, out UIRouteTarget target)
		{
			return default(bool);
		}*/

		private void _ExecuteGotoCharInfoCommand(Command command, Action finishCb)
		{
		}

		/*private static UIPageStackParam _PageStackParamToCharInfo(object charArgs)
		{
			return default(UIPageStackParam);
		}*/

		private void _SignalGotoCharInfoReceiver(Command command)
		{
		}

		public virtual AbstractResRefCollecter DontInvoke_PlzImplInternalResRefCollector()
		{
			return null;
		}

		[SerializeField]
		private float _clickAutoDelay = 0.2f;
		[SerializeField]
		private float _soundDefaultFadeTime = 0.5f;
		private Action m_onStoryEnd;
		private ListSet<string> m_loopSoundChannels = new();
		private Coroutine m_delayCoroutine;
		private EventPool.EventCallbackDelegate m_onClickCallback;
		private Action m_gotoFinishCb;
		private string m_gotoWaitForSignal;
		private Action m_gotoCharInfoFinishCb;
		private string m_gotoCharInfoWaitForSignal;
		public class InternalSoundRefCollector : AbstractResRefCollecter
		{
			public InternalSoundRefCollector()
			{
			}
			public override void GatherResRefs(Command command, HashSet<string> references)
			{
			}
			public override bool useForResBan
			{
				get
				{
					return default(bool);
				}
			}
		}
		public class InternalMusicRefCollector : AbstractResRefCollecter
		{
			public InternalMusicRefCollector()
			{
			}
			public override void GatherResRefs(Command command, HashSet<string> references)
			{
			}
			public override bool useForResBan
			{
				get
				{
					return default(bool);
				}
			}
		}
	}
}