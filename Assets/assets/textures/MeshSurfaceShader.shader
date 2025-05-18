Shader "Custom/MeshSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
            float4 color : COLOR; // Vertex color
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // If no vertex color exists, default to gray (0.5, 0.5, 0.5)
            fixed4 finalColor = tex2D(_MainTex, IN.uv_MainTex) * _Color * (IN.color.rgb == float3(0,0,0) ? float3(0.5, 0.5, 0.5) : IN.color.rgb);
            o.Albedo = finalColor.rgb;
            o.Alpha = finalColor.a;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}