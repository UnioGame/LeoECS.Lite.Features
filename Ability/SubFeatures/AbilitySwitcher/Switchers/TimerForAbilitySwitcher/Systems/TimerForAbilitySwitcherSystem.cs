namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Switchers.TimerForAbilitySwitcher.Systems
{
	using System;
	using System.Linq;
	using Aspects;
	using Common.Components;
	using Components;
	using Leopotam.EcsLite;
	using Tools;
	using UniGame.Core.Runtime.Extension;
	using UniGame.LeoEcs.Shared.Extensions;
	using UniGame.Runtime.ObjectPool.Extensions;
	using UnityEngine;
	using UnityEngine.Pool;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Switch ability by timer. Await for <see cref="ApplyAbilitySelfRequest"/>
	/// and <see cref="TimerForAbilitySwitcherReadyComponent"/>
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class TimerForAbilitySwitcherSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private TimerForAbilitySwitcherAspect _aspect;
		private EcsFilter _filter;
		private EcsFilter _abilityFilter;
		
		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<TimerForAbilitySwitcherReadyComponent>()
				.End();
			_abilityFilter = _world
				.Filter<ApplyAbilitySelfRequest>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var timerData = ref _aspect.TimerForAbilitySwitcherComponent.Get(entity);
				var time = timerData.IsUnscaledTime ? Time.unscaledTime : Time.time;
				
				foreach (var requestEntity in _abilityFilter)
				{
					ref var applyAbilitySelfRequest = ref _aspect.ApplyAbilitySelfRequest.Get(requestEntity);
					if (!applyAbilitySelfRequest.Value.Unpack(_world, out var requestAbilityEntity))
						continue;
					if (requestAbilityEntity == entity)
						continue;
					
					ref var timerAbilityOwnerComponent = ref _aspect.OwnerComponent.Get(entity);
					var timerAbilityOwner = timerAbilityOwnerComponent.Value;
					if (!timerAbilityOwner.Unpack(_world, out var timerAbilityOwnerEntity))
						continue;
					if (requestEntity != timerAbilityOwnerEntity)
						continue;
					var delay = timerData.Delay;
					timerData.DumpTime = time + delay;
					
					var switchEntity = _world.NewEntity();
					ref var request = ref _aspect.AbilitySwitchRequest.Add(switchEntity);
					request.OldAbility = requestAbilityEntity.PackedEntity(_world);
					request.NewAbility = entity.PackedEntity(_world);
					_aspect.TimerForAbilitySwitcherReadyComponent.TryRemove(entity);
					break;
				}
			}
		}
	}
}