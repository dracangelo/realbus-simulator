// Player-bus livery paint. Imported bus packs are usually delivered with a
// single untextured white body material, so the repair pass could only give the
// whole shell one flat colour. This shader keeps the body panels' lighting and
// shadows but paints a real transit livery onto them without relying on the
// model's UV atlas: the bands are measured in the bus's own local frame
// (origin, right, forward and up are supplied by BusLiveryPaint). That makes a
// two-tone body, a roof band, a beltline pinstripe and a dirt-darkened skirt
// work on every imported mesh, however its UVs happen to be laid out.
Shader "RealBus/BusPaint"
{
    Properties
    {
        // Kept for tooling that reads the conventional albedo slot (and so a
        // material without it never logs a missing-property error). The zoned
        // paint below is what actually renders.
        _Color ("Fallback Tint", Color) = (1, 1, 1, 1)
        _Zone0 ("Roof", Color) = (0.10, 0.13, 0.18, 1)
        _Zone1 ("Front", Color) = (0.72, 0.12, 0.10, 1)
        _Zone2 ("Left Side", Color) = (0.72, 0.12, 0.10, 1)
        _Zone3 ("Right Side", Color) = (0.72, 0.12, 0.10, 1)
        _Zone4 ("Rear", Color) = (0.10, 0.13, 0.18, 1)
        _Zone5 ("Accent / Wheel Arches", Color) = (0.92, 0.62, 0.06, 1)
        _Smoothness ("Clear-coat Smoothness", Range(0, 1)) = 0.62
        _Metallic ("Metallic", Range(0, 1)) = 0.16
        _ColourDepth ("Colour Depth", Range(0, 1)) = 0.55

        // Set every frame by BusLiveryPaint. Kept as properties so a material
        // instance owns its own bus pose and two buses never share a band.
        _LiveryOrigin ("Livery Origin (world)", Vector) = (0, 0, 0, 0)
        _LiveryRight ("Livery Right (world)", Vector) = (1, 0, 0, 0)
        _LiveryForward ("Livery Forward (world)", Vector) = (0, 0, 1, 0)
        _LiveryUp ("Livery Up (world)", Vector) = (0, 1, 0, 0)
        _BusWidth ("Bus Width (m)", Float) = 2.55
        _BusLength ("Bus Length (m)", Float) = 12
        _BusHeight ("Bus Height (m)", Float) = 3.4
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry+120" }
        LOD 250

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        float4 _Zone0;
        float4 _Zone1;
        float4 _Zone2;
        float4 _Zone3;
        float4 _Zone4;
        float4 _Zone5;
        float _Smoothness;
        float _Metallic;
        float _ColourDepth;

        float4 _LiveryOrigin;
        float4 _LiveryRight;
        float4 _LiveryForward;
        float4 _LiveryUp;
        float _BusWidth;
        float _BusLength;
        float _BusHeight;

        struct Input
        {
            float3 worldPos;
            float3 worldNormal;
        };

        // A narrow, soft-edged band. smoothstep twice gives a rounded edge
        // instead of the hard stair-step a plain step() produces at distance.
        float Band(float value, float centre, float halfWidth, float feather)
        {
            return 1.0 - smoothstep(halfWidth, halfWidth + feather, abs(value - centre));
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // The bus's own frame, not world space: the livery must stay put
            // when the bus turns, pitches or rolls.
            float3 p = IN.worldPos - _LiveryOrigin.xyz;
            float halfWidth = max(0.25, _BusWidth * 0.5);
            float halfLength = max(0.5, _BusLength * 0.5);
            float height = max(0.5, _BusHeight);

            float x = dot(p, _LiveryRight.xyz) / halfWidth;   // -1 (left) .. 1 (right)
            float y = saturate(dot(p, _LiveryUp.xyz) / height); // 0 (skirt) .. 1 (roof)
            float z = dot(p, _LiveryForward.xyz) / halfLength;  // -1 (rear) .. 1 (front)

            // Side base: a mirror-symmetric pair so the left and right flanks
            // can carry different panel colours without a visible seam.
            float3 side = x < 0.0 ? _Zone2.rgb : _Zone3.rgb;
            float3 paint = side;

            // Nose and tail wrap the side colours around the corners. The
            // blend is deliberately wide so the transition reads as a painted
            // corner rather than a texture seam on low-poly meshes.
            float nose = smoothstep(0.48, 0.78, z);
            float tail = smoothstep(0.48, 0.78, -z);
            paint = lerp(paint, _Zone1.rgb, nose);
            paint = lerp(paint, _Zone4.rgb, tail);

            // Roof band. Most imported buses have a separate roof mesh or a
            // roof strip inside the body mesh; both sit above 80% of the
            // body height.
            float roof = smoothstep(0.78, 0.92, y);
            paint = lerp(paint, _Zone0.rgb, roof);

            // Wheel arches and the lower skirt carry the accent, with a soft
            // transition so dirt does not look like a painted stripe.
            float skirt = 1.0 - smoothstep(0.055, 0.19, y);
            paint = lerp(paint, _Zone5.rgb, skirt * 0.9);

            // Double beltline pinstripe below the window line. On a 3.4 m
            // body these sit at roughly 1.15 m and 1.3 m, on the lower body
            // panel rather than across the glass.
            float stripe = saturate(
                Band(y, 0.34, 0.008, 0.012) +
                Band(y, 0.39, 0.006, 0.010));
            paint = lerp(paint, _Zone5.rgb, stripe * 0.92);

            // A subtle forward-rising accent on the flanks: the diagonal
            // sweep that separates a fleet livery from a flat repaint. Kept
            // low-contrast so it works with every palette.
            float sweepLine = 0.34 + 0.09 * z;
            float sweep = Band(y, sweepLine, 0.012, 0.03) * (1.0 - roof) * (1.0 - skirt);
            paint = lerp(paint, _Zone0.rgb, sweep * 0.55 * _ColourDepth);

            // Paint depth: darker at the skirt (road grime) and a little
            // brighter toward the roof (sky light and clear-coat).
            float grime = 1.0 - smoothstep(0.0, 0.24, y);
            paint *= lerp(1.0, 0.74, grime * 0.42);
            float sky = smoothstep(0.45, 1.0, y);
            paint *= lerp(0.97, 1.04, sky);

            // Very low-frequency panel variation. A high-frequency hash would
            // crawl as the bus moves; these broad sinusoidal bands stay stable
            // and just stop the paint reading as a single flat fill.
            float panel = sin(z * 5.5 + y * 1.7) * 0.5 + 0.5;
            paint *= lerp(1.0, 1.012, panel * _ColourDepth);

            o.Albedo = paint;
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness * lerp(0.78, 1.0, sky);
            o.Occlusion = 1.0;
        }
        ENDCG
    }

    FallBack "Standard"
}
