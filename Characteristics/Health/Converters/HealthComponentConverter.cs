namespace Game.Ecs.Characteristics.Health.Converters
{
    using System;
    using System.Threading;
    using Base.Components.Requests;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public class HealthComponentConverter : LeoEcsConverter
    {
        [FormerlySerializedAs("_maxHealth")] 
        [SerializeField] 
        public float maxHealth;
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var createCharacteristicRequest = ref world.AddComponent<CreateCharacteristicRequest<HealthComponent>>(entity);
            createCharacteristicRequest.Value = maxHealth;
            createCharacteristicRequest.MaxValue = maxHealth;
            createCharacteristicRequest.MinValue = 0;
            createCharacteristicRequest.Owner = world.PackEntity(entity);
            
            ref var healthComponent = ref world.AddComponent<HealthComponent>(entity);
            healthComponent.Health = maxHealth;
            healthComponent.MaxHealth = maxHealth;
        }
    }
}
