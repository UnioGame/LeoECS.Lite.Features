namespace Game.Ecs.GameEffects.RetargetEffect.Aspects
{
	using System;
	using Ability.SubFeatures.Target.Components;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Retarget effect aspect
	/// </summary>
	[Serializable]
	public class RetargetEffectAspect : EcsAspect
	{
		// Stores the duration of the retarget effect
		public EcsPool<RetargetComponent> RetargetComponent;
		// Marks the target as untargetable
		public EcsPool<UntargetableComponent> UntargetableComponent;
	}
}