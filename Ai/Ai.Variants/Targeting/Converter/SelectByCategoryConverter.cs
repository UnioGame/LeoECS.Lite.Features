namespace Game.Ecs.GameAi.Targeting.Converters
{
    using System;
    using Ai.Ai.Variants.Attack.Converters;
    using AI.Components;
    using AI.Converters;
    using Characteristics.CriticalChance.Components;
    using Game.Ecs.GameAi.MoveToTarget.Converters;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using Components;
    using Core.Components;
    using Data;
    using Shared.Generated;
    using UnityEngine;

    [Serializable]
    public class SelectByCategoryConverter : EcsComponentSubPlannerConverter, 
        IMoveByConverter, 
        IAttackConverter
    {
        [SerializeField]
        private SelectByCategoryComponent _value;

        [SerializeReference]
        private RadiusType _radiusType;
        
        public override void Apply(EcsWorld world, int entity, ActionType actionId)
        {
            base.Apply(world, entity, actionId);
            
            var targetRequestEntity = world.NewEntity();
            ref var ownerComponent = ref world.AddComponent<OwnerComponent>(targetRequestEntity);
            ownerComponent.Value = entity.PackedEntity(world);
            ref var selectComponent = ref world.AddComponent<SelectByCategoryComponent>(targetRequestEntity);
            _value.Apply(ref selectComponent);
            selectComponent.ActionId = (int)actionId;

            switch (_radiusType)
            {
                case RadiusType.Sensor: 
                    world.AddComponent<AiSensorComponent>(targetRequestEntity);
                    break;
                case RadiusType.AttackRange:
                    world.AddComponent<AttackRangeComponent>(targetRequestEntity);
                    break;
            }
        }
    }
}