// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DissolveWithLight"
{
	Properties
	{
		_MainTexture("MainTexture", 2D) = "white" {}
		_FadeTexture("FadeTexture", 2D) = "white" {}
		_FadeIntensity("FadeIntensity", Range( 0 , 1.2)) = 0.2295798
		[HDR][Gamma]_Color0("Color 0", Color) = (1.844302,1.083488,0.7249614,0.4431373)
		_Float1("Float 1", Float) = 0.02
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaToMask On
		Cull Off
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"

			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform sampler2D _MainTexture;
			uniform float4 _MainTexture_ST;
			uniform float _FadeIntensity;
			uniform sampler2D _FadeTexture;
			uniform float4 _FadeTexture_ST;
			uniform float _Float1;
			uniform float4 _Color0;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float2 uv_MainTexture = i.ase_texcoord1.xy * _MainTexture_ST.xy + _MainTexture_ST.zw;
				float4 tex2DNode1 = tex2D( _MainTexture, uv_MainTexture );
				float2 uv_FadeTexture = i.ase_texcoord1.xy * _FadeTexture_ST.xy + _FadeTexture_ST.zw;
				float4 tex2DNode2 = tex2D( _FadeTexture, uv_FadeTexture );
				float temp_output_7_0 = step( _FadeIntensity , ( tex2DNode2.r + _Float1 ) );
				float temp_output_10_0 = ( temp_output_7_0 - step( _FadeIntensity , tex2DNode2.r ) );
				float4 lerpResult16 = lerp( tex2DNode1 , ( tex2DNode1.a * temp_output_10_0 * _Color0 ) , temp_output_10_0);
				float4 appendResult15 = (float4((lerpResult16).rgba.rgb , ( tex2DNode1.a * temp_output_7_0 )));
				
				
				finalColor = appendResult15;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19105
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;25;999.9775,-271.115;Float;False;True;-1;2;ASEMaterialInspector;100;5;DissolveWithLight;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;True;2;5;False;;10;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;True;True;2;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;RenderType=Opaque=RenderType;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;1;True;False;;False;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;458.8473,68.99407;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1336.544,-547.3286;Inherit;True;Property;_MainTexture;MainTexture;0;0;Create;True;0;0;0;False;0;False;-1;d3efd66e592b4dd4daf2dbd2a537ad21;89e9b2446a84704468304aec1c059a87;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1524.91,254.3959;Inherit;True;Property;_FadeTexture;FadeTexture;1;0;Create;True;0;0;0;False;0;False;-1;14938580c7fb2154ba4c07b45399a1e0;14938580c7fb2154ba4c07b45399a1e0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;4;-971.8311,-162.4978;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;15;708.304,-301.8795;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ComponentMaskNode;17;291.0715,-318.4363;Inherit;False;True;True;True;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;16;-32.17537,-328.3703;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;10;-584.2493,103.6588;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-1119.47,370.6232;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;7;-827.8808,376.7034;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-1385.857,497.6219;Inherit;False;Property;_Float1;Float 1;4;0;Create;True;0;0;0;False;0;False;0.02;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-305.7289,-295.3222;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;13;-684.1915,-156.7178;Inherit;False;Property;_Color0;Color 0;3;2;[HDR];[Gamma];Create;True;0;0;0;False;0;False;1.844302,1.083488,0.7249614,0.4431373;5.672221,2.196749,4.562455,0.4431373;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-1572.614,-46.77369;Inherit;False;Property;_FadeIntensity;FadeIntensity;2;0;Create;True;0;0;0;False;0;False;0.2295798;0.3517737;0;1.2;0;1;FLOAT;0
WireConnection;25;0;15;0
WireConnection;18;0;1;4
WireConnection;18;1;7;0
WireConnection;4;0;3;0
WireConnection;4;1;2;1
WireConnection;15;0;17;0
WireConnection;15;3;18;0
WireConnection;17;0;16;0
WireConnection;16;0;1;0
WireConnection;16;1;14;0
WireConnection;16;2;10;0
WireConnection;10;0;7;0
WireConnection;10;1;4;0
WireConnection;6;0;2;1
WireConnection;6;1;5;0
WireConnection;7;0;3;0
WireConnection;7;1;6;0
WireConnection;14;0;1;4
WireConnection;14;1;10;0
WireConnection;14;2;13;0
ASEEND*/
//CHKSM=ACAA8A8A964A3F0FB13D7E0932115699AE34ADDB