namespace Game.Ecs.Presets.Assets
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public class FogShaderPresets
    {
        public bool autoUpdate = false;
        
        [OnValueChanged(nameof(AutoUpdate))]
        public Color mainFogColor = Color.blue;
        [OnValueChanged(nameof(AutoUpdate))]
        public float distance0 = 5.0f;
        [OnValueChanged(nameof(AutoUpdate))]
        public float distance100 = 5.5f;
        [OnValueChanged(nameof(AutoUpdate))]
        [Range(0f, 10f)] public float heightScale = 1.0f;
        [OnValueChanged(nameof(AutoUpdate))]
        public float heightOffset = 0.0f;
        [OnValueChanged(nameof(AutoUpdate))]
        public Color bottomColor = Color.red;
        [OnValueChanged(nameof(AutoUpdate))]
        public bool secondColor = true;
        [OnValueChanged(nameof(AutoUpdate))]
        public bool bottomFog = false;
        [OnValueChanged(nameof(AutoUpdate))]
        [Range(-1f, 1f)] public float bottomAlphaMultiply = -0.8f;
        [OnValueChanged(nameof(AutoUpdate))]
        public float bottom0 = 2.0f;
        [OnValueChanged(nameof(AutoUpdate))]
        public float bottom100 = -5.0f;

        private static readonly int MainFogColor = Shader.PropertyToID("MainFogColor");
        private static readonly int Distance0 = Shader.PropertyToID("Distance0");
        private static readonly int Distance100 = Shader.PropertyToID("Distance100");
        private static readonly int HeightScale = Shader.PropertyToID("HeightScale");
        private static readonly int HeightOffset = Shader.PropertyToID("HeightOffset");
        private static readonly int BottomColor = Shader.PropertyToID("BottomColor");
        private static readonly int SecondColor = Shader.PropertyToID("SecondColor");
        private static readonly int BottomFog = Shader.PropertyToID("BottomFog");
        private static readonly int BottomAlphaMultiply = Shader.PropertyToID("BottomAlphaMultiply");
        private static readonly int Bottom0 = Shader.PropertyToID("Bottom0");
        private static readonly int Bottom100 = Shader.PropertyToID("Bottom100");

        
        private static FogShaderPresets _bufferPreset = new FogShaderPresets();

        public void AutoUpdate()
        {
            if (!autoUpdate) return;
            
            ApplyToShader();
        }
        
        public static void Lerp(FogShaderPresets from, FogShaderPresets to, float progress)
        {
            switch (progress)
            {
                case >= 1:
                    to.ApplyToShader();
                    return;
                case <= 0:
                    from.ApplyToShader();
                    return;
            }

            _bufferPreset.ApplyShader(from);
            _bufferPreset.ApplyLerp(from, to, progress);
            _bufferPreset.ApplyToShader();
        }

        public void ApplyShader(FogShaderPresets source)
        {
            mainFogColor = source.mainFogColor;
            distance0 = source.distance0;
            distance100 = source.distance100;
            heightScale = source.heightScale;
            heightOffset = source.heightOffset;
            bottomColor = source.bottomColor;
            secondColor = source.secondColor;
            bottomFog = source.bottomFog;
            bottomAlphaMultiply = source.bottomAlphaMultiply;
            bottom0 = source.bottom0;
            bottom100 = source.bottom100;
        }

        public void ApplyLerp(FogShaderPresets from, FogShaderPresets target, float progress)
        {
            //Boolean not need to Lerp
            secondColor = target.secondColor;
            bottomFog = target.bottomFog;
            
            //Colors
            mainFogColor = Color.Lerp(from.mainFogColor, target.mainFogColor, progress);
            bottomColor = Color.Lerp(from.bottomColor, target.bottomColor, progress);
            
            //Float
            distance0 = Mathf.Lerp(from.distance0, target.distance0, progress);
            distance100 = Mathf.Lerp(from.distance100, target.distance100, progress);
            heightScale = Mathf.Lerp(from.heightScale, target.heightScale, progress);
            heightOffset = Mathf.Lerp(from.heightOffset, target.heightOffset, progress);
            bottomAlphaMultiply = Mathf.Lerp(from.bottomAlphaMultiply, target.bottomAlphaMultiply, progress);
            bottom0 = Mathf.Lerp(from.bottom0, target.bottom0, progress);
            bottom100 = Mathf.Lerp(from.bottom100, target.bottom100, progress);
        }
        
        public void BakeActiveFogShaderSettings()
        {
            mainFogColor = Shader.GetGlobalColor(MainFogColor);
            distance0 = Shader.GetGlobalFloat(Distance0);
            distance100 = Shader.GetGlobalFloat(Distance100);
            heightScale = Shader.GetGlobalFloat(HeightScale);
            heightOffset = Shader.GetGlobalFloat(HeightOffset);
            bottomColor = Shader.GetGlobalColor(BottomColor);
            var secondColorToInt = Shader.GetGlobalInt(SecondColor);
            var bottomFogToInt = Shader.GetGlobalInt(BottomFog);
            bottomAlphaMultiply = Shader.GetGlobalFloat(BottomAlphaMultiply);
            bottom0 = Shader.GetGlobalFloat(Bottom0);
            bottom100 = Shader.GetGlobalFloat(Bottom100);

            secondColor = secondColorToInt == 1;
            bottomFog = bottomFogToInt == 1;
        }
        
        public void ApplyToShader()
        {
            var secondColorToInt = secondColor ? 1 : 0;
            var bottomFogToInt = bottomFog ? 1 : 0;

            Shader.SetGlobalColor(MainFogColor, mainFogColor);
            Shader.SetGlobalFloat(Distance0, distance0);
            Shader.SetGlobalFloat(Distance100, distance100);
            Shader.SetGlobalFloat(HeightScale, heightScale);
            Shader.SetGlobalFloat(HeightOffset, heightOffset);
            Shader.SetGlobalColor(BottomColor, bottomColor);
            Shader.SetGlobalInt(SecondColor, secondColorToInt);
            Shader.SetGlobalInt(BottomFog, bottomFogToInt);
            Shader.SetGlobalFloat(BottomAlphaMultiply, bottomAlphaMultiply);
            Shader.SetGlobalFloat(Bottom0, bottom0);
            Shader.SetGlobalFloat(Bottom100, bottom100);
        }
    }
}