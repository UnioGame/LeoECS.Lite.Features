namespace Ai.Ai.Variants.Prioritizer.Data
{
    using System;
    using Game.Code.GameLayers.Category;
    using Game.Ecs.GameLayers.Category.Components;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniModules.UniGame.Core.Runtime.DataStructure;

    [Serializable]
    public class CategoryComparer : IPriorityComparer
    {
        public SerializableDictionary<CategoryId, int> Value;
        
        public int Compare(EcsWorld world, int source, int current, int potential)
        {
            var categoryPool = world.GetPool<CategoryIdComponent>();
            ref var currentCategoryComponent = ref categoryPool.Get(current);
            ref var potentialCategoryComponent = ref categoryPool.Get(potential);

            var potentialValue = Value[potentialCategoryComponent.Value];
            var currentValue = Value[currentCategoryComponent.Value];
            if (potentialValue == currentValue)
            {
                return 0;
            }
            if (potentialValue > currentValue)
            {
                return 1;
            }

            return -1;
        }
        
        [Button]
        public void FillDefaultPriorities()
        {
            var keys = Enum.GetValues(typeof(CategoryId)) as CategoryId[];
            Value = new SerializableDictionary<CategoryId, int>();
            foreach (var k in keys)
            {
                Value.Add(k, 0);
            }
        }
    }
}