namespace Game.Ecs.Ability.SubFeatures.Target.Systems
{
    using Common.Components;
    using Components;
    using Core.Components;
    using Ecs.Effects;
    using Ecs.Effects.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    [ECSDI]
    public sealed class InstantlyApplyAbilityEffectsSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ApplyAbilityEffectsSelfRequest> _applyRequestPool;
        private EcsPool<EffectsComponent> _effectsPool;
        private EcsPool<OwnerComponent> _ownerPool;
        private EcsPool<AbilityTargetsComponent> _targetsPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<CanInstantlyApplyEffects>()
                .Inc<ApplyAbilityEffectsSelfRequest>()
                .Inc<EffectsComponent>()
                .Inc<OwnerComponent>()
                .Inc<AbilityTargetsComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _ownerPool.Get(entity);

                ref var effects = ref _effectsPool.Get(entity);
                ref var targets = ref _targetsPool.Get(entity);

                var amount = targets.Count;
                for (var i = 0; i < amount; i++)
                {
                    ref var target = ref targets.Entities[i];
                    effects.Effects.CreateRequests(_world, owner.Value, target);
                }
            }
        }
    }
}