namespace Game.Ecs.Gameplay.Tutorial.Systems
{
	using System;
	using System.Linq;
	using Aspects;
	using Components;
	using Leopotam.EcsLite;
	using UnityEngine;
	using UnityEngine.Pool;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Run tutorial actions.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class RunTutorialActionsSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private TutorialTriggerPointAspect _aspect;
		private EcsFilter _requestFilter;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_requestFilter = _world
				.Filter<RunTutorialActionsRequest>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var requestEntity in _requestFilter)
			{
				ref var requestComponent = ref _aspect.RunTutorialActionsRequest.Get(requestEntity);
				if (!requestComponent.Source.Unpack(_world, out var sourceEntity))
					continue;
				ref var actionsComponent = ref _aspect.TutorialActions.Get(sourceEntity);
				foreach (var action in actionsComponent.Actions)
				{
					var actionEntity = _world.NewEntity();
					ref var actionOwnerComponent = ref _aspect.Owner.Add(actionEntity);
					actionOwnerComponent.Value = _world.PackEntity(sourceEntity);
					action.ComposeEntity(_world, actionEntity);
				}
			}
		}
	}
}