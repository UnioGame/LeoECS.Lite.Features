namespace Game.Ecs.Ability.Systems
{
    using System;
    using System.Collections;
    using System.Linq;
    using Aspects;
    using Common.Components;
    using Components;
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
    /// mark ability ro as processing ability or not
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class UpdateAbilityProcessingStatusSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _abilityRootFilter;
        private EcsFilter _abilityInUseFilter;
        
        private AbilityAspect _abilityAspect;
        private AbilityOwnerAspect _abilityOwnerAspect;
        
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityRootFilter = _world
                .Filter<AbilityMapComponent>()
                .End();
            
            _abilityInUseFilter = _world
                .Filter<AbilityIdComponent>()
                .Inc<AbilityUsingComponent>()
                .Inc<ActiveAbilityComponent>()
                .Inc<OwnerComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _abilityRootFilter)
            {
                var targetAbility = -1;
                
                foreach (var abilityEntity in _abilityInUseFilter)
                {
                    ref var ownerComponent = ref _abilityAspect.Owner.Get(abilityEntity);
                    if (!ownerComponent.Value.Unpack(_world, out var ownerEntity))
                        continue;
                    
                    if (ownerEntity != entity) continue;

                    targetAbility = abilityEntity;
                }

                if (targetAbility < 0)
                {
                    _abilityOwnerAspect.AbilityInProcessing.TryRemove(entity);
                    continue;
                }

                ref var slotComponent = ref _abilityAspect.AbilitySlot.Get(targetAbility);
                ref var progressComponent = ref _abilityOwnerAspect.AbilityInProcessing.GetOrAddComponent(entity);
                
                progressComponent.Ability = _world.PackEntity(targetAbility);
                progressComponent.Slot = slotComponent.SlotType;
                progressComponent.IsDefault = _abilityAspect.Default.Has(targetAbility);
            }
        }
    }
}