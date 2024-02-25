namespace Game.Ecs.Effects.Systems
{
    using Components;
    using Leopotam.EcsLite;
    using UnityEngine;

    public sealed class DestroyEffectViewSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<EffectViewComponent>()
                .Inc<DestroyEffectViewSelfRequest>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var viewPool = _world.GetPool<EffectViewComponent>();

            foreach (var entity in _filter)
            {
                ref var view = ref viewPool.Get(entity);
                if (view.ViewInstance != null)
                {
                    Object.Destroy(view.ViewInstance);
                }
                
                _world.DelEntity(entity);
            }
        }
    }
}