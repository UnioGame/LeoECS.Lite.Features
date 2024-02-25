namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Switchers.UsageCounterForAbilitySwitcher.Systems
{
	using System;
	using AbilitySwitcher.Components;
	using Aspects;
	using Components;
	using Game.Ecs.Ability.Common.Components;
	using Leopotam.EcsLite;
	using Tools;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	/// <summary>
	/// Counts usages of ability and switches it to another ability after count of usages.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class UsageCounterForAbilitySwitcherSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private UsageCounterForAbilitySwitcherAspect _aspect;
		private EcsFilter _abilityFilter;
		private EcsFilter _usageCounterFilter;
		private AbilityTools _abilityTools;

		public UsageCounterForAbilitySwitcherSystem(AbilityTools abilityTools)
		{
			_abilityTools = abilityTools;
		}
		
		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_abilityFilter = _world
				.Filter<ApplyAbilitySelfRequest>()
				.End();
			_usageCounterFilter = _world
				.Filter<UsageCounterForAbilitySwitcherComponent>()
				.Inc<AbilitySwitcherComponent>()
				.Exc<AbilityUsingComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var requestAbilityEntity in _abilityFilter)
			{
				ref var requestAbilityComponent = ref _aspect.ApplyAbilitySelfRequest.Get(requestAbilityEntity);
				if (!requestAbilityComponent.Value.Unpack(_world, out var abilityEntity))
					continue;
				
				foreach (var counterEntity in _usageCounterFilter)
				{
					ref var owner = ref _aspect.Owner.Get(counterEntity);
					ref var usageCounter = ref _aspect.UsageCounterForAbilitySwitcher.Get(counterEntity);
					if (!_abilityTools.TryGetAbilityById(ref owner.Value, usageCounter.abilityId, out var currentAbilityEntity))
						continue;
					if (currentAbilityEntity != abilityEntity)
						continue;
					usageCounter.count++;
					if (usageCounter.count < usageCounter.baseCount)
						continue;
					usageCounter.count = 0;
					var requestEntity = _world.NewEntity();
					ref var request = ref _aspect.AbilitySwitchRequest.Add(requestEntity);
					request.OldAbility = currentAbilityEntity.PackedEntity(_world);
					request.NewAbility = counterEntity.PackedEntity(_world);
				}
			}
		}
	}
}