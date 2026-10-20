// Roof surface: tarmac/gravel sheet with seams, patches and a per-building tint.
//
// Roofs used to share the facade atlas or a white imported material, which is why
// every low building in the city read as a sheet of paper seen from the bus. The
// pattern is world-space, so a 4 m roof and a 40 m roof have the same gravel and
// seam spacing, and there is no texture to alias at distance.
Shader "RealBus/Roof"
{
    Properties
    {
        _RoofTint ("Roof Tone", Color) = (0.30, 0.30, 0.31, 1)
        _SeamColor ("Seam", Color) = (0.20, 0.20, 0.21, 1)
        _GravelStrength ("Gravel Strength", Range(0, 1)) = 0.55
        _SeamSpacing ("Seam Spacing (metres)", Float) = 1.6
        _Smoothness ("Smoothness", Range(0, 1)) = 0.08
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
        LOD 150

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        float4 _RoofTint;
        float4 _SeamColor;
        float _GravelStrength;
        float _SeamSpacing;
        float _Smoothness;

        struct Input
        {
            float3 worldPos;
            float3 worldNormal;
        };

        float Hash21(float2 p)
        {
            p = frac(p * float2(123.34, 456.21));
            p += dot(p, p + 45.32);
            return frac(p.x * p.y);
        }

        // Two octaves of tiling value noise, cheap enough for a whole city.
        float Noise(float2 p)
        {
            float2 i = floor(p);
            float2 f = frac(p);
            f = f * f * (3.0 - 2.0 * f);
            float a = Hash21(i);
            float b = Hash21(i + float2(1, 0));
            float c = Hash21(i + float2(0, 1));
            float d = Hash21(i + float2(1, 1));
            return lerp(lerp(a, b, f.x), lerp(c, d, f.x), f.y);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 p = IN.worldPos.xz;

            // Seams run on a grid like an asphalt sheet or felt roof; each cell is
            // slightly different because the sheet underneath has aged differently.
            float2 g = p / max(0.5, _SeamSpacing);
            float2 edge = min(frac(g), 1.0 - frac(g));
            float seam = 1.0 - smoothstep(0.012, 0.03, min(edge.x, edge.y));
            float cellTone = Hash21(floor(g));

            float gravel = Noise(p * 2.6) * 0.65 + Noise(p * 11.0) * 0.35;
            float patch = smoothstep(0.55, 0.85, Noise(p * 0.09 + 11.0));

            float3 albedo = _RoofTint.rgb * lerp(1.0 - _GravelStrength * 0.35, 1.0 + _GravelStrength * 0.25, gravel);
            albedo *= lerp(0.92, 1.06, cellTone);
            albedo = lerp(albedo, albedo * 0.72, patch * 0.5);
            albedo = lerp(albedo, _SeamColor.rgb, seam * 0.65);

            // The roof plane is only ever seen at a distance or from above; keeping it
            // matte stops it from mirroring the sky as a blue sheet.
            float up = saturate(abs(IN.worldNormal.y));
            albedo *= lerp(0.86, 1.0, up);

            o.Albedo = albedo;
            o.Smoothness = _Smoothness;
            o.Metallic = 0;
            o.Occlusion = 1;
        }
        ENDCG
    }

    FallBack "Standard"
}
