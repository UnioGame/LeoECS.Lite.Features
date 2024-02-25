namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Switchers.TimerForAbilitySwitcher.Systems
{
	using System;
	using System.Linq;
	using Aspects;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.Core.Runtime.Extension;
	using UniGame.LeoEcs.Shared.Extensions;
	using UniGame.Runtime.ObjectPool.Extensions;
	using UnityEngine;
	using UnityEngine.Pool;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Evaluate timer for ability switcher.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class EvaluateTimerForAbilitySwitcherSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private TimerForAbilitySwitcherAspect _aspect;
		private EcsFilter _filter;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<TimerForAbilitySwitcherComponent>()
				.Exc<TimerForAbilitySwitcherReadyComponent>()
				.End();
			
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var timerData = ref _aspect.TimerForAbilitySwitcherComponent.Get(entity);
				if (!(timerData.DumpTime < Time.time)) 
					continue;
				_aspect.TimerForAbilitySwitcherReadyComponent.Add(entity);
			}
		}
	}
}