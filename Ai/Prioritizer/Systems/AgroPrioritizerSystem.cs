namespace Ai.Ai.Variants.Prioritizer.Systems
{
    using System;
    using System.Linq;
    using Aspects;
    using Components;
    using Game.Ecs.Ai.Targeting.Aspects;
    using Game.Ecs.TargetSelection.Aspects;
    using Game.Ecs.TargetSelection.Components;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine;
    using UnityEngine.Pool;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AgroPrioritizerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        
        private TargetingAspect _targetingAspect;
        private TargetSelectionAspect _targetSelectionAspect;
        private PrioritizerAspect _prioritizerAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<PrioritizerComponent>()
                .Inc<TargetsSelectionResultComponent>()
                .Exc<AgroComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var priorityEntity in _filter)
            {
                ref var priorityComponent = ref _prioritizerAspect.Priority.Get(priorityEntity);
                ref var results = ref _targetSelectionAspect.TargetSelectionResult.Get(priorityEntity);
                
                /*var priorityTarget = (int)default;
                if (_targetingAspect.AttackEventTarget.Has(priorityEntity))
                {
                    ref var attackEventTargetComponent = ref _targetingAspect.AttackEventTarget.Get(priorityEntity);
                    if (attackEventTargetComponent.Value.Unpack(_world, out var attackEventTargetEntity))
                    {
                        priorityTarget = attackEventTargetEntity;
                    }
                }
                else if (results.Count < 1)
                {
                    continue;
                }*/
                
                var priorityTarget = (int)default;
                for (int i = 0; i < results.Count; i++)
                {
                    var result = results.Values[i];
                    if (!result.Unpack(_world, out var targetEntity))
                    {
                        continue;
                    }

                    bool conditionsPassed = true;
                    foreach (var condition in priorityComponent.AgroConditions)
                    {
                        if (!condition.Check(_world, priorityEntity, targetEntity))
                        {
                            conditionsPassed = false;
                            break;
                        }
                    }

                    if (!conditionsPassed)
                    {
                        continue;
                    }

                    if (priorityTarget == default)
                    {
                        priorityTarget = targetEntity;
                        continue;
                    }

                    foreach (var comparer in priorityComponent.Comparers)
                    {
                        var compareValue = comparer.Compare(_world, priorityEntity, priorityTarget, targetEntity);
                        if (compareValue == 0)
                        {
                            continue;
                        }
                        
                        if (compareValue == 1)
                        {
                            priorityTarget = targetEntity;
                        }

                        break;
                    }
                }

                if (priorityTarget != default)
                {
                    ref var chaseTargetComponent = ref _prioritizerAspect.Chase.Add(priorityEntity);
                    chaseTargetComponent.Value = priorityTarget.PackedEntity(_world);
                    _prioritizerAspect.Agro.TryAdd(priorityEntity);
                }
            }
        }
    }
}