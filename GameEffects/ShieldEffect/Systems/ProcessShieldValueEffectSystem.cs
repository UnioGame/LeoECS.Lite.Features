namespace Game.Ecs.GameEffects.ShieldEffect.Systems
{
    using Characteristics.Shield.Components;
    using Components;
    using Effects.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;

    public sealed class ProcessShieldValueEffectSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ShieldEffectComponent>()
                .Inc<EffectComponent>()
                .Exc<DestroyEffectSelfRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var effectPool = _world.GetPool<EffectComponent>();
            var shieldPool = _world.GetPool<ShieldComponent>();
            var destroyRequestPool = _world.GetPool<DestroyEffectSelfRequest>();

            foreach (var entity in _filter)
            {
                ref var effect = ref effectPool.Get(entity);
                if (!effect.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                if (!shieldPool.Has(destinationEntity))
                {
                    destroyRequestPool.Add(entity);
                }
            }
        }
    }
}