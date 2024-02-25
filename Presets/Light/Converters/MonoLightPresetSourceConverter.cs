namespace Game.Ecs.Presets.Light.Converters
{
    using System;
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Converters;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    public sealed class MonoLightPresetSourceConverter : MonoLeoEcsConverter<LightPresetSourceConverter>
    {
    }
    
    [Serializable]
    public sealed class LightPresetSourceConverter : LeoEcsConverter
    {
        public string targetId = nameof(LightPresetComponent);
        
        public bool useLight;
        
        [HideIf(nameof(useLight))]
        [FoldoutGroup("Light Preset")]
        [InlineProperty]
        [HideLabel]
        public LightPreset lightPreset;

        [ShowIf(nameof(useLight))]
        public Light light;
        
        public float duration;

        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var presetComponent = ref world.GetOrAddComponent<PresetComponent>(entity);
            ref var presetSourceComponent = ref world.GetOrAddComponent<PresetSourceComponent>(entity);
            ref var dataComponent = ref world.GetOrAddComponent<LightPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var durationComponent = ref world.GetOrAddComponent<PresetDurationComponent>(entity);
            ref var activePresetSource = ref world.GetOrAddComponent<ActivePresetSourceComponent>(entity);

            var lightTargetPreset = useLight && light != null ? light.CreateLightPreset() : lightPreset;

            idComponent.Value = targetId.GetHashCode();
            dataComponent.Value = lightTargetPreset;
            durationComponent.Value = duration;
        }
    }
}