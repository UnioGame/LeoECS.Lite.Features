namespace Ability.Systems
{
    using System;
    using Components;
    using Game.Ecs.Ability.Aspects;
    using Game.Ecs.Ability.Common.Components;
    using Game.Ecs.Ability.SubFeatures.Target.Aspects;
    using Game.Ecs.Animations.Components.Requests;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Система ожидания триггера анимации. как только триггер срабатывает, система создает реквест на применение эффекта способности
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AbilityAwaitAnimationTriggerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<ApplyAbilityEffectsSelfRequest> _requestPool;
        private EcsPool<AbilityAwaitAnimationTriggerComponent> _awaitPool;
        private TargetAbilityAspect _targetAspect;
        private AbilityAspect _abilityAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AbilityEvaluationComponent>()
                .Inc<AbilityUsingComponent>()
                .Inc<AbilityAwaitAnimationTriggerComponent>()
                .Inc<AnimationTriggerRequest>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var abilityEntity in _filter)
            {
                _world.RemoveComponent<AnimationTriggerRequest>(abilityEntity);
                ref var selectedTargetsComponent = ref _targetAspect.SelectedTargets.Get(abilityEntity);

                //check if target exists
                for (var index = 0; index < selectedTargetsComponent.Count; index++)
                {
                    var packedEntity = selectedTargetsComponent.Entities[index];
                    if (!packedEntity.Unpack(_world, out var targetEntity))
                    {
                        _abilityAspect.CompleteAbility.Add(abilityEntity);
                    }
                }

                _requestPool.Add(abilityEntity);
            }
        }
    }
}