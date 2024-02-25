namespace Game.Ecs.Presets.Systems
{
    using System;
    using System.Linq;
    using Components;
    using Leopotam.EcsLite;
    using Time.Service;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UniModules.UniCore.Runtime.Time;
    using UnityEngine;
    using UnityEngine.Pool;

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
    [ECSDI]
    public class ApplyMaterialPresetToTargetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _targetFilter;

        private EcsPool<PresetApplyingDataComponent> _applyingDataPool;
        private EcsPool<MaterialPresetComponent> _materialDataPool;
        private EcsPool<PresetProgressComponent> _progressPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _targetFilter = _world
                .Filter<MaterialPresetComponent>()
                .Inc<PresetTargetComponent>()
                .Inc<PresetApplyingComponent>()
                .Inc<PresetProgressComponent>()
                .Inc<PresetApplyingDataComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var targetEntity in _targetFilter)
            {
                ref var dataComponent = ref _applyingDataPool.Get(targetEntity);
                if(!dataComponent.Source.Unpack(_world,out var sourceEntity))
                    continue;

                ref var targetMaterialData = ref _materialDataPool.Get(targetEntity);
                ref var sourceMaterialData = ref _materialDataPool.Get(sourceEntity);

                ref var progressComponent = ref _progressPool.Get(targetEntity);

                var targetMaterial = targetMaterialData.Value;
                var sourceMaterial = sourceMaterialData.Value;
                
                targetMaterialData.Value.Lerp(targetMaterial,sourceMaterial,progressComponent.Value);
            }
        }
    }
}