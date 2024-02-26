namespace Game.Ecs.Gameplay.Tutorial.Actions.OpenWindowAction.Aspects
{
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	public class OpenWindowActionAspect : EcsAspect
	{
		public EcsPool<OpenWindowActionComponent> OpenWindowAction;
		public EcsPool<CompletedOpenWindowActionComponent> CompletedOpenWindowAction;
	}
}