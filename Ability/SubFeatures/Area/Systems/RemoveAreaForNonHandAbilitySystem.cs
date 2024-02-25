namespace Game.Ecs.Ability.SubFeatures.Area.Systems
{
    using Aspects;
    using Common.Components;
    using Components;
    using Leopotam.EcsLite;

    public sealed class RemoveAreaForNonHandAbilitySystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private AreaAspect _areaAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AreableAbilityComponent>()
                .Inc<AreaLocalPositionComponent>()
                .Exc<AbilityInHandComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                _areaAspect.AreaPosition.Del(entity);
            }
        }
    }
}