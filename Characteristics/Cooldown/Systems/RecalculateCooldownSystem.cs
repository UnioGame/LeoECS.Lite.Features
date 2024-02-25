namespace Game.Ecs.Characteristics.Cooldown.Systems
{
    using System;
    using Base.Modification;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Timer.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    [Serializable]
    public sealed class RecalculateCooldownSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<CooldownComponent> cooldownPool;
        private EcsPool<BaseCooldownComponent> baseCooldownPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<RecalculateCooldownSelfRequest>()
                .Inc<BaseCooldownComponent>()
                .Inc<CooldownComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var baseCooldown = ref baseCooldownPool.Get(entity);
                ref var cooldown = ref cooldownPool.Get(entity);

                cooldown.Value = baseCooldown.Modifications.Apply(baseCooldown.Value);
            }
        }
    }
}