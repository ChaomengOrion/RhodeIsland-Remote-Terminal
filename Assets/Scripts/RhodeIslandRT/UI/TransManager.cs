// Created by ChaomengOrion
// Create at 2022-08-05 20:09:26
// Last modified on 2022-08-05 22:15:11

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

namespace RhodeIsland.RemoteTerminal.UI
{
    public class TransManager : SerializedMonoBehaviour
    {
        [SerializeField, DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.CollapsedFoldout)]
        protected Dictionary<string, AnimationData[]> animationDatas = new();

        [Button("DoTrans")]
        public async UniTask DoTrans(string name, float eachDelay)
        {
            float delay = 0f;
            foreach (AnimationData data in animationDatas[name])
            {
                if (data.delay > delay)
                {
                    delay = data.delay;
                }
                foreach (TransElement obj in data.objs)
                {
                    foreach (UIAnimation animation in data.animations)
                    {
                        animation.DoTrans(obj);
                    }
                    await UniTask.Delay((int)(eachDelay * 1000));
                }
            }
            await UniTask.Delay((int)(delay * 1000));
        }

        [Serializable]
        protected struct AnimationData
        {
            public List<TransElement> objs;
            public List<UIAnimation> animations;
            public float delay;
        }

        public abstract class UIAnimation
        {
            public abstract void DoTrans(TransElement obj);
        }

        public class MoveAnimation : UIAnimation
        {
            public bool resetPos = true;
            [ShowIf("resetPos")]
            public Vector2 fromPos;
            public Vector2 toPos;
            public float t;
            public Ease ease = Ease.Linear;

            public override void DoTrans(TransElement obj)
            {
                RectTransform rect = obj.rectTransform;
                if (!rect) return;
                rect.DOKill();
                if (resetPos)
                    rect.anchoredPosition = fromPos;
                rect.DOAnchorPos(toPos, t).SetEase(ease).Play();
            }
        }

        public class FadeAnimation : UIAnimation
        {
            public bool resetAlpha = true;
            [ShowIf("resetAlpha")]
            public float fromAlpha;
            public float toAlpha;
            public float t;
            public Ease ease = Ease.Linear;

            public override void DoTrans(TransElement obj)
            {
                Graphic image = obj.graphic;
                if (!image) return;
                image.DOKill();
                if (resetAlpha)
                {
                    Color c = image.color;
                    c.a = fromAlpha;
                    image.color = c;
                }
                image.DOFade(toAlpha, t).SetEase(ease).Play();
            }
        }

        public class CanvasGroupAnimation : UIAnimation
        {
            public bool resetAlpha = true;
            [ShowIf("resetAlpha")]
            public float fromAlpha;
            public float toAlpha;
            public float t;
            public bool blockRaycast;
            public Ease ease = Ease.Linear;

            public override void DoTrans(TransElement obj)
            {
                CanvasGroup group = obj.canvasGroup;
                if (!group) return;
                group.DOKill();
                if (resetAlpha)
                    group.alpha = fromAlpha;
                group.DOFade(toAlpha, t).SetEase(ease).Play();
                group.blocksRaycasts = blockRaycast;
            }
        }

        public class ActiveAnimation : UIAnimation
        {
            public bool setTo;
            public float delay;

            public override void DoTrans(TransElement obj)
            {
                if (obj.obj)
                {
                    if (delay < 0.001f)
                        obj.obj.SetActive(setTo);
                    else
                        DoTrans(obj.obj).Forget();
                }
            }

            public async UniTaskVoid DoTrans(GameObject obj)
            {
                await UniTask.Delay((int)(delay * 1000));
                obj.SetActive(setTo);
            }
        }
    }
}