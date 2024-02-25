namespace Game.Ecs.GameEffects.FreezeEffect.Components
{
    using Leopotam.EcsLite;

    /// <summary>
    /// Says that the freezing effect is used on the target
    /// </summary>
    public struct FreezeTargetEffectComponent
    {
        public EcsPackedEntity Source;
        // Creating time ability + Duration
        public float DumpTime;
    }
}