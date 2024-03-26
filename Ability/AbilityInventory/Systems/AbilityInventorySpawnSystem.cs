namespace Game.Ecs.AbilityInventory.Systems
{
	using System;
	using Ability.Common.Components;
	using Code.Services.AbilityLoadout.Abstract;
	using Components;
	using Core.Components;
	using Leopotam.EcsLite;
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
	public class AbilityInventorySpawnSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private IAbilityLoadoutService _service;

		public AbilityInventorySpawnSystem(IAbilityLoadoutService abilityLoadoutAbilityService)
		{
			_service = abilityLoadoutAbilityService;
		}
		
		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			
			_filter = _world
				.Filter<AbilityInventoryProfileComponent>()
				.Inc<AbilityMapComponent>()
				.Exc<AbilityInventorySpawnDoneComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				foreach (var slotData in _service.AbilitySlotData)
				{
					var requestEntity = _world.NewEntity();
					ref var request = ref _world.GetOrAddComponent<EquipAbilityIdSelfRequest>(requestEntity);
					
					request.AbilityId = slotData.ability;
					request.AbilitySlot = slotData.slotType;
					request.Owner = _world.PackEntity(entity);
					request.IsUserInput = true;
					request.IsDefault = slotData.slotType == 0;
				}
				
				_world.AddComponent<AbilityInventorySpawnDoneComponent>(entity);
			}
			
		}
	}
}