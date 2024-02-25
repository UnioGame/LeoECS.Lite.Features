namespace Game.Ecs.GameEffects.RetargetEffect.Systems
{
	using System;
	using System.Linq;
	using Ability.SubFeatures.Target.Components;
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
	/// Remove untargetable mark from target
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class RetargetEffectSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private RetargetEffectAspect _aspect;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<RetargetComponent>()
				.Inc<UntargetableComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var retargetComponent = ref _aspect.RetargetComponent.Get(entity);
				if (retargetComponent.Value > Time.time)
					continue;
				_aspect.UntargetableComponent.Del(entity);
				_aspect.RetargetComponent.Del(entity);
			}
		}
	}
}