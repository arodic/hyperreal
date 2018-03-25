Shader "custom/glitch" {
    Properties {
        _Texture ("Base Texture", 2D) = "white" {}
        _Glitch ("Glitch", Range(0, 1)) = 0.0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "lib/noiseSimplex.cginc"
            #include "lib/curlNoise.cginc"
            #pragma target 4.0
            uniform sampler2D _Texture;
            uniform float _Glitch;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float3 pos1 : TEXCOORD1;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos1 = v.vertex;
                float3 sn1 = snoise3(v.vertex * 1.0 + float3(0.0, _Time.y * 0.1, 0.0) * 10.) * _Glitch * 1.0;
                float3 sn2 = sn1 * snoise3(v.vertex * 5.0 + float3(0.0, _Time.y * 0.2, 0.0) * 10.) * _Glitch * 1.0;
                o.pos = UnityObjectToClipPos( v.vertex + sn2 );
                return o;
            }
            [maxvertexcount(3)]
            void geom(triangle VertexOutput input[3], inout TriangleStream<VertexOutput> OutputStream)
            {
              VertexOutput g0 = input[0];
              VertexOutput g1 = input[1];
              VertexOutput g2 = input[2];
              float sn = (0.5 * snoise(g2.pos1 * 100.0) + .5);
              if (sn < (_Glitch * 1.05)) return;
              // if (distance(g0.pos, g1.pos) > 10.0) return;
              // if (distance(g1.pos, g2.pos) > 10.0) return;
              OutputStream.Append(g0);
              OutputStream.Append(g1);
              OutputStream.Append(g2);
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 color = tex2D(_Texture, i.uv0);
                return color;
            }
            ENDCG
        }
    }
}
