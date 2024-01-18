Shader "Custom/CircleTransition"
{
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" { }
        _TransitionProgress("Transition Progress", Range(0, 1)) = 0
    }

    CGINCLUDE
    #include "UnityCG.cginc"
    ENDCG

    SubShader{
        Tags { "Queue" = "Overlay" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma exclude_renderers gles xbox360 ps3
            ENDCG

            SetTexture[_MainTex] {
                combine primary
            }
        }
    }

    SubShader{
        Tags { "Queue" = "Overlay" }
        ZWrite On
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma exclude_renderers gles xbox360 ps3
            ENDCG

            SetTexture[_MainTex] {
                combine primary
            }

            SetTexture[_MainTex] {
                combine add
                constantColor(1,1,1,1)
                combine previous * constant
            }
        }
    }
}
