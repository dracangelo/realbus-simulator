// Procedural city facade.
//
// The old material tiled one 2048px atlas across every wall, so a 60 m tower and a
// 6 m shed showed the same window rows at different scales — and at any distance the
// window grid aliased into moving noise. This shader draws the facade instead:
//
//   * UVs from the mesh are one unit per storey along the wall and one unit per
//     floor vertically (see OSMBuildingMeshBuilder.BuildExtrudedMesh).
//   * Windows, frames, sills, floor bands and a ground-floor shopfront are analytic
//     shapes, so their edges are anti-aliased with fwidth and stay crisp at any
//     resolution. Once a storey is smaller than a few pixels the mask fades to the
//     average wall/glass mix, which removes moiré shimmer entirely.
//   * _Variant, set per building from a hash of its OSM id, selects punched windows,
//     a ribbon curtain wall or twin narrow windows. _FacadeTint is the building tint
//     (a distinct name from _Color so a per-renderer property block does not also
//     recolour the Standard-shader roof props sharing the renderer).
//   * _BaseY / _AoHeight give the wall foot a contact-shadow gradient so buildings
//     sit on the pavement instead of floating on flat ambient light.
Shader "RealBus/Facade"
{
    Properties
    {
        _MainTex ("Facade (mesh UVs only)", 2D) = "white" {}
        _FacadeTint ("Facade Tint", Color) = (0.72, 0.70, 0.66, 1)
        _GlassColor ("Glass", Color) = (0.09, 0.115, 0.15, 1)
        _SkyColor ("Window Sky Reflection", Color) = (0.42, 0.52, 0.65, 1)
        _TrimColor ("Frame / Sill", Color) = (0.46, 0.45, 0.42, 1)
        _Variant ("Facade Variant (0-1)", Float) = 0.35
        _Shopfront ("Ground Floor Shopfront", Range(0, 1)) = 0
        _Smoothness ("Smoothness", Range(0, 1)) = 0.18
        _BaseY ("Building Base Y (world)", Float) = 0
        _AoHeight ("Base Shadow Height (metres)", Float) = 3.5
        _AoStrength ("Base Shadow Strength", Range(0, 1)) = 0.5
        _GrimeStrength ("Ground Grime", Range(0, 1)) = 0.28
        _TopShade ("Eave Shade Strength", Range(0, 1)) = 0.15
        _WallTex ("Wall Detail Atlas", 2D) = "gray" {}
        _WallNormal ("Wall Detail Normal", 2D) = "bump" {}
        _WallTile ("Wall Detail Repeats Per Storey", Float) = 2
        _WallDetail ("Wall Detail Strength", Range(0, 1)) = 0.38
        _WallNormalStrength ("Wall Normal Strength", Range(0, 2)) = 0.65
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _WallTex;
        sampler2D _WallNormal;
        float4 _FacadeTint;
        float4 _GlassColor;
        float4 _SkyColor;
        float4 _TrimColor;
        float _Variant;
        float _Shopfront;
        float _Smoothness;
        float _BaseY;
        float _AoHeight;
        float _AoStrength;
        float _GrimeStrength;
        float _TopShade;
        float _WallTile;
        float _WallDetail;
        float _WallNormalStrength;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        float Hash21(float2 p)
        {
            p = frac(p * float2(123.34, 456.21));
            p += dot(p, p + 45.32);
            return frac(p.x * p.y);
        }

        // Soft rectangle in cell space; `aa` is the pixel footprint in cell units.
        float Rect(float2 p, float2 lo, float2 hi, float aa)
        {
            float2 w = max(aa, 0.0015);
            float mask = smoothstep(lo.x - w, lo.x + w, p.x) * (1.0 - smoothstep(hi.x - w, hi.x + w, p.x));
            mask *= smoothstep(lo.y - w, lo.y + w, p.y) * (1.0 - smoothstep(hi.y - w, hi.y + w, p.y));
            return mask;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 cell = IN.uv_MainTex;
            float2 f = frac(cell);
            // Pixel size in storeys. Small values mean the window grid is resolvable;
            // above ~0.25 a storey is 4 px or less and the pattern must fade.
            float aa = max(fwidth(cell.x), fwidth(cell.y));
            float far = saturate((aa - 0.18) * 2.2);

            float floorId = floor(cell.y);
            float columnId = floor(cell.x);

            // ── Window aperture, chosen by the building's variant ─────────────
            float outer = Rect(f, float2(0.28, 0.26), float2(0.72, 0.80), aa);
            outer = lerp(outer, Rect(f, float2(0.05, 0.34), float2(0.95, 0.82), aa), step(_Variant, 0.34));
            float twin = step(0.72, _Variant);
            float twinMask = max(Rect(f, float2(0.14, 0.26), float2(0.40, 0.80), aa),
                                 Rect(f, float2(0.60, 0.26), float2(0.86, 0.80), aa));
            outer = lerp(outer, twinMask, twin);

            // Ground floor: a shopfront is a wide glazed band with a fascia sign
            // above it, which is what actually sells a street at bus height.
            float shopCell = _Shopfront * (1.0 - smoothstep(0.94, 1.0, cell.y));
            float shopGlass = Rect(f, float2(0.03, 0.05), float2(0.97, 0.70), aa) * shopCell;
            float fascia = Rect(f, float2(0.02, 0.76), float2(0.98, 0.96), aa) * shopCell;
            outer = max(outer * (1.0 - shopCell), shopGlass * 0.999);

            // Frame band just outside the glass; the glass itself is inset.
            float inner = Rect(f, float2(0.31, 0.29), float2(0.69, 0.77), aa);
            inner = lerp(inner, Rect(f, float2(0.07, 0.37), float2(0.93, 0.79), aa), step(_Variant, 0.34));
            inner = lerp(inner, max(Rect(f, float2(0.16, 0.29), float2(0.38, 0.77), aa),
                                     Rect(f, float2(0.62, 0.29), float2(0.84, 0.77), aa)), twin);
            inner = max(inner * (1.0 - shopCell), Rect(f, float2(0.055, 0.075), float2(0.945, 0.66), aa) * shopCell);

            // Shopfront mullions and door.
            float mullion = shopCell * (1.0 - smoothstep(0.0, 0.03, abs(f.x - 0.25))) +
                            shopCell * (1.0 - smoothstep(0.0, 0.03, abs(f.x - 0.75)));
            mullion = saturate(mullion);
            inner *= 1.0 - mullion * 0.9;
            float door = Rect(f, float2(0.40, 0.05), float2(0.60, 0.56), aa) * shopCell;
            inner *= 1.0 - door * 0.55;

            // Fade the whole pattern to its average once it is smaller than a pixel:
            // the flat average colour is what a mipmap would converge to, but without
            // sampling a texture every frame.
            float coverage = lerp(0.18, 0.55, smoothstep(0.0, 0.6, _Variant)) * (1.0 - 0.35 * shopCell);
            inner = lerp(inner, coverage, far);
            outer = lerp(outer, coverage * 1.15, far);
            fascia = lerp(fascia, coverage * 0.35, far);

            // ── Wall ──────────────────────────────────────────────────────────
            float grain = Hash21(float2(columnId * 0.37 + floorId * 1.7, columnId + floorId * 3.1));

            // Wall material: one cell of the four-way atlas (brick, concrete panels,
            // stucco, stone) chosen from the same id hash that picks the window
            // rhythm, repeated per storey. Two neighbouring blocks rarely share both.
            float tileIndex = min(3.0, floor(_Variant * 4.0));
            float2 tileOrigin = float2(fmod(tileIndex, 2.0) * 0.5, floor(tileIndex * 0.5) * 0.5);
            float2 wallLocal = frac(f * max(1.0, _WallTile));
            float2 wallUV = tileOrigin + wallLocal * 0.5;
            float3 detail = tex2D(_WallTex, wallUV).rgb;
            // Fade to flat once a storey is a few pixels, so the atlas never sparkles.
            detail = lerp(detail, 1.0, far);
            float3 wallNormal = tex2D(_WallNormal, wallUV).rgb * 2.0 - 1.0;

            float3 wall = _FacadeTint.rgb * lerp(1.0, detail, _WallDetail) * lerp(0.93, 1.06, grain);
            // A slightly darker band where each floor meets the next reads as a
            // slab edge and keeps tall walls from looking like one stretched sheet.
            wall *= lerp(0.88, 1.0, smoothstep(0.0, 0.10, f.y));

            // ── Glass ─────────────────────────────────────────────────────────
            float windowNoise = Hash21(float2(columnId, floorId));
            float3 glass = lerp(_GlassColor.rgb, _SkyColor.rgb, 0.30 + 0.45 * saturate((f.y - 0.3) * 1.6));
            glass *= lerp(0.86, 1.12, windowNoise);
            // Reflected sky band across the top third of each pane. Without this the
            // windows read as black holes punched in a wall.
            glass += _SkyColor.rgb * (0.22 + 0.12 * windowNoise) * smoothstep(0.35, 0.95, f.y);

            float3 albedo = wall;
            albedo = lerp(albedo, _TrimColor.rgb * lerp(0.82, 1.05, grain), outer * 0.9);
            albedo = lerp(albedo, glass, inner);
            // Fascia boards are painted, one per shop, varied by variant.
            albedo = lerp(albedo, _FacadeTint.rgb * lerp(0.45, 0.75, _Variant), fascia);

            // ── Contact shadow at the base and shade under the eaves ──────────
            float heightAboveBase = max(0.0, IN.worldPos.y - _BaseY);
            float contact = lerp(1.0 - _AoStrength, 1.0, saturate(heightAboveBase / max(0.5, _AoHeight)));
            // The facade is only ever drawn on the vertical wall submesh, so the wall
            // factor is constant. (Reading worldNormal here would force INTERNAL_DATA
            // on the Input struct as soon as surf() writes o.Normal.)
            float vertical = 1.0;
            // Keep the contact shadow visible in the albedo but let the Standard
            // lighting model darken only the indirect term through o.Occlusion, so
            // the wall does not turn black where direct sunlight still reaches it.
            albedo *= lerp(1.0, contact, 0.65);

            // Dirt and splash-back collect in the first few metres of a wall. A
            // little low-frequency noise breaks up the horizontal band, and tapering
            // the effect upward keeps it from looking like a painted skirting board.
            float grimeBand = 1.0 - saturate(heightAboveBase / max(1.0, _AoHeight * 2.8));
            float grimeNoise = 0.55 + 0.45 * Hash21(floor(IN.worldPos.xz * 0.9));
            float grime = saturate(grimeBand * grimeNoise * _GrimeStrength);
            albedo *= lerp(1.0, 0.72, grime);

            float eave = saturate(heightAboveBase / max(1.0, _AoHeight * 8.0));
            albedo *= lerp(1.0 - _TopShade, 1.0, 1.0 - eave * 0.3);

            // Wall relief, faded out on glass and at distance so it never aliases.
            float relief = _WallNormalStrength * (1.0 - inner) * (1.0 - far);
            o.Normal = normalize(float3(wallNormal.xy * relief, max(0.2, wallNormal.z)));

            // Masonry is matte, window frames are painted and slightly smoother, and
            // glass gets enough smoothness to pick up the sky reflection probe. Grime
            // dulls the base without making the whole facade look wet.
            float smoothness = _Smoothness;
            smoothness = lerp(smoothness, 0.34, outer * 0.65);
            smoothness = lerp(smoothness, 0.84, inner);
            // Dirt dulls masonry and painted trim; glass stays in the 0.8-1.0
            // window-reflection range instead of reading as a dusty wall.
            smoothness *= lerp(1.0, 0.72, grime * (1.0 - inner));

            o.Albedo = albedo;
            o.Smoothness = smoothness;
            o.Metallic = 0;
            o.Occlusion = saturate(contact);
        }
        ENDCG
    }

    FallBack "Standard"
}
