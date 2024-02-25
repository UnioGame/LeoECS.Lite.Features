namespace Game.Ecs.GameEffects.LevitationEffect.Systems
{
	using Aspects;
	using Components;
	using Core.Components;
	using DG.Tweening;
	using Effects.Components;
	using Leopotam.EcsLite;
	using UniCore.Runtime.ProfilerTools;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Components;
	using Unity.IL2CPP.CompilerServices;
	using UnityEngine;

	/// <summary>
	/// Rise up object to up
	/// </summary>

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	[ECSDI]
	public sealed class LevitationEffectSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _filter;
		private LevitationEffectAspect _aspect;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<LevitationEffectComponent>()
				.Inc<EffectComponent>()
				.End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var effect = ref _aspect.Effect.Get(entity);
				
				if (!effect.Destination.Unpack(_world, out var destinationEntity)) continue;
				if (_aspect.EmptyTarget.Has(destinationEntity)) continue;
				if (!effect.Source.Unpack(_world, out var sourceEntity)) continue;
				
				ref var transformSourceComponent = ref _aspect.Transform.Get(sourceEntity);
				var transformSource = transformSourceComponent.Value;
				
				if(transformSource == null) continue;
				
				ref var transformDestinationComponent = ref _aspect.Transform.Get(destinationEntity);
				var transform = transformDestinationComponent.Value;
				ref var levitationEffectComponent = ref _aspect.LevitationEffect.Get(entity);
				var height = levitationEffectComponent.Height;
				var durationLevitation = levitationEffectComponent.Duration;
				var rotation = levitationEffectComponent.Rotation;
				var durationRotation = levitationEffectComponent.DurationRotation;
				var sequence = DOTween.Sequence();
				var position = transformSource.position + transformSource.up * height;
				
				sequence.Append(transform.DOMove(position, durationLevitation));
				sequence.Join(transform.DORotate(rotation, durationRotation, RotateMode.WorldAxisAdd).SetLoops(-1));
			}
		}
	}
}