namespace NAMESPACE
{
    using System;
    using Ai.Ai.Variants.Attack.Converters;
    using Ai.Ai.Variants.Prioritizer.Components;
    using Game.Code.GameLayers.Category;
    using Game.Ecs.AI.Converters;
    using Game.Ecs.Core.Components;
    using Game.Ecs.GameAi.MoveToTarget.Converters;
    using Game.Ecs.Shared.Generated;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniModules.UniGame.Core.Runtime.DataStructure;

    [Serializable]
    public class CategoryPrioritizerConverter : EcsComponentSubPlannerConverter,
        IMoveByConverter, 
        IAttackConverter
    {
        public CategoryPrioritizerComponent _categoryPrioritizer;
        
        public override void Apply(EcsWorld world, int entity, ActionType actionId)
        {
            base.Apply(world, entity, actionId);

            var requestEntity = world.NewEntity();
            ref var ownerComponent = ref world.AddComponent<OwnerComponent>(requestEntity);
            ownerComponent.Value = entity.PackedEntity(world);
            ref var prioritizerComponent = ref world.AddComponent<CategoryPrioritizerComponent>(requestEntity);
            prioritizerComponent.Value = new SerializableDictionary<CategoryId, int>();
            prioritizerComponent.ActionId = actionId;
            foreach (var kvp in _categoryPrioritizer.Value)
            {
                prioritizerComponent.Value.Add(kvp.Key, kvp.Value);
            }
        }

        [Button]
        public void FillDefaultPriorities()
        {
            var keys = Enum.GetValues(typeof(CategoryId)) as CategoryId[];
            _categoryPrioritizer.Value = new SerializableDictionary<CategoryId, int>();
            foreach (var k in keys)
            {
                _categoryPrioritizer.Value.Add(k, 0);
            }
        }
    }
}