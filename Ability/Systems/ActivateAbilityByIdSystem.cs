namespace Game.Ecs.Ability.Systems
{
    using System;
    using System.Linq;
    using Common.Components;
    using Components.Requests;
    using Core.Components;
    using Leopotam.EcsLite;
    using Tools;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

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
    public class ActivateAbilityByIdSystem : IEcsInitSystem, IEcsRunSystem
    {
        private AbilityTools _abilityTools;
        private EcsWorld _world;

        private EcsFilter _abilityFilter;
        private EcsFilter _requestFilter;

        private EcsPool<OwnerComponent> _ownerPool;
        private EcsPool<AbilityIdComponent> _abilityIdPool;
        private EcsPool<ActivateAbilityByIdRequest> _activateRequestPool;
        private EcsPool<ActivateAbilityRequest> _activateAbilityRequestPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            
            _requestFilter = _world
                .Filter<ActivateAbilityByIdRequest>()
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
                ref var request = ref _activateRequestPool.Get(requestEntity);
                if(!request.Target.Unpack(_world,out var targetEntity)) continue;

                foreach (var abilityEntity in _abilityFilter)
                {
                    ref var ownerComponent = ref _ownerPool.Get(abilityEntity);
                    if (!ownerComponent.Value.EqualsTo(request.Target)) continue;
                    
                    ref var abilityId = ref _abilityIdPool.Get(abilityEntity);
                    if (abilityId.AbilityId != request.AbilityId) continue;

                    ref var activateRequest = ref _activateAbilityRequestPool.GetOrAddComponent(requestEntity);
                    activateRequest.Ability = _world.PackEntity(abilityEntity);
                    activateRequest.Target = request.Target;
                    
                    break;
                }
                
            }
        }
    }
}