namespace Game.Ecs.GameResources.Components
{
    using System;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime;

    [Serializable]
    public struct GameResourceHandleComponent : IEcsAutoReset<GameResourceHandleComponent>
    {
        public EcsPackedEntity Source;
        public EcsPackedEntity Owner;
        public string Resource;
        public ILifeTime LifeTime;
        
        public void AutoReset(ref GameResourceHandleComponent c)
        {
            c.Resource = string.Empty;
            c.LifeTime = null;
            c.Owner = default;
            c.Source = default;
        }
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
