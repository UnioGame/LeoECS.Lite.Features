namespace Game.Ecs.Effects.Systems
{
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    [ECSDI]
    public sealed class ValidateEffectSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EffectAspect _effectAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<CreateEffectSelfRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _effectAspect.Create.Get(entity);
                if (!request.Destination.Unpack(_world, out _))
                    _effectAspect.Create.Del(entity);
            }
        }
    }
}