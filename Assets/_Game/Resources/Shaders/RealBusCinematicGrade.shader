// A single-pass cinematic grade for the Built-in Render Pipeline.
//
// The project intentionally ships no URP/HDRP or post-processing package. This
// shader provides the brief's exposure/colour-grading step without pulling in a
// dependency or changing every material: a gentle exposure trim, a small
// contrast/saturation grade, a warm/cool tint, a soft shadow lift and a vignette.
//
// The source is linear in the project's Linear colour space. Keep all of the
// maths in linear and let the hardware convert on the way to the display.
Shader "Hidden/RealBus/CinematicGrade"
{
    Properties
    {
        _MainTex ("Source", 2D) = "white" {}
    }

    SubShader
    {
        Cull Off
        ZWrite Off
        ZTest Always
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #pragma target 3.0
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _Exposure;
            float _Contrast;
            float _Saturation;
            float4 _Tint;
            float _ShadowLift;
            float _Vignette;
            float _VignetteStart;

            float Luminance(float3 color)
            {
                return dot(color, float3(0.2126, 0.7152, 0.0722));
            }

            fixed4 frag (v2f_img i) : SV_Target
            {
                float3 color = tex2D(_MainTex, i.uv).rgb;

                // Exposure is a camera trim, not a second sun. The lighting pass
                // already responds to cloud and the time of day.
                color *= max(0.0, _Exposure);

                // A warm/cool balance, kept close to white so sunlit facades do
                // not clip their red channel.
                color *= _Tint.rgb;

                // Contrast around linear middle grey (0.18). This is subtle by
                // design: the scene has real sun and ambient, not a flat plate.
                color = (color - 0.18) * max(0.5, _Contrast) + 0.18;

                // Saturation in linear luminance. Overcast and night reduce the
                // amount slightly through the C# rules.
                float luma = Luminance(color);
                color = lerp(luma.xxx, color, max(0.0, _Saturation));

                // Lift only the deepest shadows, with a faint cool fill. This
                // keeps a shaded wall from turning into a black hole without
                // washing out the contrast between sunlit and shaded sides.
                float shadowMask = 1.0 - smoothstep(0.0, 0.24, Luminance(color));
                color += _ShadowLift * shadowMask * float3(0.86, 0.94, 1.0);

                // Protect the highlights from hard clipping after the warm tint.
                color = color / (1.0 + max(0.0, max(color.r, max(color.g, color.b)) - 1.0) * 0.22);

                // A soft optical vignette, strongest in the corners and absent
                // from the centre where the road and cockpit instruments live.
                float2 offset = i.uv - 0.5;
                float radius = length(offset) * 1.41421356;
                float vignette = smoothstep(_VignetteStart, 1.0, radius);
                color *= 1.0 - _Vignette * vignette;

                return fixed4(saturate(color), 1.0);
            }
            ENDCG
        }
    }

    Fallback Off
}
