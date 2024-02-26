namespace Game.Ecs.Gameplay.Tutorial.Actions.FreezingTimeAction.Systems
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
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Freezing time action
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class FreezingTimeActionSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private FreezingTimeActionAspect _aspect;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<FreezingTimeActionComponent>()
				.Exc<CompletedFreezingTimeActionComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var freezingTimeActionComponent = ref _aspect.FreezingTimeAction.Get(entity);
				var requestEntity = _world.NewEntity();
				ref var requestComponent = ref _aspect.FreezingTimeRequest.Add(requestEntity);
				requestComponent.Duration = freezingTimeActionComponent.Duration;
				requestComponent.TimeScale = freezingTimeActionComponent.TimeScale;
				_aspect.CompletedFreezingTimeAction.Add(entity);
			}
		}
	}
}