namespace Game.Ecs.GameEffects.LevitationEffect.Aspects
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
	/// Levitation effect aspect
	/// </summary>
	[Serializable]
	public class LevitationEffectAspect : EcsAspect
	{
		public EcsPool<EmptyTargetComponent> EmptyTarget;
		public EcsPool<LevitationEffectComponent> LevitationEffect;
		public EcsPool<TransformComponent> Transform;
		public EcsPool<EffectComponent> Effect;
	}
}