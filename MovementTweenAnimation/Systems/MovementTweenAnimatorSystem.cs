namespace Movement.Systems.NavMesh.Animation
{
    using System;
    using Animations.Animator.Data;
    using Animations.Animatror.Aspects;
    using Game.Ecs.Characteristics.Speed.Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Movement.Components;
    using Leopotam.EcsLite;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class MovementTweenAnimatorSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<SpeedComponent> _speedPool;
        private EcsPool<AnimatorComponent> _animatorPool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<MovementAnimationInfoComponent> _animationInfoPool;

        private AnimationsAnimatorAspect _animatorAspect;
        
        private readonly AnimationClipId _idleClipId;
        private readonly AnimationClipId _walkClipId;
        
        private AnimatorsMap _animatorsMap;

        public MovementTweenAnimatorSystem(AnimationClipId idleClipId, AnimationClipId walkClipId)
        {
            _idleClipId = idleClipId;
            _walkClipId = walkClipId;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<AnimatorComponent>()
                .Inc<TransformComponent>()
                .Inc<MovementAgentComponent>()
                .Inc<SpeedComponent>()
                .Inc<MovementAnimationInfoComponent>()
                .Exc<ImmobilityComponent>()
                .Exc<PrepareToDeathComponent>()
                .End();
            
            _animatorsMap = _world.GetGlobal<AnimatorsMap>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var speedComponent = ref _speedPool.Get(entity);
                var speed = speedComponent.Value;
                
                ref var transform = ref _transformPool.Get(entity);
                ref var animationInfo = ref _animationInfoPool.Get(entity);
                ref var animatorComponent = ref _animatorPool.Get(entity);
                
                var animator = animatorComponent.Value;
                var controller = animator.runtimeAnimatorController;
                if(controller == null) continue;
                if (animator == null || !animator.isActiveAndEnabled) continue;

                var speedValue = animationInfo.RunSpeed != 0
                    ? speed / animationInfo.RunSpeed
                    : 1;
            
                animator.speed = speedValue > animationInfo.MaxRunSpeed
                    ? speedValue / animationInfo.MaxRunSpeed
                    : 1.0f;
                
                if (!_animatorsMap.data.TryGetValue(_idleClipId, out var idleStateData)
                    || !_animatorsMap.data.TryGetValue(_walkClipId, out var movementStateData))
                {
                    GameLog.LogError("AnimatorMapComponent has no such states");
                    continue;
                }
                
                if (speed != 0)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash !=  movementStateData.stateNameHash)
                    {
                        animator.Play(movementStateData.stateNameHash);
                    }
                }
                else
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash !=  idleStateData.stateNameHash)
                    {
                        animator.Play(idleStateData.stateNameHash);
                    }
                }
            }
        }
    }
}