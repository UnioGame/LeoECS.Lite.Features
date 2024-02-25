namespace Game.Ecs.Ability.AbilityUtilityView.Radius.AggressiveRadius.Components
{
    using System.Collections.Generic;
    using Leopotam.EcsLite;

    public struct AggressiveRadiusViewStateComponent : IEcsAutoReset<AggressiveRadiusViewStateComponent>
    {
        private List<EcsPackedEntity> _entities;
        private List<EcsPackedEntity> _previousEntities;

        public IReadOnlyList<EcsPackedEntity> Entities => _entities;
        public IReadOnlyList<EcsPackedEntity> PreviousEntities => _previousEntities;

        public void SetEntities(IEnumerable<EcsPackedEntity> entities)
        {
            _previousEntities.Clear();
            _previousEntities.AddRange(Entities);
            
            _entities.Clear();
            _entities.AddRange(entities);
        }

        public void AutoReset(ref AggressiveRadiusViewStateComponent c)
        {
            c._entities ??= new List<EcsPackedEntity>();
            c._entities.Clear();
            
            c._previousEntities ??= new List<EcsPackedEntity>();
            c._previousEntities.Clear();
        }
    }
}