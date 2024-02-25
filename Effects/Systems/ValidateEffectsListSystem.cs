namespace Game.Ecs.Effects.Systems
{
    using System.Collections.Generic;
    using Components;
    using Leopotam.EcsLite;

    public sealed class ValidateEffectsListSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private List<EcsPackedEntity> _cacheList = new List<EcsPackedEntity>();

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<EffectsListComponent>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var listPool = _world.GetPool<EffectsListComponent>();

            foreach (var entity in _filter)
            {
                ref var list = ref listPool.Get(entity);
                _cacheList.Clear();
                _cacheList.AddRange(list.Effects);
                
                list.Effects.Clear();
                
                foreach (var effectPackedEntity in _cacheList)
                {
                    if(!effectPackedEntity.Unpack(_world, out _))
                        continue;
                    
                    list.Effects.Add(effectPackedEntity);
                }
            }
        }
    }
}