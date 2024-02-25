namespace Game.Ecs.GameEffects.CriticalEffect
{
	using System;
	using Components;
	using Effects;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Extensions;

	[Serializable]
	public class CriticalEffectConfiguration : EffectConfiguration
	{
		protected override void Compose(EcsWorld world, int effectEntity)
		{
			world.AddComponent<CriticalEffectComponent>(effectEntity);
		}
	}
}