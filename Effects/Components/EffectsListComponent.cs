namespace Game.Ecs.Effects.Components
{
    using System.Collections.Generic;
    using Leopotam.EcsLite;

    /// <summary>
    /// Список эффектов на игровой сущности. При обработке эффектов, они добавляются в этот список или
    /// убираются из него. Также список автоматически валидируется на момент мертвых эффектов.
    /// </summary>
    public struct EffectsListComponent : IEcsAutoReset<EffectsListComponent>
    {
        public List<EcsPackedEntity> Effects;
        
        public void AutoReset(ref EffectsListComponent c)
        {
            c.Effects ??= new List<EcsPackedEntity>();
            c.Effects.Clear();
        }
    }
}