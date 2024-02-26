namespace Game.Ecs.Gameplay.Tutorial.Actions.EquipAbilityAction.Aspects
{
	using System;
	using AbilityInventory.Components;
	using Components;
	using Core.Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	[Serializable]
	public class EquipAbilityActionAspect : EcsAspect
	{
		public EcsPool<EquipAbilityActionComponent> EquipAbilityAction;
		public EcsPool<EquipAbilityIdSelfRequest> EquipAbilityIdRequest;
		public EcsPool<CompletedEquipAbilityActionComponent> CompletedEquipAbilityAction;
		public EcsPool<ChampionComponent> Champion;
	}
}