namespace Ai.Ai.Variants.Attack.Converters
{
    using System;
    using System.Collections.Generic;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UnityEngine;
    using Components;
    using Game.Ecs.AI.Abstract;

    [Serializable]
    public class AttackPlannerConverter : PlannerConverter<AttackPlannerComponent>, 
        ILeoEcsGizmosDrawer
    {
        [SerializeReference]
        [InlineProperty]
        public List<IAttackConverter> converters = new List<IAttackConverter>();

        protected override void OnApplyComponents(GameObject target, EcsWorld world, int entity)
        {
            foreach (var converter in converters)
            {
                converter.Apply(world, entity, actionId);
            }
        }

        public void DrawGizmos(GameObject target)
        {
            foreach (var converter in converters)
            {
                if (converter is not ILeoEcsGizmosDrawer drawer)
                {
                    continue;
                }

                drawer.DrawGizmos(target);
            }
        }
    }
}