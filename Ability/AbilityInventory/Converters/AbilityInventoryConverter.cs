namespace Game.Ecs.AbilityInventory.Converters
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using Code.Services.AbilityLoadout.Data;
	using Components;
	using Leopotam.EcsLite;
	using Sirenix.OdinInspector;
	using UniGame.LeoEcs.Converter.Runtime;
	using UniGame.LeoEcs.Converter.Runtime.Abstract;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;
	using UnityEngine.Serialization;

	[Serializable]
	public class AbilityInventoryConverter : LeoEcsConverter
	{
		[SerializeField]
		public bool userInput;
		
		public bool autoEquipByProfile = true;
		
		public override void Apply(GameObject target, EcsWorld world, int entity)
		{
			world.GetOrAddComponent<AbilityInventoryComponent>(entity);
			if(autoEquipByProfile)
				world.GetOrAddComponent<AbilityInventoryProfileComponent>(entity);
		}
	}
}