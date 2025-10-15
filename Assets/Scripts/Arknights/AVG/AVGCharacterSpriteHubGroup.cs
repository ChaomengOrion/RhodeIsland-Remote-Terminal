// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using UnityEngine;
//using XLua;
using RhodeIsland.Arknights;
using RhodeIsland.Arknights.AVG;

namespace Torappu.AVG
{
	public class AVGCharacterSpriteHubGroup : MonoBehaviour//, IHotfixable
	{
		public void SetImage(AlphaSplitImageHolder imageHolder, int body, string alias, float blackStart, float blackEnd)
		{
			if (body < 0 || spriteGroups.Length <= body)
			{
				body = 0;
				Debug.LogError(string.Format("[AVG] No body index {0} for character holder {1}, use default instead during script [{2}]", body, name, AVGController.instance.storyId));
			}
			int i = 0;
			AVGCharacterSpriteHub.SpriteConfig[] spriteConfigs = spriteGroups[body].sprites;
			AVGCharacterSpriteHub.SpriteConfig spriteConfig;
			for (; i < spriteConfigs.Length; i++)
			{
				if (spriteConfigs[body].alias == alias)
				{
					spriteConfig = spriteConfigs[i];
					goto Finded;
				}
			}
			if (!string.IsNullOrEmpty(alias))
			{
				Debug.LogError(string.Format("[AVG] No alias {0} for character holder {1}, use default instead.", alias, name));
			}
			spriteConfig = spriteConfigs[0];
		Finded:
			if (spriteConfig == null)
			{
				if (!string.IsNullOrEmpty(alias))
				{
					Debug.LogError(string.Format("[AVG] No alias {0} for character holder {1}, use default instead.", alias, name));
				}
				spriteConfig = spriteConfigs[0];
			}
			_PickSetImageImpl(imageHolder, spriteConfig, body, blackStart, blackEnd);
		}

		public void SetImage(AlphaSplitImageHolder imageHolder, int body, int index, float blackStart, float blackEnd)
		{
			if (body < 0 || spriteGroups.Length <= body)
			{
				body = 0;
				Debug.LogError(string.Format("[AVG] No body index {0} for character holder {1}, use default instead during script [{2}]", body, name, AVGController.instance.storyId));
			}
			if (index < 0 || spriteGroups[body].sprites.Length <= index)
			{
				index = 0;
				Debug.LogError(string.Format("[AVG] No index {0} for body group {1} of character holder {2}, use default instead during script [{3}]", index, body, name, AVGController.instance.storyId));
			}
			_PickSetImageImpl(imageHolder, spriteGroups[body].sprites[index], body, blackStart, blackEnd);
		}

		private void _PickSetImageImpl(AlphaSplitImageHolder imageHolder, AVGCharacterSpriteHub.SpriteConfig targetConfig, int body, float blackStart, float blackEnd)
		{
			AVGCharacterSpriteHub.SpriteConfig faceConfig, config = targetConfig;
			if (!_HasFace(body))
			{
				config = null;
				faceConfig = config;
			}
			else
			{
				faceConfig = null;
				if (!targetConfig.isWholeBody)
				{
					targetConfig = spriteGroups[body].sprites[^1];
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
			_SetImage(imageHolder, targetConfig, faceConfig, body, blackStart, blackEnd);
		}

		private void _SetImage(AlphaSplitImageHolder imageHolder, AVGCharacterSpriteHub.SpriteConfig charConfig, AVGCharacterSpriteHub.SpriteConfig faceConfig, int body, float blackStart, float blackEnd)
		{
			RectTransform rectTransform = GetComponent<RectTransform>();
			if (rectTransform)
			{
				GUIUtils.AssignLocalSettings(imageHolder.image.rectTransform, rectTransform);
			}
			Vector2 size = new(charConfig.sprite.texture.width, charConfig.sprite.texture.height);
			if (imageHolder != null)
			{
				Vector2 faceScale = new(MathUtil.IsZero(spriteGroups[body].faceSize.x) ? 0f : size.x / spriteGroups[body].faceSize.x, MathUtil.IsZero(spriteGroups[body].faceSize.y) ? 0f : size.y / spriteGroups[body].faceSize.y);
				Vector2 faceOffset = new(MathUtil.IsZero(spriteGroups[body].facePos.x) ? 0f : -spriteGroups[body].facePos.x / spriteGroups[body].faceSize.x, MathUtil.IsZero(spriteGroups[body].facePos.y) ? 0f : 1f - (1024f - spriteGroups[body].facePos.y) / spriteGroups[body].faceSize.y); ;
				imageHolder.SetSprite(charConfig, faceConfig, new()
				{
					faceOffset = faceOffset,
					faceScale = faceScale,
					blackStart = blackStart,
					blackEnd = blackEnd,
				});
			}
		}

		private bool _HasFace(int body)
		{
			return !MathUtil.Similar(spriteGroups[body].faceSize, Vector2.zero);
		}

		public SpriteConfigGroup[] spriteGroups;

		[Serializable]
		public class SpriteConfigGroup
		{
			public AVGCharacterSpriteHub.SpriteConfig[] sprites;

			public Vector3 facePos;

			public Vector2 faceSize;
		}
	}
}
