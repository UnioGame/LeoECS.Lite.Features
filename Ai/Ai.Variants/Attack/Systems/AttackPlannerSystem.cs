namespace Ai.Ai.Variants.Attack.Systems
{
    using System;
    using System.Linq;
    using Aspects;
    using Components;
    using Game.Ecs.AI.Components;
    using Game.Ecs.AI.Data;
    using Game.Ecs.AI.Systems;
    using Game.Ecs.Characteristics.CriticalChance.Components;
    using Game.Ecs.Core.Death.Components;
    using Game.Ecs.TargetSelection.Aspects;
    using Game.Ecs.TargetSelection.Components;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using Unity.Mathematics;
    using UnityEngine;
    using UnityEngine.Pool;

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
    public class AttackPlannerSystem : BasePlannerSystem<AttackActionComponent>, IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        
        private AiAttackAspect _attackAspect;
        private TargetSelectionAspect _targetSelectionAspect;
        private EcsPool<TransformPositionComponent> _positionPool;
        private EcsPool<AttackRangeComponent> _attackRangePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AiAgentComponent>()
                .Inc<AttackPlannerComponent>()
                .Inc<AttackRangeComponent>()
                .Inc<TargetsSelectionResultComponent>()
                .Exc<DisabledComponent>()
                .End();
        }

        public override void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var resultComponent = ref _targetSelectionAspect.TargetSelectionResult.Get(entity);
                ref var attackRangeComponent = ref _attackRangePool.Get(entity);
                ref var attackPlannerComponent = ref _attackAspect.Planner.Get(entity);
                int priority = AiConstants.PriorityNever;

                var minDistanceSqr = float.MaxValue;
                var minDistanceTarget = (EcsPackedEntity)default;
                if (resultComponent.Results.TryGetValue((int)_actionId, out var result) && result.Ready && result.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        var packedTarget = result.Values[i];
                        if (!packedTarget.Unpack(_world, out var targetEntity))
                        {
                            continue;
                        }
                        
                        ref var targetPositionComponent = ref _positionPool.Get(targetEntity);
                        ref var selfPositionComponent = ref _positionPool.Get(entity);

                        var distanceSqr = math.distancesq(targetPositionComponent.Position, selfPositionComponent.Position);
                        if (distanceSqr < minDistanceSqr)
                        {
                            minDistanceSqr = distanceSqr;
                            minDistanceTarget = packedTarget;
                        }
                    }
                }

                if (!minDistanceTarget.EqualsTo(default))
                {
                    ref var attackComponent = ref _attackAspect.Action.GetOrAddComponent(entity);
                    attackComponent.Value = minDistanceTarget;
                    priority = attackPlannerComponent.PlannerData.Priority;
                }
                
                ApplyPlanningResult(systems, entity, new AiPlannerData
                {
                    Priority = priority
                });
            }
        }
    }
}