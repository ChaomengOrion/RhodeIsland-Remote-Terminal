// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
//using XLua;
using Torappu.AVG;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGCharacterSlot : MonoBehaviour//, IHotfixable
	{
		public Image currentImage
		{
			get
			{
				return _foreImage;
			}
		}

		public void Set(string key, float duration, Color color)
		{
			Set(key, duration, color, false, true, 0f, 0f);
		}

		public void Set(string key, float duration, Color color, bool dontFadeIfSameChar, bool resetOffsetPos = true, float blackStart = 0f, float blackEnd = 0f)
		{
			if (dontFadeIfSameChar)
			{
				if (_GetIdWithoutAliasOrIndex(m_currentKey) == _GetIdWithoutAliasOrIndex(key))
				{
					duration = 0f;
				}
			}
			if (m_currentKey == key)
			{
				if (string.IsNullOrEmpty(key))
				{
					_foreImage.enabled = false;
					_backImage.enabled = false;
				}
				if (MathUtil.GT(duration, 0f))
				{
					_foreImage.DOColorWithoutAlpha(color, duration);
				}
				else
				{
					_foreImage.SetColorWithoutAlpha(color);
				}
			}
			else
			{
				if (resetOffsetPos)
				{
					if (!string.IsNullOrEmpty(key))
					{
						if (string.IsNullOrEmpty(m_currentKey) || m_currentKey.ToLower() != EMPTY_CHARACTER)
						{
							_offset.localPosition = Vector3.zero;
						}
					}
				}
				_offset.localScale = Vector3.one;
				_SwapImages(ref _foreImage, ref _backImage, ref m_foreImageHolder, ref m_backImageHolder);
				if (_LoadImage(m_foreImageHolder, key, blackStart, blackEnd))
				{
					m_currentKey = key;
					_foreImage.SetAlpha(0f);
					_foreImage.DOColor(color, duration)
						.SetEase(_fadeEase)
						.SetUpdate(false)
						.SetIgnoreTimeScale(true)
						.Play();
				}
				else
				{
					m_currentKey = null;
				}
				_backImage.DOKill();
				_backImage.DOFade(0f, duration)
					.SetEase(_fadeEase)
					.SetUpdate(true)
					.SetIgnoreTimeScale(true)
					.OnComplete(() => _backImage.enabled = false)
					.Play();
			}
		}

		public void Clear(float duration = 0f)
		{
			Set(null, duration, _foreImage.color);
		}

		public void OnReset()
		{
			Clear(0f);
			_offset.DOKill();
			_offset.localPosition = Vector3.zero;
			_offset.localScale = Vector3.one;
			_backImage.DOKill();
			m_currentKey = null;
			m_foreImageHolder?.Reset();
			m_backImageHolder?.Reset();
		}

		public Tween MoveChar(float x, float y, float fadeTime)
		{
			if (MathUtil.IsZero(fadeTime))
            {
				_offset.localPosition += new Vector3(x, y);
				return null;
            }
			else
            {
				return _offset.DOLocalMove(_offset.localPosition + new Vector3(x, y), fadeTime, false).SetEase(Ease.OutCubic);
            }
		}

		public Tween CharJump(float x, float y, float jumpPower, int times, float fadeTime)
		{
			return null;
		}

		public Tween CharZoom(float xPos, float yPos, float Scale, float fadeTime)
		{
			return null;
		}

		public Tween SetCharPos(float x, float y, float fadeTime = 0f)
		{
			if (MathUtil.IsZero(fadeTime))
            {
				_offset.transform.localPosition = new Vector3(x, y, _offset.transform.localPosition.z);
				return null;
			}
			else
            {
				return _offset.DOLocalMove(new Vector3(x, y, _offset.transform.localPosition.z), fadeTime, false).SetEase(Ease.OutCubic);
            }
		}

		public static string GetCharPath(string key)
        {
			_TryParseBody(ref key, out _);
			if (_TryParseAlias(ref key, out _))
			{
				return ResourceRouter.GetCharacterPath(key);
			}
			_TryParseIndex(ref key, out _);
			return ResourceRouter.GetCharacterPath(key);
		}

		private static bool _LoadImage(AlphaSplitImageHolder imageHolder, string key, float blackStart, float blackEnd)
		{
			Image image = imageHolder.image;
			image.DOKill();
			image.color = Color.white;
			if (string.IsNullOrEmpty(key))
			{
				image.sprite = null;
				image.color = new(1f, 1f, 1f, 1f);
				image.enabled = false;
				return false;
			}
			string rawkey = key;
			image.enabled = true;
			bool bodySuc = _TryParseBody(ref key, out int body);
			if (_TryParseAlias(ref key, out string alias))
            {
				if (!bodySuc)
                {
					AVGCharacterSpriteHub spriteHubAlias = AVGController.instance.assetLoader.Load<AVGCharacterSpriteHub>(ResourceRouter.GetCharacterPath(key));
					if (spriteHubAlias)
                    {
						spriteHubAlias.SetImage(imageHolder, alias, blackStart, blackEnd);
						return true;
					}
					body = 0;
				}
				AVGCharacterSpriteHubGroup spriteHubGroupAlias = AVGController.instance.assetLoader.Load<AVGCharacterSpriteHubGroup>(ResourceRouter.GetCharacterPath(key));
				if (spriteHubGroupAlias)
				{
					spriteHubGroupAlias.SetImage(imageHolder, body, alias, blackStart, blackEnd);
					return true;
				}
				Debug.LogError(string.Format("[AVG] Error to load character pic: [{0}] during story [{1}]", rawkey, AVGController.instance.storyId));
				image.sprite = null;
				image.color = new(1f, 1f, 1f, 1f);
				image.enabled = false;
				return false;
			}
			_TryParseIndex(ref key, out int index);
			if (!bodySuc)
            {
				AVGCharacterSpriteHub spriteHub = AVGController.instance.assetLoader.Load<AVGCharacterSpriteHub>(ResourceRouter.GetCharacterPath(key));
				if (spriteHub)
				{
					spriteHub.SetImage(imageHolder, index, blackStart, blackEnd);
					return true;
				}
			}
			AVGCharacterSpriteHubGroup spriteHubGroup = AVGController.instance.assetLoader.Load<AVGCharacterSpriteHubGroup>(ResourceRouter.GetCharacterPath(key));
			if (!spriteHubGroup)
			{
				Debug.LogError(string.Format("[AVG] Error to load character pic: [{0}] during story [{1}]", rawkey, AVGController.instance.storyId));
				image.sprite = null;
				image.color = new(1f, 1f, 1f, 1f);
				image.enabled = false;
				return false;
			}
			spriteHubGroup.SetImage(imageHolder, body, index, blackStart, blackEnd);
			return true;
		}

		private static void _SwapImages(ref Image lhs, ref Image rhs, ref AlphaSplitImageHolder lhsHolder, ref AlphaSplitImageHolder rhsHolder)
		{
			(lhs, rhs) = (rhs, lhs);
			(lhsHolder, rhsHolder) = (rhsHolder, lhsHolder);
			AVGUtils.SwapUnderSameParent(lhs.transform, rhs.transform);
		}

		private static bool _TryParseAlias(ref string key, out string alias)
		{
			int last = key.LastIndexOf(ALIAS_TOKEN);
			if (key != null && last >= 0)
            {
				alias = key[last..];
				key = key[..last];
				return true;
            }
			else
            {
				alias = null;
				return false;
			}
		}

		private static bool _TryParseIndex(ref string key, out int index)
		{
			if (key == null)
            {
				index = 0;
				return false;
			}
			int last = key.LastIndexOf(INDEX_TOKEN);
			if (last < 0)
            {
				index = 0;
				return false;
			}
			if (int.TryParse(key[(last + 1)..], out index))
            {
				key = key[..last];
				--index;
				return true;
            }
			else
            {
				index = 0;
				return false;
			}
		}

		private static bool _TryParseBody(ref string key, out int body)
		{
			if (key == null)
			{
				body = -1;
				return false;
			}
			int last = key.LastIndexOf(BODY_TOKEN);
			if (last < 0)
			{
				body = -1;
				return false;
			}
			if (int.TryParse(key[(last + 1)..], out body))
			{
				--body;
				key = key[..last];
				return true;
			}
			else
			{
				body = -1;
				return false;
			}
		}

		private static string _GetIdWithoutAliasOrIndex(string key)
		{
			if (key != null)
            {
				int last = key.LastIndexOf(ALIAS_TOKEN);
				if (last < 0)
				{
					last = key.LastIndexOf(INDEX_TOKEN);
					if (last < 0)
                    {
						return key;
					}
				}
				return key[..last];
			}
			return null;
		}

		private void Awake()
		{
			m_foreImageHolder = new(_foreImage);
			m_backImageHolder = new(_backImage);
			m_foreImageHolder.loadType = AlphaSplitImageHolder.ShaderLoadType.AVG;
			m_backImageHolder.loadType = AlphaSplitImageHolder.ShaderLoadType.AVG;
		}

		public const char INDEX_TOKEN = '#';

		public const char ALIAS_TOKEN = '@';

		public const char BODY_TOKEN = '$';

		public const string EMPTY_CHARACTER = "char_empty";

		[SerializeField]
		private Image _foreImage;

		[SerializeField]
		private Image _backImage;

		[SerializeField]
		private RectTransform _offset;

		[SerializeField]
		private Ease _fadeEase = Ease.Linear;

		private string m_currentKey;

		private float m_currBlackStart;

		private float m_currBlackEnd;

		private string m_currentFaceKey;

		private AlphaSplitImageHolder m_foreImageHolder;

		private AlphaSplitImageHolder m_backImageHolder;
	}
}