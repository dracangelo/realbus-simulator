// Road surface: world-space tiling so a long Mapbox/OSM ribbon never stretches its
// asphalt, a tiling albedo + tangent-space normal map for the grain, and a
// low-frequency wear mask for patches and polished wheel paths. The material's
// _Color is the asphalt tone (linear), so the texture supplies variation rather
// than the whole colour.
Shader "RealBus/Asphalt"
{
    Properties
    {
        _Color ("Asphalt Tone", Color) = (0.34, 0.345, 0.35, 1)
        _MainTex ("Asphalt Grain (albedo)", 2D) = "white" {}
        _NormalMap ("Asphalt Grain (normal)", 2D) = "bump" {}
        _WearMap ("Wear / Patch Mask", 2D) = "gray" {}
        _WorldTile ("World Tile Size (metres)", Float) = 7
        _NormalStrength ("Normal Strength", Range(0, 2)) = 1
        _Smoothness ("Smoothness", Range(0, 1)) = 0.2
        _WearStrength ("Wear Strength", Range(0, 1)) = 0.45
        _WearTile ("Wear Tile Size (metres)", Float) = 38
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry+100" }
        LOD 250
        // OSM way winding is not reliable enough to assume every ribbon faces up.
        Cull Off
        // Keep the road in front of the map tile even where the depth buffer is
        // too coarse to resolve the vertical separation, e.g. a kilometre down a
        // straight arterial. The geometric gap does the work up close; this
        // removes the last coplanar tie at distance.
        Offset -1, -1

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NormalMap;
        sampler2D _WearMap;
        float4 _Color;
        float _WorldTile;
        float _NormalStrength;
        float _Smoothness;
        float _WearStrength;
        float _WearTile;

        struct Input
        {
            float3 worldPos;
            // uv2: x = lane coordinate (0..laneCount), y = metres along the ribbon
            // uv3: x = lane count, y = road width in metres
            float2 roadUV;
            float2 roadMeta;
        };

        void vert (inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.roadUV = v.texcoord1.xy;
            o.roadMeta = v.texcoord2.xy;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // One shared world-space UV for the aggregate: no stretching along a
            // ribbon, whether it is a 5 m service lane or a 500 m arterial.
            float2 uv = IN.worldPos.xz / max(1.0, _WorldTile);
            float3 grain = tex2D(_MainTex, uv).rgb;

            float laneCount = max(1.0, IN.roadMeta.x);
            float across = saturate(IN.roadUV.x / laneCount);
            float lane = frac(IN.roadUV.x);

            // Patchy wear: old repairs and repeated wheel paths read darker and
            // less glossy than fresh asphalt.
            float wear = tex2D(_WearMap, IN.worldPos.xz / max(1.0, _WearTile)).r;
            float wearAmount = saturate((wear - 0.42) * 2.2) * _WearStrength;

            float3 albedo = lerp(grain, grain * 0.74, wearAmount) * _Color.rgb;

            // Wheel paths are polished and slightly darker than the crown. Using the
            // lane coordinate means the tracks follow every bend instead of being
            // painted in world X/Z and drifting across the road. Smoothstep rather
            // than a gaussian: the road fills much of the screen and the fragment
            // cost has to stay cheap on mobile.
            float leftWheel = 1.0 - smoothstep(0.0, 0.085, abs(lane - 0.27));
            float rightWheel = 1.0 - smoothstep(0.0, 0.085, abs(lane - 0.73));
            float wheel = saturate(leftWheel + rightWheel);
            albedo *= lerp(1.0, 0.82, wheel * 0.45);

            // Grit, dust and spilled loads collect at the kerb. This is the cheapest
            // edge detail that stops a road ending in a razor-sharp line.
            float edge = 1.0 - smoothstep(0.015, 0.10, min(across, 1.0 - across));
            albedo = lerp(albedo, albedo * float3(0.78, 0.72, 0.62), edge * 0.55);

            // The normal map is generated at runtime as plain RGB-encoded normals, so
            // decode it directly: UnpackNormal would assume the DXT5nm swizzle that a
            // runtime-created texture does not have.
            float3 normalSample = tex2D(_NormalMap, uv).rgb * 2.0 - 1.0;
            normalSample.xy *= _NormalStrength;
            // Wheel paths are smoother than the surrounding aggregate.
            normalSample.xy *= lerp(1.0, 0.45, wheel * 0.5);

            o.Albedo = albedo;
            o.Normal = normalize(normalSample);
            o.Smoothness = _Smoothness * lerp(1.0, 0.55, wearAmount) * lerp(1.0, 1.25, wheel * 0.35);
            o.Metallic = 0;
            o.Occlusion = 1.0;
        }
        ENDCG
    }

    FallBack "Standard"
}
