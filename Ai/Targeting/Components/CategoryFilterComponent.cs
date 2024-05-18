namespace Game.Ecs.Ai.Targeting.Components
{
    using System;
    using Code.GameLayers.Category;
    using Code.GameLayers.Relationship;
    using UniGame.LeoEcs.Shared.Abstract;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CategoryFilterComponent : IApplyableComponent<CategoryFilterComponent>
    {
        public RelationshipId Relationship;
        public CategoryId CategoryId;
        
        public void Apply(ref CategoryFilterComponent component)
        {
            component.Relationship = Relationship;
            component.CategoryId = CategoryId;
        }
    }
}