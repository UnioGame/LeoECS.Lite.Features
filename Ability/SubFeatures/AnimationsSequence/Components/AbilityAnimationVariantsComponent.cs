namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations.Components
{
    using System;
    using System.Collections.Generic;
    using Leopotam.EcsLite;

    /// <summary>
    /// list of animation variants
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityAnimationVariantsComponent : IEcsAutoReset<AbilityAnimationVariantsComponent>
    {
        public List<EcsPackedEntity> Variants;
        
        public void AutoReset(ref AbilityAnimationVariantsComponent c)
        {
            c.Variants ??= new List<EcsPackedEntity>();
            c.Variants.Clear();
        }
    }
}