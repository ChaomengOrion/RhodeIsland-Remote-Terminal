Shader "RhodeIsland/UICircleMask"
{
	Properties
	{   
		_CenterX("CenterX", float) = 0.5
		_CenterY("CenterY", float) = 0.5
		_Width("Width", float) = 0.5
		_Height("Height", float) = 0.15
		_LengthRate("LengthRate", float) = 0.1
		_LengthRateTransition("LengthRateLerp", float) = 0.01
		_Enable("Enable", Integer) = 1

		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Mask("Base (RGB)", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)

		_StencilComp("Stencil Comparison", float) = 8
		_Stencil("Stencil ID", float) = 0
		_StencilOp("Stencil Operation", float) = 0
		_StencilWriteMask("Stencil Write Mask", float) = 255
		_StencilReadMask("Stencil Read Mask", float) = 255

		_ColorMask("Color Mask", float) = 15
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Stencil
		{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile_local _ UNITY_UI_CLIP_RECT
			#pragma multi_compile_local _ UNITY_UI_ALPHACLIP

			struct appdata_t
			{
				fixed2 uv : TEXCOORD0;
				float4 vertex : POSITION;
				float4 color : COLOR;
			};

			struct v2f
			{
				fixed2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
			};

			sampler2D _MainTex;
			fixed4 _Color;
			sampler2D _Mask;
			half _CenterX;
			half _CenterY;
			half _Width;
			half _Height;
			half _LengthRate;
			half _LengthRateTransition;
			bool _Enable;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif

				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{ 
				half4 c = tex2D(_MainTex, IN.uv) * IN.color;
				if (_Enable)
				{
					half2 pt = IN.uv - half2(_CenterX, (1 - _CenterY));
					half h = (pt.x * pt.x) / (_Width * _Width) + (pt.y * pt.y) / (_Height * _Height); // x^2 / w^2 + y^2 / h^2

					half sub = h - _LengthRate;
					if (h > _LengthRate && sub < _LengthRateTransition)
					{
						c.a = c.a - sub / _LengthRateTransition;
						fixed4 mask = tex2D(_Mask, IN.uv);
						c.a *= mask.a;
						return c;
					}
					else if (h > _LengthRate)
					{
						discard;
					}
				}
				fixed4 mask = tex2D(_Mask, IN.uv);
				c.a *= mask.a;
				return c;
			}
			ENDCG
		}
	}
}