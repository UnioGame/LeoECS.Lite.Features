namespace Game.Ecs.Effects.Components
{
    using Leopotam.EcsLite;

    /// <summary>
    /// Компонент длительности эффекта.
    /// </summary>
    public struct EffectDurationComponent : IEcsAutoReset<EffectDurationComponent>
    {
        /// <summary>
        /// Длительность эффекта.
        /// </summary>
        public float Duration;
        
        /// <summary>
        /// Время создания эффекта. Если <see cref="CreatingTime"/> + <see cref="Duration"/>
        /// больше или равен текущему времени, то эффект будет уничтожен.
        /// </summary>
        public float CreatingTime;
        
        public void AutoReset(ref EffectDurationComponent c)
        {
            c.Duration = 0.0f;
            c.CreatingTime = float.MinValue;
        }
    }
}