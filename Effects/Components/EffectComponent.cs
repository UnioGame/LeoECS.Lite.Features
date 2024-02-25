namespace Game.Ecs.Effects.Components
{
    using Leopotam.EcsLite;

    /// <summary>
    /// Компонент эффекта на цели.
    /// </summary>
    public struct EffectComponent
    {
        public EcsPackedEntity Source;
        public EcsPackedEntity Destination;
    }
}