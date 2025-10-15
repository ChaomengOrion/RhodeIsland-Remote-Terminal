Shader "RhodeIsland/RemoteTerminal/UI/ParticleAB"
{
    Properties
    {
        _ColorA("A", Color) = (0, 0, 0, 0.5)
        _ColorB("B", Color) = (1, 1, 1, 0.5)
    }
    SubShader
    {
        Tags
        { 
            "RenderType" = "Opaque"
            "Queue" = "Transparent"
        }

        Pass
        {
            Cull back

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            float4 _ColorA;

            float4 vert(float4 v : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(v);
            }

            float4 frag(float4 v : SV_POSITION) : SV_Target
            {
                return _ColorA;
            }
            ENDCG
        }

        Pass
        {
            Cull front

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            float4 _ColorB;

            float4 vert(float4 v : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(v);
            }

            float4 frag(float4 v : SV_POSITION) : SV_Target
            {
                return _ColorB;
            }
            ENDCG
        }
    }
}
