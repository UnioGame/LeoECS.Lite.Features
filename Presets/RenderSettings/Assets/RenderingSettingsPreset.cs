namespace Game.Ecs.Presets.Assets
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Rendering;

    [Serializable]
    public class RenderingSettingsPreset
    {
        #region inspector

        public bool showProSettings;
        
        public SourceMode sourceMode;
        [ColorUsage(true, true)] [ShowIf(nameof(IsGradientSelected))] public Color skyColor;
        [ColorUsage(true, true)] [ShowIf(nameof(IsGradientSelected))] public Color equatorColor;
        [ColorUsage(true, true)] [ShowIf(nameof(IsGradientSelected))] public Color groundColor;
        [ColorUsage(true, true)] [HideIf(nameof(IsGradientSelected))] public Color ambientColor;

        [ShowIf(nameof(showProSettings))] public float ambientIntensity;
        [ShowIf(nameof(showProSettings))] public DefaultReflectionMode defaultReflectionMode;
        [ShowIf(nameof(showProSettings))] public int defaultReflectionResolution;
        [ShowIf(nameof(showProSettings))] public float flareFadeSpeed;
        [ShowIf(nameof(showProSettings))] public float flareStrength;
        [ShowIf(nameof(showProSettings))] public float haloStrength;
        [ShowIf(nameof(showProSettings))] public int reflectionBounces;
        [ShowIf(nameof(showProSettings))] public float reflectionIntensity;
        [ShowIf(nameof(showProSettings))] public Material skybox;
        [ShowIf(nameof(showProSettings))] public Color subtractiveShadowColor;
        [ShowIf(nameof(showProSettings))] public Light sun;

        #endregion

        #region static data

        private static RenderingSettingsPreset _bufferPreset = new RenderingSettingsPreset();

        public static void Lerp(RenderingSettingsPreset from, RenderingSettingsPreset to, float progress)
        {
            if (progress >= 1)
            {
                to.ApplyToRendering();
                return;
            }

            if (progress <= 0)
            {
                from.ApplyToRendering();
                return;
            }

            _bufferPreset.ApplySettings(from);
            _bufferPreset.ApplyLerp(from, to, progress);
            _bufferPreset.ApplyToRendering();
        }

        #endregion
        
        public void BakeActiveRenderingSettings()
        {
            equatorColor = RenderSettings.ambientEquatorColor;
            groundColor = RenderSettings.ambientGroundColor;
            ambientIntensity = RenderSettings.ambientIntensity;
            ambientColor = RenderSettings.ambientLight;
            sourceMode = (SourceMode)RenderSettings.ambientMode;
            skyColor = RenderSettings.ambientSkyColor;
            defaultReflectionMode = RenderSettings.defaultReflectionMode;
            defaultReflectionResolution = RenderSettings.defaultReflectionResolution;
            flareFadeSpeed = RenderSettings.flareFadeSpeed;
            flareStrength = RenderSettings.flareStrength;
            haloStrength = RenderSettings.haloStrength;
            reflectionBounces = RenderSettings.reflectionBounces;
            reflectionIntensity = RenderSettings.reflectionIntensity;
            skybox = RenderSettings.skybox;
            subtractiveShadowColor = RenderSettings.subtractiveShadowColor;
            sun = RenderSettings.sun;
        }

        public void ApplyLerp(RenderingSettingsPreset from, RenderingSettingsPreset target, float progress)
        {
            equatorColor = Color.Lerp(from.equatorColor, target.equatorColor, progress);
            groundColor = Color.Lerp(from.groundColor, target.groundColor, progress);
            skyColor = Color.Lerp(from.skyColor, target.skyColor, progress);
            subtractiveShadowColor = Color.Lerp(from.subtractiveShadowColor, target.subtractiveShadowColor, progress);
            
            if(sun !=null && target.sun != null && from.sun != null)
                sun.color = Color.Lerp(from.sun.color, target.sun.color, progress);

            ambientIntensity = target.ambientIntensity;
            ambientColor = target.ambientColor;
            sourceMode = target.sourceMode;
            reflectionBounces = target.reflectionBounces;
            defaultReflectionMode = target.defaultReflectionMode;
            defaultReflectionResolution = target.defaultReflectionResolution;

            reflectionIntensity = Mathf.Lerp(from.reflectionIntensity, target.reflectionIntensity, progress);
            haloStrength = Mathf.Lerp(from.haloStrength, target.haloStrength, progress);
            flareFadeSpeed = Mathf.Lerp(from.flareFadeSpeed, target.flareFadeSpeed, progress);
            flareStrength = Mathf.Lerp(from.flareStrength, target.flareStrength, progress);

            if(skybox !=null && target.skybox != null && from.skybox != null)
                skybox.Lerp(from.skybox, target.skybox, progress);
        }
        
        public void ApplyToRendering()
        {
            RenderSettings.ambientEquatorColor = equatorColor;
            RenderSettings.ambientGroundColor = groundColor;
            RenderSettings.ambientIntensity = ambientIntensity;
            RenderSettings.ambientLight = ambientColor;
            RenderSettings.ambientMode = (AmbientMode)sourceMode;
            RenderSettings.ambientSkyColor = skyColor;
            RenderSettings.defaultReflectionMode = defaultReflectionMode;
            RenderSettings.defaultReflectionResolution = defaultReflectionResolution;
            RenderSettings.flareFadeSpeed = flareFadeSpeed;
            RenderSettings.flareStrength = flareStrength;
            RenderSettings.haloStrength = haloStrength;
            RenderSettings.reflectionBounces = reflectionBounces;
            RenderSettings.reflectionIntensity = reflectionIntensity;
            RenderSettings.skybox = skybox;
            RenderSettings.subtractiveShadowColor = subtractiveShadowColor;
            RenderSettings.sun = sun;
        }

        public void ApplySettings(RenderingSettingsPreset source)
        {
            equatorColor = source.equatorColor;
            groundColor = source.groundColor;
            ambientIntensity = source.ambientIntensity;
            ambientColor = source.ambientColor;
            sourceMode = source.sourceMode;
            skyColor = source.skyColor;
            defaultReflectionMode = source.defaultReflectionMode;
            defaultReflectionResolution = source.defaultReflectionResolution;
            flareFadeSpeed = source.flareFadeSpeed;
            flareStrength = source.flareStrength;
            haloStrength = source.haloStrength;
            reflectionBounces = source.reflectionBounces;
            reflectionIntensity = source.reflectionIntensity;
            skybox = source.skybox;
            subtractiveShadowColor = source.subtractiveShadowColor;
            sun = source.sun;
        }
        
        private bool IsGradientSelected()
        {
            return sourceMode == SourceMode.Gradient;
        }
    }
}