namespace Game.Ecs.Gameplay.CriticalAttackChance.Systems
{
    using System;
    using Components;
    using Game.Ecs.Characteristics.CriticalChance.Components;
    using Leopotam.EcsLite;
    using Requests;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Random = UnityEngine.Random;

    /// <summary>
    /// add ot remove from character CriticalAttackMarkerComponent as a critical attack marker
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class RecalculateCriticalChanceSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _damageFilter;
        
        private EcsPool<CriticalChanceComponent> _criticalChancePool;
        private EcsPool<CriticalAttackMarkerComponent> _criticalMarkerPool;
        private EcsPool<RecalculateCriticalChanceSelfRequest> _recalculaltePool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _damageFilter = _world
                .Filter<RecalculateCriticalChanceSelfRequest>()
                .Inc<CriticalChanceComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var sourceEntity in _damageFilter)
            {
                _criticalMarkerPool.TryRemove(sourceEntity);
                _recalculaltePool.TryRemove(sourceEntity);

                ref var criticalChance = ref _criticalChancePool.Get(sourceEntity);
                var isCritical = Random.Range(0.0f, 100.0f) < criticalChance.Value;
                
                if(!isCritical) continue;
                
                _criticalMarkerPool.GetOrAddComponent(sourceEntity);
            }
        }
    }
}