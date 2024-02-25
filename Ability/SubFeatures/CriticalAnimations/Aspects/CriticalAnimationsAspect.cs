namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations.Aspects
{
	using System;
	using Ability.Components.Requests;
	using AbilityAnimation.Components;
	using Common.Components;
	using Components;
	using Core.Components;
	using Gameplay.CriticalAttackChance.Components;
	using Leopotam.EcsLite;
	using Target.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Critical animations aspect
	/// </summary>
	[Serializable]
	public class CriticalAnimationsAspect : EcsAspect
	{
		public EcsPool<CriticalAbilityTargetComponent> CriticalAbilityTarget;
		public EcsPool<CriticalAttackMarkerComponent> CriticalAttackMarker;
		public EcsPool<OwnerComponent> Owner;
		public EcsPool<AbilityTargetsComponent> AbilityTargets;
		public EcsPool<AbilityAnimationOptionComponent> AbilityAnimationOption;
		
		// requests
		public EcsPool<ApplyAbilitySelfRequest> ApplyAbilitySelfRequest;
		public EcsPool<CompleteAbilitySelfRequest> CompleteAbilitySelfRequest;
		public EcsPool<RestartAbilityCooldownSelfRequest> RestartAbilityCooldownSelfRequest;
		public EcsPool<ResetAbilityCooldownSelfRequest> ResetAbilityCooldownSelfRequest;
	}
}