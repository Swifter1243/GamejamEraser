Shader "Unlit/Scanline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSpread ("Blur Spread", Range(0, 0.03)) = 0.01
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
                i.uv = i.uv * 2 - 1;
                float f = pow(abs(i.uv.x), 2);
                //i.uv.y = lerp(i.uv.y, pow(i.uv.y, 3), 0.3);
                i.uv.y *= 1 + f * 0.1;
                i.uv = (i.uv + 1) * 0.5;

                float4 col = getScreenColor(i.uv);

                // col = col + pow(col, 1.8);

                float4 offset = float4(1, 0.99, 0.998, 0.2);
                float4 sinColor = sin(offset * (i.uv.y * 300 + _Time.y * 3 + hashwithoutsine11(_Time.y * 200)));
                float4 cutColor = col * sinColor;

                col = lerp(col, cutColor, 0.2);

                float4 average = 0;

                col = lerp(col, col * 20, 0.1);

                for (int j = -_Iterations; j < _Iterations; j++)
                {
                    float percent = 2. * j / _Iterations;
                    float2 newUV = float2(i.uv.x + _BlurSpread * percent, i.uv.y);
                    float falloff = saturate(1 - abs(percent));
                    float4 newCol = getScreenColor(newUV) * float4(1, 0.6, 0.3, 0);
                    average += newCol * lerp(1, falloff, 0.9);

                    if (abs(j) == 1) average += newCol ;
                }

                average /= _Iterations;

                col += average;
                col += sinColor * 0.01;

                col *= 1 - f;
                
                return col;
            }
            ENDCG
        }
    }
}
