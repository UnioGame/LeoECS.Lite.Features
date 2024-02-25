namespace Game.Code.GameLayers.Layer
{
    using System.Runtime.CompilerServices;

    public static class LayerIdExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFlag(this ref LayerId mask,ref LayerId layer)
        {
            return (mask & layer) == layer;
        }
    }
}