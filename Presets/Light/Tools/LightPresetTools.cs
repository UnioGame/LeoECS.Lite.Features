namespace Game.Ecs.Presets.Components
{
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public static class LightPresetTools
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LightPreset Lerp(ref LightPreset fromValue, ref LightPreset toValue, float progress)
        {
            var result = fromValue;

            LerpPreset(ref result, ref toValue, progress);

            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LightPreset Lerp(Light light,ref LightPreset fromValue, ref LightPreset toValue, float progress)
        {
            var result = Lerp(ref fromValue, ref toValue, progress);
            result.ApplyToLight(light);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LerpPreset(ref LightPreset from, ref LightPreset to, float progress)
        {
            from.Color = Color.Lerp(from.Color, to.Color, progress);
            from.Intencivity = Mathf.Lerp(from.Intencivity, to.Intencivity, progress);
            from.Range = Mathf.Lerp(from.Range, to.Range, progress);
            from.ShadowStrength = Mathf.Lerp(from.ShadowStrength, to.ShadowStrength, progress);
            from.SpotAngle = Mathf.Lerp(from.SpotAngle, to.SpotAngle, progress);
            from.Type = to.Type;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyFromLight(this Light light, ref LightPreset lightPreset)
        {
            lightPreset.Color = light.color;
            lightPreset.Intencivity = light.intensity;
            lightPreset.ShadowStrength = light.shadowStrength;
            lightPreset.Range = light.range;
            lightPreset.SpotAngle = light.spotAngle;
            lightPreset.Type = light.type;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LightPreset CreateLightPreset(this Light light)
        {
            var lightPreset = new LightPreset();
            light.CopyFromLight(ref lightPreset);
            return lightPreset;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ApplyToLight(this LightPreset lightPreset,Light light)
        {
            light.color = lightPreset.Color;
            light.intensity = lightPreset.Intencivity;
            light.range = lightPreset.Range;
            light.shadowStrength = lightPreset.ShadowStrength;
            light.spotAngle = lightPreset.SpotAngle;
            light.type = lightPreset.Type;
        }
    }
}