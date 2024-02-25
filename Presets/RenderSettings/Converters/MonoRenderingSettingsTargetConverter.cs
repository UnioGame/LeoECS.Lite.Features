namespace Game.Ecs.Presets.Converters
{
    using Abstract;
    using Sirenix.OdinInspector;
    using System;
    using System.Threading;
    using Assets;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Converters;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    public sealed class MonoRenderingSettingsTargetConverter : MonoLeoEcsConverter<RenderingSettingsTargetConverter>
    {
    }
    
    [Serializable]
    public sealed class RenderingSettingsTargetConverter : LeoEcsConverter, IPresetAction
    {
        [ShowIf(nameof(IsEnabled))]
        public string id = nameof(RenderingSettingsPresetComponent);
        [ShowIf(nameof(IsEnabled))]
        public bool showButtons;
        
        [ShowIf(nameof(IsEnabled))]
        [HideLabel]
        public RenderingSettingsPreset sourcePreset;

        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var sourceComponent = ref world.GetOrAddComponent<RenderingSettingsPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var presetTargetComponent = ref world.GetOrAddComponent<PresetTargetComponent>(entity);
            
            idComponent.Value = id.GetHashCode();
            sourceComponent.Value = sourcePreset;
        }
        
        [Button]
        [ShowIf(nameof(showButtons))]
        public void Bake()
        {
            sourcePreset.BakeActiveRenderingSettings();
        }

        [Button]
        [ShowIf(nameof(showButtons))]
        public void ApplyToTarget()
        {
            sourcePreset.ApplyToRendering();
        }
    }
}