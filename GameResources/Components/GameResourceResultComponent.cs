namespace Game.Ecs.GameResources.Components
{
    using System;
    using Object = UnityEngine.Object;

    [Serializable]
    public struct GameResourceResultComponent
    {
        /// <summary>
        /// Идентификатор по которому был загружен ресурс
        /// </summary>
        public string ResourceId;

        /// <summary>
        /// загруженный ресурс
        /// </summary>
        public Object Resource;
    }
}