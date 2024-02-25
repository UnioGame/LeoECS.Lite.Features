namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations.Behaviours
{
    using System;
    using Code.Animations;
    using UnityEngine.AddressableAssets;

    [Serializable]
    public class AssetReferenceAnimationsSequence : AssetReferenceT<AnimationLink>
    {
        public AssetReferenceAnimationsSequence(string guid) : base(guid)
        {
        }
    }
}