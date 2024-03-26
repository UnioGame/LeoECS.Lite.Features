namespace Game.Ecs.AbilityInventory.Systems
{
    using System;
    using Ability.Aspects;
    using Ability.Tools;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Assembling ability
    /// </summary>
#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    [Serializable]
    public class AbilityBuildByConfigurationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private AbilityTools _abilityTools;
        private EcsWorld _world;
        private EcsFilter _filter;
        
        private AbilityInventoryAspect _inventoryAspect;
        private AbilityAspect _ability;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            
            _filter = _world
                .Filter<EquipAbilitySelfRequest>()
                .Inc<AbilityConfigurationComponent>()
                .Inc<AbilityEquipComponent>()
                .Inc<AbilityBuildingComponent>()
                .Exc<AbilityLoadingComponent>()
                .Exc<AbilityInventoryCompleteComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var abilityEntity in _filter)
            {
                ref var requestComponent = ref _inventoryAspect.Equip.GetOrAddComponent(abilityEntity);
                ref var configurationDataComponent = ref _inventoryAspect.Configuration.GetOrAddComponent(abilityEntity);

                var abilityConfiguration = configurationDataComponent.Value;

                var buildData = new AbilityBuildData()
                {
                    AbilityId = requestComponent.AbilityId,
                    Slot = requestComponent.AbilitySlot,
                    IsUserInput = requestComponent.IsUserInput,
                    IsDefault = requestComponent.IsDefault,
                    IsBlocked = requestComponent.IsBlocked,
                };

                _abilityTools.BuildAbility(abilityEntity,
                    ref requestComponent.Target,
                    abilityConfiguration,ref buildData);
                
                _inventoryAspect.Complete.Add(abilityEntity);
            }
        }
        
    }
}