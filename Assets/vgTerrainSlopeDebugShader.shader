Shader "_Shaders/Terrain Slope Debug" 
{
	Properties 
	{
		_SlopeMax ("Maximum Allowed Slope", FLOAT) = 0.682
		_SlopeRange ("Slope Tolerance", FLOAT) = 0.743

		// set by terrain engine (and must be exposed this way)
		_Control ("Control (RGBA)", 2D) = "red" {}
		_Splat0 ("Layer 0 (R)", 2D) = "white" {}
		_Splat1 ("Layer 1 (G)", 2D) = "white" {}
		_Splat2 ("Layer 2 (B)", 2D) = "white" {}
		_Splat3 ("Layer 3 (A)", 2D) = "white" {}
		_Normal0 ("Normal 0 (R)", 2D) = "bump" {}
		_Normal1 ("Normal 1 (G)", 2D) = "bump" {}
		_Normal2 ("Normal 2 (B)", 2D) = "bump" {}
		_Normal3 ("Normal 3 (A)", 2D) = "bump" {}	
	}

	SubShader 
	{
	
		Tags 
		{
			"SplatCount" = "4"
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
		}

		Pass
		{
			Tags
			{
				"LightMode" = "ForwardBase"
			}

			CGPROGRAM

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#include "Lighting.cginc"

			#pragma vertex vert 
			#pragma fragment frag
			#pragma multi_compile_fwdbase

			uniform float _SlopeMax;
			uniform float _SlopeRange;

			uniform sampler2D _Control;
			uniform sampler2D _Splat0;
			uniform sampler2D _Splat1;
			uniform sampler2D _Splat2;
			uniform sampler2D _Splat3;
			uniform sampler2D _Normal0;
			uniform sampler2D _Normal1;
			uniform sampler2D _Normal2;
			uniform sampler2D _Normal3;

			struct VSOut
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
				LIGHTING_COORDS(1,2)
				
			};

			VSOut vert(appdata_full v)
			{
				VSOut output;
				output.pos = mul(UNITY_MATRIX_MVP, v.vertex );
				output.uv = v.texcoord.xy;

				float dotWithUp = dot( v.normal, fixed3( 0.0, 1.0, 0.0 ) );

				if( dotWithUp < _SlopeMax )
				{
					output.color.xyz = fixed3( 1.0, 0.0, 0.0 );
				}
				else if ( dotWithUp < _SlopeRange )
				{
					output.color.xyz = fixed3( 1.0, 1.0, 0.0 );
				}
				else
				{
					output.color.xyz = fixed3( 0.0, 1.0, 0.0 );
				}

				output.color.w = 1.0;

				TRANSFER_VERTEX_TO_FRAGMENT(output);
								
				return output;
			}

			float4 frag(VSOut i) : COLOR
			{
				float  atten = LIGHT_ATTENUATION(i);
				float2 uv_Control = i.uv;
				half4 splat_control = tex2D (_Control, uv_Control);
				
				return i.color;
			}

			ENDCG
		}
	}

	Fallback "Diffuse"
}