namespace Game.Ecs.Ability.SubFeatures.Selection.Systems
{
    using System;
    using Common.Components;
    using Components;
    using Leopotam.EcsLite;

    public sealed class ClearSelectionForNonInHandAbilitySystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<SelectedTargetsComponent>()
                .Exc<AbilityInHandComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var targetsPool = _world.GetPool<SelectedTargetsComponent>();

            foreach (var entity in _filter)
            {
                ref var targets = ref targetsPool.Get(entity);
                targets.SetEntities(Array.Empty<EcsPackedEntity>(),0);
            }
        }
    }
}