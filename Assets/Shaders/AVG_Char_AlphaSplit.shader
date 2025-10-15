Shader "Torappu/UI/AVG_Char_AlphaSplist"
{
    Properties
    {
        _HGDynamicTex ("Dynamic Texture", 2D) = "black" { }
        _HGDynamicAlphaTex ("Dynamic Texture Alpha Mask", 2D) = "black" { }
        _AlphaTex ("Sprite Alpha Texture", 2D) = "white" { }
        _MainTex ("Sprite Texture", 2D) = "white" { }
        _Color ("Tint", Color) = (1,1,1,1)
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
        _BlackStart ("Black Mask Start", Float) = 0
        _BlackEnd ("Black Mask End", Float) = 0
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
        _SoftMask ("Mask", 2D) = "white" { }
    }
    SubShader
    {
         Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
         Pass
         {
              Name "DEFAULT"
              Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
              Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
              ZTest Off
              ZWrite Off
              Cull Off
              Stencil {
                   ReadMask 0
                   WriteMask 0
                   Pass Keep
                   Fail Keep
                   ZFail Keep
              }

              CGPROGRAM
              #pragma vertex vert 
			  #pragma fragment frag

			  struct a2v
			  {
				  float4 vertex : POSITION;
                  float4 color : COLOR;
				  float2 uv : TEXCOORD;
			  };

   	  		  struct v2f
			  {
				  float4 clip_pos : SV_POSITION;
				  half4 color : COLOR;
				  float4 uv : TEXCOORD;
				  float4 uv2 : TEXCOORD1;
			  };

			  half4 _Color;
			  half4 _HGDynamicTex_ST;

			  v2f vert(a2v v)
			  {
				  v2f o;
                  o.clip_pos = UnityObjectToClipPos(v.vertex);
                  o.color = v.color * _Color;
                  o.uv.zw = v.uv.xy * _HGDynamicTex_ST.xy + _HGDynamicTex_ST.zw;
                  o.uv.xy = v.uv.xy;
                  o.uv2 = v.vertex;
				  return o;
			  }

              half4 _TextureSampleAdd;
              float4 _ClipRect;
              float _BlackStart;
              float _BlackEnd;
              sampler2D _MainTex;
              sampler2D _AlphaTex;
              sampler2D _HGDynamicTex;
              sampler2D _HGDynamicAlphaTex;

			  half4 frag(v2f i) : SV_Target
			  {
                  float4 main;
                  main.xyz = tex2D(_MainTex, i.uv.xy) + _TextureSampleAdd.xyz;
                  main.w = tex2D(_AlphaTex, i.uv.xy).x + _TextureSampleAdd.w;
                  bool face = i.uv.z >= 0 && i.uv.w >= 0 && 1 >= i.uv.z && 1 >= i.uv.w;
                  half4 raw;
                  if (face)
                  {
                      float4 dynamic;
                      dynamic.xyz = tex2D(_HGDynamicTex, i.uv.zw) + _TextureSampleAdd.xyz;
                      dynamic.w = tex2D(_HGDynamicAlphaTex, i.uv.zw).x + _TextureSampleAdd.w;
                      raw = dynamic * i.color;
                  }
                  else
                      raw = main * i.color;
                  bool black = _BlackEnd < _BlackStart && 1 >= _BlackStart && _BlackEnd >= 0;
                  half4 result;
                  result.xyz = black ? raw.xyz * clamp((_BlackStart - i.uv.y) / _BlackStart - _BlackEnd, 0, 1) : raw.xyz;
                  result.w = raw.w * (i.uv2.x >= _ClipRect.x && i.uv2.y >= _ClipRect.y && _ClipRect.z >= i.uv2.x && _ClipRect.w >= i.uv2.y);
                  return result;
			  }
              ENDCG
        }
    }
}