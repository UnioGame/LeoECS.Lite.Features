namespace Game.Ecs.GameEffects.HealingEffect.Systems
{
    using Characteristics.Base.Components.Requests.OwnerRequests;
    using Characteristics.Health.Components;
    using Components;
    using Effects.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    [ECSDI]
    public sealed class ProcessHealingEffectSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<EffectComponent> effectPool;
        private EcsPool<HealingEffectComponent> healingPool;
        private EcsPool<ChangeCharacteristicBaseRequest<HealthComponent>> changeHealthPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<EffectComponent>()
                .Inc<ApplyEffectSelfRequest>()
                .Inc<HealingEffectComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var effect = ref effectPool.Get(entity);
                ref var healing = ref healingPool.Get(entity);

                var healthRequestEntity = _world.NewEntity();
                ref var healthRequest = ref changeHealthPool.Add(healthRequestEntity);
                healthRequest.Source = effect.Source;
                healthRequest.Target = effect.Destination;
                healthRequest.Value = healing.Value;
            }
        }
    }
}