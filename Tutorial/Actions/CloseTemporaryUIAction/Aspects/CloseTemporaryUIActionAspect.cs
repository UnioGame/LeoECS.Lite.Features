namespace Game.Ecs.Gameplay.Tutorial.Actions.CloseTemporaryUIAction.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	[Serializable]
	public class CloseTemporaryUIActionAspect : EcsAspect
	{
		public EcsPool<CompletedCloseTemporaryUIActionComponent> CompletedCloseTemporaryUIAction;
	}
}