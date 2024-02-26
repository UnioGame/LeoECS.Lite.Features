namespace Game.Ecs.Gameplay.Tutorial.Triggers.ActionTrigger.Systems
{
	using System;
	using Aspects;
	using Components;
	using Leopotam.EcsLite;
	using Tutorial.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Sends request to run tutorial actions.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class ActionTriggerSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _startLevelFilter;
		private EcsFilter _requestFilter;
		private EcsFilter _actionTriggerFilter;
		private ActionTriggerAspect _aspect;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			
			_startLevelFilter = _world
				.Filter<TutorialReadyComponent>()
				.End();
			
			_requestFilter = _world
				.Filter<ActionTriggerRequest>()
				.End();
			
			_actionTriggerFilter = _world
				.Filter<ActionTriggerComponent>()
				.Exc<CompletedActionTriggerComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			if (_startLevelFilter.GetEntitiesCount() == 0)
				return;
			foreach (var entity in _requestFilter)
			{
				ref var request = ref _aspect.ActionTriggerRequest.Get(entity);
				foreach (var actionTriggerEntity in _actionTriggerFilter)
				{
					ref var actionTrigger = ref _aspect.ActionTrigger.Get(actionTriggerEntity);
					var actionId = actionTrigger.ActionId.ToString();
					if (!actionId.Equals(request.ActionId)) 
						continue;
					_aspect.CompletedActionTrigger.Add(actionTriggerEntity);
					
					var requestEntity = _world.NewEntity();
					ref var runTutorialRequest = ref _aspect.RunTutorialActionsRequest.Add(requestEntity);
					runTutorialRequest.Source = _world.PackEntity(actionTriggerEntity);
				}
			}
		}
	}
}