namespace Game.Ecs.Presets.Systems
{
    using System;
    using System.Linq;
    using Components;
    using Leopotam.EcsLite;
    using Time.Service;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;

    /// <summary>
    /// update preset progression
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class CalculatePresetProgressSystem : IEcsInitSystem, IEcsRunSystem
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
                .Inc<PresetApplyingDataComponent>()
                .Inc<PresetProgressComponent>()
                .End();
            
            _applyingDataPool = _world.GetPool<PresetApplyingDataComponent>();
            _applyingPool = _world.GetPool<PresetApplyingComponent>();
            _progressPool = _world.GetPool<PresetProgressComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var targetEntity in _targetFilter)
            {
                ref var dataComponent = ref _applyingDataPool.Get(targetEntity);

                var timePassed = GameTime.Time - dataComponent.StartTime;
                var duration = dataComponent.Duration;
                var progress = duration <= 0 ? 1f : timePassed / duration;
                
                ref var progressComponent = ref _progressPool.Get(targetEntity);
                progressComponent.Value = progress;
            }
        }
    }
}