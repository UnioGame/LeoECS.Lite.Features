namespace Game.Ecs.Ability.AbilityUtilityView.Radius.AggressiveRadius.Systems
{
    using AbilityUtilityView.Components;
    using Component;
    using Components;
    using Core.Components;
    using Core.Death.Components;
    using Leopotam.EcsLite;

    public sealed class ProcessAggressiveAbilityRadiusWhenDeadSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<DestroyComponent>()
                .Inc<VisibleUtilityViewComponent>()
                .Inc<AggressiveRadiusViewStateComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            var statePool = _world.GetPool<AggressiveRadiusViewStateComponent>();
            var hideRadiusPool = _world.GetPool<HideRadiusRequest>();

            foreach (var entity in _filter)
            {
                ref var state = ref statePool.Get(entity);
                foreach (var packedEntity in state.Entities)
                {
                    var hideRequestEntity = _world.NewEntity();
                    ref var hideRequest = ref hideRadiusPool.Add(hideRequestEntity);

                    hideRequest.Source = _world.PackEntity(entity);
                    hideRequest.Destination = packedEntity;
                }
            }
        }
    }
}