namespace Game.Ecs.GameEffects.DamageEffect.DamageTypes
{
	using System;
	using Abstracts;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Extensions;

	[Serializable]
	public class MagicDamageType : IDamageType
	{
		public void Compose(EcsWorld world, int effectEntity)
		{
			world.AddComponent<MagicDamageComponent>(effectEntity);
		}
	}
}