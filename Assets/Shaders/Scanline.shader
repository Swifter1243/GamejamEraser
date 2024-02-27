Shader "Unlit/Scanline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSpread ("Blur Spread", Range(0, 0.02)) = 0.01
        _Iterations ("Iterations", Integer) = 10
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);

            float4 getScreenColor(float2 uv) {
                return UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, uv);
            }

            float _BlurSpread;
            int _Iterations;

            float hashwithoutsine11(float p)
            {
                p = frac(p * .1031);
                p *= p + 33.33;
                p *= p + p;
                return frac(p);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = getScreenColor(i.uv);

                float4 cutColor = col * sin(i.uv.y * 300 + _Time.y * 3 + hashwithoutsine11(_Time.y * 200));

                col = lerp(col, cutColor, 0.2);

                float4 average = 0;

                for (int j = 0; j < _Iterations; j++)
                {
                    float2 newUV = float2(i.uv.x + _BlurSpread * j, i.uv.y);
                    average += getScreenColor(newUV) * float4(1, 0.3, 0.2, 0);
                }

                average /= _Iterations;

                col += average;
                
                return col;
            }
            ENDCG
        }
    }
}
