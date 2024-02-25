using Game.Ecs.Presets.Directional_Light.Converters;

namespace Game.Ecs.Presets.Converters
{
    using Game.Ecs.Presets.SpotLightSettings.Converters;
    using Abstract;
    using System;
    using System.Threading;
    using Game.Ecs.Presets.FogShaderSettings.Converters;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Converters;
    using UnityEngine;

    public class MonoGeneralLightSettingsPresetTargetConverter : MonoLeoEcsConverter<GeneralLightSettingsPresetTargetConverter>
    {
        
    }

    [Serializable]
    public sealed class GeneralLightSettingsPresetTargetConverter : LeoEcsConverter, IPresetAction
    {
        [PropertySpace(SpaceBefore = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Rendering")]
        public RenderingSettingsTargetConverter renderingConverter =
            new RenderingSettingsTargetConverter() { showButtons = false };

        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Fog Shader")]
        public FogShaderSettingsTargetConverter fogShaderConverter =
            new FogShaderSettingsTargetConverter() { showButtons = false };
        
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Spot Light")]
        public SpotLightSettingsTargetConverter spotLightConverter =
            new SpotLightSettingsTargetConverter() { showButtons = false };
        
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Directional Light")]
        public DirectionalLightSettingsTargetConverter directionalLightConverter =
            new DirectionalLightSettingsTargetConverter() { showButtons = false };
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            if (renderingConverter.IsEnabled)
                renderingConverter.Apply(target, world, world.NewEntity());
            if (fogShaderConverter.IsEnabled)
                fogShaderConverter.Apply(target, world, world.NewEntity());
            if (spotLightConverter.IsEnabled)
                spotLightConverter.Apply(target, world, world.NewEntity());
            if (directionalLightConverter.IsEnabled)
                directionalLightConverter.Apply(target, world, world.NewEntity());
        }

        [ButtonGroup]
        public void Bake()
        {
            renderingConverter.Bake();
            fogShaderConverter.Bake();
            spotLightConverter.Bake();
            directionalLightConverter.Bake();
        }

        [ButtonGroup]
        public void ApplyToTarget()
        {
            renderingConverter.ApplyToTarget();
            fogShaderConverter.ApplyToTarget();
            spotLightConverter.ApplyToTarget();
            directionalLightConverter.ApplyToTarget();
        }
    }
}