namespace Game.Ecs.Effects.Systems
{
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    public sealed class ProcessEffectDurationSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter= _world.Filter<EffectComponent>()
                .Inc<EffectDurationComponent>()
                .Exc<DestroyEffectSelfRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var durationPool = _world.GetPool<EffectDurationComponent>();
            var destroyPool = _world.GetPool<DestroyEffectSelfRequest>();

            foreach (var entity in _filter)
            {
                ref var duration = ref durationPool.Get(entity);
                if(duration.Duration < 0.0f)
                    continue;
                
                var deadTime = duration.CreatingTime + duration.Duration;
                if(Time.time < deadTime && !Mathf.Approximately(deadTime, Time.time))
                    continue;

                destroyPool.TryAdd(entity);
            }
        }
    }
}