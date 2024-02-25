namespace Game.Ecs.Presets.FogShaderSettings.Systems
{
    using Game.Ecs.Presets.Components;
    using Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using System;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Apply fog shader preset in game.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ApplyFogShaderSettingsPresetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _targetFilter;

        private EcsPool<PresetApplyingDataComponent> _applyingDataPool;
        private EcsPool<FogShaderSettingsPresetComponent> _presetPool;
        private EcsPool<PresetProgressComponent> _progressPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _targetFilter = _world
                .Filter<FogShaderSettingsPresetComponent>()
                .Inc<PresetTargetComponent>()
                .Inc<PresetApplyingComponent>()
                .Inc<PresetApplyingDataComponent>()
                .Inc<PresetProgressComponent>()
                .End();

            _progressPool = _world.GetPool<PresetProgressComponent>();
            _applyingDataPool = _world.GetPool<PresetApplyingDataComponent>();
            _presetPool = _world.GetPool<FogShaderSettingsPresetComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var targetEntity in _targetFilter)
            {
                ref var applyingDataComponent = ref _applyingDataPool.Get(targetEntity);
                if (!applyingDataComponent.Source.Unpack(_world, out var sourceEntity))
                    continue;

                ref var targetPresetComponent = ref _presetPool.GetOrAddComponent(targetEntity);
                ref var presetComponent = ref _presetPool.GetOrAddComponent(sourceEntity);
                ref var progressComponent = ref _progressPool.GetOrAddComponent(targetEntity);

                var activePreset = targetPresetComponent.Value;
                var sourcePreset = presetComponent.Value;
                
                activePreset.ApplyLerp(activePreset,sourcePreset,progressComponent.Value);
                activePreset.ApplyToShader();
            }
        }
    }
}