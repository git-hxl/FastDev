//消融效果
Shader "Bigger/2D Effect/Dissolve"
{
	Properties
	{
		_MainTex("Base (RGB), Alpha (A)", 2D) = "black" {}
		_NoiseTex("NoiseTex (R)",2D) = "white"{}
		_DissolveSpeed("DissolveSpeed (Second)",Float) = 1
		_EdgeWidth("EdgeWidth",Range(0,0.5)) = 0.1
		_EdgeColor("EdgeColor",Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags
		{ 
			"RenderType" = "Transparent" 
		}

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _NoiseTex;
			uniform float _DissolveSpeed;
			uniform float _EdgeWidth;
			uniform float4 _EdgeColor;

			float4 frag(v2f_img i) :COLOR
			{
				float DissolveFactor = saturate(_Time.y / _DissolveSpeed);
				float noiseValue = tex2D(_NoiseTex,i.uv).r;
				if (noiseValue <= DissolveFactor)
				{
					discard;
				}
				float4 texColor = tex2D(_MainTex,i.uv);
				float EdgeFactor = saturate((noiseValue - DissolveFactor) / (_EdgeWidth*DissolveFactor));
				float4 BlendColor = texColor * _EdgeColor;
				float4 col = lerp(texColor, BlendColor, 1 - EdgeFactor);
				return col;
			}
			ENDCG
		}
	}

	FallBack Off
}