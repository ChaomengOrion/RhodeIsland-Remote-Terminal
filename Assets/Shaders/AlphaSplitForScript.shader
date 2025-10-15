Shader "RhodeIsland/AlphaSplistForScript"
{
    Properties
    {
        _AlphaTex ("Sprite Alpha Texture", 2D) = "white" { }
        _MainTex ("Sprite Texture", 2D) = "white" { }
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

              CGPROGRAM
              #pragma vertex vert 
			  #pragma fragment frag

			  struct a2v
			  {
				  float4 vertex : POSITION;
				  float2 uv : TEXCOORD;
			  };

   	  		  struct v2f
			  {
				  float4 clip_pos : SV_POSITION;
				  float2 uv : TEXCOORD;
				  float4 uv2 : TEXCOORD1;
			  };

			  half4 _Color;

			  v2f vert(a2v v)
			  {
				  v2f o;
                  o.clip_pos = UnityObjectToClipPos(v.vertex);
                  o.uv = v.uv;
                  o.uv2 = v.vertex;
				  return o;
			  }

              sampler2D _MainTex;
              sampler2D _AlphaTex;

			  half4 frag(v2f i) : SV_Target
			  {
                  float4 main;
                  main.xyz = tex2D(_MainTex, i.uv.xy);
                  main.w = tex2D(_AlphaTex, i.uv.xy).x;
                  return main;
			  }
              ENDCG
        }
    }
}