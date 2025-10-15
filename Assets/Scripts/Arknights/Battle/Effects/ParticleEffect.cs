// Created by ChaomengOrion
// Create at 2022-08-12 23:36:40
// Last modified on 2022-08-13 14:34:19

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
//using XLua;
using RhodeIsland.Arknights.Battle.Effects;
using RhodeIsland;

namespace Torappu.Battle.Effects
{
	public class ParticleEffect : MonoBehaviour
	{
		protected new ParticleSystem particleSystem => m_particleSystem;

		protected ParticleSystem[] particleSystems
        {
			get
            {
				if (particleSystems == null)
                {
					m_particleSystems = GetComponentsInChildren<ParticleSystem>(true);
				}
				return m_particleSystems;
            }
        }


		[DebuggerHidden]
		private IEnumerator _CheckIfAlive()
		{
			if (_maxLifetime >= 0f)
            {
				yield return new WaitForSecondsRealtime(_maxLifetime);
				Destroy(gameObject);
				yield break;
            }
			while (true)
			{
				if (!particleSystem)
				{
					Destroy(gameObject);
					yield break;
				}
				yield return new WaitForSeconds(0.5f);
				if (!particleSystem.IsAlive(true))
				{
					Destroy(gameObject);
					yield break;
				}
			}
		}

		protected void OnEnable()
		{
			StartCoroutine(_CheckIfAlive());
		}

		protected void Awake()
		{
			m_particleSystem = GetComponentInChildren<ParticleSystem>();
			m_animator = GetComponent<Animator>();
		}


		private const string CHILD_NAME_ROTATION_Y = "rotation_y";

		private const string CHILD_NAME_ROTATION_Z = "rotation_z";

		private const string CHILD_NAME_FLIP_X = "flip_x";

		private const float CHECK_IF_ALIVE_DELTA = 0.5f;

		[SerializeField]
		private float _delayToPlay;
		[SerializeField]
		private float _delayToFinish = 0.5f;
		[SerializeField]
		private float _maxLifetime = -1f;
		[SerializeField]
		private bool _randomPlayDelay;
		[SerializeField]
		private bool _allowAutoReuse = true;
		[SerializeField]
		private bool _usePlaybackSpeed;
		[SerializeField]
		private int _preloadCnt = 10;
		[SerializeField]
		private RotateType _rotateType;
		//[SerializeField]
		//private SpawnLocation _spawnLocation = SpawnLocation.FOOT_POINT;
		[SerializeField]
		private bool _leftIsDefault;
		[SerializeField]
		private bool _useBodyRotation;
		//[SerializeField]
		//private SharedConsts.Direction _mainDir;
		[SerializeField]
		private bool _holdByOwner;
		[SerializeField]
		private bool _overwriteHeight;
		[SerializeField]
		private float _heightOffset;
		[SerializeField]
		private Transform _rotationY;
		[SerializeField]
		private Transform _rotationZ;
		[SerializeField]
		private Transform _flipZ;
		private Animator m_animator;
		private ParticleSystem m_particleSystem;
		private ParticleSystem[] m_particleSystems;

