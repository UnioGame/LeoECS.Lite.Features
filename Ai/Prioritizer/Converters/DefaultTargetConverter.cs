namespace Ai.Ai.Variants.Prioritizer.Converters
{
    using System;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using Unity.IL2CPP.CompilerServices;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public class DefaultTargetConverter : EcsComponentConverter
    {
        public sealed override void Apply(EcsWorld world, int entity)
        {
            
        }
    }
}