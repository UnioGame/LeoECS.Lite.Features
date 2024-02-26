namespace Game.Ecs.Gameplay.Tutorial.Actions.RestrictUITapAreaAction.Aspects
{
	using System;
	using Components;
	using Core.Components;
	using Leopotam.EcsLite;
	using Triggers.ActionTrigger.Components;
	using Tutorial.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
	
	[Serializable]
	public class RestrictUITapAreaActionAspect : EcsAspect
	{
		public EcsPool<RestrictUITapAreaActionComponent> RestrictUITapAreaAction;
		public EcsPool<ActivateRestrictUITapAreaComponent> ActivateRestrictUITapArea;
		public EcsPool<RestrictUITapAreaActionReadyComponent> RestrictUITapAreaActionReady;
		public EcsPool<RestrictUITapAreaComponent> RestrictUITapArea;
		public EcsPool<RestrictUITapAreasComponent> RestrictUITapAreas;
		public EcsPool<CompletedRestrictUITapAreaActionComponent> CompletedRestrictUITapAreaAction;
		public EcsPool<CompletedRestrictUITapAreaComponent> CompletedRestrictUITapArea;
		public EcsPool<ActionTriggerRequest> ActionTriggerRequest;
		public EcsPool<CompletedRunRestrictActionsComponent> CompletedRunRestrictActions;
		public EcsPool<OwnerComponent> Owners;
	}
}