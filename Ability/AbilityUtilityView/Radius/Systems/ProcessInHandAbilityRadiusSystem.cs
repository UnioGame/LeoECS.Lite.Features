namespace Game.Ecs.Ability.AbilityUtilityView.Radius.Systems
{
    using Characteristics.Radius.Component;
    using Common.Components;
    using Component;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UnityEngine;

    public sealed class ProcessInHandAbilityRadiusSystem : IEcsRunSystem,IEcsInitSystem
    {
        private const float TimeToShow = 0.5f;
        
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AbilityInHandComponent>()
                .Inc<AbilityActiveTimeComponent>()
                .Inc<RadiusComponent>()
                .Inc<OwnerComponent>()
                .Inc<RadiusViewComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var activateTimePool = _world.GetPool<AbilityActiveTimeComponent>();
            var ownerPool = _world.GetPool<OwnerComponent>();
            var visiblePool = _world.GetPool<VisibleUtilityViewComponent>();
            var radiusViewPool = _world.GetPool<RadiusViewComponent>();
            var radiusPool = _world.GetPool<RadiusComponent>();
            var showRadiusPool = _world.GetPool<ShowRadiusRequest>();
            var hideRadiusPool = _world.GetPool<HideRadiusRequest>();

            foreach (var entity in _filter)
            {
                ref var owner = ref ownerPool.Get(entity);
                if(!owner.Value.Unpack(_world, out var ownerEntity))
                    continue;
                
                if(!visiblePool.Has(ownerEntity))
                    continue;

                var packedEntity = _world.PackEntity(entity);
                ref var activeTime = ref activateTimePool.Get(entity);
                
                if(activeTime.Time < TimeToShow && !Mathf.Approximately(activeTime.Time, TimeToShow))
                {
                    var hideRequestEntity = _world.NewEntity();
                    ref var hideRequest = ref hideRadiusPool.Add(hideRequestEntity);
                
                    hideRequest.Source = packedEntity;
                    hideRequest.Destination = owner.Value;
                    
                    continue;
                }

                ref var radius = ref radiusPool.Get(entity);
                
                var showRequestEntity = _world.NewEntity();
                ref var showRequest = ref showRadiusPool.Add(showRequestEntity);
                
                showRequest.Source = packedEntity;
                showRequest.Destination = owner.Value;

                ref var radiusView = ref radiusViewPool.Get(entity);
                
                showRequest.Radius = radiusView.RadiusView;
                showRequest.Root = radiusView.Root;
                
                var size = radius.Value * 2.0f;
                showRequest.Size = new Vector3(size, size, size);
            }
        }
    }
}