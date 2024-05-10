namespace Game.Ecs.AI.Converters
{
    using System;
    using Leopotam.EcsLite;
    using Shared.Generated;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;

    public abstract class EcsComponentSubPlannerConverter : IEcsComponentConverter, IEcsComponentSubPlannerConverter
    {
        public bool IsEnabled => true;
        public string Name => GetType().Name;
        
        public bool IsMatch(string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) return true;

            if (GetType().Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        public virtual void Apply(EcsWorld world, int entity)
        {
            
        }

        public virtual void Apply(EcsWorld world, int entity, ActionType actionId)
        {
            Apply(world, entity);
        }
    }
}