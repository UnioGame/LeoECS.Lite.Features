namespace Ability.SubFeatures.AbilityAnimation.Systems
{
    using System;
    using System.Collections.Generic;
    using Animations.Animator.Data;
    using Animations.Animatror.Components;
    using Game.Ecs.Ability.Aspects;
    using Game.Ecs.Ability.Common.Components;
    using Game.Ecs.Ability.SubFeatures.AbilityAnimation.Aspects;
    using Game.Ecs.Ability.SubFeatures.AbilityAnimation.Components;
    using Game.Ecs.Ability.SubFeatures.AbilityAnimation.Data;
    using Game.Ecs.Ability.Tools;
    using Game.Ecs.AbilityInventory.Components;
    using Game.Ecs.Characteristics.AttackSpeed.Components;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsLite;
    using UniCore.Runtime.ProfilerTools;
    using UnityEngine;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Timer.Components;

    /// <summary>
    /// Ускоряем анимацию атаки в зависимости от скорости атаки
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class UpdateAttackAnimationSpeedSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly AnimatorParametersMap _parametersMap;
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _abilityFilter;
        private AbilityAspect _abilityAspect;
        private AbilityAnimationAspect _animationAspect;
        private AbilityTools _abilityTool;
        private AnimationsIdsMap _animatorGlobalMap;
        private Stack<EcsPackedEntity> _stack = new Stack<EcsPackedEntity>();

        public UpdateAttackAnimationSpeedSystem(AnimatorParametersMap parametersMap)
        {
            _parametersMap = parametersMap;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AnimatorComponent>()
                .Inc<RecalculateAnimationAttackSpeedSelfRequest>()
                .Inc<AbilityInHandLinkComponent>()
                .Inc<AttackAbilityIdComponent>()
                .Inc<AnimationsLengthMapComponent>()
                .End();
            _abilityFilter = _world.Filter<AbilityIdComponent>()
                .Inc<AbilityInventoryCompleteComponent>()
                .Inc<CooldownComponent>()
                .Inc<OwnerComponent>()
                .End();
            _abilityTool = _world.GetGlobal<AbilityTools>();
            _animatorGlobalMap = _world.GetGlobal<AnimationsIdsMap>();
        }
        public void Run(IEcsSystems systems)
        {
            foreach (var ownerEntity in _filter)
            {
                ref var attackAbilityIdComponent = ref _world.GetComponent<AttackAbilityIdComponent>(ownerEntity);
                var abilityEntity = _abilityTool.GetAbilityBySlot(ownerEntity, attackAbilityIdComponent.Value);
                
                if(abilityEntity == -1) continue;
                
                ref var cooldownComponent = ref _abilityAspect.Cooldown.Get(abilityEntity);
                ref var abilityAnimationIDComponent = ref _animationAspect.ClipId.Get(abilityEntity);
                
                var animationId = abilityAnimationIDComponent.animationId;
                ref var animationsLengthMap = ref _animationAspect.AnimationsLengthMap.Get(ownerEntity);
                if (!animationsLengthMap.value.TryGetValue((AnimationClipId)animationId, out var animationLength))
                {
                    continue;
                }
                if(animationLength < cooldownComponent.Value)
                {
                    _animationAspect.RecalculateAttackSpeed.Del(ownerEntity);
                    continue;
                }
                ref var animatorComponent = ref _animationAspect.Animator.Get(ownerEntity);
                var ratio = Math.Clamp(animationLength / cooldownComponent.Value,0,100) ;
                
                animatorComponent.Value.SetFloat(_parametersMap.attackSpeed, ratio);
                
                _animationAspect.RecalculateAttackSpeed.Del(ownerEntity);
            }
        }
    }
}