// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MADFINGER/CG/Diffuse/Simple" { 
Properties {
_MainTex ("Base (RGB)", 2D) = "white"
}
SubShader {
Pass {
		 CGPROGRAM
		 #pragma vertex vert
		 #pragma fragment frag
		 #include "UnityCG.cginc"
		 struct v2f {
		 	float4 pos : SV_POSITION;
		 	half2 uv : TEXCOORD0;
		 };
		 v2f vert (appdata_base v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord;
			return o;
		 }
		 sampler2D _MainTex;
		 fixed4 frag (v2f i) : COLOR0 {
		 	return tex2D (_MainTex, i.uv);
		 }
		 ENDCG
		} 
	} 
}