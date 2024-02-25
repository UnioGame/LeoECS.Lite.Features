namespace Game.Ecs.Ability.AbilityUtilityView.Radius.Systems
{
    using Component;
    using Core.Components;
    using Leopotam.EcsLite;

    public sealed class ProcessAbilityRadiusWhenOwnerDeadSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<OwnerDestroyedEvent>()
                .Inc<RadiusViewStateComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var radiusStatePool = _world.GetPool<RadiusViewStateComponent>();
            var hideRadiusPool = _world.GetPool<HideRadiusRequest>();
            
            foreach (var entity in _filter)
            {
                ref var radiusState = ref radiusStatePool.Get(entity);
                foreach (var packedEntity in radiusState.RadiusViews)
                {
                    var hideRequestEntity = _world.NewEntity();
                    ref var hideRequest = ref hideRadiusPool.Add(hideRequestEntity);
                
                    hideRequest.Source = _world.PackEntity(entity);;
                    hideRequest.Destination = packedEntity.Key;
                }
            }
        }
    }
}