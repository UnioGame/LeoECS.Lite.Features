namespace Game.Ecs.GameEffects.LevitationEffect
{
	using System;
	using Components;
	using Effects;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;
	using UnityEngine.Serialization;

	[Serializable]
	public sealed class LevitationEffectConfiguration : EffectConfiguration
	{
		public float height = 5f;
		public float durationLevitation = 1f;
		public Vector3 rotation = new Vector3(0, 0, 0);
		public float durationRotation = 1f;
		protected override void Compose(EcsWorld world, int effectEntity)
		{
			ref var levitationComponent = ref world.AddComponent<LevitationEffectComponent>(effectEntity);
			levitationComponent.Height = height;
			levitationComponent.Duration = durationLevitation;
			levitationComponent.Rotation = rotation;
			levitationComponent.DurationRotation = durationRotation;
		}
	}
}