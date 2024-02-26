namespace Game.Ecs.Gameplay.Tutorial.Actions.FreezingTimeAction.Aspects
{
	using Components;
	using FreezingTime.Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	public class FreezingTimeActionAspect : EcsAspect
	{
		public EcsWorld World;
		
		public EcsPool<FreezingTimeActionComponent> FreezingTimeAction;
		public EcsPool<FreezingTimeRequest> FreezingTimeRequest;
		public EcsPool<CompletedFreezingTimeActionComponent> CompletedFreezingTimeAction;
	}
}