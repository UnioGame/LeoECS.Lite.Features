using Game.Code.GameLayers.Category;
using Game.Code.GameLayers.Relationship;
using UnityEngine;

namespace Game.Ecs.GameAi.Targeting.Components
{
    using System;
    using UniGame.LeoEcs.Shared.Abstract;

    [Serializable]
    public struct SelectByCategoryComponent : IApplyableComponent<SelectByCategoryComponent>
    {
        public RelationshipId Relationship;
        public CategoryId CategoryId;

        [HideInInspector]
        public int ActionId;
                
        public void Apply(ref SelectByCategoryComponent component)
        {
            component.Relationship = Relationship;
            component.CategoryId = CategoryId;
        }
    }
}