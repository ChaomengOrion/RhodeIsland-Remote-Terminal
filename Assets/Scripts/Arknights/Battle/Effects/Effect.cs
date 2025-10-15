// Created by ChaomengOrion
// Create at 2022-08-12 23:51:23
// Last modified on 2022-08-13 14:24:44

using System;
using System.Collections.Generic;
using Torappu.Fx;
using UnityEngine;
//using XLua;
using RhodeIsland.Arknights.ObjectPool;

namespace RhodeIsland.Arknights.Battle.Effects
{
	//MODIFY
	public abstract class Effect : MonoBehaviour//, IEffectSource, IReusableObject/*, IHotfixable*/, IReusable, IPtrObject
	{
		protected Effect()
		{
		}

		/*public PlayerSide playerSide
		{
			get
			{
				return PlayerSide.DEFAULT;
			}
			set
			{
			}
		}*/

		public uint instanceUid
		{
			get
			{
				return 0U;
			}
			private set
			{
			}
		}

		public abstract bool allowAutoReuse { get; }

		public abstract int preloadCnt { get; }

		protected abstract SpawnLocation spawnLocation { get; }

		protected abstract bool useBodyDirection { get; }

		protected abstract bool holdByOwner { get; }

		protected virtual bool overwriteHeight
		{
			get
			{
				return default(bool);
			}
		}

		protected virtual float heightOffset
		{
			get
			{
				return 0f;
			}
		}

		protected virtual float delayToPlay
		{
			get
			{
				return 0f;
			}
		}

		protected internal virtual float delayToRecycle
		{
			get
			{
				return 0f;
			}
		}

		protected virtual bool randomPlayDelay
		{
			get
			{
				return default(bool);
			}
		}

		protected virtual bool usePlaybackSpeed
		{
			get
			{
				return default(bool);
			}
		}
		protected bool isFinished
		{
			get
			{
				return default(bool);
			}
		}
		public bool isPaused
		{
			get
			{
				return default(bool);
			}
			set
			{
			}
		}
		public bool isPausedByOthers
		{
			get
			{
				return default(bool);
			}
		}

		protected float playbackSpeed
		{
			get
			{
				return 0f;
			}
		}
		protected Transform bodyTransform
		{
			get
			{
				return null;
			}
		}
		/*public void Play(Entity entity, Vector3 direction, float playbackSpeed)
		{
		}
		public void Play(float playbackSpeed)
		{
		}
		public void Play(Vector3 direction, float playbackSpeed)
		{
		}
		public void Play(ILocatable locatable, float playbackSpeed)
		{
		}*/

		public void FinishMe(bool immediately = false)
		{
			if (m_isFinished) return;
			m_isFinished = true;
			foreach (Behaviour item in m_behaviours)
			{
				item.OnFinish();
			}
			OnFinish();
			if (immediately || !MathUtil.GT(delayToRecycle, 0f))
			{

			}
			else
			{

			}
		}

		protected virtual void OnFinish()
		{
		}
		protected virtual void OnBeforePlay()
		{
		}
		public virtual void OnAllocate()
		{
		}
		public virtual void OnRecycle()
		{
		}
		public void GatherEffects(List<string> effects)
		{
		}

		protected virtual void Awake()
		{
			if (!_bodyTransofrm) _bodyTransofrm = transform;
			foreach (var item in GetComponents<Behaviour>())
			{
				item.Init(this);
			}
			m_mainFxDelay = GetComponent<FxDelay>();
		}

		protected virtual void OnEnable()
		{
			if (m_mainFxDelay)
            {
				m_mainFxDelay.enabled = true;
            }
		}

		public virtual void FaceTo(Vector3 direction)
		{
		}
		protected virtual void DoPlay()
		{
		}
		protected virtual void UpdatePlaybackSpeed(float playbackSpeed)
		{
		}
		protected virtual bool SetPaused(bool value)
		{
			return default(bool);
		}
		public virtual bool SetPausedByOthers(bool value)
		{
			return default(bool);
		}
		protected virtual void ClearTrailRenderers()
		{
		}
		private void _PlayInternal(Vector3 direction, float playbackSpeed)
		{
		}
		/*private void _InitLocationFromEntity(Entity entity)
		{
		}*/
		private void _SetEffectLikeCameraEffect()
		{
		}
		private void _InitLocation()
		{
		}
		/*public static void FinishEffects(IList<ObjectPtr<Effect>> effects)
		{
		}*/

		private const string CHILD_NAME_BODY_TRANSFORM = "body";
		private static uint s_globalCounter;
		[HideInInspector]
		[SerializeField]
		private Transform _bodyTransofrm;
		//private ObjectPtr<Entity> m_owner;
		private Effect.Behaviour[] m_behaviours;
		protected bool m_isStarted;
		private bool m_isFinished;
		private bool m_isPaused;
		private bool m_isPausedByOthers;
		private float m_playbackSpeed;
		private TrailRenderer[] m_trailRenderers;
		private FxDelay m_mainFxDelay;

		public class Behaviour : MonoBehaviour//, IHotfixable
		{
			public Behaviour()
			{
			}

			protected Effect effect
			{
				get
				{
					return null;
				}
				private set
				{
				}
			}

			/*protected ObjectPtr<Entity> owner
			{
				get
				{
					return default(ObjectPtr<Entity>);
				}
			}*/

			protected Effect.SpawnLocation spawnLocation
			{
				get
				{
					return Effect.SpawnLocation.NONE;
				}
			}

			protected bool isPaused
			{
				get
				{
					return default(bool);
				}
				set
				{
				}
			}

			public bool isFinished
			{
				get
				{
					return default(bool);
				}
			}
			public virtual void Init(Effect effect)
			{
			}
			public virtual void OnPlay()
			{
			}
			public virtual void OnFinish()
			{
			}
			public virtual void OnRecycle()
			{
			}
			public virtual void OnPaused(bool paused)
			{
			}
			protected void FaceTo(Vector3 direction)
			{
			}
		}

		public enum SpawnLocation
		{
			NONE,
			FOOT_POINT,
			HIT_POINT,
			MUZZLE_POINT,
			HEAD_POINT,
			GROUND_CENTER,
			MUZZLE_POINT_WITHOUT_ROTATION,
			GROUND_CENTER_WITH_ZERO_HEIGHT,
			MP_SPECIAL_0,
			MP_SPECIAL_1,
			MP_SPECIAL_2,
			MP_SPECIAL_3,
			CAMERA,
			MP_SPECIAL_4,
			MP_SPECIAL_5,
			MP_SPECIAL_6,
			MP_SPECIAL_7
		}
	}
}