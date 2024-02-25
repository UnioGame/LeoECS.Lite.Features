namespace Game.Ecs.Gameplay.CriticalAttackChance.Systems
{
    using System;
    using Damage.Components;
    using Game.Ecs.Characteristics.CriticalChance.Components;
    using Leopotam.EcsLite;
    using Requests;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// recalculate is next attack critical or not
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class DetectAttackDamageEventSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _damageFilter;

        private EcsPool<MadeDamageEvent> _damagePool;
        private EcsPool<CriticalChanceComponent> _criticalComponent;
        private EcsPool<RecalculateCriticalChanceSelfRequest> _recalculaltePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _damageFilter = _world
                .Filter<MadeDamageEvent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _damageFilter)
            {
                ref var eventComponent = ref _damagePool.Get(entity);
                if(!eventComponent.Source.Unpack(_world,out var sourceEntity))continue;
                
                if(!_criticalComponent.Has(sourceEntity))continue;

                _recalculaltePool.GetOrAddComponent(sourceEntity);
            }
        }
    }
}