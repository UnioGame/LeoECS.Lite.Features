namespace Game.Ecs.GameAi.ActivateAbility.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Code.Ai.ActivateAbility;
    using Game.Ecs.AI.Abstract;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UnityEngine;

    [Serializable]
    public class ActivateAbilityPlannerConverter : PlannerConverter<ActivateAbilityPlannerComponent>, ILeoEcsGizmosDrawer
    {
        [SerializeReference] 
        [InlineProperty]
        private List<IAbilityByConverter> _converters = new List<IAbilityByConverter>();

        protected override void OnApplyComponents(GameObject target, EcsWorld world, int entity)
        {
            //world.AddComponentToEntity<MoveToGoalComponent>(entity);

            foreach (var converter in _converters)
                converter.Apply(world, entity);
        }

        public void DrawGizmos(GameObject target)
        {
            foreach (var converter in _converters)
            {
                if (converter is not ILeoEcsGizmosDrawer drawer)
                    continue;
                drawer.DrawGizmos(target);
            }
        }
    }
}