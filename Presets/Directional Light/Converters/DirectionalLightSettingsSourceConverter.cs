namespace Game.Ecs.Presets.Directional_Light.Converters
{
    using Assets;
    using System;
    using System.Threading;
    using Abstract;
    using Game.Ecs.Presets.Components;
    using Game.Ecs.Presets.Converters;
    using Components;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    public class DirectionalLightSettingsSourceConverter : EcsComponentConverter, IPresetAction
    {
        [ShowIf(nameof(isEnabled))]
        public string targetId = nameof(DirectionalLightSettingsPresetComponent);
        [ShowIf(nameof(isEnabled))]
        public float duration;
        [ShowIf(nameof(isEnabled))]
        public bool showButtons;

        [ShowIf(nameof(isEnabled))]
        [FoldoutGroup("Directional Light Settings")]
        [HideLabel]
        public DirectionalLightPresets preset = new DirectionalLightPresets()
        {
            showTargetValue = false
        };

        public override void Apply(EcsWorld world, int entity)
        {
            ref var presetComponent = ref world.GetOrAddComponent<PresetComponent>(entity);
            ref var presetSourceComponent = ref world.GetOrAddComponent<PresetSourceComponent>(entity);
            ref var dataComponent = ref world.GetOrAddComponent<DirectionalLightSettingsPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var durationComponent = ref world.GetOrAddComponent<PresetDurationComponent>(entity);
            ref var activePresetSource = ref world.GetOrAddComponent<ActivePresetSourceComponent>(entity);

            idComponent.Value = targetId.GetHashCode();
            dataComponent.Value = preset;
            durationComponent.Value = duration;
        }

        private void SearchTarget(bool apply)
        {
            var directionalLightsTargets =
                UnityEngine.Object.FindObjectsOfType<MonoDirectionalLightSettingsTargetConverter>(includeInactive: true);

            var generalLightTargets =
                UnityEngine.Object.FindObjectsOfType<MonoGeneralLightSettingsPresetTargetConverter>(
                    includeInactive: true);

            foreach (var directionalLightTarget in directionalLightsTargets)
            {
                if (directionalLightTarget.converter.id != targetId) continue;

                var sourcePreset = directionalLightTarget.converter.sourcePreset;

                preset.SetSourceConverter(apply, sourcePreset);
                break;
            }

            foreach (var directionalLightTarget in generalLightTargets)
            {
                if (directionalLightTarget.converter.directionalLightConverter.id != targetId) continue;

                var sourcePreset = directionalLightTarget.converter.directionalLightConverter.sourcePreset;

                preset.SetSourceConverter(apply, sourcePreset);
                break;
            }
        }

        [Button]
        [ShowIf(nameof(showButtons))]
        public void Bake()
        {
            SearchTarget(false);
        }

        [Button]
        [ShowIf(nameof(showButtons))]
        public void ApplyToTarget()
        {
            SearchTarget(true);
        }
    }
}