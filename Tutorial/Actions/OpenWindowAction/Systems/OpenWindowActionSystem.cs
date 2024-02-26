namespace Game.Ecs.Gameplay.Tutorial.Actions.OpenWindowAction.Systems
{
	using System;
	using System.Linq;
	using Aspects;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.Core.Runtime.Extension;
	using UniGame.Runtime.ObjectPool.Extensions;
	using UnityEngine;
	using UnityEngine.Pool;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.ViewSystem.Extensions;

	/// <summary>
	/// Opening selected window.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class OpenWindowActionSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private OpenWindowActionAspect _aspect;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<OpenWindowActionComponent>()
				.Exc<CompletedOpenWindowActionComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var openWindowActionComponent = ref _aspect.OpenWindowAction.Get(entity);
				var view = openWindowActionComponent.View;
				var layoutType = openWindowActionComponent.LayoutType;
				_world.MakeViewRequest(view, layoutType);
				_aspect.CompletedOpenWindowAction.Add(entity);
			}
		}
	}
}