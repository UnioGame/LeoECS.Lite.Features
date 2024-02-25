namespace Game.Code.GameLayers.Relationship
{
    using System.Runtime.CompilerServices;
#if ENABLE_IL2CPP
    using System.Runtime.CompilerServices;
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public static class RelationshipExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFlag(this RelationshipId mask, RelationshipId layer)
        {
            return (mask & layer) == layer;
        }
    }
}