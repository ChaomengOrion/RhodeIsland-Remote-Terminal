// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using UnityEngine;
using UnityEngine.UI;
using Torappu.UI;
using Torappu.AVG;

namespace RhodeIsland.Arknights.AVG
{
	public class AlphaSplitImageHolder
	{
		public AlphaSplitImageHolder(Image image)
		{
			m_image = image;
		}

		public Image image
		{
			get
			{
				return m_image;
			}
		}

		public ShaderLoadType loadType { private get; set; }

		public void SetSpriteLegency(AVGCharacterSpriteHub.SpriteConfig config, float blackStart, float blackEnd, float blackStartValue = 0f, float blackEndValue = 1f)
        {
            if (!config.alphaTex)
            {
                m_image.material = null;
            }
			else
			{
				if (!MathUtil.Equals(blackStart, blackEnd) || !MathUtil.IsZero(blackStart))
				{
					m_alphaSplitMaterial = new(Shader.Find("Torappu/UI/AVG_AlphaSplit"));

					m_alphaSplitMaterial.SetFloat("_BlackStart", 1f - blackStart);
					m_alphaSplitMaterial.SetFloat("_BlackEnd", 1f - blackEnd);
					m_alphaSplitMaterial.SetFloat("_BlackEndValue", blackStartValue);
					m_alphaSplitMaterial.SetFloat("_BlackEndValue", 1f - blackEndValue);
				}
				else
                {
					m_alphaSplitMaterial = new(Shader.Find("Torappu/UI/AlphaSplit"));
				}
				m_alphaSplitMaterial.SetTexture("_AlphaTex", config.alphaTex);
				m_image.material = m_alphaSplitMaterial;
			}
			m_image.sprite = config.sprite;
		}

        public void SetSprite(AVGCharacterSpriteHub.SpriteConfig config, AVGCharacterSpriteHub.SpriteConfig faceConfig, CharSpriteConfig spriteConfig)
		{
			UIShaderProfile profile = _LoadShaderProfile();
			if (!profile)
            {
				Debug.LogError("[AVG.AlphaSplitImageHolder] Failed to load UI_SHADER_PROFILE!");
            }
			else
            {
				m_alphaSplitMaterial = new(profile.avgCharSplitShader);
				m_alphaSplitMaterial.SetTexture("_AlphaTex", config.alphaTex);
				if (faceConfig != null)
                {
					m_alphaSplitMaterial.SetTexture("_HGDynamicTex", faceConfig.sprite.texture);
					m_alphaSplitMaterial.SetTexture("_HGDynamicAlphaTex", faceConfig.alphaTex);
					m_alphaSplitMaterial.SetTextureScale("_HGDynamicTex", spriteConfig.faceScale);
					m_alphaSplitMaterial.SetTextureOffset("_HGDynamicTex", spriteConfig.faceOffset);
				}
				else
                {
					m_alphaSplitMaterial.SetTextureScale("_HGDynamicTex", Vector2.zero);
					m_alphaSplitMaterial.SetTextureOffset("_HGDynamicTex", new(-1f, -1f));
				}
				m_alphaSplitMaterial.SetFloat("_BlackStart", 1f - spriteConfig.blackStart);
				m_alphaSplitMaterial.SetFloat("_BlackEnd", 1f - spriteConfig.blackEnd);
				m_image.material = m_alphaSplitMaterial;
				m_image.sprite = config.sprite;
			}
		}

		public void Reset()
		{
			m_image.enabled = false;
			m_image.sprite = null;
			if (m_alphaSplitMaterial)
            {
				m_alphaSplitMaterial.SetTexture("_AlphaTex", null);
            }
		}

		private UIShaderProfile _LoadShaderProfile()
		{
			//MODIFY-ONLY-AVG
			//return AVGController.instance.assetLoader.Load<UIShaderProfile>(ResourceUrls.GetUiShaderProfilePath());
			return AVGController.instance.avgUIShaderProfile;
		}

		private Image m_image;
		private Material m_alphaSplitMaterial;

		public enum ShaderLoadType
		{
			AVG,
			UI
		}
	}
}
