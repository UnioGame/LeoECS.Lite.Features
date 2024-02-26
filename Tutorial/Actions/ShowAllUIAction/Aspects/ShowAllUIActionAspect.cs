namespace Game.Ecs.Gameplay.Tutorial.Actions.ShowAllUIAction.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	[Serializable]
	public class ShowAllUIActionAspect : EcsAspect
	{
		public EcsPool<ShowAllUIActionComponent> ShowAllUIAction;
		public EcsPool<ShowAllUIActionEvent> ShowAllUIActionEvent;
		public EcsPool<CompletedShowAllUIComponent> CompletedShowAllUI;
	}
}