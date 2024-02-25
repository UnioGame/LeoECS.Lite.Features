namespace Game.Ecs.Ability.AbilityUtilityView.Radius.AggressiveRadius.Converters
{
    using System.Threading;
    using Code.GameLayers.Category;
    using Code.GameLayers.Layer;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UnityEngine;

    public sealed class AggressiveRadiusViewMonoConverter : MonoLeoEcsConverter
    {
        [SerializeField]
        private GameObject _noTargetRadiusView;
        [SerializeField]
        private GameObject _targetCloseRadiusView;
        [SerializeField]
        private GameObject _hasTargetRadiusView;
        
        [CategoryIdMask]
        [SerializeField]
        private CategoryId _categoryId;
        [LayerIdMask]
        [SerializeField]
        private LayerId _layerMask;
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var radiusViewPool = world.GetPool<AggressiveRadiusViewDataComponent>();
            ref var radiusView = ref radiusViewPool.Add(entity);
            
            radiusView.NoTargetRadiusView = _noTargetRadiusView;
            radiusView.TargetCloseRadiusView = _targetCloseRadiusView;
            radiusView.HasTargetRadiusView = _hasTargetRadiusView;

            radiusView.CategoryId = _categoryId;
            radiusView.LayerMask = _layerMask;

            var viewStatePool = world.GetPool<AggressiveRadiusViewStateComponent>();
            viewStatePool.Add(entity);
        }
    }
}