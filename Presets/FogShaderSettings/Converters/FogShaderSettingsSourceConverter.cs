namespace Game.Ecs.Presets.FogShaderSettings.Converters
{
    using Assets;
    using Game.Ecs.Presets.Components;
    using Components;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Shared.Extensions;
    using System;
    using System.Threading;
    using Abstract;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;

    /// <summary>
    /// Fog shader convertor
    /// </summary>
    [Serializable]
    public class FogShaderSettingsSourceConverter : EcsComponentConverter,IPresetAction
    {
        [ShowIf(nameof(isEnabled))]
        public string targetId = nameof(FogShaderSettingsPresetComponent);
        [ShowIf(nameof(isEnabled))]
        public float duration;
        [ShowIf(nameof(isEnabled))]
        public bool showButtons;

        [ShowIf(nameof(isEnabled))]
        [FoldoutGroup("Fog Shader Settings")]
        [HideLabel]
        public FogShaderPresets preset = new FogShaderPresets();

        public override void Apply(EcsWorld world, int entity)
        {
            ref var presetComponent = ref world.GetOrAddComponent<PresetComponent>(entity);
            ref var presetSourceComponent = ref world.GetOrAddComponent<PresetSourceComponent>(entity);
            ref var dataComponent = ref world.GetOrAddComponent<FogShaderSettingsPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var durationComponent = ref world.GetOrAddComponent<PresetDurationComponent>(entity);
            ref var activePresetSource = ref world.GetOrAddComponent<ActivePresetSourceComponent>(entity);

            idComponent.Value = targetId.GetHashCode();
            dataComponent.Value = preset;
            durationComponent.Value = duration;
        }

        [Button] 
        [ShowIf(nameof(showButtons))]
        public void Bake()
        {
            preset.BakeActiveFogShaderSettings();
        }

        [Button] 
        [ShowIf(nameof(showButtons))]
        public void ApplyToTarget()
        {
            preset.ApplyToShader();
        }
    }
}