namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Aspects
{
	using System;
	using Ability.Components.Requests;
	using Common.Components;
	using Components;
	using Core.Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Ability switcher aspect
	/// </summary>
	[Serializable]
	public class AbilitySwitcherAspect : EcsAspect
	{
		public EcsPool<OwnerComponent> Owner;
		
		// requests
		public EcsPool<AbilitySwitcherRequest> AbilitySwitchRequest;
		public EcsPool<CompleteAbilitySelfRequest> CompleteAbilitySelfRequest;
		public EcsPool<RestartAbilityCooldownSelfRequest> RestartAbilityCooldownSelfRequest;
		public EcsPool<ResetAbilityCooldownSelfRequest> ResetAbilityCooldownSelfRequest;
	}
}