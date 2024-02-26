namespace Game.Ecs.Gameplay.Tutorial.Triggers.DistanceTrigger.Systems
{
	using System;
	using System.Linq;
	using Aspects;
	using Components;
	using Core.Components;
	using Leopotam.EcsLite;
	using Tutorial.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using Unity.Mathematics;

	/// <summary>
	/// Sends request to run tutorial actions when champion is in trigger distance.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class TutorialTriggerDistanceSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private DistanceTriggerAspect _aspect;
		private EcsFilter _startLevelFilter;
		private EcsFilter _championFilter;
		private EcsFilter _distanceTriggerPointFilter;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();

			_startLevelFilter = _world
				.Filter<TutorialReadyComponent>()
				.End();
			
			_championFilter = _world
				.Filter<ChampionComponent>()
				.End();
			
			_distanceTriggerPointFilter = _world
				.Filter<DistanceTriggerPointComponent>()
				.Exc<CompletedDistanceTriggerPointComponent>()
				.End();
			
		}

		public void Run(IEcsSystems systems)
		{
			if (_startLevelFilter.GetEntitiesCount() == 0)
				return;
			if (_championFilter.GetEntitiesCount() == 0)
				return;
			var championEntity = _championFilter.GetRawEntities().FirstOrDefault();
			ref var positionComponent = ref _aspect.Position.Get(championEntity);
			ref var position = ref positionComponent.Position;
			
			foreach (var triggerEntity in _distanceTriggerPointFilter)
			{
				ref var triggerOwnerComponent = ref _aspect.Owner.Get(triggerEntity);
				if (!triggerOwnerComponent.Value.Unpack(_world, out var triggerOwnerEntity))
					continue;
				
				ref var triggerTransformComponent = ref _aspect.Position.Get(triggerOwnerEntity);
				var triggerTransform = triggerTransformComponent.Position;
				
				ref var distanceTriggerPointComponent = ref _aspect.DistanceTriggerPoint.Get(triggerEntity);
				
				var distance = math.distance(position, triggerTransform);
				if (distance > distanceTriggerPointComponent.TriggerDistance)
					continue;
				
				_aspect.CompletedDistanceTriggerPoint.Add(triggerEntity);
				
				var requestEntity = _world.NewEntity();
				ref var request = ref _aspect.RunTutorialActionsRequest.Add(requestEntity);
				request.Source = _world.PackEntity(triggerEntity);
			}
		}
	}
}