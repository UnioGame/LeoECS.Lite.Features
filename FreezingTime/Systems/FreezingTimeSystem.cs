namespace Game.Ecs.Gameplay.FreezingTime.Systems
{
	using System;
	using Aspects;
	using Components;
	using Leopotam.EcsLite;
	using PrimeTween;
	using UnityEngine;
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
		private Tween _tween;
		private bool _newTimeScale;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_freezingTimeRequestFilter = _world
				.Filter<FreezingTimeRequest>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			if (_newTimeScale)
			{
				var entityEvent = _world.NewEntity();
				ref var freezingTimeEvent = ref _aspect.freezingTimeCompletedEvent.Add(entityEvent);
				freezingTimeEvent.TimeScale = Time.timeScale;
				_newTimeScale = false;
			}
			
			foreach (var requestEntity in _freezingTimeRequestFilter)
			{
				ref var request = ref _aspect.freezingTimeRequest.Get(requestEntity);
				var oldScale = Time.timeScale;
				var newScale = request.TimeScale;
				newScale = Mathf.Clamp(newScale, 0f, 1f);
				var duration = request.Duration;

				if(_tween.isAlive) _tween.Stop();
				
				_tween = Tween.GlobalTimeScale(newScale, duration)
					.OnComplete(this,x => x._newTimeScale = true);
			}
		}
	}
}