// Created by ChaomengOrion
// Create at 2022-07-29 00:46:33
// Last modified on 2022-07-31 17:40:18

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using DG.Tweening;
using Torappu.Building.Vault;
using Animation = Spine.Animation;
using UnityEngine.EventSystems;

namespace RhodeIsland.RemoteTerminal.UI.Character
{
    public class SpineCharacter : MonoBehaviour
    {
        [SerializeField]
        private SkeletonAnimation _skeletonAnimation;
        [SerializeField]
        private MeshFilter _meshFilter;
        [SerializeField]
        private MeshRenderer _meshRenderer;
        [SerializeField]
        private MeshCollider _meshCollider;
        [SerializeField]
        private Shader _shader;
        [SerializeField]
        private RectTransform _moveAbleRect;
        [SerializeField]
        private float _minMoveDis = 50f;

        private VCharacter.Options m_options;
        private Animation m_animation;
        private SkeletonData m_skeletonData;
        private Coroutine m_stateCoroutine = null;
        private Tweener m_moveTweener = null;
        private State m_state;
        /// <summary>
        /// true -> right
        /// flase -> left 
        /// </summary>
        private bool m_direction = true;

        private void Awake()
        {
            _skeletonAnimation.OnMeshAndMaterialsUpdated += _OnMeshAndMaterialsUpdated;
        }

        public void OnClick()
        {
            if (m_stateCoroutine != null)
                StopCoroutine(m_stateCoroutine);
            m_moveTweener?.Kill();
            _SetToInteract();
        }

        public void SetUp(BuildingSpineHolder holder)
        {
            if (m_stateCoroutine != null)
                StopCoroutine(m_stateCoroutine);
            m_moveTweener?.Kill();
            m_state = State.NONE;
            m_options = holder.options;
            _skeletonAnimation.ClearState();
            foreach (var asset in holder.skeletonData.atlasAssets)
            {
                asset.PrimaryMaterial.shader = _shader;
            }
            _skeletonAnimation.skeletonDataAsset = holder.skeletonData;
            _skeletonAnimation.Initialize(true);
            m_skeletonData = _skeletonAnimation.SkeletonDataAsset.GetSkeletonData(false);
            _SetToRelax();
        }

        public void ResetPos()
        {
            transform.DOKill();
            transform.localPosition = Vector3.zero;
        }

        private void _OnMeshAndMaterialsUpdated(SkeletonRenderer skeletonRenderer)
        {
            _meshCollider.sharedMesh = _meshFilter.sharedMesh;
        }

        public void PlayState(State state, bool loop)
        {
            DLog.Log(state);
            if (state == m_state)
            {
                _skeletonAnimation.loop = loop;
                return;
            }
            m_animation = m_skeletonData.FindAnimation(GetStateKey(state));
            _skeletonAnimation.state.SetAnimation(0, m_animation, loop);
            m_state = state;
        }

        public void PlayState(State state, bool loop, out float time)
        {
            DLog.Log(state);
            m_animation = m_skeletonData.FindAnimation(GetStateKey(state));
            time = m_animation.Duration;
            _skeletonAnimation.state.SetAnimation(0, m_animation, loop);
            m_state = state;
        }

        public bool HasAnimation(State state)
        {
            return m_skeletonData.FindAnimation(GetStateKey(state)) != null;
        }

        [Sirenix.OdinInspector.Button("BT")]
        public void Test()
        {
            SkeletonData data = _skeletonAnimation.SkeletonDataAsset.GetSkeletonData(false);
            foreach (var item in data.Animations)
            {
                foreach (Timeline timeline in item.Timelines)
                {
                    if (timeline is EventTimeline eventTimeline)
                    {
                        for (int i = 0; i < eventTimeline.Events.Length; i++)
                        {
                            DLog.Log(eventTimeline.Events[i].Data.Name + ' ' + eventTimeline.Events[i].Time.ToString());
                        }
                    }
                }
            }
        }

