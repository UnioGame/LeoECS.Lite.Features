namespace Game.Ecs.GameResources.Components
{
    using Leopotam.EcsLite;

    /// <summary>
    /// Компонент указывает, что над данной энтити ведется работа по загрузке ресурсов
    /// </summary>
    public struct GameResourceTaskComponent : IEcsAutoReset<GameResourceTaskComponent>
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

        /// <summary>
        /// время загрузки
        /// </summary>
        public float LoadingDuration;

        /// <summary>
        /// Время начало загрузки
        /// </summary>
        public float LoadingStartTime;
        
        public void AutoReset(ref GameResourceTaskComponent c)
        {
            c.LoadingDuration = 0;
            c.LoadingStartTime = 0;
            c.RequestOwner = new EcsPackedEntity();
            c.RequestOwner = new EcsPackedEntity();
            c.Resource = string.Empty;
        }
    }
}