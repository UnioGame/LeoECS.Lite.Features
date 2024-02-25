namespace Game.Ecs.GameEffects.DamageEffect.Systems
{
    using Characteristics.AbilityPower.Components;
    using Characteristics.Attack.Components;
    using Components;
    using Effects.Components;
    using Gameplay.Damage.Components.Request;
    using Leopotam.EcsLite;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;

    public sealed class ProcessDamageEffectSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<DamageEffectRequestCompleteComponent> _damageEffectRequestCompletePool;
        private EcsPool<EffectComponent> _effectPool;
        private EcsPool<DamageEffectComponent> _damagePool;
        private EcsPool<AbilityPowerComponent> _abilityPowerPool;
        private EcsPool<ApplyDamageRequest> _requestPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<EffectComponent>()
                .Inc<ApplyEffectSelfRequest>()
                .Inc<DamageEffectComponent>()
                .Exc<DamageEffectRequestCompleteComponent>()
                .End();

            _damageEffectRequestCompletePool = _world.GetPool<DamageEffectRequestCompleteComponent>();
            _effectPool = _world.GetPool<EffectComponent>();
            _damagePool = _world.GetPool<DamageEffectComponent>();
            _abilityPowerPool = _world.GetPool<AbilityPowerComponent>();
            _requestPool = _world.GetPool<ApplyDamageRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var effect = ref _effectPool.Get(entity);
                if (!effect.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                var abilityPower = 1f;
                if (_abilityPowerPool.Has(entity))
                {
                    ref var abilityDamage = ref _abilityPowerPool.Get(entity);
                    abilityPower = abilityDamage.Value;
                }
                
                ref var damage = ref _damagePool.Get(entity);
                var requestEntity = _world.NewEntity();
                ref var request = ref _requestPool.Add(requestEntity);
                request.Source = effect.Source;
                request.Destination = effect.Destination;
                request.Value = damage.Value * abilityPower;
                _damageEffectRequestCompletePool.Add(entity);
            }
        }
    }
}