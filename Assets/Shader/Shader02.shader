Shader "Unlit/Shader02"
{
    Properties
    {
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}
        _Tint ("Tint", Color) = (1,1,1,1)
        _KecepatanDenyut ("Kecepatan Denyut", Float) = 3
        _KecepatanGulir ("Kecepatan Gulir", Float) = 0
    }
    SubShader
    {
        Tags {
            "RenderType"="Transparent"
            "RenderPipeline" = "UniversalPipeline"
            "Queue"="Transparent"
            "IgnoreProjector"="True"
        }
        
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            Name "Unlit"
            Tags { "LightMode"="UniversalForward" }
            
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            Texture2D _MainTex;
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _Tint;
                float _KecepatanDenyut;
                float _KecepatanGulir;
            CBUFFER_END

            struct attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // ========== Lanjutan dari Pass ==========
            // ... (kode sebelumnya tetap sama)

            // ========== Bagian ini ditambahkan ==========
            
            varyings vert(attributes input)
            {
                varyings output;
                
                // Konversi posisi dari object space ke clip space
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                
                // Transformasi UV dengan tiling & offset
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                
                // Tambahkan efek gulir horizontal (scroll)
                float waktu = _Time.y; // Waktu dalam detik
                output.uv.x += _KecepatanGulir * waktu;
                
                return output;
            }

            float4 frag(varyings input) : SV_Target
            {
                // Sampling texture
                float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
                
                // Efek denyut (pulse) menggunakan sin waktu
                float pulse = 1.0 + 0.1 * sin(_Time.y * _KecepatanDenyut);
                
                // Aplikasikan tint dan efek denyut ke warna
                float4 finalColor = texColor * _Tint * pulse;
                
                return finalColor;
            }

            ENDHLSL
        }
    }
    // Fallback untuk kompatibilitas
    Fallback "Sprites/Default"
}
