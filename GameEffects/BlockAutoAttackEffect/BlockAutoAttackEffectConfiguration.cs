namespace Game.Ecs.GameEffects.BlockAutoAttackEffect
{
	using System;
	using Components;
	using Effects;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Extensions;

	[Serializable]
	public class BlockAutoAttackEffectConfiguration : EffectConfiguration
	{
		#region Inspector

		public float silenceDuration = 1f;

		#endregion
		protected override void Compose(EcsWorld world, int effectEntity)
		{
			ref var blockAttackComponent = ref world.AddComponent<BlockAutoAttackEffectComponent>(effectEntity);
			blockAttackComponent.Duration = silenceDuration;
		}
	}
}