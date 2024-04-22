namespace Game.Ecs.GameAi.MoveToTarget.Converters
{
    using System;
    using Code.GameLayers.Category;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

    [Serializable]
    public class MoveToPointActionTargetConverter : LeoEcsConverter
    {
        [SerializeField]
        private int _priority = -1;

        [SerializeField]
        private Transform _point;

        [SerializeField]
        [CategoryIdMask]
        public CategoryId _category;
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var point = _point == null
                ? target.transform
                : _point;

            var poiPool = world.GetPool<MoveToPoiComponent>();
            ref var component = ref poiPool.Add(entity);
            component.Position = point.position;
            component.Priority = _priority;
            component.CategoryId = _category;
        }
    }
}