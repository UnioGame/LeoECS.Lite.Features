namespace Game.Ecs.GameEffects.JumpForwardEffect.Systems
{
    using System;
    using Component;
    using Core.Components;
    using Effects.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;
    using UnityEngine.AI;

    /// <summary>
    /// Prepares data for jump forward effect
    /// </summary>

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public sealed class ApplyJumpForwardEffectSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<JumpForwardEffectEvaluationComponent> _jumpForwardEvaluationComponentPool;
        private EcsPool<JumpForwardEffectComponent> _jumpForwardComponentPool;
        private EcsPool<JumpForwardEffectRequest> _jumpForwardRequestPool;
        private EcsPool<OwnerComponent> _ownerComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<JumpForwardEffectRequest>()
                .End();
            
            _jumpForwardRequestPool = _world.GetPool<JumpForwardEffectRequest>();
            _jumpForwardEvaluationComponentPool = _world.GetPool<JumpForwardEffectEvaluationComponent>();
            _jumpForwardComponentPool = _world.GetPool<JumpForwardEffectComponent>();
            _ownerComponentPool = _world.GetPool<OwnerComponent>();
            _transformComponentPool = _world.GetPool<TransformComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var jumpForwardRequest = ref _jumpForwardRequestPool.Get(entity);
                if (!jumpForwardRequest.Source.Unpack(_world, out var abilityEntity))
                    continue;
                
                ref var jumpForwardComponent = ref _jumpForwardComponentPool.Get(abilityEntity);
                ref var ownerComponent = ref _ownerComponentPool.Get(abilityEntity);
                
                if (!ownerComponent.Value.Unpack(_world, out var root)) continue;
                
                ref var jumpForwardEffectEvaluationComponent = ref _jumpForwardEvaluationComponentPool.GetOrAddComponent(root);
                ref var transformComponent = ref _transformComponentPool.Get(root);
                var rootTransform = transformComponent.Value;
                jumpForwardEffectEvaluationComponent.SourceTransform = rootTransform;
                var position = rootTransform.position;
                var rawEndPosition = position + (rootTransform.forward * jumpForwardComponent.Distance);
                jumpForwardEffectEvaluationComponent.EndPosition = CalculateEndPosition(rawEndPosition, 
                    jumpForwardComponent.Distance / 2, position);
                
                var distance  = Vector3.Distance(position, jumpForwardEffectEvaluationComponent.EndPosition);
                var percent = distance / jumpForwardComponent.Distance;
                jumpForwardEffectEvaluationComponent.Duration = jumpForwardComponent.Duration * percent;
            }
        }
        
        private Vector3 CalculateEndPosition(Vector3 rawEndPosition, float distance, Vector3 defaultPos)
        {
            return NavMesh.SamplePosition(rawEndPosition, out var hit, distance, NavMesh.AllAreas) ? hit.position : defaultPos;
        }
    }
}