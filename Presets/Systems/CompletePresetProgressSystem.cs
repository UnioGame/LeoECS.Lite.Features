namespace Game.Ecs.Presets.Systems
{
    using System;
    using Components;
    using Leopotam.EcsLite;

    /// <summary>
    /// Apply material preset to target system.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class CompletePresetProgressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _targetFilter;

        private EcsPool<PresetApplyingDataComponent> _applyingDataPool;
        private EcsPool<PresetApplyingComponent> _applyingPool;
        private EcsPool<PresetProgressComponent> _progressPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _targetFilter = _world
                .Filter<PresetApplyingComponent>()
                .Inc<PresetProgressComponent>()
                .Inc<PresetApplyingDataComponent>()
                .End();
            
            _world.GetPool<PresetIdComponent>();
            _applyingDataPool = _world.GetPool<PresetApplyingDataComponent>();
            _applyingPool = _world.GetPool<PresetApplyingComponent>();
            _progressPool = _world.GetPool<PresetProgressComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var targetEntity in _targetFilter)
            {
                ref var progressComponent = ref _progressPool.Get(targetEntity);
                var progress = progressComponent.Value;

                if (progress < 1f) continue;
                
                _applyingPool.Del(targetEntity);
                _applyingDataPool.Del(targetEntity);
            }
        }
    }
}