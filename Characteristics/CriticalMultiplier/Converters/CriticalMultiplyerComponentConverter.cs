namespace Game.Ecs.Characteristics.CriticalMultiplier.Converters
{
    using System;
    using Components;
    using Game.Ecs.Characteristics.Base.Components.Requests;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public class CriticalMultiplierConverter : LeoEcsConverter
    {
        public float criticalMultilplier = 100;
        
        public float minLimitValue = 0f;
        public float maxLimitValue = 1000f;
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var createCharacteristicRequest = ref world
                .GetOrAddComponent<CreateCharacteristicRequest<CriticalMultiplierComponent>>(entity);
            createCharacteristicRequest.Value = criticalMultilplier;
            createCharacteristicRequest.MaxValue = maxLimitValue;
            createCharacteristicRequest.MinValue = minLimitValue;
            createCharacteristicRequest.Owner = world.PackEntity(entity);

            ref var valueComponent = ref world.GetOrAddComponent<CriticalMultiplierComponent>(entity);
            valueComponent.Value = criticalMultilplier;
        }
    }
}
