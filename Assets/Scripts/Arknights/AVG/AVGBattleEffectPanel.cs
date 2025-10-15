// Created by ChaomengOrion
// Create at 2022-08-12 22:58:11
// Last modified on 2022-08-13 13:18:12

using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using XLua;
using RhodeIsland.Arknights.Resource;
using Torappu.Battle.Effects;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGBattleEffectPanel : ExecutorComponent, IContainsResRefs
	{
		public override Dictionary<string, Executor> GetExecutors()
		{
			return new()
            {
				["effect"] = _ExecuteEffect
            };
		}

		public override void OnReset()
		{
			base.OnReset();
			_ClearAllSeq();
			_effectContainer.ClearAllChildren();
		}

		private void _ClearAllSeq()
		{
            foreach (Sequence item in m_sequences)
            {
				item.Kill();
			}
		}

		protected bool _ExecuteEffect(Command command)
		{
			string name = Command.GetOrDefault("name", string.Empty, command.TryGetParam);
			int layer = Command.GetOrDefault("layer", 0, command.TryGetParam);
			float delay = Command.GetOrDefault("delay", 0f , command.TryGetParam);
			float x = Command.GetOrDefault("x", 0f, command.TryGetParam);
			float y = Command.GetOrDefault("y", 0f, command.TryGetParam);
			float rox = Command.GetOrDefault("rox", 0f, command.TryGetParam);
			float roy = Command.GetOrDefault("roy", 0f, command.TryGetParam);
			float roz = Command.GetOrDefault("roz", 0f, command.TryGetParam);
			ParticleEffect effect = ResourceManager.Load<ParticleEffect>(ResourceRouter.GetBattleEffectPath(name));
			if (!effect)
            {
				Debug.LogError(string.Format("[AVGEffect]Unable to load effect {0}", name));
				return false;
            }
			else
            {
				Sequence s = DOTween.Sequence();
				s.AppendInterval(delay);
				s.OnComplete(() => _GenEffect(effect, new Vector2(x, y), new Vector3(rox, roy, roz), layer));
				m_sequences.Add(s);
				return false;
            }
		}

		private ParticleEffect _GenEffect(ParticleEffect prefab, Vector2 pos, Vector3 rotate, int layer)
		{
			ParticleEffect effect = Instantiate(prefab, _effectContainer);
			GameObjectUtil.AddSortingLayerOrderRecursively(effect.gameObject, 5 * layer, true);
			effect.transform.localPosition = pos;
			effect.transform.localRotation = Quaternion.Euler(rotate);
			return effect;
		}

		protected virtual void _OnClicked(object arg)
		{
			this.InvokeEndOfFrame(FinishCommand);
		}

		protected override void ForceCommandEnd() { }

		public AbstractResRefCollecter DontInvoke_PlzImplInternalResRefCollector()
		{
			throw new NotImplementedException();
		}

		[SerializeField]
		private RectTransform _effectContainer;

		private List<Sequence> m_sequences = new();

		private class InternalResRefCollector : AbstractResRefCollecter
		{
			public override void GatherResRefs(Command command, HashSet<string> references)
			{

			}
		}
	}
}