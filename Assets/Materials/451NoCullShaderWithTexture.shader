﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/451NoCullShaderWithTexture"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		//Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
                float3 vertexWC : TEXCOORD3;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
            float4 LightPosition;
			float4x4 MyXformMat;
            fixed4 LightColor;
            float  LightNear;
            float  LightFar;
			v2f vert (appdata v)
			{
				v2f o;
				
				o.vertex = UnityObjectToClipPos(v.vertex);   // World to NDC

				o.vertex = mul(MyXformMat, v.vertex);
                o.vertex = mul(UNITY_MATRIX_VP, o.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			//	o.uv = v.uv; // no specific placement support

                o.vertexWC = mul(UNITY_MATRIX_M, v.vertex); // this is in WC space!
                // this is not pretty but we don't have access to inverse-transpose ...
                float3 p = v.vertex + v.normal;
                p = mul(UNITY_MATRIX_M, float4(p,1)); 
                o.normal = normalize(p - o.vertexWC); // NOTE: this is in the world space!!
				return o;
			}
			
            // our own function
            fixed4 ComputeDiffuse(v2f i) {
                 float3 l = LightPosition - i.vertexWC;
                float d = length(l);
                l = l / d;
                float strength = 1;
                
                float ndotl = clamp(dot(i.normal, l), 0, 1);
                if (d > LightNear) {
                    if (d < LightFar) {
                        float range = LightFar - LightNear;
                        float n = d - LightNear;
                        strength = smoothstep(0, 1, 1.0 - (n*n) / (range*range));
                    }
                    else {
                        strength = 0;
                    }
                }
                return ndotl * strength;
            }

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
                
                float diff = ComputeDiffuse(i);
                return col * diff;
			}

			ENDCG
		}
	}
}