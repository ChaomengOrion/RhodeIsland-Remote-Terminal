// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
//using Colorful;
using UnityEngine;
//using XLua;
using DG.Tweening;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGCameraEffect : ExecutorComponent, IFadeTimeRatio
	{
		public override Dictionary<string, Executor> GetExecutors()
		{
			return new()
			{
				["cameraeffect"] = _ExecuteCameraEffect,
				["camerashake"] = _ExecuteCameraShake
			};
		}

		public override void OnReset()
		{
			base.OnReset();
			_sceneRoot.DOKill();
			_ResetCameraLocation();
			_ClearEffects();
		}

		private void _ClearEffects()
		{
            foreach (KeyValuePair<string, Component> item in _usedEffects)
            {
				Destroy(item.Value);
			}
			_usedEffects.Clear();
		}

		private void _ClearEffect(string effect)
		{
			if (_usedEffects.TryGetValue(effect, out Component component))
            {
				Destroy(component);
				_usedEffects.Remove(effect);
			}
		}

		private bool _ExecuteCameraEffect(Command command)
		{
			bool block = command.param.GetBool("block", false);
			string effect = command.param.GetString("effect", string.Empty);
			float fadetime = command.param.GetFloat("fadetime", _defaultFadetime);
			bool keep = command.param.GetBool("keep", false);
			if (effect == null || effect != "Grayscale")
            {
				return false;
            }
			return false;
			//float initamount = command.param.GetFloat("initamount", )
			return block;
		}

		private T _AddEffect<T>(string effect) where T : Component
		{
			T com;
			if (_usedEffects.ContainsKey(effect))
            {
				com = (T)_usedEffects[effect];
            }
            else
            {
				com = _sceneCamera.gameObject.AddComponent<T>();
				_usedEffects.Add(effect, com);
            }
			return com;
		}

		private void _ResetCameraLocation()
		{
			_sceneRoot.anchoredPosition = Vector2.zero;
			_sceneRoot.localPosition = Vector3.zero;
		}

		private bool _ExecuteCameraShake(Command command)
		{
			_sceneRoot.DOKill();
			if (command.param.GetBool("stop", false))
            {
				_ResetCameraLocation();
				return false;
            }
			else
            {
				float duration = command.param.GetFloat("duration", -1f);
				bool block = command.param.GetBool("block", false);
				float xstrength = command.param.GetFloat("xstrength", 1f);
				float ystrength = command.param.GetFloat("ystrength", 0f);
				int vibrato = command.param.GetInt("vibrato", 10);
				float randomness = command.param.GetFloat("randomness", 90f);
				bool fadeout = command.param.GetBool("fadeout", false);
				float fadetime = CalculateFadetime(duration);
				if (!NeedSkipAnimation(fadetime))
				{
					_sceneRoot.DOShakePosition(fadetime > 0 ? fadetime : 10f, new Vector2(xstrength, ystrength), vibrato, randomness, false, fadeout)
						.OnComplete(() =>
						{
							_sceneRoot.anchoredPosition = Vector2.zero;
							_sceneRoot.localPosition = Vector3.zero;
							FinishCommand();
						})
						.SetLoops(fadetime >= 0 ? 1 : -1)
						.SetIgnoreTimeScale(true)
						.Play();
					return block;
				}
			}
			return false;
		}

		protected override void ForceCommandEnd()
		{
			_sceneRoot.DOKill();
			_ResetCameraLocation();
		}

		public float CalculateFadetime(float initialFadetime)
		{
			return initialFadetime * AVGController.instance.animateRatio;
		}

		public bool NeedSkipAnimation(float fadetime)
		{
			return MathUtil.IsZero(fadetime);
		}

		[SerializeField]
		private Camera _sceneCamera;

		[SerializeField]
		private RectTransform _sceneRoot;

		[SerializeField]
		private float _defaultFadetime = 0.4f;

		private Dictionary<string, Component> _usedEffects = new();
	}
}