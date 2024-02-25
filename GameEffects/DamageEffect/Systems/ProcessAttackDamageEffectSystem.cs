namespace Game.Ecs.GameEffects.DamageEffect.Systems
{
    using Characteristics.Attack.Components;
    using Characteristics.Dodge.Components;
    using Components;
    using Effects.Components;
    using Gameplay.CriticalAttackChance.Components;
    using Gameplay.Damage.Components.Request;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine;

    [ECSDI]
    public sealed class ProcessAttackDamageEffectSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<EffectComponent> _effectPool;
        private EcsPool<AttackDamageComponent> _attackDamagePool;
        private EcsPool<CriticalAttackMarkerComponent> _criticalChancePool;
        private EcsPool<ApplyDamageRequest> _requestPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<EffectComponent>()
                .Inc<ApplyEffectSelfRequest>()
                .Inc<AttackDamageEffectComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var effect = ref _effectPool.Get(entity);

                if (!effect.Source.Unpack(_world, out var sourceEntity) || !_attackDamagePool.Has(sourceEntity))
                    continue;

                ref var attackDamage = ref _attackDamagePool.Get(sourceEntity);

                var damage = attackDamage.Value;

                var requestEntity = _world.NewEntity();
                ref var request = ref _requestPool.Add(requestEntity);
                
                request.Source = effect.Source;
                request.Effector = _world.PackEntity(entity);
                request.Destination = effect.Destination;
                request.Value = damage;
                request.IsCritical = _criticalChancePool.Has(sourceEntity);
            }
        }
    }
}