namespace Game.Ecs.GameAi.MoveToTarget.Components
{
    using Code.GameLayers.Category;
    using Unity.Mathematics;

    public struct MoveToPoiComponent
    {
        public float Priority;
        public float3 Position;
        public CategoryId CategoryId;
    }
}
