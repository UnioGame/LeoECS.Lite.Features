namespace Game.Ecs.Presets.Converters
{
    using Game.Ecs.Presets.Directional_Light.Converters;
    using Game.Ecs.Presets.SpotLightSettings.Converters;
    using Game.Ecs.Presets.FogShaderSettings.Converters;
    using System.Threading;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UniGame.LeoEcs.Converter.Runtime;
    using System;
    using Abstract;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;

    public class MonoGeneralLightSettingsPresetSourceConverter : MonoLeoEcsConverter
    {
        public GeneralLightSettingsPresetSourceConverter generalLightConverter = new();
        
        public sealed override void Apply(GameObject target, EcsWorld world, int entity)
        {
            generalLightConverter.Apply(world, entity);
        }
    }


    [Serializable]
    public sealed class GeneralLightSettingsPresetSourceConverter : EcsComponentConverter, IPresetAction
    {
        [PropertySpace(SpaceBefore = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Rendering")]
        [OnValueChanged(nameof(AutoUpdate))]
        public RenderingSettingsSourceConverter renderingConverter = 
            new RenderingSettingsSourceConverter() { showButtons = false };

        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Fog Shader")]
        [OnValueChanged(nameof(AutoUpdate))]
        public FogShaderSettingsSourceConverter fogShaderConverter = 
            new FogShaderSettingsSourceConverter() { showButtons = false };
        
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Spot Light")]
        [OnValueChanged(nameof(AutoUpdate))]
        public SpotLightSettingsSourceConverter spotLightConverter = 
            new SpotLightSettingsSourceConverter() { showButtons = false };
        
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Directional Light")]
        [OnValueChanged(nameof(AutoUpdate))]
        public DirectionalLightSettingsSourceConverter directionalLightConverter = 
            new DirectionalLightSettingsSourceConverter() { showButtons = false };


        public override void Apply(EcsWorld world, int entity)
        {
            if(renderingConverter.isEnabled)
                renderingConverter.Apply(world, world.NewEntity());
            if(fogShaderConverter.isEnabled)
                fogShaderConverter.Apply(world, world.NewEntity());
            if(spotLightConverter.isEnabled)
                spotLightConverter.Apply(world, world.NewEntity()); 
            if(directionalLightConverter.isEnabled)
                directionalLightConverter.Apply(world, world.NewEntity());
        }

        private void AutoUpdate()
        {
            fogShaderConverter.ApplyToTarget();
        }

        [ButtonGroup]
        public void Bake()
        {
            fogShaderConverter.Bake();
            renderingConverter.Bake();
            spotLightConverter.Bake();
            directionalLightConverter.Bake();
        }

        [ButtonGroup]
        public void ApplyToTarget()
        {
            fogShaderConverter.ApplyToTarget();
            renderingConverter.ApplyToTarget();
            spotLightConverter.ApplyToTarget();
            directionalLightConverter.ApplyToTarget();
        }
    }
}