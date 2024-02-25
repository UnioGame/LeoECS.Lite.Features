namespace Game.Ecs.GameEffects.DamageEffect.Systems
{
    using Characteristics.Attack.Components;
    using Characteristics.CriticalMultiplier.Components;
    using Characteristics.SplashDamage.Components;
    using Components;
    using Effects.Components;
    using Gameplay.CriticalAttackChance.Components;
    using Gameplay.Damage.Components.Request;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

    /// <summary>
    /// deal part of attack damage based on SplashDamageComponent
    /// </summary>
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    public sealed class ProcessSplashAttackDamageEffectSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<EffectComponent> _effectPool;
        private EcsPool<AttackDamageComponent> _attackDamagePool;
        private EcsPool<SplashDamageComponent> _splashDamagePool;
        private EcsPool<CriticalAttackMarkerComponent> _criticalMarkerPool;
        private EcsPool<CriticalMultiplierComponent> _criticalMultiplierPool;
        private EcsPool<ApplyDamageRequest> _requestPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<EffectComponent>()
                .Inc<ApplyEffectSelfRequest>()
                .Inc<SplashAttackDamageEffectComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var effect = ref _effectPool.Get(entity);

                if (!effect.Source.Unpack(_world, out var sourceEntity) || !_splashDamagePool.Has(sourceEntity))
                    continue;

                ref var splashDamage = ref _splashDamagePool.Get(sourceEntity);
                if (splashDamage.Value == 0) continue;
                ref var attackDamage = ref _attackDamagePool.Get(sourceEntity);

                var damage = attackDamage.Value;

                var requestEntity = _world.NewEntity();
                ref var request = ref _requestPool.Add(requestEntity);
                
                request.Source = effect.Source;
                request.Effector = _world.PackEntity(entity);
                request.Destination = effect.Destination;
                var criticalMultiplier = _criticalMarkerPool.Has(sourceEntity)
                    ? 1 + _criticalMultiplierPool.Get(sourceEntity).Value / 100
                    : 1;
                request.Value = damage * criticalMultiplier * splashDamage.Value / 100;
            }
        }
    }
}