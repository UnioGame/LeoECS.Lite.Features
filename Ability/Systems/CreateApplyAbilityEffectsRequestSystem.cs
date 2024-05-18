namespace Game.Ecs.Ability.Common.Systems
{
    using System;
    using Components;
    using global::Ability.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class CreateApplyAbilityEffectsRequestSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<AbilityEffectMilestonesComponent> _milestonesPool;
        private EcsPool<AbilityEvaluationComponent> _evaluationPool;
        private EcsPool<ApplyAbilityEffectsSelfRequest> _requestPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AbilityEffectMilestonesComponent>()
                .Inc<AbilityEvaluationComponent>()
                .Inc<AbilityUsingComponent>()
                .Exc<AbilityAwaitAnimationTriggerComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var milestones = ref _milestonesPool.Get(entity);
                ref var evaluation = ref _evaluationPool.Get(entity);

                var evaluationTime = evaluation.EvaluateTime;
                for (var i = 0; i < milestones.Milestones.Length; i++)
                {
                    ref var milestone = ref milestones.Milestones[i];
                    if (milestone.IsApplied)
                        continue;

                    if (milestone.Time > evaluationTime && !Mathf.Approximately(milestone.Time, evaluationTime))
                        continue;

                    milestone.IsApplied = true;
                    _requestPool.Add(entity);
                }
            }
        }
    }
}