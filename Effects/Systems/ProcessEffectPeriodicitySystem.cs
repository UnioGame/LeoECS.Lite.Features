namespace Game.Ecs.Effects.Systems
{
    using Components;
    using Leopotam.EcsLite;
    using Time.Service;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// System for processing periodicity of effects
    /// </summary>
    public sealed class ProcessEffectPeriodicitySystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<EffectComponent>()
                .Inc<EffectPeriodicityComponent>()
                .Exc<DestroyEffectSelfRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var periodicityPool = _world.GetPool<EffectPeriodicityComponent>();
            var applyRequestPool = _world.GetPool<ApplyEffectSelfRequest>();

            foreach (var entity in _filter)
            {
                ref var periodicity = ref periodicityPool.Get(entity);
                if (periodicity.Periodicity < 0.0f)
                {
                    if (periodicity.LastApplyingTime < Time.time)
                    {
                        periodicity.LastApplyingTime = float.MaxValue;
                        applyRequestPool.TryAdd(entity);
                    }
                    
                    continue;
                }
                
                var nextApplyingTime = periodicity.LastApplyingTime + periodicity.Periodicity;
                if(GameTime.Time < nextApplyingTime && !Mathf.Approximately(nextApplyingTime, GameTime.Time))
                    continue;

                periodicity.LastApplyingTime = GameTime.Time;
                
                applyRequestPool.TryAdd(entity);
            }
        }
    }
}