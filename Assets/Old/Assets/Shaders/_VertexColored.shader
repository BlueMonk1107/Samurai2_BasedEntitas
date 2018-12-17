Shader "MADFINGER/Diffuse/Vertex Colored" {
Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
}
 
SubShader {
	Tags {"IgnoreProjector"="True"}
    Pass {
        ColorMaterial AmbientAndDiffuse
        SetTexture [_MainTex] {
            Combine texture * primary, texture * primary
        }
    }
}
 
Fallback " VertexLit", 1
}