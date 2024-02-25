namespace Game.Ecs.GameLayers.Category.Converters
{
    using System;
    using System.Threading;
    using Code.GameLayers.Category;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public sealed class CategoryIdConverter : GameObjectConverter
    {
        [SerializeField]
        public CategoryId categoryId;

        protected override void OnApply(GameObject target, EcsWorld world, int entity)
        {
            ref var category = ref world.AddComponent<CategoryIdComponent>(entity);
            category.Value = categoryId;
        }
    }
}
