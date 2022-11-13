Shader "Tecnocampus/PortalShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _MaskTex("Mask texture", 2D) = "white" {}
        _Cutout("Cutout", Range(0.0, 1.0)) = 0.5
        _OffsetFreq("OffesetFreq", Range(5.0, 50.0)) = 30.0
        _Freq("Frequency", Range(0.2, 20.0)) = 1.0
        _Amp("Amplitude", Range(0.001, 0.02)) = 0.005
    }
        SubShader
        {
            Tags{ "Queue" = "Geometry" "IgnoreProjector" = "True" "RenderType" = "Opaque" }
            Lighting Off
            Cull Back
            ZWrite On
            ZTest Less

            Fog{ Mode Off }
            Pass
            {
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
                    float2 uv : TEXCOORD0;
                    float4 screenPos : TEXCOORD1;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    o.screenPos = ComputeScreenPos(o.vertex);
                    return o;
                }

                sampler2D _MainTex;
                sampler2D _MaskTex;
                float _Cutout;
                float _Amp;
                float _OffsetFreq;
                float _Freq;

                fixed4 frag(v2f i) : SV_Target
                {
                    float c = sin(_Time.y) * 0.4 + 0.5;
                    i.screenPos /= i.screenPos.w;
                    fixed4 l_MaskColor = tex2D(_MaskTex, i.uv);
                    if (l_MaskColor.r > c)
                        clip(-1);
                    
                    float distCenter = sqrt((i.screenPos.x - 0.5) * (i.screenPos.x - 0.5) + (i.screenPos.y - 0.5) * (i.screenPos.y - 0.5));
                    float offset = sin(_Time.w * _Freq + distCenter * _OffsetFreq)*_Amp;
                    fixed4 col = tex2D(_MainTex, float2(i.screenPos.x + offset, i.screenPos.y + offset));
                    return col;
                }
                ENDCG
            }
        }
}