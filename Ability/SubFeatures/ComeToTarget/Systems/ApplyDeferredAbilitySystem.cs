namespace Game.Ecs.Ability.SubFeatures.ComeToTarget.Systems
{
    using System;
    using Common.Components;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ApplyDeferredAbilitySystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<DeferredAbilityComponent> _deferredPool;
        private EcsPool<AbilityInHandComponent> _inHandPool;
        private EcsPool<AbilityValidationSelfRequest> _requestPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<DeferredAbilityComponent>()
                .Inc<OwnerComponent>()
                .Exc<UpdateComePointComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            

            foreach (var entity in _filter)
            {
                _deferredPool.Del(entity);

                if(!_inHandPool.Has(entity))
                    continue;
                
                _requestPool.GetOrAddComponent(entity);
            }
        }
    }
}