namespace Game.Ecs.Effects.Components
{
    using Code.Configuration.Runtime.Effects.Abstract;
    using Leopotam.EcsLite;

    /// <summary>
    /// Запрос создания эффекта на цель.
    /// </summary>
    public struct CreateEffectSelfRequest
    {
        public EcsPackedEntity Source;
        public EcsPackedEntity Destination;

        public IEffectConfiguration Effect;
    }
}