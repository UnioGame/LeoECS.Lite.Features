namespace Game.Ecs.Ai.Targeting.Systems
{
    using System;
    using AI.Components;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SelectByAttackDealerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private TargetingAspect _targetingAspect;

        private EcsPool<TEMPORARY_HealthChangedEvent> _damageEventPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<SelectByAttackEventComponent>()
                .Inc<TEMPORARY_HealthChangedEvent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var selectByAttackEventComponent = ref _targetingAspect.SelectByAttackEvent.Get(entity);
                ref var attackEventTargetComponent = ref _targetingAspect.AttackEventTarget.GetOrAddComponent(entity);
                ref var damageEvent = ref _damageEventPool.Get(entity);
                attackEventTargetComponent.Duration = selectByAttackEventComponent.Duration;
                attackEventTargetComponent.Value = damageEvent.Dealer;
            }
        }
    }
}