namespace Game.Ecs.Presets.Light.Converters
{
    using System;
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using Modules.UnioModules.UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Converters;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    public sealed class MonoLightPresetTargetConverter : MonoLeoEcsConverter<LightPresetTargetConverter>
    {
    }
    
    [Serializable]
    public sealed class LightPresetTargetConverter : LeoEcsConverter
    {
        public string id = nameof(LightPresetComponent);

        public Light light;

        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var lightPresetComponent = ref world.GetOrAddComponent<LightPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var presetTargetComponent = ref world.GetOrAddComponent<PresetTargetComponent>(entity);
            ref var lightComponent = ref world.GetOrAddComponent<LightComponent>(entity);

            var lightPreset = new LightPreset();
            
            lightPreset.Color = light.color;
            lightPreset.Intencivity = light.intensity;
            lightPreset.SpotAngle = light.spotAngle;
            lightPreset.Range = light.range;
            lightPreset.ShadowStrength = light.shadowStrength;
            lightPreset.Type = light.type;
            
            idComponent.Value = id.GetHashCode();
            lightComponent.Value = light;
            lightPresetComponent.Value = lightPreset;
        }
    }
}