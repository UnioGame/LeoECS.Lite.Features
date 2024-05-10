namespace Game.Ecs.GameAi.MoveToTarget.Systems
{
    using System;
    using Cysharp.Threading.Tasks;
    using Game.Ecs.AI.Components;
    using Game.Ecs.AI.Systems;
    using Components;
    using Core.Death.Components;
    using Ai.Ai.Variants.MoveToTarget.Aspects;
    using Ai.Ai.Variants.Prioritizer.Aspects;
    using Ai.Ai.Variants.Prioritizer.Components;
    using AI.Data;
    using TargetSelection;
    using TargetSelection.Aspects;
    using UniGame.LeoEcs.Shared.Components;
    using Leopotam.EcsLite;
    using Movement.Components;
    using TargetSelection.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class MoveToTargetPlannerSystem : BasePlannerSystem<MoveToTargetActionComponent>, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        private AiMoveAspect _moveAspect;
        private PrioritizerAspect _prioritizerAspect;

        private EcsPool<TransformPositionComponent> _positionPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AiAgentComponent>()
                .Inc<MoveToTargetPlannerComponent>()
                .Inc<PrioritizedTargetComponent>()
                .Exc<ImmobilityComponent>()
                .Exc<DisabledComponent>()
                .End();
        }

        public override void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var moveToTargetPlannerComponent = ref _moveAspect.Planner.Get(entity);
                int priority = AiConstants.PriorityNever;
                ref var resultComponent = ref _prioritizerAspect.Target.Get(entity);
                var result = resultComponent.Value[(int)_actionId];
                if (result.Unpack(_world, out var targetEntity))
                {
                    ref var positionComponent = ref _positionPool.Get(targetEntity);
                    ref var moveToTargetActionComponent = ref _moveAspect.MoveAction.GetOrAddComponent(entity);
                    moveToTargetActionComponent.Position = positionComponent.Position;
                    priority = moveToTargetPlannerComponent.PlannerData.Priority;
                }
                
                ApplyPlanningResult(systems, entity, new AiPlannerData
                {
                    Priority = priority
                });
            }
        }

        protected override UniTask OnInitialize(IEcsSystems systems)
        {
            return UniTask.CompletedTask;
        }
    }
}