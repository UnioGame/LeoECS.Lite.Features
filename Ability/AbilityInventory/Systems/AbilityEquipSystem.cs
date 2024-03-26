namespace Game.Ecs.AbilityInventory.Systems
{
	using System;
	using System.Collections.Generic;
	using Ability.Aspects;
	using Ability.Common.Components;
	using Ability.Components;
	using Ability.Tools;
	using Aspects;
	using Code.Configuration.Runtime.Ability;
	using Code.Configuration.Runtime.Ability.Description;
	using Components;
	using Core.Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;

	/// <summary>
	/// Equip ability to slot
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class AbilityEquipSystem : IEcsInitSystem, IEcsRunSystem
	{
		private AbilityTools _abilityTools;
		private AbilityInventoryAspect _inventoryAspect;
		private AbilityAspect _abilityAspect;
		private AbilityOwnerAspect _abilityOwnerAspect;
		
		private EcsWorld _world;
		private EcsFilter _filter;
		
		private List<int> _removedEntities = new List<int>();

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_abilityTools = _world.GetGlobal<AbilityTools>();
			
			_filter = _world
				.Filter<AbilityInventoryCompleteComponent>()
				.Inc<EquipAbilitySelfRequest>()
				.Inc<AbilityBuildingComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			_removedEntities.Clear();
			
			foreach (var abilityEntity in _filter)
			{
				ref var requestComponent = ref _inventoryAspect.Equip.Get(abilityEntity);
				ref var ownerComponent = ref _inventoryAspect.Owner.Get(abilityEntity);

				var isUserInput  = requestComponent.IsUserInput;
				ref var slotType = ref requestComponent.AbilitySlot;
				ref var isDefault = ref requestComponent.IsDefault;
				ref var abilityId = ref requestComponent.AbilityId;
				var packedAbility =  _world.PackEntity(abilityEntity);

				if (!ownerComponent.Value.Unpack(_world, out var ownerAbilityEntity))
					continue;
				
				ref var abilityMapComponent = ref _abilityOwnerAspect.AbilityMap.Get(ownerAbilityEntity);

				if (slotType >= 0)
				{
					var abilitySlotEntity = abilityMapComponent.AbilityEntities[slotType];
					if (abilitySlotEntity.Unpack(_world, out var oldAbilityEntity))
						_removedEntities.Add(oldAbilityEntity);
					abilityMapComponent.AbilityEntities[slotType] = packedAbility;
				}

				_abilityAspect.Active.Add(abilityEntity);

				if (isDefault)
				{
					_abilityAspect.Default.GetOrAddComponent(abilityEntity);
					_abilityTools.ChangeInHandAbility(_world,ownerAbilityEntity,abilityEntity);
				}
				
				if (isUserInput)
					_abilityAspect.Input.GetOrAddComponent(abilityEntity);

				var eventEntity = _world.NewEntity();
				ref var abilityEquipChangedEvent = ref _inventoryAspect.EquipChanged.Add(eventEntity);
				abilityEquipChangedEvent.AbilityId = abilityId;
				abilityEquipChangedEvent.AbilitySlot = slotType;
				abilityEquipChangedEvent.Owner = ownerComponent.Value;
				abilityEquipChangedEvent.AbilityEntity = packedAbility;
				
				_inventoryAspect.Building.Del(abilityEntity);
				_inventoryAspect.Equip.Del(abilityEntity);
			}
			
			foreach (var removedEntity in _removedEntities)
				_world.DelEntity(removedEntity);
		}
	}
}