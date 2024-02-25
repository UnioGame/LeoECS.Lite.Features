namespace Game.Ecs.Characteristics.AbilityPower.Converters
{
	using System;
	using Base.Components.Requests;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Converter.Runtime;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[Serializable]
	public class AbilityPowerConverter : LeoEcsConverter
	{
		public float abilityPower;
		
		public override void Apply(GameObject target, EcsWorld world, int entity)
		{
			ref var attackDamageComponent = ref world.GetOrAddComponent<AbilityPowerComponent>(entity);
			attackDamageComponent.Value = abilityPower;
            
			ref var createCharacteristicRequest = ref world
				.GetOrAddComponent<CreateCharacteristicRequest<AbilityPowerComponent>>(entity);
			createCharacteristicRequest.Value = abilityPower;
			createCharacteristicRequest.MaxValue = 1000;
			createCharacteristicRequest.MinValue = 0;
			createCharacteristicRequest.Owner = world.PackEntity(entity);
		}
	}
}