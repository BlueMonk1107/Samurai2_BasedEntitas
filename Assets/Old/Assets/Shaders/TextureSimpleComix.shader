Shader "MADFINGER/Diffuse/SimpleComix" { 
Properties {
      _MainTex ("Base (RGB)", 2D) = "white"
      _SecondTex ("Base (RGB)", 2D) = "white"
   }
   SubShader {
      Pass {
            SetTexture [_SecondTex]
            SetTexture [_MainTex] 
      } 
   } 
}
