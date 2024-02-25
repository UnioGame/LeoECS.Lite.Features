namespace Game.Code.Configuration.Runtime.Effects.Abstract
{
    using Leopotam.EcsLite;

    public interface IEffectConfiguration
    {
        TargetType TargetType { get; }

        void ComposeEntity(EcsWorld world, int effectEntity);
    }
}