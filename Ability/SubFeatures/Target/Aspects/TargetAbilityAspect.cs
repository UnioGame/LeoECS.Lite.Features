namespace Game.Ecs.Ability.SubFeatures.Target.Aspects
{
	using System;
	using Characteristics.SplashDamage.Components;
	using Components;
	using Core.Components;
	using Core.Death.Components;
	using Leopotam.EcsLite;
	using Movement.Components;
	using Selection.Components;
	using UniGame.LeoEcs.Shared.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Target aspect.
	/// </summary>
	[Serializable]
	public class TargetAbilityAspect : EcsAspect
	{
		public EcsPool<SoloTargetComponent> SoloTarget;
		public EcsPool<MultipleTargetsComponent> MultipleTargets;
		public EcsPool<AbilityTargetsComponent> AbilityTargets;
		public EcsPool<SplashEffectSourceComponent> SplashApplyEffects;
		public EcsPool<OwnerComponent> Owner;
		public EcsPool<SplashDamageComponent> SplashCharacteristics;
		public EcsPool<PrepareToDeathComponent> PrepareToDeath;
		public EcsPool<DisabledComponent> Disabled;
		public EcsPool<SelectedTargetsComponent> SelectedTargets;
		
		public EcsPool<TransformComponent> Transform;
		public EcsPool<TransformPositionComponent> Position;
		public EcsPool<TransformDirectionComponent> Direction;
		
		public EcsPool<EntityAvatarComponent> Avatar;
		public EcsPool<UnderTheTargetComponent> UnderTheTarget;
		public EcsPool<CanLookAtComponent> CanLookAt;
		
		//requests
		public EcsPool<RotateToPointSelfRequest> RotateTo;
	}
}