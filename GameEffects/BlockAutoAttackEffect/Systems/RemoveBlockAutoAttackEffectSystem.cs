namespace Game.Ecs.GameEffects.BlockAutoAttackEffect.Systems
{
	using System;
	using System.Linq;
	using Ability.Common.Components;
	using AbilityInventory.Components;
	using Leopotam.EcsLite;
	using Time.Service;
	using UniGame.Core.Runtime.Extension;
	using UniGame.Runtime.ObjectPool.Extensions;
	using UnityEngine;
	using UnityEngine.Pool;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Remove block auto attack effect system.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class RemoveBlockAutoAttackEffectSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _abilityFilter;
		private EcsPool<AbilityPauseComponent> _pauseAbilityPool;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_abilityFilter = _world
				.Filter<AbilityPauseComponent>()
				.Inc<DefaultAbilityComponent>()
				.Exc<AbilityMetaComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var abilityEntity in _abilityFilter)
			{
				ref var pauseAbilityComponent = ref _pauseAbilityPool.Get(abilityEntity);
				if (pauseAbilityComponent.Duration > GameTime.Time)
					continue;
				_pauseAbilityPool.Del(abilityEntity);
			}
		}
	}
}