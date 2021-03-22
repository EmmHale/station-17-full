Shader "Unlit/OutlineShader"
{
    Properties
    {
        _MaterialColor("Material Color", color) = (1, 1, 1, 1)

        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", color) = (1, 1, 1, 1)
        _OutlineWidth("Outline Width", Range(1.0, 10.0)) = 1.01
    }

        CGINCLUDE
        #pragma vertex vert
        #pragma fragment frag
            // make fog work
        #pragma multi_compile_fog

        #include "UnityCG.cginc"

        struct appdata
        {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float2 uv : TEXCOORD0;
        };

        struct v2f
        {
            float2 uv : TEXCOORD0;
            UNITY_FOG_COORDS(1)
            float4 vertex : SV_POSITION;
            float3 normal : NORMAL;
        };

        sampler2D _MainTex;
        float4 _MainTex_ST;
        float4 _OutlineColor;
        float _OutlineWidth;
        float4 _MaterialColor;

        v2f vert(appdata v)
        {
            v.vertex.xyz *= _OutlineWidth; //Create area normal to pixels of the model

            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            UNITY_TRANSFER_FOG(o,o.vertex);

            return o;
        }

        /*fixed4 frag(v2f i) : SV_Target
        {
            // sample the texture
            fixed4 col = tex2D(_MainTex, i.uv) + _OutlineColor;
            // apply fog
            UNITY_APPLY_FOG(i.fogCoord, col);
            return col;
        }*/

        ENDCG

    SubShader
    {
            Tags{ "RenderType" = "Opaque" "Queue" = "Transparent" }
                LOD 100

                //Render Outline
        Pass
        {
            ZWRITE Off //Do not write to "depth?" buffer
            CGPROGRAM

            half4 frag(v2f i) : COLOR
            {
                return _OutlineColor;
            }

            Material
            {
                Diffuse[_MaterialColor]
            }

            ENDCG
        }

        //Normal Render
        Pass
        {
            ZWRITE ON //write to z buffer

            Material
            {
                Diffuse[Color]
                Ambient[Color]
            }

            Lighting On

            SetTexture[_MainTex]
            {
                //Combine previous * primary DOUBLE
            }
        }
    }
}
