namespace Game.Ecs.Gameplay.Tutorial.Actions.CloseTemporaryUIAction.Systems
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
	using UniGame.LeoEcs.ViewSystem.Components;

	/// <summary>
	/// Close temporary UI. Send event to close temporary UI.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class CloseTemporaryUIActionSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private CloseTemporaryUIActionAspect _aspect;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<CloseTemporaryUIActionComponent>()
				.Exc<CompletedCloseTemporaryUIActionComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				var request = _world.NewEntity();
				_world.AddComponent<CloseAllViewsRequest>(request);
				_aspect.CompletedCloseTemporaryUIAction.Add(entity);
			}
		}
	}
}