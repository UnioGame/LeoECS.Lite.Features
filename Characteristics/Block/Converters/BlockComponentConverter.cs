namespace Game.Ecs.Characteristics.Block.Converters
{
    using System;
    using System.Threading;
    using Components;
    using Game.Ecs.Characteristics.Base.Components.Requests;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public class BlockComponentConverter : LeoEcsConverter
    {
        public float block = 0f;
        
        [SerializeField] 
        [MaxValue(100)]
        public float maxDodge = 100f;
        [MinValue(0)]
        public float minDodge = 0f;
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var createCharacteristicRequest = ref world.GetOrAddComponent<CreateCharacteristicRequest<BlockComponent>>(entity);
            createCharacteristicRequest.Value = block;
            createCharacteristicRequest.MaxValue = maxDodge;
            createCharacteristicRequest.MinValue = minDodge;
            createCharacteristicRequest.Owner = world.PackEntity(entity);
            
            ref var healthComponent = ref world.GetOrAddComponent<BlockComponent>(entity);
            healthComponent.Value = block;
            healthComponent.MaxValue = maxDodge;
            healthComponent.MinValue = minDodge;
        }
    }
}
