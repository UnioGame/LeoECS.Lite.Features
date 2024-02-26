namespace Game.Ecs.Gameplay.Tutorial.Actions.EquipAbilityAction
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Abstracts;
	using Code.Configuration.Runtime.Ability;
	using Code.Services.AbilityLoadout.Data;
	using Components;
	using Leopotam.EcsLite;
	using Sirenix.OdinInspector;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;


	public class EquipAbilityActionConfiguration : TutorialAction
	{
		#region Inspector
        
		public List<AbilityCell> abilityCells;

		#endregion
		
		protected override void Composer(EcsWorld world, int entity)
		{
			ref var equipAbilityActionComponent = ref world.AddComponent<EquipAbilityActionComponent>(entity);
			equipAbilityActionComponent.AbilityCells.AddRange(abilityCells);
		}
	}
}