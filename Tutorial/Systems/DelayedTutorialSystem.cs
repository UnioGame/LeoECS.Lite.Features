namespace Game.Ecs.Gameplay.Tutorial.Systems
{
	using System;
	using System.Linq;
	using Aspects;
	using Components;
	using Leopotam.EcsLite;
	using Time.Service;
	using UniGame.Core.Runtime.Extension;
	using UniGame.LeoEcs.Shared.Extensions;
	using UniGame.Runtime.ObjectPool.Extensions;
	using UnityEngine;
	using UnityEngine.Pool;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Delayed  system. Trigger/Action compose after delay
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class DelayedTutorialSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private TutorialTriggerPointAspect _aspect;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<DelayedTutorialComponent>()
				.Exc<CompletedDelayedTutorialComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var delayedEffect = ref _aspect.DelayedTutorial.Get(entity);
				var nextApplyingTime = delayedEffect.LastApplyingTime + delayedEffect.Delay;
				if(Time.unscaledTime < nextApplyingTime && !Mathf.Approximately(nextApplyingTime, Time.unscaledTime))
					continue;
				delayedEffect.Context.ComposeEntity(_world, entity);
				_aspect.CompletedDelayedTutorial.Add(entity);
			}
		}
	}
}