Shader "MADFINGER/Transparent/Lighting Off ZBuffer On" { 
   Properties { 
      _Color ("Main Color", Color) = (1,1,1,1) 
      _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {} 
   } 
   Category { 
      ZWrite On 
      Alphatest Greater 0 
      Tags {Queue=Transparent} 
      Blend SrcAlpha OneMinusSrcAlpha 
       
      SubShader { 
         Pass { 
            Lighting Off 
            SetTexture [_MainTex] 
            { 
               constantColor [_Color] 
               Combine texture * constant, texture * constant 
            } 
         } 
      } 
   } 
} 