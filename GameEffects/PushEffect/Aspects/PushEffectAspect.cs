namespace Game.Ecs.GameEffects.PushEffect.Aspects
{
	using System;
	using Ability.SubFeatures.Target.Components;
	using Components;
	using Effects.Components;
	using Leopotam.EcsLite;
	using Modules.UnioModules.UniGame.LeoEcsLite.LeoEcs.Shared.Components;
	using UniGame.LeoEcs.Shared.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Push effect aspect
	/// </summary>
	[Serializable]
	public class PushEffectAspect : EcsAspect
	{
		// Effect data 
		public EcsPool<EffectComponent> Effect;
		// Push effect data
		public EcsPool<PushEffectDataComponent> PushEffectData;
		// Transform for entity
		public EcsPool<TransformComponent> Transform;
		// Say that effect is completed
		public EcsPool<CompletedPushEffectComponent> CompletedPushEffect;
		// Says that entity is not target for push effect
		public EcsPool<EmptyTargetComponent> EmptyTarget;
	}
}