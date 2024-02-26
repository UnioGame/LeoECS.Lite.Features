namespace Game.Ecs.Gameplay.Tutorial.Triggers.DistanceTrigger.Aspects
{
	using Game.Ecs.Gameplay.Tutorial.Components;
	using Components;
	using Core.Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	public class DistanceTriggerAspect : EcsAspect
	{
		public EcsWorld World;
		
		public EcsPool<DistanceTriggerPointComponent> DistanceTriggerPoint;
		public EcsPool<CompletedDistanceTriggerPointComponent> CompletedDistanceTriggerPoint;
		public EcsPool<TransformPositionComponent> Position;
		public EcsPool<OwnerComponent> Owner;
		public EcsPool<RunTutorialActionsRequest> RunTutorialActionsRequest;
	}
}