namespace Game.Ecs.AbilityInventory.Systems
{
	using System;
	using Ability.Common.Components;
	using Code.Services.AbilityLoadout.Abstract;
	using Components;
	using Core.Components;
	using Cysharp.Threading.Tasks;
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
	public class AbilityInventoryUpdateSlotDataSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IAbilityLoadoutService _abilityService;
		
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsFilter _eventFilter;
		
		private EcsPool<AbilityIdComponent> _abilityIdPool;
		private EcsPool<AbilityEquipChangedEvent> _eventPool;

		public AbilityInventoryUpdateSlotDataSystem(IAbilityLoadoutService abilityLoadoutAbilityService)
		{
			_abilityService = abilityLoadoutAbilityService;
		}

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
				
			_filter = _world
				.Filter<AbilityInventoryComponent>()
				.Inc<ChampionComponent>()
				.Exc<AbilityInventorySaveCompleteEvent>()
				.End();

			_eventFilter = _world
				.Filter<AbilityEquipChangedEvent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var eventEntity in _eventFilter)
			{
				ref var eventComponent = ref _eventPool.Get(eventEntity);
				if(!eventComponent.Owner.Unpack(_world,out var ownerEntity))
					continue;

				foreach (var entity in _filter)
				{
					if(entity != ownerEntity) continue;
						
					_abilityService
						.EquipAsync(eventComponent.AbilityId,eventComponent.AbilitySlot)
						.Forget();
					
					var saveEventEntity = _world.NewEntity();
					
					ref var saveEventComponent = ref _world
						.AddComponent<AbilityInventorySaveCompleteEvent>(saveEventEntity);
					saveEventComponent.Value = _world.PackEntity(entity);
				}
			}
			
		}

	}
}