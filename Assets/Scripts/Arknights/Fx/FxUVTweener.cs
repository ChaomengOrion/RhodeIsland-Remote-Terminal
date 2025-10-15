// Created by ChaomengOrion
// Create at 2022-08-13 14:46:29
// Last modified on 2022-08-13 15:08:12

using UnityEngine;

namespace Torappu.Fx
{
	public class FxUVTweener : MonoBehaviour
	{
		private Material activeMaterial
		{
			get
			{
				if (useSharedMaterial)
                {
					return m_sharedMaterial;
                }
				else
                {
					return m_renderer.materials[0];
                }
			}
		}

		private string secondMapPropertyName
		{
			get
			{
				return string.Format("{0}{1}", secondMapName, "_ST");
			}
		}

		private void Awake()
		{
			m_renderer = GetComponent<Renderer>();
			if (useSharedMaterial)
			{
				Debug.LogWarning("TODO"); //TODO
			}
			if (useSecondMap)
			{
				m_secondMapSTProp = secondMapPropertyName;
				m_secondMapST = activeMaterial.GetVector(m_secondMapSTProp);
			}
		}

		private void OnEnable()
		{
			Vector2 offset;
			if (keepInitOffset)
            {
				offset = activeMaterial.mainTextureOffset;
            }
			else
            {
				offset = Vector2.zero;
			}
			m_v2 = offset;
			if (useSecondMap)
            {
				if (!keepInitOffset)
                {
					m_secondMapST.z = 0f;
					m_secondMapST.w = 0f;
                }
            }
		}

		private void Update()
		{
			if (xspeed != 0f || yspeed != 0f)
            {
				m_v2.x += Time.deltaTime * xspeed;
				m_v2.y += Time.deltaTime * yspeed;
				if (protectMainUV)
                {
					m_v2.x -= (int)m_v2.x;
					m_v2.y -= (int)m_v2.y;
				}
				activeMaterial.mainTextureOffset = m_v2;
			}
			if (useSecondMap && (secondXSpeed != 0f || secondYSpeed != 0f))
            {
				m_secondMapST.z += Time.deltaTime * secondXSpeed;
				m_secondMapST.w += Time.deltaTime * secondYSpeed;
				if (protectSecondUV)
				{
					m_secondMapST.z -= (int)m_secondMapST.z;
					m_secondMapST.w -= (int)m_secondMapST.w;
				}
				activeMaterial.SetVector(m_secondMapSTProp, m_secondMapST);
			}
		}

		private void OnDestroy()
		{
			if (!useSharedMaterial)
            {
				Destroy(activeMaterial);
            }
		}

		private const string MATERIAL_KEY = "FX_UV_TWEENER";

		private const float SPEED_SCALE = 2f;

		public bool keepInitOffset;

		public bool useSharedMaterial;

		public bool protectMainUV;

		public float xspeed;

		public float yspeed;

		[HideInInspector]
		public bool useSecondMap;

		public bool protectSecondUV;

		public string secondMapName = "_DissolveTex";

		public float secondXSpeed;

		public float secondYSpeed;

		private Material m_sharedMaterial;

		private Renderer m_renderer;

		private Vector2 m_v2;

		private Vector4 m_secondMapST;

		private string m_secondMapSTProp;
	}
}