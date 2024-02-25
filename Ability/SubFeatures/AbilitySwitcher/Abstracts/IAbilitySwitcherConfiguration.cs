namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Abstracts
{
	using Leopotam.EcsLite;

	public interface IAbilitySwitcherConfiguration
	{
		void Compose(EcsWorld world, int abilityEntity);
	}
}