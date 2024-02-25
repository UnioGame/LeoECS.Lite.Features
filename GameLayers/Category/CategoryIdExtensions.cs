namespace Game.Code.GameLayers.Category
{
    using System.Runtime.CompilerServices;

    public static class CategoryIdExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFlag(this CategoryId mask, CategoryId category)
        {
            return (mask & category) == category;
        }
    }
}