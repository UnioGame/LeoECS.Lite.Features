namespace Game.Ecs.Characteristics.Dodge.Converters
{
    using System;
    using System.Threading;
    using Base.Components.Requests;
    using Components;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public class DodgeComponentConverter : LeoEcsConverter
    {
        public float dodge = 0f;
        
        [SerializeField] 
        [MaxValue(100)]
        public float maxDodge = 100f;
        [MinValue(0)]
        public float minDodge = 0f;
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var createCharacteristicRequest = ref world.GetOrAddComponent<CreateCharacteristicRequest<DodgeComponent>>(entity);
            createCharacteristicRequest.Value = dodge;
            createCharacteristicRequest.MaxValue = maxDodge;
            createCharacteristicRequest.MinValue = minDodge;
            createCharacteristicRequest.Owner = world.PackEntity(entity);

            ref var healthComponent = ref world.AddComponent<DodgeComponent>(entity);
            healthComponent.Value = dodge;
        }
    }
}
