namespace Game.Ecs.Ability.UserInput.Systems
{
    using System;
    using System.Collections.Generic;
    using Common.Components;
    using Input.Components.Ability;
    using Leopotam.EcsLite;
    using Tools;
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
    public sealed class RestoreDefaultInHandAbilitySystem : IEcsRunSystem, IEcsInitSystem
    {
        private AbilityTools _abilityTools;
        
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<AbilityMapComponent> _abilityMapPool;
        private EcsPool<AbilityInHandLinkComponent> _abilityInHandLinkPool;
        private EcsPool<DefaultAbilityComponent> _defaultAbilityPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            
            _filter = _world
                .Filter<AbilityMapComponent>()
                .Inc<AbilityInHandLinkComponent>()
                .Exc<AbilityUpInputRequest>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var abilityMap = ref _abilityMapPool.Get(entity);
                ref var abilityInHandLinkComponent = ref _abilityInHandLinkPool.Get(entity);
                ref var inHandAbility = ref abilityInHandLinkComponent.AbilityEntity;
                
                if (!inHandAbility.Unpack(_world, out var abilityInHand))
                    continue;
                
                if (_defaultAbilityPool.Has(abilityInHand)) continue;
                
                if(_abilityTools.IsAnyAbilityInUse(entity)) continue;
                
                foreach (var packedAbilityEntity in abilityMap.AbilityEntities)
                {
                    if(!packedAbilityEntity.Unpack(_world,out var abilityEntity))
                        continue;
                    if (!_defaultAbilityPool.Has(abilityEntity)) continue;
                    
                    _abilityTools.ChangeInHandAbility(_world, entity,abilityEntity);
                    
                    break;
                }

            }
        }
        
    }
}