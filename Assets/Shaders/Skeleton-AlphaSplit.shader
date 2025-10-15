Shader "RhodeIsland/Spine/Skeleton-AlphaSplit"
{
    Properties
    {
        _Color ("Tint Color", Color) = (1, 1, 1, 1)
        _MainTex ("Texture to blend", 2D) = "black" { }
        _AlphaTex ("Alpha Texture", 2D) = "white" { }
    }
    SubShader
    {
        LOD 100
        Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
        Pass {
            LOD 100
            Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
            Blend One OneMinusSrcAlpha, One OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            Fog { Mode Off }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;
            sampler2D _MainTex;
            sampler2D _AlphaTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;
                col.xyz = tex2D(_MainTex, i.uv).xyz;
                col.w = tex2D(_AlphaTex, i.uv).x;
                col = col * _Color;
                return col;
            }
            ENDCG
        }
    }
}