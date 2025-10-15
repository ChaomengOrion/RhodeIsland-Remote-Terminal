// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGImagePanel : ExecutorComponent, IContainsResRefs, IFadeTimeRatio
	{
		public override Dictionary<string, Executor> GetExecutors()
		{
			return new()
			{
				["image"] = _ExecuteImage,
				["imagetween"] = _ExecuteImageTween
            };
		}

		public override Dictionary<string, Gather> GetGathers()
        {
            return new()
            {
				["image"] = _GatherImage
			};
        }

        public override void OnReset()
		{
			base.OnReset();
			_ResetImage(_backImage);
			_ResetImage(_foreImage);
		}

		public virtual AbstractResRefCollecter DontInvoke_PlzImplInternalResRefCollector()
		{
			throw new NotImplementedException();
		}

		protected bool _ExecuteImage(Command command)
		{
			float fadetime = Command.GetOrDefault("fadetime", 0f, command.TryGetParam);
			bool block = Command.GetOrDefault("block", false, command.TryGetParam);
			if (MathUtil.LE(fadetime, 0f))
            {
				block = false;
            }
			fadetime = CalculateFadetime(fadetime);
			_backImage.DOKill();
			_backImage.transform.DOKill();
			_SwapImages(ref _foreImage, ref _backImage);
			Tweener tweener;
			if (_LoadImage(_foreImage, command))
            {
				_foreImage.SetAlpha(0f);
				_foreImage.transform.localPosition = new(
					command.param.GetFloat("x", 0f),
					command.param.GetFloat("y", 0f)
					);
				_foreImage.transform.localScale = new(
					command.param.GetFloat("xScale", 1f),
					command.param.GetFloat("yScale", 1f)
					);
				_foreImage.transform.localRotation = Quaternion.identity;
				tweener = _foreImage.DOFade(1f, fadetime).SetIgnoreTimeScale(true).SetAutoKill();
			}
			else
            {
				tweener = _backImage.DOFade(0f, fadetime).SetIgnoreTimeScale(true).SetAutoKill();
			}
			tweener.SetEase(_fadeEase).OnKill(() =>
            {
				_ResetImage(_backImage);
				if (block)
                {
					FinishCommand();
                }
            }).Play();
			return block;
		}

		protected string _GatherImage(Command command)
		{
			if (command.TryGetParam("image", out string path))
			{
				return ResourceRouter.GetImagePath(path);
			}
			return null;
		}

        protected bool _ExecuteImageTween(Command command)
		{
			float duration = Command.GetOrDefault("duration", 0f, command.TryGetParam);
			bool block = Command.GetOrDefault("block", false, command.TryGetParam);
			if (MathUtil.LE(duration, 0f))
            {
				block = false;
            }
			Vector3 _localPosition = _foreImage.rectTransform.localPosition;
			Vector3 _localScale = _foreImage.rectTransform.localScale;
			Dictionary<string, object> param = command.param;
			float xFrom = param.GetFloat("xFrom", _localPosition.x),
				yFrom = param.GetFloat("yFrom", _localPosition.y),
				xScaleFrom = param.GetFloat("xScaleFrom", _localScale.x),
				yScaleFrom = param.GetFloat("yScaleFrom", _localScale.y),
				xTo = param.GetFloat("xTo", _localPosition.x),
				yTo = param.GetFloat("yTo", _localPosition.y),
				xScaleTo = param.GetFloat("xScaleTo", _localScale.x),
				yScaleTo = param.GetFloat("yScaleTo", _localScale.y);
			Ease ease = param.GetEnum("ease", Ease.Linear);
			bool loop = param.GetBool("loop", false);
			if (block && loop)
            {
				Debug.LogError("Loop and block both true when tween background! Will cause intinity lop!");
            }
			_foreImage.rectTransform.localPosition = new(xFrom, yFrom, _localPosition.z);
			_foreImage.rectTransform.localScale = new(xScaleFrom, yScaleFrom, _localScale.z);
			int loops = loop ? -1 : 1;
			Tweener move = _foreImage.rectTransform.DOLocalMove(new(xTo, yTo, _localPosition.z), duration, false)
				.SetEase(ease).SetLoops(loops).OnComplete(FinishCommand).SetIgnoreTimeScale(true).Play();
			Tweener scale = _foreImage.rectTransform.DOScale(new Vector3(xScaleTo, yScaleTo, _localScale.z), duration)
				.SetEase(ease).SetLoops(loops).SetIgnoreTimeScale(true).Play();
			if (MathUtil.LE(duration, 0f))
			{
				move.Complete();
				scale.Complete();
			}
			return block;
		}

		//EMPTY
		protected override void ForceCommandEnd() { }

		private bool _LoadImage(Image image, Command command)
		{
			if (command.TryGetParam("image", out string path))
            {
				if (!string.IsNullOrEmpty(path))
                {
					Sprite sprite = _LoadSprite(path);
					if (sprite)
                    {
						image.sprite = sprite;
						image.SetNativeSize();
						command.TryGetParam("tiled", out bool tiled);
						if (tiled)
                        {
							image.type = Image.Type.Tiled;
                        }
						else
                        {
							image.type = Image.Type.Simple;
                        }
						float width = Command.GetOrDefault("width", 1f, command.TryGetParam);
						float height = Command.GetOrDefault("height", 1f, command.TryGetParam);
						Vector2 size = image.rectTransform.sizeDelta;
						image.rectTransform.sizeDelta = new(width * size.x, height * size.y);
						if (command.TryGetParam("screenadapt", out string screenadapt))
						{
							if (SCREEN_ADAPT_FUNCTION_MAP.ContainsKey(screenadapt))
                            {
								Vector2 res = SCREEN_ADAPT_FUNCTION_MAP[screenadapt].Invoke(image.rectTransform.sizeDelta, _screenAdaptReferenceResolution);
								image.rectTransform.sizeDelta = res;
							}
							else
                            {
								Debug.LogWarning("[AVG] screenadapt param not exist.it is must be in [width, height, showall, coverall, fill]");
                            }
						}
						image.color = Color.white;
						image.enabled = true;
						return true;
					}
					Debug.LogError(string.Format("Failed to load image: [{0}]", path));
				}
            }
			image.sprite = null;
			image.color = new(1f, 1f, 1f, 0f);
			image.enabled = false;
			return false;
		}

		protected virtual Sprite _LoadSprite(string key)
		{
			string path = ResourceRouter.GetImagePath(key);
			return assetLoader.Load<Sprite>(path);
		}

		private static void _SwapImages(ref Image lhs, ref Image rhs)
		{
            (rhs, lhs) = (lhs, rhs);
			AVGUtils.SwapUnderSameParent(lhs.transform, rhs.transform);
        }

        private static void _ResetImage(Image img)
		{
			img.DOKill();
			img.transform.DOKill();
			img.sprite = null;
			img.SetAlpha(0f);
			img.enabled = false;
		}

		private static Vector2 _AdaptScreenWidth(Vector2 target, Vector2 reference)
		{
			return new(reference.x, target.y * reference.x / target.x);
		}

		private static Vector2 _AdaptScreenHeight(Vector2 target, Vector2 reference)
		{
			return new(target.x  * reference.y / target.y, reference.y);
		}

		private static Vector2 _AdaptScreenShowAll(Vector2 target, Vector2 reference)
		{
			if (target.x / target.y <= reference.x / reference.y)
            {
				return _AdaptScreenHeight(target, reference);
            }
			else
            {
				return _AdaptScreenWidth(target, reference);
            }
		}

		private static Vector2 _AdaptScreenCoverAll(Vector2 target, Vector2 reference)
		{
			if (reference.x / reference.y <= target.x / target.y)
			{
				return _AdaptScreenHeight(target, reference);
			}
			else
			{
				return _AdaptScreenWidth(target, reference);
			}
		}

		private static Vector2 _AdaptScreenFill(Vector2 target, Vector2 reference)
		{
			return reference;
		}

		public float CalculateFadetime(float initialFadetime)
		{
			return initialFadetime * AVGController.instance.animateRatio;
		}

		public bool NeedSkipAnimation(float fadetime)
		{
			return MathUtil.IsZero(fadetime);
		}

		private static readonly Dictionary<string, Func<Vector2, Vector2, Vector2>> SCREEN_ADAPT_FUNCTION_MAP = new()
        {
			["width"] = _AdaptScreenWidth,
			["height"] = _AdaptScreenHeight,
			["showall"] = _AdaptScreenShowAll,
			["coverall"] = _AdaptScreenCoverAll,
			["fill"] = _AdaptScreenFill
        };

		[SerializeField]
		protected Image _foreImage;
		[SerializeField]
		protected Image _backImage;
		[SerializeField]
		protected Ease _fadeEase = Ease.Linear;
		[SerializeField]
		protected Vector2 _screenAdaptReferenceResolution;

		private class InternalResRefCollector : AbstractResRefCollecter
		{
			public InternalResRefCollector()
			{
			}
			public override void GatherResRefs(Command command, HashSet<string> references)
			{
			}
		}
	} 
}