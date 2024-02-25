namespace Game.Ecs.Characteristics.SplashDamage.Converters
{
	using System;
	using Base.Components.Requests;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Converter.Runtime;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[Serializable]
	public class SplashDamageConverter : LeoEcsConverter
	{
		public float _splashDamage;
		
		public override void Apply(GameObject target, EcsWorld world, int entity)
		{
			ref var splashDamageComponent = ref world.GetOrAddComponent<SplashDamageComponent>(entity);
			splashDamageComponent.Value = _splashDamage;
            
			ref var createCharacteristicRequest = ref world.GetOrAddComponent<CreateCharacteristicRequest<SplashDamageComponent>>(entity);
			createCharacteristicRequest.Value = _splashDamage;
			createCharacteristicRequest.MaxValue = 1000;
			createCharacteristicRequest.MinValue = 0;
			createCharacteristicRequest.Owner = world.PackEntity(entity);
		}
	}
}