namespace Game.Ecs.Ability.AbilityUtilityView.Radius.Converters
{
    using System;
    using System.Threading;
    using Component;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

    [Serializable]
    public sealed class RadiusViewConverter : LeoEcsConverter
    {
        [SerializeField]
        public GameObject noTargetRadiusView;
        [SerializeField]
        public GameObject hasTargetRadiusView;

        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var radiusViewPool = world.GetPool<RadiusViewDataComponent>();
            ref var radiusView = ref radiusViewPool.Add(entity);
            
            radiusView.InvalidRadiusView = noTargetRadiusView;
            radiusView.ValidRadiusView = hasTargetRadiusView;
        }
    }
}