namespace Game.Ecs.GameResources.Components
{
    using System;
    using Leopotam.EcsLite;
    using UnityEngine.Serialization;
    using Object = UnityEngine.Object;

    [Serializable]
    public struct GameResourceSpawnCompleteEvent
    {
        public EcsPackedEntity Source;
        public EcsPackedEntity SpawnedEntity;
        public string ResourceId;
        public Object Resource;
    }
    
    
}