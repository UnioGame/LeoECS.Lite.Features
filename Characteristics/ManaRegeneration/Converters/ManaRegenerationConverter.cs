namespace Game.Ecs.Characteristics.ManaRegeneration.Converters
{
	using System;
	using Base.Components.Requests;
	using Components;
	using Leopotam.EcsLite;
	using Sirenix.OdinInspector;
	using UniGame.LeoEcs.Converter.Runtime;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[Serializable]
	public class ManaRegenerationConverter : LeoEcsConverter
	{
		[ShowInInspector, PropertyRange(0f, 1f)]
		public float tickTime = 0.2f;
		public float manaRegeneration = 80f;

		public override void Apply(GameObject target, EcsWorld world, int entity)
		{
			ref var createCharacteristicRequest = ref world.AddComponent<CreateCharacteristicRequest<ManaRegenerationComponent>>(entity);
			createCharacteristicRequest.Value = manaRegeneration;
			createCharacteristicRequest.MaxValue = float.MaxValue;
			createCharacteristicRequest.MinValue = 0;
			createCharacteristicRequest.Owner = world.PackEntity(entity);

			ref var manaRegenerationComponent = ref world.AddComponent<ManaRegenerationComponent>(entity);
			manaRegenerationComponent.Value = manaRegeneration;
			
			ref var manaRegenerationTimerComponent = ref world.AddComponent<ManaRegenerationTimerComponent>(entity);
			manaRegenerationTimerComponent.TickTime = tickTime;
			manaRegenerationTimerComponent.LastTickTime = Time.time;
		}
	}
}