        private void _UpdateState()
        {
            if (m_state == State.SLEEP)
            {
                if (Random.Range(0f, 1f) > 0.4f)
                {
                    _SetToRelax();
                }
                else
                {
                    _SetToSit();
                }
            }
            else if (m_state == State.SIT)
            {
                if (Random.Range(0f, 1f) > 0.4f)
                {
                    _SetToRelax();
                }
                else
                {
                    _SetToSleep();
                }
            }
            else if (m_state == State.MOVE)
            {
                if (Random.Range(0f, 1f) > 0.4f)
                {
                    _SetToRelax();
                }
                else
                {
                    _SetToMove();
                }
            }
            else if (_TryFetchUI())
            {
                //TODO
            }
            else
            {
                float k = Random.Range(0f, 1f);
                if (k > 0.6f)
                {
                    _SetToRelax();
                }
                else if (k > 0.15f || !HasAnimation(State.SIT) || !HasAnimation(State.SLEEP))
                {
                    _SetToMove();
                }
                else if (k > 0.12f)
                {
                    _SetToSleep();
                }
                else if (k > 0.05f || !HasAnimation(State.SPEICAL))
                {
                    _SetToSit();
                }
                else
                {
                    _SetToSpeical();
                }
            }
        }

        private bool _TryFetchUI()
        {
            return false;
        }

        private void _SetDirection(bool dir)
        {
            if (dir != m_direction)
            {
                m_direction = dir;
                transform.DOScaleX(dir ? 1f : -1f, 0.1f);
            }
        }

        private void _SetToInteract()
        {
            PlayState(State.INTERACT, false, out float t);
            m_stateCoroutine = this.InvokeAsync(_SetToRelax, t);
        }

        private void _SetToMove()
        {
            float speed = Random.Range(Mathf.Max(0f, m_options.moveSpeed - 0.2f), m_options.moveSpeed + 0.1f) * 10f;
            float x = Random.Range(0f, _moveAbleRect.rect.width) - _moveAbleRect.rect.width / 2f;
            float y = Random.Range(0f, _moveAbleRect.rect.height) - _moveAbleRect.rect.height / 2f;
            Vector3 pos = _moveAbleRect.localToWorldMatrix.MultiplyPoint(new(Mathf.Sign(x) * Mathf.Lerp(_minMoveDis, _moveAbleRect.rect.width / 2f, Mathf.Abs(x) / (_moveAbleRect.rect.width / 2f)), y));
            float dis = Vector2.Distance(pos, transform.position);
            PlayState(State.MOVE, true);
            transform.DOKill();
            _SetDirection(pos.x - transform.position.x >= 0f);
            m_moveTweener = transform.DOMove(pos, dis / speed).SetEase(Ease.Linear).Play();
            m_stateCoroutine = this.InvokeAsync(_UpdateState, dis / speed);
        }

        private void _SetToSleep()
        {
            float sleepTime = Random.Range(20f, 80f);
            PlayState(State.SLEEP, true);
            m_stateCoroutine = this.InvokeAsync(_UpdateState, sleepTime);
        }

        private void _SetToSit()
        {
            float sitTime = Random.Range(5f, 35f);
            PlayState(State.SIT, true);
            m_stateCoroutine = this.InvokeAsync(_UpdateState, sitTime);
        }

        private void _SetToRelax()
        {
            float relaxTime = Random.Range(m_options.idleTimeRange.x, m_options.idleTimeRange.y);
            PlayState(State.RELAX, true);
            m_stateCoroutine = this.InvokeAsync(_UpdateState, relaxTime);
        }

        private void _SetToSpeical()
        {
            PlayState(State.SPEICAL, false, out float t);
            m_stateCoroutine = this.InvokeAsync(_SetToRelax, t);
        }

        public static string GetStateKey(State state)
        {
            return state switch
            {
                State.INTERACT => "Interact",
                State.MOVE => "Move",
                State.RELAX => "Relax",
                State.SIT => "Sit",
                State.SLEEP => "Sleep",
                State.SPEICAL => "Speical",
                _ => string.Empty
            };
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public enum State
        {
            INTERACT,
            MOVE,
            RELAX,
            SIT,
            SLEEP,
            SPEICAL,
            NONE
        }
    }
}