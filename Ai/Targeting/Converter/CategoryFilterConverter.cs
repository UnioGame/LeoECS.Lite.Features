namespace Game.Ecs.Ai.Targeting.Converters
{
    using System;
    using Code.GameLayers.Category;
    using Code.GameLayers.Relationship;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]

    [Serializable]
    public class CategoryFilterConverter : ITargetSelectorConverter
    {
        [SerializeField]
        private CategoryId _categoryId;

        [SerializeField]
        private RelationshipId _relationshipId;

        public void Apply(EcsWorld world, int entity)
        {
            ref var categoryFilterComponent = ref world.AddComponent<CategoryFilterComponent>(entity);
            categoryFilterComponent.CategoryId = _categoryId;
            categoryFilterComponent.Relationship = _relationshipId;
        }
    }
}