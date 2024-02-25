namespace Game.Ecs.GameResources.Components
{
    using System;
    using Data;
    using Game.Code.DataBase.Runtime;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Abstract;
    using UnityEngine;
    using UnityEngine.Serialization;

    /// <summary>
    /// Spawn Request for some game world entity
    /// Это OneShot компонент, он будет убит после обработкой систему спавна
    /// </summary>
    [Serializable]
    public struct GameResourceSpawnRequest : 
        IApplyableComponent<GameResourceSpawnRequest>, 
        IEcsAutoReset<GameResourceSpawnRequest>
    {
        /// <summary>
        /// id of game entity in game database
        /// </summary>
        public string ResourceId;

        /// <summary>
        /// link to source entity
        /// </summary>
        [FormerlySerializedAs("RequestOwner")] 
        public EcsPackedEntity Source;

        /// <summary>
        /// spawned entity Owner
        /// </summary>
        [FormerlySerializedAs("OwnerEntity")] 
        public EcsPackedEntity Owner;

        /// <summary>
        /// entity target for spawn
        /// </summary>
        [FormerlySerializedAs("TargetEntity")] 
        public EcsPackedEntity Target;
        
        /// <summary>
        /// parent entity for spawn
        /// </summary>
        public EcsPackedEntity ParentEntity;
        
        /// <summary>
        /// spawn location data
        /// </summary>
        public GamePoint LocationData;

        /// <summary>
        /// spawn parent object
        /// </summary>
        public Transform Parent;

        public void Apply(ref GameResourceSpawnRequest data)
        {
            data.ResourceId = ResourceId;
            data.Source = Source;
            data.Owner = Owner;
            data.LocationData = LocationData;
            data.Parent = Parent;
            data.Target = Target;
            data.ParentEntity = ParentEntity;
        }

        public void AutoReset(ref GameResourceSpawnRequest c)
        {
            c.ResourceId = (GameResourceRecordId)string.Empty;
            c.Parent = default;
            c.LocationData = GamePoint.Zero;
            c.Owner = default;
            c.Source = default;
            c.Target = default;
            c.ParentEntity = default;
        }
    }
}