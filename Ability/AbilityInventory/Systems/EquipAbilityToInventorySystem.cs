namespace Game.Ecs.AbilityInventory.Systems
{
    using System;
    using Ability.Common.Components;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    
    /// <summary>
    /// Validate ability before equip
    /// </summary>
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class EquipAbilityToInventorySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filterRequest;
        
        private AbilityInventoryAspect _abilityInventory;
        private AbilityMetaAspect _metaAspect;
        private EcsFilter _metaFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
			
            _filterRequest = _world
                .Filter<EquipAbilitySelfRequest>()
                .Exc<AbilityLoadingComponent>()
                .Exc<AbilityBuildingComponent>()
                .Exc<AbilityMetaLinkComponent>()
                .End();

            _metaFilter = _world
                .Filter<AbilityMetaComponent>()
                .Inc<AbilityIdComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var abilityEntity in _filterRequest)
            {
                ref var requestComponent = ref _abilityInventory.Equip.Get(abilityEntity);
                var metaExists = false;
                
                foreach (var metaEntity in _metaFilter)
                {
                    ref var metaIdComponent = ref _metaAspect.Id.Get(metaEntity);
                    metaExists = metaIdComponent.AbilityId == requestComponent.AbilityId;
                    
                    if(!metaExists) continue;

                    ref var metaLinkComponent = ref _abilityInventory.MetaLink.GetOrAddComponent(abilityEntity);
                    metaLinkComponent.Value = _world.PackEntity(metaEntity);
                    
                    break;
                }

                if (metaExists) continue;
                
                ref var metaRequestComponent = ref _abilityInventory.LoadMeta.Add(abilityEntity);
                metaRequestComponent.AbilityId = requestComponent.AbilityId;
                _abilityInventory.Loading.Add(abilityEntity);
            }
        }
    }
    
    
}