namespace Game.Ecs.Effects.Systems
{
    using Components;
    using Leopotam.EcsLite;

    public sealed class ProcessAppliedEffectsSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<ApplyEffectSelfRequest>()
                .Exc<DestroyEffectSelfRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var eventPool = _world.GetPool<EffectAppliedSelfEvent>();

            foreach (var entity in _filter)
            {
                eventPool.Add(entity);
            }
        }
    }
}