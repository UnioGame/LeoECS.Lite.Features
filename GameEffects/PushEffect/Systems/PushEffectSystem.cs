using UnityEngine.AI;

namespace Game.Ecs.GameEffects.PushEffect.Systems
{
	using System;
	using System.Collections.Generic;
	using Aspects;
	using Components;
	using DG.Tweening;
	using Leopotam.EcsLite;
	using UnityEngine;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Push object system
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class PushEffectSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		
		private PushEffectAspect _aspect;
		private EcsFilter _pushEffectFilter;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_pushEffectFilter = _world
				.Filter<PushEffectDataComponent>()
				.Exc<CompletedPushEffectComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var pushEntity in _pushEffectFilter)
			{
				ref var pushEffectData = ref _aspect.PushEffectData.Get(pushEntity);
				var distance = pushEffectData.Distance;
				var durationOffset = pushEffectData.DurationOffset;
				
				ref var effect = ref _aspect.Effect.Get(pushEntity);
				
				if (!effect.Destination.Unpack(_world, out var targetEntity)) continue;
				
				if (_aspect.EmptyTarget.Has(targetEntity))
				{
					_aspect.CompletedPushEffect.Add(pushEntity);
					continue;
				}
				
				ref var transformComponent = ref _aspect.Transform.Get(targetEntity);
				var targetTransform = transformComponent.Value;
				
				if(targetTransform == null) continue;
				
				var targetPosition = targetTransform.position;

				if (!effect.Source.Unpack(_world, out var sourceEntity)) continue;
				
				ref var sourceTransformComponent = ref _aspect.Transform.Get(sourceEntity);
				var sourceTransform = sourceTransformComponent.Value;
				
				if(sourceTransform == null) continue;
				
				var sourcePosition = sourceTransform.position;
				var direction = targetPosition - sourcePosition;
				var normalize = Vector3.Normalize(direction);
				normalize *= pushEffectData.FromSource ? 1 : -1;

				var finalPosition = targetPosition + normalize * distance;

				if (NavMesh.SamplePosition(finalPosition, out var hit, pushEffectData.Distance, NavMesh.AllAreas))
				{
					var tween = targetTransform
						.DOMove(hit.position, durationOffset)
						.SetRecyclable(true)
						.SetEase(pushEffectData.Ease);
				}
				
				_aspect.CompletedPushEffect.Add(pushEntity);
			}
		}
	}
}