namespace Game.Ecs.Ai.Targeting.Components
{
    using System;
    using UniGame.LeoEcs.Shared.Abstract;
    using Code.GameLayers.Category;
    using Code.GameLayers.Relationship;

    [Serializable]
    public struct SelectByCategoryComponent : IApplyableComponent<SelectByCategoryComponent>
    {
        public RelationshipId Relationship;
        public CategoryId CategoryId;
        
        public void Apply(ref SelectByCategoryComponent component)
        {
            component.Relationship = Relationship;
            component.CategoryId = CategoryId;
        }
    }
}