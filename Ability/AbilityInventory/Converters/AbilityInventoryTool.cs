namespace Game.Ecs.AbilityInventory.Converters
{
	/// <summary>
	/// Convert ability meta data to entity
	/// </summary>
	using System;
	using AbilityUnlock.Components;
	using Aspects;
	using Code.Configuration.Runtime.Ability.Description;
	using Code.Services.AbilityLoadout.Data;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;
	
	[Serializable]
	[ECSDI]
	public class AbilityInventoryTool : IEcsSystem, IEcsInitSystem
	{
		private EcsWorld _world;
		private AbilityMetaAspect _metaAspect;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
		}
		
		public int Convert(AbilityItemData itemData,int entity)
		{
			var data = itemData.data;
			
			ref var abilityIdComponent = ref _metaAspect.Id.GetOrAddComponent(entity);
			ref var abilityMetaComponent = ref _metaAspect.Meta.GetOrAddComponent(entity);
			ref var abilityConfigurationComponent = ref _metaAspect.ConfigurationReference.GetOrAddComponent(entity);
			ref var visualDescriptionComponent = ref _metaAspect.Visual.GetOrAddComponent(entity);
			ref var nameComponent = ref _metaAspect.Name.GetOrAddComponent(entity);
			ref var abilitySlotTypeComponent = ref _metaAspect.Slot.GetOrAddComponent(entity);
			
			abilityConfigurationComponent.AbilityConfiguration = itemData.configurationReference;

			var visualDescription = itemData.visualDescription;
			visualDescriptionComponent.Name = visualDescription.Name;
			visualDescriptionComponent.Description = visualDescription.Description;
			visualDescriptionComponent.ManaCost = visualDescription.manaCost;
			visualDescriptionComponent.Icon = visualDescription.icon;
	
			nameComponent.Value = visualDescription.Name;
			abilityMetaComponent.AbilityId = itemData.id;
			abilityMetaComponent.SlotType = data.slotType;
			abilityMetaComponent.Hide = data.isHidden;
			abilityMetaComponent.IsBlocked = data.isBlock;
			
			if (data.isHidden)
				_world.AddComponent<AbilityInventoryHideComponent>(entity);

			if (data.isBlock)
				_metaAspect.Blocked.Add(entity);
			
			abilityIdComponent.AbilityId = (AbilityId)itemData.id;
			abilitySlotTypeComponent.SlotType = data.slotType;

			ref var abilityUnlockLevelComponent = ref _world
				.GetOrAddComponent<AbilityUnlockLevelComponent>(entity);
			abilityUnlockLevelComponent.Level = data.unlockLevel;

			return entity;
		}


	}
}