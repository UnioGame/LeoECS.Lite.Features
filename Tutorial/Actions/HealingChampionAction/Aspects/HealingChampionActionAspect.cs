namespace Game.Ecs.Gameplay.Tutorial.Actions.HealingChampionAction.Aspects
{
	using System;
	using Characteristics.Health.Components;
	using Components;
	using Effects.Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
	
	[Serializable]
	public class HealingChampionActionAspect : EcsAspect
	{
		public EcsPool<HealingChampionActionComponent> HealingChampionAction;
		public EcsPool<HealthComponent> Healths;
		public EcsPool<EffectComponent> Effects;
		public EcsPool<ApplyEffectSelfRequest> ApplyEffectRequest;
		public EcsPool<CompletedHealingChampionActionComponent> CompletedHealingChampionAction;
	}
}