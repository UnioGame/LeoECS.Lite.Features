namespace Game.Ecs.Characteristics.Health.Converters
{
    using System;
    using System.Threading;
    using Base.Components.Requests;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public class CharacteristicComponentConverter<TCharacteristic> : LeoEcsConverter
        where TCharacteristic : struct
    {
        public float value;
        public float minValue;
        public float maxValue;
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var createCharacteristicRequest = ref world.AddComponent<CreateCharacteristicRequest<TCharacteristic>>(entity);
            createCharacteristicRequest.Value = value;
            createCharacteristicRequest.MaxValue = maxValue;
            createCharacteristicRequest.MinValue = minValue;
            createCharacteristicRequest.Owner = world.PackEntity(entity);

            OnApply(target,world, entity);
            
            world.AddComponent<TCharacteristic>(entity);
        }

        protected virtual void OnApply(GameObject target, 
            EcsWorld world, int entity,
            CancellationToken cancellationToken = default)
        {
            
        }
    }
}
