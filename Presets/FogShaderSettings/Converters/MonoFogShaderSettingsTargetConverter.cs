namespace Game.Ecs.Presets.FogShaderSettings.Converters
{
    using Abstract;
    using System;
    using System.Threading;
    using Assets;
    using Game.Ecs.Presets.Components;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Converters;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using Sirenix.OdinInspector;

    public sealed class MonoFogShaderSettingsTargetConverter : MonoLeoEcsConverter<FogShaderSettingsTargetConverter>
    {
    }

    [Serializable]
    public sealed class FogShaderSettingsTargetConverter : LeoEcsConverter, IPresetAction
    {
        [ShowIf(nameof(IsEnabled))]
        private string _id = nameof(FogShaderSettingsPresetComponent);
        [ShowIf(nameof(IsEnabled))]
        public bool showButtons;

        [ShowIf(nameof(IsEnabled))]
        [HideLabel]
        public FogShaderPresets sourcePreset;

        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var sourceComponent = ref world.GetOrAddComponent<FogShaderSettingsPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var presetTargetComponent = ref world.GetOrAddComponent<PresetTargetComponent>(entity);

            idComponent.Value = _id.GetHashCode();
            sourceComponent.Value = sourcePreset;
        }

        [Button]
        [ShowIf(nameof(showButtons))]
        public void Bake()
        {
            sourcePreset.BakeActiveFogShaderSettings();
        }

        [Button]
        [ShowIf(nameof(showButtons))]
        public void ApplyToTarget()
        {
            sourcePreset.ApplyToShader();
        }
    }
}