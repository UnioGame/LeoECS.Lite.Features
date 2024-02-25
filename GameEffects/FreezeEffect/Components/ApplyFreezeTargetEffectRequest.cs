namespace Game.Ecs.GameEffects.FreezeEffect.Components
{
    using Leopotam.EcsLite;

    public struct ApplyFreezeTargetEffectRequest
    {
        public EcsPackedEntity Source;
        public EcsPackedEntity Destination;
        public float DumpTime;
    }
}