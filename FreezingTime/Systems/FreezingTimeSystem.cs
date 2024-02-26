namespace Game.Ecs.Gameplay.FreezingTime.Systems
{
	using System;
	using System.Linq;
	using Aspects;
	using Components;
	using DG.Tweening;
	using Leopotam.EcsLite;
	using Time.Service;
	using UniCore.Runtime.ProfilerTools;
	using UnityEngine;
	using UnityEngine.Pool;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Freezes time for gameplay. Await for FreezingTimeRequest.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class FreezingTimeSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private FreezingTimeAspect _aspect;
		private EcsFilter _freezingTimeRequestFilter;
		private Tweener _tweener;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_freezingTimeRequestFilter = _world
				.Filter<FreezingTimeRequest>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var requestEntity in _freezingTimeRequestFilter)
			{
				ref var request = ref _aspect.freezingTimeRequest.Get(requestEntity);
				var oldScale = Time.timeScale;
				var newScale = request.TimeScale;
				newScale = Mathf.Clamp(newScale, 0f, 1f);
				var duration = request.Duration;

				_tweener?.Kill(true);
				_tweener = DOVirtual.Float(oldScale, newScale, duration, value =>
					{
						Time.timeScale = value;
					})
					.SetUpdate(true)
					.OnComplete(() =>
					{
						var entityEvent = _world.NewEntity();
						ref var freezingTimeEvent = ref _aspect.freezingTimeCompletedEvent.Add(entityEvent);
						freezingTimeEvent.TimeScale = Time.timeScale;
					});
			}
		}
	}
}