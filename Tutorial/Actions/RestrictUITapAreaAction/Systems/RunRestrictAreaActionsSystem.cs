namespace Game.Ecs.Gameplay.Tutorial.Actions.RestrictUITapAreaAction.Systems
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
	/// ADD DESCRIPTION HERE
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class RunRestrictAreaActionsSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private RestrictUITapAreaActionAspect _aspect;
		private EcsFilter _actionsFilter;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_actionsFilter = _world
				.Filter<RestrictUITapAreaComponent>()
				.Inc<ActivateRestrictUITapAreaComponent>()
				.Exc<CompletedRunRestrictActionsComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _actionsFilter)
			{
				ref var restrictTapAreaComponent = ref _aspect.RestrictUITapArea.Get(entity);
				var actions = restrictTapAreaComponent.Value.Actions;
				foreach (var tutorialAction in actions)
				{
					var actionEntity = _world.NewEntity();
					tutorialAction.ComposeEntity(_world, actionEntity);
					ref var ownerComponent = ref _aspect.Owners.Add(actionEntity);
					ownerComponent.Value = _world.PackEntity(entity);
				}
				_aspect.CompletedRunRestrictActions.Add(entity);
			}
		}
	}
}