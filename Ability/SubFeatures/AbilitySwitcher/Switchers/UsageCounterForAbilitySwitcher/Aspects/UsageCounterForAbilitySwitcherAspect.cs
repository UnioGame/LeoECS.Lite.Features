namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Switchers.UsageCounterForAbilitySwitcher.Aspects
{
	using System;
	using AbilitySwitcher.Components;
	using Common.Components;
	using Components;
	using Core.Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Usage counter for ability switcher aspect
	/// </summary>
	[Serializable]
	public class UsageCounterForAbilitySwitcherAspect : EcsAspect
	{
		// Owner is required to get ability entity
		public EcsPool<OwnerComponent> Owner;
		// Says that ability can be used after count of usages
		public EcsPool<UsageCounterForAbilitySwitcherComponent> UsageCounterForAbilitySwitcher;
		
		// requests
		// Request to apply ability to self
		public EcsPool<ApplyAbilitySelfRequest> ApplyAbilitySelfRequest;
		// Request to switch ability
		public EcsPool<AbilitySwitcherRequest> AbilitySwitchRequest;
	}
}