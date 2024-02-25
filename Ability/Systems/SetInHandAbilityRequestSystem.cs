namespace Game.Ecs.Ability.Common.Systems
{
    
    using System;
    using Components;
    using Leopotam.EcsLite;
    using Tools;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class SetInHandAbilityRequestSystem : IEcsRunSystem,IEcsInitSystem
    {
        private AbilityTools _abilityTools;
        
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<SetInHandAbilitySelfRequest> _setInHandPool;
        private EcsPool<ActiveAbilityComponent> _activePool;

        public SetInHandAbilityRequestSystem(AbilityTools abilityTools)
        {
            _abilityTools = abilityTools;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<SetInHandAbilitySelfRequest>()
                .Inc<AbilityInHandLinkComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var setInHand = ref _setInHandPool.Get(entity);
                if(!setInHand.Value.Unpack(_world,out var nextAbilityEntity))
                    continue;
                
                // var isAnyAbilityUsing = _abilityTools.IsAnyAbilityInUse(_world, entity);
                // if(isAnyAbilityUsing) continue;

                if(!_activePool.Has(nextAbilityEntity)) continue;
                
                _abilityTools.ChangeInHandAbility(_world,entity, nextAbilityEntity);
            }
        }
    }
}