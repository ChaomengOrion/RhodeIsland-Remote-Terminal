// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using UnityEngine;
//using XLua;
using RhodeIsland.Arknights;
using RhodeIsland.Arknights.AVG;

namespace Torappu.AVG
{
	public class AVGCharacterSpriteHub : MonoBehaviour//, IHotfixable
	{
		public void SetImage(AlphaSplitImageHolder imageHolder, int index, float blackStart, float blackEnd)
		{
			if (index < 0 || sprites.Length <= index)
            {
				index = 0;
				Debug.LogError(string.Format("[AVG] No index {0} for character holder {1}, use default instead during script [{2}]", index, name, AVGController.instance.storyId));
            }
			_PickSetImageImpl(imageHolder, sprites[index], blackStart, blackEnd);
		}

		public void SetImage(AlphaSplitImageHolder imageHolder, string alias, float blackStart, float blackEnd)
		{
			int i = 0;
			SpriteConfig spriteConfig;
			for (; i < sprites.Length; i++)
            {
				if (sprites[i].alias == alias)
				{
					spriteConfig = sprites[i];
					goto Finded;
                }
			}
			if (!string.IsNullOrEmpty(alias))
			{
				Debug.LogError(string.Format("[AVG] No alias {0} for character holder {1}, use default instead.", alias, name));
			}
			spriteConfig = sprites[0];
		Finded:
			if (spriteConfig == null)
            {
				if (!string.IsNullOrEmpty(alias))
				{
					Debug.LogError(string.Format("[AVG] No alias {0} for character holder {1}, use default instead.", alias, name));
				}
				spriteConfig = sprites[0];
			}
			_PickSetImageImpl(imageHolder, spriteConfig, blackStart, blackEnd);
		}

		private void _PickSetImageImpl(AlphaSplitImageHolder imageHolder, SpriteConfig targetConfig, float blackStart, float blackEnd)
		{
			SpriteConfig faceConfig, config = targetConfig;
			if (!_HasFace())
            {
				config = null;
				faceConfig = config;
			}
			else
            {
				faceConfig = null;
				if (!targetConfig.isWholeBody)
                {
					targetConfig = sprites[^1];
					if (targetConfig != null && targetConfig == config)
                    {
						Debug.LogError(string.Format("[AVGCharSpriteHub] Hub [{0}] has face but the body sprite has been indexed.", name));
                    }
					else
                    {
						faceConfig = config;
					}
                }
            }
			_SetImage(imageHolder, targetConfig, faceConfig, blackStart, blackEnd);
		}

		private bool _HasFace()
		{
			return !MathUtil.Similar(FaceSize, Vector2.zero);
		}

		private void _SetImage(AlphaSplitImageHolder imageHolder, SpriteConfig config, SpriteConfig faceConfig, float blackStart, float blackEnd)
		{
			RectTransform rectTransform = GetComponent<RectTransform>();
			if (rectTransform)
            {
				GUIUtils.AssignLocalSettings(imageHolder.image.rectTransform, rectTransform);
            }
			Vector2 size = new(config.sprite.texture.width, config.sprite.texture.height);
			if (imageHolder != null)
            {
				Vector2 faceScale = new(MathUtil.IsZero(FaceSize.x) ? 0f : size.x / FaceSize.x, MathUtil.IsZero(FaceSize.y) ? 0f : size.y / FaceSize.y);
				Vector2 faceOffset = new(MathUtil.IsZero(FacePos.x) ? 0f : -FacePos.x / FaceSize.x, MathUtil.IsZero(FacePos.y) ? 0f : 1f - (1024f - FacePos.y) / FaceSize.y); ;
				imageHolder.SetSprite(config, faceConfig, new()
                {
					faceOffset = faceOffset,
					faceScale = faceScale,
					blackStart = blackStart,
					blackEnd = blackEnd,
                });
			}
		}

		private void _SetImageLegency(AlphaSplitImageHolder imageHolder, AlphaSplitImageHolder faceImageHolder, SpriteConfig config, SpriteConfig faceConfig, float blackStart, float blackEnd)
		{
		}

		public SpriteConfig[] sprites = new SpriteConfig[0];

		public Vector3 FacePos = new(-1f, -1f, 0f);

		public Vector2 FaceSize = Vector2.zero;

		private RectTransform m_rectTransform;

		[Serializable]
		public class SpriteConfig
		{
			public override string ToString()
			{
				string name;
				if (!sprite)
                {
					name = "(NONE)";
				}
				else
                {
					name = sprite.name;
                }
				return string.Format("{0}@{1}", name, alias);
			}

			public Sprite sprite;

			public Texture alphaTex;

			public string alias;

			public bool isWholeBody;
		}
	}
}
