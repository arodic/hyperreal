Shader "wnRainbow/Rainbow"
{
  Properties {
    _Rainbow ("Rainbow Texture", 2D) = "white" {}
    _TintOpacity ("Tint/Opacity", Color) = (1,1,1,0)
    _Fresnel ("Fresnel", Range(0.001, 10)) = 0.5
    _FresnelColor ("Fresnel Color", Color) = (0.5,0.5,0.5,1)
  }
  SubShader {
    Tags {
        "Queue"="Transparent+1"
        "RenderType"="Transparent"
    }
    GrabPass
    {
        "_BackgroundTexture"
    }
    Pass {
      Name "FORWARD"
      // Blend One OneMinusSrcAlpha
      Blend SrcAlpha OneMinusSrcAlpha
      Cull Off
      Lighting Off
      ZWrite Off
      CGPROGRAM

      #pragma target 4.0
      #pragma vertex vert
      #pragma geometry geom
      #pragma fragment frag


      #include "UnityCG.cginc"

      uniform sampler2D _Rainbow;
      uniform float4 _TintOpacity;
      uniform float _Fresnel;
      uniform float4 _FresnelColor;
      uniform float4 _Color;
      uniform float _Resolution;

      uniform sampler2D _BackgroundTexture;

      #include "rainbowGeometry.cginc"

      float4 frag(v2f i, fixed facing : VFACE) : COLOR {
        float3 normalDirection = normalize(i.normalDir);
         if (facing < 0) {
           normalDirection = -normalDirection;
         }
        float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
        fixed3 rainbow = tex2D(_Rainbow, float2(i.uv.x, 0.0)).rgb * _Color.rgb;

        float d = dot(normalDirection, viewDirection);
        float fresnel = pow(1.0 - max(0.0, dot(normalDirection, viewDirection)), _Fresnel) * _FresnelColor.a * 9.0;
        half3 finalColor = (
          rainbow.rgb * _TintOpacity.rgb +
          rainbow.rgb * _TintOpacity.a +
          rainbow.rgb * _FresnelColor.rgb * fresnel
        );
        float alpha = min((1.0 - i.life), fresnel);

        float3 background = tex2Dproj(_BackgroundTexture, i.screenUV - float4(i.viewNormal, 0.0, 0.0) * 0.05 ).rgb;

        // return fixed4(finalColor * alpha, alpha);
        return fixed4(background + finalColor, 1.0);
        // return fixed4(i.sound.w, i.sound.w, i.sound.w, 1.0);
        // return fixed4(i.debug, i.debug, i.debug, 1.0);

      }

      ENDCG
    }
  }
}
