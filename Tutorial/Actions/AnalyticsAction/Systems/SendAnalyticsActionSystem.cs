namespace Game.Ecs.Gameplay.Tutorial.Actions.AnalyticsAction.Systems
{
	using System;
	using System.Collections.Generic;
	using Aspects;
	using Components;
	using Data;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class SendAnalyticsActionSystem : IEcsInitSystem, IEcsRunSystem
	{
		private Dictionary<string,string> _emptyDictionary = new(0);
		private EcsWorld _world;
		private AnalyticsActionAspect _aspect;
		private EcsFilter _filter;
		private ITutorialAnalytics _analyticsService;
		private int _stepCounter;

		public SendAnalyticsActionSystem(ITutorialAnalytics analyticsService)
		{
			_analyticsService = analyticsService;
		}

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<AnalyticsActionComponent>()
				.Exc<CompletedAnalyticsActionComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				_stepCounter++;
				
				ref var request = ref _aspect.AnalyticsAction.Get(entity);
				_aspect.CompletedAnalyticsAction.Add(entity);
				
				if(_analyticsService == null) continue;
				
				_analyticsService.Send(new TutorialMessage()
				{
					id = _stepCounter.ToString(),
					message = request.stepName,
					data = _emptyDictionary,
				});
			}
		}
	}
}