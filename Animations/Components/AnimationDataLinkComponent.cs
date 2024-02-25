namespace Game.Ecs.Core.Components
{
    using System;
    using Code.Animations;
    using Leopotam.EcsLite;

    /// <summary>
    /// link to animation entity
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AnimationLinkComponent
    {
        public EcsPackedEntity Value;
    }
    
    [Serializable]
    public struct AnimationDataLinkComponent
    {
        public AnimationLink AnimationLink;
    }
    

}