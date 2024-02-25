namespace Game.Ecs.GameResources.Components
{
    using System;
    using Leopotam.EcsLite;
    using UnityEngine.Serialization;

    [Serializable]
    public struct GameResourceHandleComponent
    {
        /// <summary>
        /// источник реквеста
        /// </summary>
        [FormerlySerializedAs("RequestOwner")] public EcsPackedEntity Source;
        
        /// <summary>
        /// Владелец ресурса. Может быть пустым
        /// </summary>
        [FormerlySerializedAs("ResourceOwner")] public EcsPackedEntity Owner;

        /// <summary>
        /// адрес ресурса
        /// </summary>
        public string Resource;
    }
    
    
    [Serializable]
    public struct GameResourceHandleComponent<TAsset>
    {
        /// <summary>
        /// источник реквеста
        /// </summary>
        public EcsPackedEntity RequestOwner;
        
        /// <summary>
        /// Владелец ресурса. Может быть пустым
        /// </summary>
        public EcsPackedEntity ResourceOwner;

        /// <summary>
        /// адрес ресурса
        /// </summary>
        public string Resource;
    }
}
