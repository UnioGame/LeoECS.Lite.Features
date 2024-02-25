namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Switchers.TimerForAbilitySwitcher.Aspects
{
	using System;
	using AbilitySwitcher.Components;
	using Common.Components;
	using Components;
	using Core.Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Aspect for ability switcher that uses counter to switch between abilities.
	/// </summary>
	[Serializable]
	public class TimerForAbilitySwitcherAspect : EcsAspect
	{
		// Timer for ability switcher component.
		public EcsPool<TimerForAbilitySwitcherComponent> TimerForAbilitySwitcherComponent;
		// Marker component for ability switcher that uses counter to switch between abilities.
		public EcsPool<TimerForAbilitySwitcherReadyComponent> TimerForAbilitySwitcherReadyComponent;
		// Says whose ability is currently active 
		public EcsPool<OwnerComponent> OwnerComponent;
		
		//Requests
		// Request to switch ability
		public EcsPool<AbilitySwitcherRequest> AbilitySwitchRequest;
		// Request to apply ability to self
		public EcsPool<ApplyAbilitySelfRequest> ApplyAbilitySelfRequest;
	}
}