namespace Game.Ecs.AbilityInventory.Systems
{
	using System;
	using System.Collections.Generic;
	using Code.Services.Ability.Data;
	using Code.Services.AbilityLoadout.Abstract;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;
	/// <summary>
	/// Initialize ability rarity map
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
	public class AbilityRarityMapSystem : IEcsInitSystem
	{
		private EcsWorld _world;
		private IAbilityLoadoutService _service;

		public AbilityRarityMapSystem(IAbilityLoadoutService service)
		{
			_service = service;
		}
		
		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			var rarityMapEntity = _world.NewEntity();
			ref var rarityMapComponent = ref _world.AddComponent<AbilityRarityMapComponent>(rarityMapEntity);
			rarityMapComponent.DisableSlot = _service.AbilityRarityData.disableSlot;
			rarityMapComponent.Slots = new List<AbilityRaritySlot>();
			rarityMapComponent.Slots.AddRange(_service.AbilityRarityData.slots);
		}
		
	}	
}