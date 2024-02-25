namespace Game.Ecs.GameEffects.RetargetEffect
{
	using System;
	using Ability.SubFeatures.Target.Components;
	using Components;
	using Effects;
	using Effects.Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[Serializable]
	public class RetargetEffectConfiguration : EffectConfiguration
	{
		#region Inspector

		public float durationOfIgnoring;

		#endregion
		protected override void Compose(EcsWorld world, int effectEntity)
		{
			ref var effectComponent = ref world.GetComponent<EffectComponent>(effectEntity);
			if (!effectComponent.Destination.Unpack(world, out var destinationEntity))
				return;
			world.GetOrAddComponent<UntargetableComponent>(destinationEntity);
			
			ref var retargetComponent = ref world.GetOrAddComponent<RetargetComponent>(destinationEntity);
			retargetComponent.Value = Time.time + durationOfIgnoring;
		}
	}
}