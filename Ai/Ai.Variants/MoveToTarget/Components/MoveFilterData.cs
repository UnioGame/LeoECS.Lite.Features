namespace Game.Ecs.GameAi.MoveToTarget.Components
{
    using System;
    using Code.GameLayers.Category;
    using Code.GameLayers.Relationship;

    [Serializable]
    public struct MoveFilterData
    {
        /// <summary>
        /// Маска для фильтр отношения при поиске целей
        /// </summary>
        [RelationshipIdMask]
        public RelationshipId Relationship;
        
        [CategoryIdMask]
        public CategoryId CategoryId;
        
        public float SensorDistance;
    }
}