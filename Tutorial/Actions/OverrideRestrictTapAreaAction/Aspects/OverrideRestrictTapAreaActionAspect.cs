namespace Game.Ecs.Gameplay.Tutorial.Actions.OverrideRestrictTapAreaAction.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// ADD DESCRIPTION HERE
	/// </summary>
	[Serializable]
	public class OverrideRestrictTapAreaActionAspect : EcsAspect
	{
		public EcsPool<OverrideRestrictTapAreaActionComponent> OverrideRestrictTapAreaAction;
		public EcsPool<OverrideRestrictTapAreaActionReadyComponent> OverrideRestrictTapAreaActionReady;
	}
}