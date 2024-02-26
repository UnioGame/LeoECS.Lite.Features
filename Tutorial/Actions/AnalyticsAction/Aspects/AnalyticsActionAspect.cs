namespace Game.Ecs.Gameplay.Tutorial.Actions.AnalyticsAction.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	[Serializable]
	public class AnalyticsActionAspect : EcsAspect
	{
		public EcsPool<AnalyticsActionComponent> AnalyticsAction;
		public EcsPool<CompletedAnalyticsActionComponent> CompletedAnalyticsAction;
	}
}