namespace Game.Ecs.Characteristics.AttackSpeed.Converters
{
    using System;
    using System.Threading;
    using Components;
    using Ecs.Cooldown;
    using Game.Ecs.Characteristics.Base.Components.Requests;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public class AttackSpeedConverter : LeoEcsConverter
    {
        public CooldownType cooldownType = CooldownType.Speed;
        public int abilitySlotId = 0;
        
        public float attackSpeed;
        public float minLimitValue = 0;
        public float maxLimitValue = 5;
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var createCharacteristicRequest = ref world.GetOrAddComponent<CreateCharacteristicRequest<AttackSpeedComponent>>(entity);
            createCharacteristicRequest.Value = attackSpeed;
            createCharacteristicRequest.MaxValue = maxLimitValue;
            createCharacteristicRequest.MinValue = minLimitValue;
            createCharacteristicRequest.Owner = world.PackEntity(entity);

            ref var attackSpeedComponent = ref world.GetOrAddComponent<AttackSpeedComponent>(entity);
            ref var attackAbilityIdComponent = ref world.GetOrAddComponent<AttackAbilityIdComponent>(entity);
            ref var attackSpeedCooldownTypeComponent = ref world.GetOrAddComponent<AttackSpeedCooldownTypeComponent>(entity);
            attackSpeedCooldownTypeComponent.Value = cooldownType;
            attackAbilityIdComponent.Value = abilitySlotId;
        }
    }
}
