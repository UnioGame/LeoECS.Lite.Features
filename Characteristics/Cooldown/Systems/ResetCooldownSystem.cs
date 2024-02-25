namespace Game.Ecs.Characteristics.Cooldown.Systems
{
    using System;
    using Base.Components.Events;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    [Serializable]
    public sealed class ResetCooldownSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<BaseCooldownComponent> _baseCooldownPool;
        private EcsPool<RecalculateCooldownSelfRequest> _requestPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<BaseCooldownComponent>()
                .Inc<ResetCharacteristicsEvent>()
                .End();
            
           _baseCooldownPool = _world.GetPool<BaseCooldownComponent>();
           _requestPool = _world.GetPool<RecalculateCooldownSelfRequest>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var baseCooldown = ref _baseCooldownPool.Get(entity);
                baseCooldown.Modifications.Clear();
                
                if (!_requestPool.Has(entity))
                    _requestPool.Add(entity);
            }
        }
    }
}