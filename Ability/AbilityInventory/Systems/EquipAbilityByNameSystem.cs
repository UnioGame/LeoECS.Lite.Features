namespace Game.Ecs.AbilityInventory.Systems
{
    using System;
    using Ability.Common.Components;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.AddressableTools.Runtime;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Search ability in inventory
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class EquipAbilityByNameSystem : IEcsInitSystem, IEcsRunSystem
    {
        private AbilityInventoryAspect _inventoryAspect;
        private AbilityMetaAspect _metaAspect;
		
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _filterRequest;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
			
            _filter = _world
                .Filter<AbilityMetaComponent>()
                .Inc<NameComponent>()
                .Inc<AbilityIdComponent>()
                .End();
			
            _filterRequest = _world
                .Filter<EquipAbilityNameSelfRequest>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var abilityEntity in _filterRequest)
            {
                ref var requestComponent = ref _inventoryAspect.EquipByName.Get(abilityEntity);
                var abilityName = requestComponent.AbilityName;
				
                foreach (var entity in _filter)
                {
                    ref var abilityIdComponent = ref _metaAspect.Id.Get(entity);
                    ref var nameComponent = ref _metaAspect.Name.Get(entity);
					
                    if (!nameComponent.Value.Equals(abilityName)) continue;

                    ref var equipByIdRequest = ref _inventoryAspect.EquipById.GetOrAddComponent(abilityEntity);
                    equipByIdRequest.AbilityId = abilityIdComponent.AbilityId;
                    equipByIdRequest.IsUserInput = requestComponent.IsUserInput;
                    equipByIdRequest.AbilitySlot = requestComponent.AbilitySlot;
                    equipByIdRequest.Owner = requestComponent.Owner;
                    equipByIdRequest.IsDefault = requestComponent.IsDefault;
                    
                    break;
                }
            }
        }
    }
}