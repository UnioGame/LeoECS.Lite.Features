namespace Game.Ecs.Characteristics.Speed.Converters
{
    using System;
    using Game.Ecs.Characteristics.Base.Components.Requests;
    using Game.Ecs.Characteristics.Speed.Components;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public class SpeedConverter : LeoEcsConverter
    {
        public float speed = 5;

        [TitleGroup("Limits")]
        private bool overrideLimits = false;
        
        [ShowIf(nameof(overrideLimits))]
        [TitleGroup("Limits")]
        public float minValue;
        
        [TitleGroup("Limits")]
        [ShowIf(nameof(overrideLimits))]
        public float maxValue;
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var createCharacteristicRequest = ref world.GetOrAddComponent<CreateCharacteristicRequest<SpeedComponent>>(entity);
            createCharacteristicRequest.Value = speed;
            createCharacteristicRequest.MaxValue = overrideLimits ? maxValue : 10000;
            createCharacteristicRequest.MinValue = overrideLimits ? minValue : 0;
            createCharacteristicRequest.Owner = world.PackEntity(entity);

            ref var speedComponent = ref world.GetOrAddComponent<SpeedComponent>(entity);
            speedComponent.Value = speed;
        }
    }
}
