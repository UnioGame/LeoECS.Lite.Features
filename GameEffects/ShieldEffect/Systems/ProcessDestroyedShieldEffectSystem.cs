namespace Game.Ecs.GameEffects.ShieldEffect.Systems
{
    using Characteristics.Shield.Components;
    using Components;
    using Effects.Components;
    using Leopotam.EcsLite;
    using UnityEngine;

    public sealed class ProcessDestroyedShieldEffectSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ShieldEffectComponent>()
                .Inc<EffectComponent>()
                .Inc<DestroyEffectSelfRequest>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            var effectPool = _world.GetPool<EffectComponent>();
            var shieldEffectPool = _world.GetPool<ShieldEffectComponent>();
            var shieldPool = _world.GetPool<ShieldComponent>();

            foreach (var entity in _filter)
            {
                ref var effect = ref effectPool.Get(entity);
                if (!effect.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                if (!shieldPool.Has(destinationEntity))
                    continue;

                ref var shieldEffect = ref shieldEffectPool.Get(entity);
                ref var shield = ref shieldPool.Get(destinationEntity);
                shield.Value -= shieldEffect.MaxValue;

                if (shield.Value <= 0.0f || Mathf.Approximately(shield.Value, 0.0f))
                    shieldPool.Del(destinationEntity);
            }
        }
    }
}