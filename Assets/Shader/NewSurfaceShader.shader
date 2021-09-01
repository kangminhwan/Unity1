Shader "Custom/NewSurfaceShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_BlurTex("BlurMap", 2D) = "black" {}
		_Sample("Sample", Range(4, 64)) = 16
		_Effect("Effect", float) = 1
		_Threshold("Threshold", float) = 0.001
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

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
					float2 uv : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				sampler2D _BlurTex;

				float4 _MainTex_ST;
				float4 _BlurTex_ST;
				float _Sample;
				float _Effect;
				float _Threshold;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					// sample the texture
					fixed4 col = fixed4(0,0,0,0);
					//  R = X, G = Y => RG = UV offset
					float2 mDir = float2(0,0);
					float mIntensity = 0;
					float divider = 1 / _Sample;

					float4 blurSum = float4(0, 0, 0, 1);
					blurSum.xy = mDir * divider;

					for (int j = 0; j < _Sample; j++)
					{
						float scale = j * divider;
						float2 cCoord = i.uv + (.5 - i.uv) * blurSum.xy * _Effect * scale;
						float4 offset = tex2D(_BlurTex, cCoord);

						mDir = offset.rg - .4961;
						if (length(mDir) <= _Threshold)
							mDir = 0;
						else
						mDir += .4961;
						blurSum.xy += mDir * divider;
						mIntensity = offset.b;
						mIntensity = pow(mIntensity, 3);
						col += tex2D(_MainTex, cCoord) * divider;
					}
				return col;
				}
			ENDCG
			}
		}
}