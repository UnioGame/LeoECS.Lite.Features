namespace Game.Ecs.Ability.SubFeatures.Target.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Non target aspect.
	/// </summary>
	[Serializable]
	public class NonTargetAspect : EcsAspect
	{
		public EcsPool<UntargetableComponent> NonTargetComponent;
		public EcsPool<AbilityTargetsComponent> AbilityTargetsComponent;
	}
}