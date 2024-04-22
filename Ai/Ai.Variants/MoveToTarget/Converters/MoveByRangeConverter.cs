namespace Game.Ecs.GameAi.MoveToTarget.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using AI.Abstract;
    using Code.Configuration.Runtime.Effects.Abstract;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public class MoveByRangeConverter : ComponentPlannerConverter, IMoveByConverter,ILeoEcsGizmosDrawer
    {
        [SerializeField]
        private float _priority;
        [SerializeField]
        private float _radius;
        [SerializeField]
        private float _minDistance = 0.5f;
        [SerializeReference]
        private List<IEffectConfiguration> _effects = new List<IEffectConfiguration>();

        public Color rangeGizmosColor = Color.red;

        public override void Apply(EcsWorld world, int entity)
        {
            
        }

        protected override void OnApply(GameObject target, EcsWorld world, int entity)
        {
            ref var component = ref world.AddComponentToEntity<MoveByRangeComponent>(entity);

            component.Center = target.transform.position;
            component.Priority = _priority;
            component.Radius = _radius;
            component.MinDistance = _minDistance;
            component.Effects = _effects;
        }

        public void DrawGizmos(GameObject target)
        {

        }
    }
}