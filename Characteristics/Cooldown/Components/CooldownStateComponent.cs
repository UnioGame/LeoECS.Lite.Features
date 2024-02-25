namespace Game.Ecs.Characteristics.Cooldown.Components
{
    using Leopotam.EcsLite;

    /// <summary>
    /// Состояние отката.
    /// </summary>
    public struct CooldownStateComponent : IEcsAutoReset<CooldownStateComponent>
    {
        /// <summary>
        /// Время последнего использования умения.
        /// </summary>
        public float LastUseTime;

        public void AutoReset(ref CooldownStateComponent c)
        {
            c.LastUseTime = 0;
        }
    }
}