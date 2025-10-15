// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using RhodeIsland.Arknights.Resource;
using UnityEngine;
//using XLua;
using RhodeIsland.Arknights;

namespace Torappu.UI
{
	public class AutoPackSpriteHub : ScriptableObject, /*IHotfixable,*/ ISpriteHub
	{
		private void _InitMapIfNot()
		{
			if (m_map == null)
			{
				if (_keys == null)
				{
					_keys = new();
				}
				if (_values == null)
				{
					_values = new();
				}
				if (_values.Count != _keys.Count)
				{
					Debug.LogError("Data error, keys and values are not same size!");
				}
				m_map = new(StringComparer.OrdinalIgnoreCase);
				for (int i = 0; i < _keys.Count && i < _values.Count; i++)
				{
					m_map[_keys[i]] = _values[i];
				}
			}
		}

		//EMPTY
		public void Bake() { }

		public bool ContainsKey(string key)
		{
			_InitMapIfNot();
			if (!string.IsNullOrEmpty(key))
			{
				return m_map.ContainsKey(key);
			}
			return false;
		}

		public bool TryGetValue(string key, out string value)
		{
			_InitMapIfNot();
			if (string.IsNullOrEmpty(key))
			{
				value = null;
				return false;
			}
			else
			{
				return m_map.TryGetValue(key, out value);
			}
		}

		public bool TryLoadSprite(string id, AbstractAssetLoader assetLoader, out Sprite result)
		{
			result = null;
			if (assetLoader == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(id))
			{
				return false;
			}
			if (!TryGetValue(id, out string value))
			{
				return false;
			}
			result = assetLoader.Load<Sprite>(value);
			return result;
		}

		//ADD
		public bool TryLoadSprite(string id, out Sprite result)
		{
			result = null;
			if (string.IsNullOrEmpty(id))
			{
				return false;
			}
			if (!TryGetValue(id, out string value))
			{
				return false;
			}
			result = ResourceManager.Load<Sprite>(value);
			return result;
		}

		private const int DEFAULT_MAX_FILE_SIZE = 1048576;

		[SerializeField]
		private string _rootPackingTag;

		[SerializeField]
		private int _cntPerAtlas = -1;

		[SerializeField]
		private AutoPackSpriteHub.PicSize _standardPicSize;

		[SerializeField]
		private List<AutoPackSpriteHub.PicSize> _extraPicSize;

		[SerializeField]
		private AutoPackSpriteHub.CompressType _compressType;

		[SerializeField]
		private AutoPackSpriteHub.AtlasSize _atlasSize;

		[SerializeField]
		private AutoPackSpriteHub.MeshType _meshType;

		[SerializeField]
		private bool _useCntPerAtlas = true;

		[SerializeField]
		private int _maxFileSize = 100000;
		
		[SerializeField]
		private AutoPackSpriteHub.TexSize _maxTextureSize;

		[SerializeField]
		private string _extraTagRegex;

		[SerializeField]
		private AutoPackSpriteHub.ExtractTagConfig _extractTagConfig;

		[SerializeField]
		[HideInInspector]
		private string _atlasOutputPath = string.Empty;

		[SerializeField]
		[HideInInspector]
		private string _configName = string.Empty;

		[SerializeField]
		private List<string> _keys;

		[SerializeField]
		private List<string> _values;

		private Dictionary<string, string> m_map;

		public struct AtlasSettings
		{
			public string assetPath;
			public Sprite sprite;
			public int maxSize;
		}

		public enum CompressType
		{
			DEFAULT_PVRTC_ETC1,
			IOS_TRUE_COLOR_ANDROID_ETC1,
			NO_ALPHA_SPLIT_PVRTC_ETC2,
			OPAQUE_PVRTC_ETC1,
			AUTO_ALPHA_PVRTC_ETC2
		}

		public enum AtlasSize
		{
			DEFAULT_2048,
			MEDIUM_1024,
			SMALL_512
		}

		public enum MeshType
		{
			DONT_CHANGE,
			FULL_RECT,
			TIGHT
		}

		public enum TexSize
		{
			NONE,
			MEDIUM_1024,
			LARGE_2048
		}

		[Serializable]
		private struct PicSize
		{
			public bool IsValid()
			{
				return width > 0 && height > 0;
			}

			public bool Validate(Rect rect)
			{
				return default(bool);
			}

			public int width;

			public int height;
		}


		[Serializable]
		private struct ExtractTagConfig
		{
			public bool CheckIfEmpty()
			{
				if (!rules.IsNullOrEmpty())
				{
					foreach (Rule item in rules)
					{
						if (item != null && !item.IsEmpty())
						{
							return false;
						}
					}
				}
				return true;
			}

			public string ExtractTag(string assetPath)
			{
				int count = rules.SafeCount();
				if (string.IsNullOrEmpty(assetPath) || count <= 0)
				{
					return string.Empty;
				}
				else
				{
					for (int i = 0; i < count; i++)
					{
						Rule rule = rules[i];
						if (rule != null)
                        {
							string res = rule.ExtractTag(assetPath);
							if (!string.IsNullOrEmpty(res))
                            {
								return res;
                            }
                        }
					}
					return string.Empty;
                }
			}

			public List<Rule> rules;

			[Serializable]
			public class Rule
			{
				public bool IsEmpty()
				{
					return default(bool);
				}

				public string ExtractTag(string assetPath)
				{
					if (m_regex == null)
					{
						m_regex = new(pattern, RegexOptions.IgnoreCase);
					}
					
					return null;
				}

				public string regex = string.Empty;

				public string pattern;

				[NonSerialized]
				private Regex m_regex;
			}
		}
	}
}
