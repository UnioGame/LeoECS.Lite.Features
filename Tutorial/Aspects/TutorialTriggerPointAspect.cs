namespace Game.Ecs.Gameplay.Tutorial.Aspects
{
	using System;
	using Components;
	using Core.Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
	public class TutorialTriggerPointAspect : EcsAspect
	{
		public EcsWorld World;
        
		public EcsPool<TransformComponent> Transform;
		public EcsPool<RunTutorialActionsRequest> RunTutorialActionsRequest;
		public EcsPool<TutorialActionsComponent> TutorialActions;
		public EcsPool<OwnerComponent> Owner;
		public EcsPool<DelayedTutorialComponent> DelayedTutorial;
		public EcsPool<CompletedDelayedTutorialComponent> CompletedDelayedTutorial;
	}
}