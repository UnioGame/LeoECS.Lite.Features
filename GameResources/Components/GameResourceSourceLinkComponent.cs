namespace Game.Ecs.GameResources.Components
{
    using Leopotam.EcsLite;

    public struct GameResourceSourceLinkComponent
    {
        public EcsPackedEntity Source;
        public EcsPackedEntity SpawnedEntity;
    }
}