		public enum RotateType
		{
			NONE,
			FLIP_TWO_SIDE,
			FOUR_DIRECTION,
			FIXED_DIRECTION,
			ANY_DIR
		}
	}
	/*	public class ParticleEffect : Effect
	{
		public bool isFourDir => _rotateType == RotateType.FOUR_DIRECTION; // ?

		public float maxLifeTime => _maxLifetime;

		public override bool allowAutoReuse => _allowAutoReuse;

		public override int preloadCnt => _preloadCnt;

		protected override SpawnLocation spawnLocation => _spawnLocation;

		protected override bool useBodyDirection => _useBodyRotation;

		protected override bool holdByOwner => _holdByOwner;

		protected override bool overwriteHeight => _overwriteHeight;

		protected override float heightOffset => _heightOffset;

		protected override float delayToPlay => _delayToPlay;

		protected internal override float delayToRecycle => _delayToFinish;

		protected override bool randomPlayDelay => _randomPlayDelay;

		protected override bool usePlaybackSpeed => _usePlaybackSpeed;

		protected new ParticleSystem particleSystem => m_particleSystem;

		protected ParticleSystem[] particleSystems
        {
			get
            {
				if (particleSystems == null)
                {
					m_particleSystems = GetComponentsInChildren<ParticleSystem>(true);
				}
				return m_particleSystems;
            }
        }

		protected override void OnFinish()
		{
			base.OnFinish();
			particleSystem.Stop(true);
		}

		protected override void DoPlay()
		{
			base.DoPlay();
			particleSystem.Play();
		}

		protected override void OnBeforePlay()
		{
			base.OnBeforePlay();
			gameObject.SetActiveIfNecessary(true);
			particleSystem.Stop(true);
			particleSystem.Clear(true);
		}

		public override void FaceTo(Vector3 direction)
		{
		}

		[DebuggerHidden]
		private IEnumerator _CheckIfAlive()
		{
			if (_maxLifetime >= 0f)
            {
				yield return new WaitForSecondsRealtime(_maxLifetime);
				FinishMe(false);
				yield break;
            }
			while (true)
			{
				if (!particleSystem)
				{
					yield break;
				}
				yield return new WaitForSeconds(0.5f);
				if (m_isStarted && !particleSystem.IsAlive(true))
				{
					FinishMe(true);
					yield break;
				}
			}
		}

		protected override void UpdatePlaybackSpeed(float playbackSpeed)
		{
		}

		protected override bool SetPaused(bool value)
		{
			return default(bool);
		}

		public override bool SetPausedByOthers(bool value)
		{
			return default(bool);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			StartCoroutine(_CheckIfAlive());
		}

		protected override void Awake()
		{
			base.Awake();
			m_particleSystem = GetComponentInChildren<ParticleSystem>();
			m_animator = GetComponent<Animator>();
		}

		public override void OnAllocate()
		{
			base.OnAllocate();
		}

		public override void OnRecycle()
		{
			base.OnRecycle();
		}

		private const string CHILD_NAME_ROTATION_Y = "rotation_y";

		private const string CHILD_NAME_ROTATION_Z = "rotation_z";

		private const string CHILD_NAME_FLIP_X = "flip_x";

		private const float CHECK_IF_ALIVE_DELTA = 0.5f;

		[SerializeField]
		private float _delayToPlay;
		[SerializeField]
		private float _delayToFinish = 0.5f;
		[SerializeField]
		private float _maxLifetime = -1f;
		[SerializeField]
		private bool _randomPlayDelay;
		[SerializeField]
		private bool _allowAutoReuse = true;
		[SerializeField]
		private bool _usePlaybackSpeed;
		[SerializeField]
		private int _preloadCnt = 10;
		[SerializeField]
		private RotateType _rotateType;
		[SerializeField]
		private SpawnLocation _spawnLocation = SpawnLocation.FOOT_POINT;
		[SerializeField]
		private bool _leftIsDefault;
		[SerializeField]
		private bool _useBodyRotation;
		//[SerializeField]
		//private SharedConsts.Direction _mainDir;
		[SerializeField]
		private bool _holdByOwner;
		[SerializeField]
		private bool _overwriteHeight;
		[SerializeField]
		private float _heightOffset;
		[SerializeField]
		private Transform _rotationY;
		[SerializeField]
		private Transform _rotationZ;
		[SerializeField]
		private Transform _flipZ;
		private Animator m_animator;
		private ParticleSystem m_particleSystem;
		private ParticleSystem[] m_particleSystems;

		public enum RotateType
		{
			NONE,
			FLIP_TWO_SIDE,
			FOUR_DIRECTION,
			FIXED_DIRECTION,
			ANY_DIR
		}
	}*/
}