namespace Game.Ecs.Ability.Common.Systems
{
    using System;
    using Characteristics.Cooldown.Components;
    using Characteristics.Duration.Components;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Timer.Components;
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
    public sealed class EvaluateAbilitySystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<DurationComponent> _durationPool;
        private EcsPool<CooldownComponent> _cooldownPool;
        private EcsPool<AbilityEvaluationComponent> _evaluationPool;
        private EcsPool<CompleteAbilitySelfRequest> _completeAbilityPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<AbilityUsingComponent>()
                .Inc<DurationComponent>()
                .Inc<CooldownComponent>()
                .Exc<AbilityPauseComponent>()
                .End();
            
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var durationComponent = ref _durationPool.Get(entity);
                ref var cooldownComponent = ref _cooldownPool.Get(entity);

                ref var evaluationComponent = ref _evaluationPool.GetOrAddComponent(entity);
                
                var currentTime = evaluationComponent.EvaluateTime;
                var delta = durationComponent.Value - currentTime;
                
                if (delta > 0.0f && !Mathf.Approximately(delta, 0.0f))
                {
                    evaluationComponent.EvaluateTime += Time.deltaTime * Mathf.Max(1.0f, durationComponent.Value / cooldownComponent.Value);
                    continue;
                }
                
                _completeAbilityPool.Add(entity);
            }
        }
    }
}