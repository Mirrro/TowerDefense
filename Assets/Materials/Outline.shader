Shader "Custom/Outline" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Float) = 1.0
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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
            float4 _OutlineColor;
            float _OutlineThickness;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 color = tex2D(_MainTex, i.uv);

                // Edge detection
                float2 size = float2(_OutlineThickness, _OutlineThickness) / _ScreenParams.xy;
                fixed4 diff1 = tex2D(_MainTex, i.uv + float2(size.x, 0)) - color;
                fixed4 diff2 = tex2D(_MainTex, i.uv + float2(0, size.y)) - color;

                // Simple threshold for edge detection
                float edgeThreshold = 0.1;
                if (abs(diff1.r) + abs(diff1.g) + abs(diff1.b) > edgeThreshold ||
                    abs(diff2.r) + abs(diff2.g) + abs(diff2.b) > edgeThreshold) {
                    color = _OutlineColor;
                }

                return color;
            }
            ENDCG
        }
    }
}
