Shader "MADFINGER/Diffuse/SimpleCutOff" { 
   Properties { 
      _MainTex ("Base (RGB)", 2D) = "white" {}
      //_Shininess ("Shininess", Range (0.1, 1)) = 0.7
      _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
   } 
   SubShader { 
      Pass { 
      	 Alphatest Greater [_Cutoff] 
         SetTexture [_MainTex] 
      } 
   } 
}