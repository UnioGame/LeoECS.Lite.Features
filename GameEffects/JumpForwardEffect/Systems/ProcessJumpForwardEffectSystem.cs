namespace Game.Ecs.GameEffects.JumpForwardEffect.Systems
{
    using Component;
    using DG.Tweening;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;

    /// <summary>
    /// Handler for jump forward effect
    /// </summary>

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public sealed class ProcessJumpForwardEffectSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<JumpForwardEffectEvaluationComponent> _jumpForwardEvaluationComponentPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter= _world.Filter<JumpForwardEffectEvaluationComponent>()
                .End();
            _jumpForwardEvaluationComponentPool = _world.GetPool<JumpForwardEffectEvaluationComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var jumpForwardEvaluationComponent = ref _jumpForwardEvaluationComponentPool.Get(entity);
                var sourceTransform = jumpForwardEvaluationComponent.SourceTransform;
                sourceTransform.DOMove(jumpForwardEvaluationComponent.EndPosition, jumpForwardEvaluationComponent.Duration)
                    .SetEase(Ease.Linear);
                _world.TryRemoveComponent<JumpForwardEffectEvaluationComponent>(entity);
            }
        }
    }
}