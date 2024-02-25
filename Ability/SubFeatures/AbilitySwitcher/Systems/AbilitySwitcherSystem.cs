namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Systems
{
	using System;
	using System.Linq;
	using Aspects;
	using Components;
	using Leopotam.EcsLite;
	using Target.Tools;
	using Tools;
	using UniGame.Core.Runtime.Extension;
	using UniGame.LeoEcs.Shared.Extensions;
	using UniGame.Runtime.ObjectPool.Extensions;
	using UnityEngine;
	using UnityEngine.Pool;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Do ability switch. Await for <see cref="AbilitySwitcherRequest"/> and switch ability.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class AbilitySwitcherSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private AbilitySwitcherAspect _aspect;
		private EcsFilter _filter;
		private AbilityTools _abilityTools;
		
		public AbilitySwitcherSystem(AbilityTools abilityTools)
		{
			_abilityTools = abilityTools;
		}

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<AbilitySwitcherRequest>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var request = ref _aspect.AbilitySwitchRequest.Get(entity);
				if (!request.OldAbility.Unpack(_world, out var oldAbilityEntity))
					continue;
				if (!request.NewAbility.Unpack(_world, out var newAbilityEntity))
					continue;
				ref var owner = ref _aspect.Owner.Get(oldAbilityEntity);
				if (!owner.Value.Unpack(_world, out var ownerEntity))
					continue;
				
				// remove old ability
				ref var completeRequest = ref _aspect.CompleteAbilitySelfRequest.GetOrAddComponent(oldAbilityEntity);
				ref var restartAbilityCooldown = ref _aspect.RestartAbilityCooldownSelfRequest.GetOrAddComponent(oldAbilityEntity);
				
				_abilityTools.ActivateAbility(_world,ownerEntity, newAbilityEntity);
			}
		}
	}
}