namespace Game.Ecs.Characteristics.Feature
{
    using UniGame.LeoEcs.Bootstrap.Runtime;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public abstract class CharacteristicFeature : BaseLeoEcsFeature
    {
    }
}