namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Switchers.UsageCounterForAbilitySwitcher
{
	using System;
	using AbilitySwitcher.Components;
	using Abstracts;
	using Code.Configuration.Runtime.Ability.Description;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Extensions;

	[Serializable]
	public class UsageCounterForAbilitySwitcherConfiguration : IAbilitySwitcherConfiguration
	{
		#region Inspector

		public AbilityId abilityId;
		public int count;

		#endregion
		public void Compose(EcsWorld world, int abilityEntity)
		{
			ref var counterForAbilitySwitcherComponent = ref world.AddComponent<UsageCounterForAbilitySwitcherComponent>(abilityEntity);
			counterForAbilitySwitcherComponent.abilityId = abilityId;
			counterForAbilitySwitcherComponent.baseCount = count;
			counterForAbilitySwitcherComponent.count = 0;
		}
	}
}