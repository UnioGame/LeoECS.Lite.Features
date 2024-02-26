namespace Game.Ecs.Gameplay.Tutorial.Actions.EquipAbilityAction.Systems
{
	using System;
	using System.Linq;
	using Aspects;
	using Components;
	using Core.Components;
	using Leopotam.EcsLite;
	using UniGame.Core.Runtime.Extension;
	using UniGame.Runtime.ObjectPool.Extensions;
	using UnityEngine;
	using UnityEngine.Pool;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Equip ability to champion.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class EquipAbilityActionSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EquipAbilityActionAspect _aspect;
		private EcsFilter _championFilter;
		private EcsFilter _abilityActionFilter;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_championFilter = _world
				.Filter<ChampionComponent>()
				.End();
			_abilityActionFilter = _world
				.Filter<EquipAbilityActionComponent>()
				.Exc<CompletedEquipAbilityActionComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			if (_championFilter.GetEntitiesCount() == 0)
				return;
			var championEntity = _championFilter.GetRawEntities().FirstOrDefault();
			
			foreach (var equipAbilityActionEntity in _abilityActionFilter)
			{
				ref var equipAbilityActionComponent = ref _aspect.EquipAbilityAction.Get(equipAbilityActionEntity);
				
                foreach (var abilityCell in equipAbilityActionComponent.AbilityCells)
				{
					var abilityId = abilityCell.AbilityId;
					var slot = abilityCell.SlotId;
				
					var requestEntity = _world.NewEntity();
					ref var request = ref _aspect.EquipAbilityIdRequest.Add(requestEntity);
					request.AbilityId = abilityId;
					request.AbilitySlot = slot;
					request.IsUserInput = true;
					request.IsDefault = slot == 0;
					request.Owner = _world.PackEntity(championEntity);
				}
                _aspect.CompletedEquipAbilityAction.Add(equipAbilityActionEntity);
			}
		}
	}
}