namespace Game.Ecs.Ability.Systems
{
    using System;
    using Aspects;
    using Common.Components;
    using Components.Requests;
    using Core.Components;
    using Leopotam.EcsLite;
    using Tools;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    /// <summary>
    /// Activate ability by id
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ActivateAbilitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private AbilityTools _abilityTools;
        private EcsWorld _world;

        private EcsFilter _abilityFilter;
        private EcsFilter _requestFilter;
        private AbilityAspect _abilityAspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            
            _requestFilter = _world
                .Filter<ActivateAbilityRequest>()
                .End();

            _abilityFilter = _world
                .Filter<ActiveAbilityComponent>()
                .Inc<AbilityIdComponent>()
                .Inc<OwnerComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _requestFilter)
            {
                ref var request = ref _abilityAspect.ActivateAbilityOnTarget.Get(requestEntity);
                if(!request.Target.Unpack(_world,out var targetEntity))
                    continue;

                if(!request.Ability.Unpack(_world,out var targetAbilityEntity))
                    continue;

                foreach (var abilityEntity in _abilityFilter)
                {
                    ref var ownerComponent = ref _abilityAspect.Owner.Get(abilityEntity);
                    if(!ownerComponent.Value.Unpack(_world,out _)) continue;
                    if (!ownerComponent.Value.EqualsTo(request.Target)) continue;
                    
                    if (abilityEntity != targetAbilityEntity) continue;
                    
                    _abilityTools.ActivateAbility(_world,targetEntity,abilityEntity);
                    
                    break;
                }
                
            }
        }
    }
}