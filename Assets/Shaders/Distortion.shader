// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Distortion"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Noise ("Noise", 2D) = "white" {}
        _Strength("Distort Strength", float) = 1.0
        _Mask("Mask", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float4 screenPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            uniform float4 _MainTex_TexelSize;
            sampler2D _Noise;
			sampler2D _CameraOpaqueTexture;
            uniform float4 _CameraOpaqueTexture_TexelSize;
            sampler2D _Mask;
            float4 _MainTex_ST;
            float _Strength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.vertex += abs(sin(_Time.g)) * o.vertex.x;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
				o.screenPos = ComputeScreenPos(o.vertex);
                //UNITY_TRANSFER_DEPTH(o.depth);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //half2 displacement1 = tex2D( _Displacement1Tex, i.uv.xy );
                //float2 adjusted = i.uv.xy + (displacement1.rg - .5);
                // sample the texture
                //float2 uv = (i.screenPos.xy + tex2D(_Displacement1Tex, i.screenPos+i.vertex.y) * 0.01);
		        //i.screenPos.xy = TransformStereoScreenSpaceTex(i.screenPos.xy, i.screenPos.w);
                float noise = tex2D(_Noise, i.uv).rgb;
                noise = saturate(noise);
                i.screenPos.x -= noise * _Strength * _MainTex_TexelSize ;
                i.screenPos.y -= noise * _Strength * _MainTex_TexelSize ;
                fixed4 col = tex2D(_CameraOpaqueTexture, i.screenPos);//tex2D(_CameraDepthTexture, i.uv + abs(sin(_Time.x)));
                
	            //half4 col = tex2Dproj(_GrabTexture, );
                float tmp = -tex2D(_Mask, i.uv).rgb;
                if(tmp < 0.25){
                    col.a = tex2D(_Mask, i.uv).rgb;
                }else{
                    col.a = 1;
                }
                float mescouilles = col.rg;

                if(mescouilles >= 0.99){
                    col.a = 0;
                }
                //col.a = .9;
                //fixed4 displ = tex2D(_DisplacementMask, i.uv);
                //col.rgb *= saturate(displ.rgb);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //col = tex2D(_CameraDepthTexture, i.uv);
                //UNITY_OUTPUT_DEPTH(i.depth);

                return col;
            }
            ENDCG
        }
    }
}
