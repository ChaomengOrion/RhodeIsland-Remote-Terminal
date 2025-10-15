Shader "Torappu/Particles/Optimize/Dissolve AB MultiTex" {
	Properties {
		_TintColor ("Tint Color 1", Vector) = (0.5,0.5,0.5,0.5)
		_TintColorB ("Tint Color 2", Vector) = (0.5,0.5,0.5,0.5)
		_TintColorC ("Tint Color 3", Vector) = (0.5,0.5,0.5,0.5)
		_MainTex ("MainTex", 2D) = "white" {}
		_MainTexSpeed ("MainTex Tween Speed(:xy)", Vector) = (0,0,0,0)
		_DissolveTex ("DissolveTex 1 (Blend)", 2D) = "white" {}
		_DissolveTexB ("DissolveTex 2 (Blend)", 2D) = "white" {}
		_DissolveTexC ("DissolveTex 3 (Add)", 2D) = "white" {}
		_TexSpeed ("Tween Speed 1( main:xy, dissolve1:zw)", Vector) = (0,0,0,0)
		_TexSpeed2 ("Tween Speed 2( dissolve2:xy, dissolve3:zw)", Vector) = (0,0,0,0)
		_Amount ("Amount (xyz ,[0,1])", Vector) = (0.5,0.5,0.5,0.5)
		_BorderWidth ("Border Width (xyz, [0.001,1])", Vector) = (0.1,0.1,0.1,0.1)
		[Enum(Off, 0, On, 4)] _ZTest ("ZTest", Float) = 4
		[KeywordEnum(ONE, TWO, THREE)] _Dissolve ("Dissolve Mode", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}