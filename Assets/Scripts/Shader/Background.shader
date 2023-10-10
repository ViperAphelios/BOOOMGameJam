Shader "Custom/Background"
{
    Properties
    {
        [PerRendererData]
        _MainTex ("Texture", 2D) = "white" {}

		[Header(General Settings)]
        _Thickness ("Width", float) = 10

        [Header(Solid Settings)]
		_SolidOutline ("Outline Color Base", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags 
        { 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
        }

        Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

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
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            uniform float4 _MainTex_TexelSize;

            fixed4 _SolidOutline;
            fixed _Thickness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.screenPos = ComputeScreenPos (o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				// float thicknessX = _Thickness / _MainTex_TexelSize.z;
				// float thicknessY = _Thickness / _MainTex_TexelSize.w;
                // float y = i.uv.y;
                // float x = i.uv.x;
                
				// float thicknessX = _Thickness / _ScreenParams.x;
				// float thicknessY = _Thickness / _ScreenParams.y;

                // float y = i.screenPos.y;
                // float x = i.screenPos.x;
                
                // if(_Thickness <= 0 || 
                //     (y + thicknessY < 1 && y - thicknessY > 0 &&
                //     x + thicknessX < 1 && x - thicknessX > 0))
                // {
                //     // sample the texture
                //     fixed4 col = tex2D(_MainTex, i.uv);
                //     // if(col.a <= 0)
                //     // {
                //         return col;
                //     // }
                // }

                float2 screenPos = i.screenPos.xy / i.screenPos.w;
                screenPos.xy *= _ScreenParams.xy;
                float y = screenPos.y;
                float x = screenPos.x;

                if(_Thickness <= 0
                ||
                    (y + _Thickness < _ScreenParams.y && y - _Thickness > 0 &&
                    x + _Thickness < _ScreenParams.x && x - _Thickness > 0)
                    )
                {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    col.rgb *= col.a;
                    return col;
                }
                
                fixed4 outlineC = _SolidOutline;
                outlineC.rgb *= outlineC.a;
                return outlineC;
            }
            ENDCG
        }
    }
}
