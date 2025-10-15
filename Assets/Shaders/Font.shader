Shader "Custom/Font"
{
    Properties
    {
        [HideInInspector] _MainTex("Main Tex", 2D) = "white" {}

        _StencilComp("Stencil Comparison", float) = 8
		_Stencil("Stencil ID", float) = 0
		_StencilOp("Stencil Operation", float) = 0
		_StencilWriteMask("Stencil Write Mask", float) = 255
		_StencilReadMask("Stencil Read Mask", float) = 255
		_ColorMask("Color Mask", float) = 15
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Opaque" }

            Stencil
            {
                Ref[_Stencil]
                Comp[_StencilComp]
                Pass[_StencilOp]
                ReadMask[_StencilReadMask]
                WriteMask[_StencilWriteMask]
            }

            GrabPass
            {
                "_Grab"
            }

            Cull Off
            Lighting Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
		    ColorMask[_ColorMask]

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f {
                    float4 vertex : POSITION;
                    float4 uvgrab : TEXCOORD0;
                    float2 uvmain : TEXCOORD1;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);

                    #if UNITY_UV_STARTS_AT_TOP
                    float scale = -1.0;
                    #else
                    float scale = 1.0;
                    #endif

                    o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y * scale) + o.vertex.w) * 0.5;
                    o.uvgrab.zw = o.vertex.zw;

                    o.uvmain = TRANSFORM_TEX(v.texcoord, _MainTex);
                    return o;
                }

                sampler2D _Grab;

                half4 frag(v2f i) : COLOR
                {
                    half4 result;
                    half3 c = tex2Dproj(_Grab, UNITY_PROJ_COORD(float4(i.uvgrab.x, i.uvgrab.y, i.uvgrab.z, i.uvgrab.w))).rgb;
                    half l = (c.r + c.g + c.b) / 3;
                    result.rgb = l > 0.5 ? half3(0, 0, 0) : half3(1, 1, 1);
                    result.a = tex2D(_MainTex, i.uvmain).a;
                    return result;
                }
                ENDCG
            }
    }
}