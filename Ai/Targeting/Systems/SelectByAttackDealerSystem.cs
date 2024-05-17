namespace Game.Ecs.Ai.Targeting.Systems
{
    using System;
    using AI.Aspects;
    using AI.Components;
    using Aspects;
    using Components;
    using Core.Components;
    using global::Ai.Ai.Variants.Prioritizer.Aspects;
    using Leopotam.EcsLite;
    using Shared.Components;
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

        private AIAspect _aiAspect;
        private TargetingAspect _targetingAspect;
        private PrioritizerAspect _prioritizerAspect;

        private EcsPool<ChildrenComponent> _childrenPool;
        private EcsPool<TEMPORARY_HealthChangedEvent> _damageEventPool;
        private EcsPool<OwnerComponent> _ownerPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<SelectByAttackEventComponent>()
                .Inc<TEMPORARY_HealthChangedEvent>()
                .Exc<TargetingLock>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if (_aiAspect.GroupAgent.Has(entity))
                {
                    ref var ownerComponent = ref _ownerPool.Get(entity);
                    if (!ownerComponent.Value.Unpack(_world, out var targetGroupEntity))
                    {
                        continue;
                    }

                    ref var childrenComponent = ref _childrenPool.Get(targetGroupEntity);
                    foreach (var groupAgent in childrenComponent.Children)
                    {
                        if (!groupAgent.Unpack(_world, out var targetGroupAgent))
                        {
                            continue;
                        }

                        _prioritizerAspect.Agro.TryAdd(targetGroupAgent);
                    }
                }
                else
                {
                    _prioritizerAspect.Agro.TryAdd(entity);
                }
            }
        }
    }
}