namespace Game.Ecs.Core.Death.Components
{
    using Leopotam.EcsLite;
    using UnityEngine.Playables;

    public struct DeathAnimationComponent : IEcsAutoReset<DeathAnimationComponent>
    {
        public PlayableAsset Animation;

        public void AutoReset(ref DeathAnimationComponent c)
        {
            c.Animation = null;
        }
    }
}