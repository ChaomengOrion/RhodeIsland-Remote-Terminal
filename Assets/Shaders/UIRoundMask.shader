//对图片的mask处理,只有椭圆部分
Shader "Unlit/UIRoundMask"
{
	Properties
	{
		_MainTex("Base (RGB), Alpha (A)", 2D) = "black" {}
		_CenterX("CenterX", float) = 0.5
		_CenterY("CenterY", float) = 0.5
		_Width("Width", float) = 0.5
		_Height("Height", float) = 0.15
		_LengthRate("LengthRate", float) = 0.1
		_LengthRateTransition("LengthRateLerp", float) = 0.01
	}

	SubShader
	{
		LOD 200

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}

		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Fog{ Mode Off }
			//Offset -1,-1
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag            
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			half4 _MainTex_ST;
			half _CenterX;
			half _CenterY;
			half _Width;
			half _Height;
			half _LengthRate;
			half _LengthRateTransition;

			struct appdata_t
			{
				half4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};

			struct v2f
			{
				half4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};

			v2f o;

			v2f vert(appdata_t v)
			{
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				o.color = v.color;
				return o;
			}

			fixed4 frag(v2f IN) : COLOR
			{
				half2 pt = IN.texcoord - half2(_CenterX,(1 - _CenterY));
				half h = (pt.x * pt.x) / (_Width * _Width) + (pt.y * pt.y) / (_Height * _Height); // x^2 / w^2 + y^2 / h^2

				half4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
				half sub = _LengthRate - h;
				if (h < _LengthRate && sub < _LengthRateTransition)
				{
					c.a = c.a - sub / _LengthRateTransition;
					return c;
				}
				else if (h < _LengthRate)
				{
					discard;
				}
				return c;
			}
			ENDCG
		}
	}


	SubShader
	{
		LOD 100

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}

		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Fog{ Mode Off }
			//Offset - 1,-1
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMaterial AmbientAndDiffuse

			SetTexture[_MainTex]
			{
				Combine Texture * Primary
			}
		}
	}
}
