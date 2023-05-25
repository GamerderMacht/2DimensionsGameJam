Shader "Custom/GlitchEffectShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Intensity ("Intensity", Range(0, 1)) = 0.1
    }
 
    SubShader {
        Tags {
            "RenderType"="Opaque"
            "Queue"="Geometry"
        }
        LOD 100
 
        Pass {
            Stencil {
                Ref 1
                Comp always
                Pass replace
            }
 
            Cull Off
            Lighting Off
            ZWrite Off
            ZTest Always
            Blend SrcAlpha OneMinusSrcAlpha
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            #pragma multi_compile_instancing
 
            #include "UnityCG.cginc"
 
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float _Intensity;
 
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target {
                float2 glitchOffset1 = float2(
                    sin(_Time.y) * _Intensity,
                    cos(_Time.x) * _Intensity
                );
                float2 glitchOffset2 = float2(
                    cos(_Time.y) * _Intensity,
                    sin(_Time.x) * _Intensity
                );
 
                fixed4 col = tex2D(_MainTex, i.uv + glitchOffset1) +
                             tex2D(_MainTex, i.uv + glitchOffset2);
                return col / 2.0;
            }
            ENDCG
        }
    }
    FallBack "Universal Render Pipeline/Lit"
}
