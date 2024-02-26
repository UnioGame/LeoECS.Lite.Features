namespace Game.Ecs.Gameplay.Tutorial.Triggers.StepTrigger.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsLite;
	using Tutorial.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// tutorial trigger aspect
	/// </summary>
	[Serializable]
	public class StepTriggerAspect : EcsAspect
	{
		public EcsPool<StepTriggerComponent> StepTrigger;
		public EcsPool<StepTriggerReadyComponent> StepTriggerReady;
		public EcsPool<CompletedStepTriggerComponent> CompletedStepTrigger;
		public EcsPool<RunTutorialActionsRequest> RunTutorialActionsRequest;
	}
}