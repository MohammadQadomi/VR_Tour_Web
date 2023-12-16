Shader "Unlit/SH_AlwaysVisibleWithTextureTilingOffset"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Always visible color", Color) = (0, 0, 0, 1)
        _Tiling("Tiling", Vector) = (1, 1, 0, 0)
        _Offset("Offset", Vector) = (0, 0, 0)
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" }
            LOD 100

            Pass
            {
                Cull Off
                ZWrite Off
                ZTest Always
                Blend SrcAlpha OneMinusSrcAlpha

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float4 vertex : SV_POSITION;
                    float4 color : COLOR;
                    float2 uv : TEXCOORD0;
                };

                float4 _Color;
                sampler2D _MainTex;
                float2 _Tiling;
                float2 _Offset;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.color = _Color;
                    o.uv = v.uv * _Tiling + _Offset; // Apply tiling and offset to UV coordinates
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Sample the texture using UV coordinates
                    fixed4 texColor = tex2D(_MainTex, i.uv);

                // Apply the sampled texture color with the original alpha
                fixed4 finalColor = texColor * i.color.a;

                return finalColor;
                }

            ENDCG
            }
        }
}
