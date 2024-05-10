namespace Ai.Ai.Variants.Prioritizer.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.GameLayers.Category.Components;
    using Game.Ecs.TargetSelection.Aspects;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CategoryPrioritizerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private TargetSelectionAspect _targetSelectionAspect;
        private PrioritizerAspect _prioritizerAspect;
        private EcsPool<CategoryIdComponent> _categoryPool;
        private EcsPool<OwnerComponent> _ownerPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<CategoryPrioritizerComponent>()
                .Inc<OwnerComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var priorityEntity in _filter)
            {
                ref var priorityComponent = ref _prioritizerAspect.Priority.Get(priorityEntity);
                var actionId = (int)priorityComponent.ActionId;
                ref var ownerComponent = ref _ownerPool.Get(priorityEntity);
                if (!ownerComponent.Value.Unpack(_world, out var ownerEntity))
                {
                    continue;
                }
                
                ref var results = ref _targetSelectionAspect.TargetSelectionResult.Get(ownerEntity);
                var result = results.Results[actionId];
                if (result.Count < 1)
                {
                    continue;
                }
                
                var priorityTarget = (EcsPackedEntity)default;
                var priority = -1;
                for (int i = 0; i < result.Count; i++)
                {
                    var entity = result.Values[i];  
                    if (!entity.Unpack(_world, out var targetEntity))
                    {
                        continue;
                    }

                    ref var categoryIdComponent = ref _categoryPool.Get(targetEntity);
                    var targetPriority = priorityComponent.Value[categoryIdComponent.Value];
                    if (targetPriority > priority)
                    {
                        priority = targetPriority;
                        priorityTarget = entity;
                    }
                }

                ref var prioritizedTargetComponent = ref _prioritizerAspect.Target.Get(ownerEntity);
                prioritizedTargetComponent.Value[actionId] = priorityTarget;
            }
        }
    }
}