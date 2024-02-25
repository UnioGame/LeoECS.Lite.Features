namespace Game.Ecs.Ability.SubFeatures.Area.Systems
{
    using Characteristics.Radius.Component;
    using Common.Components;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    public sealed class SetupAreaByJoystickSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<RadiusComponent> _radiusPool;
        private EcsPool<AbilityVelocityEvent> _velocityPool;
        private EcsPool<AreaLocalPositionComponent> _areaPositionPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<AreableAbilityComponent>()
                .Inc<RadiusComponent>()
                .Inc<AbilityVelocityEvent>()
                .End();
            
            _radiusPool = _world.GetPool<RadiusComponent>();
            _velocityPool = _world.GetPool<AbilityVelocityEvent>();
            _areaPositionPool = _world.GetPool<AreaLocalPositionComponent>();
        }

        public void Run(IEcsSystems systems)
        {


            foreach (var entity in _filter)
            {
                ref var radius = ref _radiusPool.Get(entity);
                ref var velocity = ref _velocityPool.Get(entity);

                var localPosition = velocity.Value * radius.Value;

                ref var areaPosition = ref _areaPositionPool.GetOrAddComponent(entity);
                areaPosition.Value = localPosition;
            }
        }
    }
}