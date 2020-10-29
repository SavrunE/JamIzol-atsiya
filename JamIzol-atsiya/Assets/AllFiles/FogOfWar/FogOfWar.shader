Shader "Custom/Transparent/FogOfWar" {
	Category{
		Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		ZWrite Off
		Alphatest Greater 0
		Blend SrcAlpha OneMinusSrcAlpha
		SubShader
	{
		Pass{
		ColorMaterial AmbientAndDiffuse
		Fog{Mode Off}
		Lighting Off
		SeparateSpecular Off
}
}
	}
}