//投影仪使用shader
Shader "Bigger/2D Effect/Projector" {
	Properties{
		_ShadowTex("Cookie", 2D) = "gray" {}
		_FalloffTex("FallOff", 2D) = "white" {}
	}
	Subshader{
		Tags{ "Queue" = "Transparent" }
		Pass{
			ZWrite Off
			ColorMask RGB
			Blend DstColor Zero
			Blend SrcAlpha OneMinusSrcAlpha
			Offset -2,-2

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"

			struct v2f {
				float4 uvShadow : TEXCOORD0;
				float4 uvFalloff : TEXCOORD1;
				UNITY_FOG_COORDS(2)
				float4 pos : SV_POSITION;
			};

			float4x4 unity_Projector;
			float4x4 unity_ProjectorClip;

			v2f vert(float4 vertex : POSITION)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(vertex);
				o.uvShadow = mul(unity_Projector, vertex);
				o.uvFalloff = mul(unity_ProjectorClip, vertex);
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}

			sampler2D _ShadowTex;
			sampler2D _FalloffTex;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 texS = tex2Dproj(_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
				fixed4 res = texS;

				UNITY_APPLY_FOG_COLOR(i.fogCoord, res, fixed4(1,1,1,1));
				return res;
			}
			ENDCG
		}
	}
}