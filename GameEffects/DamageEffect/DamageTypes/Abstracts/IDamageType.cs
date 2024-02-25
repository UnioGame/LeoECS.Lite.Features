namespace Game.Ecs.GameEffects.DamageEffect.DamageTypes.Abstracts
{
	using Leopotam.EcsLite;

	public interface IDamageType
	{
		void Compose(EcsWorld world, int effectEntity);
	}
}