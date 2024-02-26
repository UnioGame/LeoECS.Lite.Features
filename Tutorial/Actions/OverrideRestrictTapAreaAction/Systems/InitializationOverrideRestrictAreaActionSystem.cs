namespace Game.Ecs.Gameplay.Tutorial.Actions.OverrideRestrictTapAreaAction.Systems
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
	public class InitializationOverrideRestrictAreaActionSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private OverrideRestrictTapAreaActionAspect _aspect;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<OverrideRestrictTapAreaActionComponent>()
				.Exc<OverrideRestrictTapAreaActionReadyComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var restrictTapAreaComponent = ref _aspect.OverrideRestrictTapAreaAction.Get(entity);
				restrictTapAreaComponent.Value.ComposeEntity(_world, entity);
				_aspect.OverrideRestrictTapAreaActionReady.Add(entity);
			}
		}
	}
}