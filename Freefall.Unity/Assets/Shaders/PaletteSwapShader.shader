﻿Shader "Custom/PaletteSwapShader" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_PaletteTex ("Palette (4-color RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { 
			"Queue"="Transparent" 
			"IgnoreProjector"="True"
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha
		
		Pass {
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				half2 texcoord : TEXCOORD0;
			};
			
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
				
				#if PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif
				
				return OUT;
			}
			
			sampler2D _MainTex;
			sampler2D _PaletteTex;
			
			fixed4 frag(v2f IN): COLOR 
			{
				half4 tc = tex2D(_MainTex, IN.texcoord);
				
				float uv_p_x = tc.r < .25 ? 0.1 : (tc.r < .5 ? 0.51 : (tc.r < .75 ? 0.1 : 0.51));
				float uv_p_y = tc.r < .25 ? 0.51 : (tc.r < .5 ? 0.51 : (tc.r < .75 ? 0.1 : 0.1));
				float2 uv_p = float2(uv_p_x, uv_p_y);
				half4 pc = tex2D(_PaletteTex, uv_p);
				
				//return fixed4(uv_p_x, uv_p_x, uv_p_x, tc.a);
				//return fixed4(uv_p_y, uv_p_y, uv_p_y, tc.a);
				//return fixed4(tc.r, tc.g, tc.b, tc.a);
				return fixed4(pc.r, pc.g, pc.b, tc.a);
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
