namespace Game.Ecs.TargetSelection.Components
{
    using System;
    using Code.GameLayers.Category;
    using Code.GameLayers.Layer;
    using Code.GameLayers.Relationship;
    using GameLayers.Category.Components;
    using Leopotam.EcsLite;
    
    /// <summary>
    /// mark entity as target for range selection
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SqrRangeTargetSelectionComponent
    {
        public float Radius;
        public CategoryId Category;
        public RelationshipId Relationship;
        public LayerId Layer;
        public EcsPackedEntity Target;
    }
    
}