namespace Game.Ecs.Gameplay.Tutorial.Triggers.ActionTrigger.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsLite;
	using Tutorial.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	[Serializable]
	public class ActionTriggerAspect : EcsAspect
	{
		public EcsPool<ActionTriggerComponent> ActionTrigger;
		public EcsPool<ActionTriggerRequest> ActionTriggerRequest;
		public EcsPool<CompletedActionTriggerComponent> CompletedActionTrigger;
		public EcsPool<RunTutorialActionsRequest> RunTutorialActionsRequest;
	}
}