namespace Game.Ecs.Ability.SubFeatures.Self.Systems
{
    using Common.Components;
    using Components;
    using Core.Components;
    using Ecs.Effects;
    using Ecs.Effects.Components;
    using Leopotam.EcsLite;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif
	

    /// <summary>
    /// Apply ability effects to self.
    /// </summary>
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public sealed class SelfApplyAbilityEffectsSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<SelfEffectsComponent> _effectsPool;
        private EcsPool<OwnerComponent> _ownerPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<SelfEffectsComponent>()
                .Inc<ApplyAbilityEffectsSelfRequest>()
                .Inc<OwnerComponent>()
                .End();
            
            _effectsPool = _world.GetPool<SelfEffectsComponent>();
            _ownerPool = _world.GetPool<OwnerComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _ownerPool.Get(entity);
                ref var effects = ref _effectsPool.Get(entity);
                
                effects.Effects.CreateRequests(_world, owner.Value, owner.Value);
            }
        }
    }
}