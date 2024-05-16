namespace Ability.SubFeatures.AbilityAnimation.Systems
{
    using System;
    using Components;
    using Game.Ecs.Ability.Aspects;
    using Game.Ecs.Ability.Common.Components;
    using Game.Ecs.Ability.SubFeatures.AbilityAnimation.Data;
    using Game.Ecs.Ability.Tools;
    using Game.Ecs.Animation.Components;
    using Game.Ecs.Animation.Data;
    using Game.Ecs.Characteristics.AttackSpeed.Components;
    using Game.Ecs.Characteristics.Base.Components;
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
        private EcsPool<AnimatorComponent> _animatorPool;
        private AbilityTools _abilityTool;
        private AnimationsIdsMap _animatorGlobalMap;

        public UpdateAttackAnimationSpeedSystem(AnimatorParametersMap parametersMap)
        {
            _parametersMap = parametersMap;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AnimatorComponent>()
                .Inc<CharacteristicChangedComponent<AttackSpeedComponent>>()
                .Inc<AbilityInHandLinkComponent>()
                .Inc<AttackAbilityIdComponent>()
                .Inc<AnimationsLengthMapComponent>()
                .End();
            _abilityFilter = _world.Filter<AbilityCooldownComponent>()
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
                
                ref var baseCooldownComponent = ref _abilityAspect.BaseCooldown.Get(abilityEntity);
                ref var cooldownComponent = ref _abilityAspect.Cooldown.Get(abilityEntity);
                ref var abilityAnimationIDComponent = ref _world.GetOrAddComponent<TriggeredAnimationIdComponent>(abilityEntity);
                
                var animationId = abilityAnimationIDComponent.animationId;
                ref var animationsLengthMap = ref _world.GetComponent<AnimationsLengthMapComponent>(ownerEntity);
                if (!animationsLengthMap.value.TryGetValue((AnimationClipId)animationId, out var animationLength))
                {
                    GameLog.LogError("UpdateAttackAnimationSystem: Animation length not found in map");
                    continue;
                }
                if(animationLength < cooldownComponent.Value) continue;
                
                //todo сейчас использовал старый кулдаун и базовый кулдаун. Не уверен что это будет работать.
                //todo если не будет работать, то нужно будет использовать мой новый AbilityCooldownValues
                //todo ref var abilityCooldownComponent = ref _abilityAspect.AbilityCooldownValues.Get(abilityEntity);
                ref var animatorComponent = ref _animatorPool.Get(ownerEntity);
                Debug.Log(
                    $"Cooldown component value is {cooldownComponent.Value} and base cooldown component value is {baseCooldownComponent.Value}");
                Debug.Log(
                    $"New attack speed animator parameter was set from {animatorComponent.Value.GetFloat(_parametersMap.attackSpeed)} to {cooldownComponent.Value / baseCooldownComponent.Value}");
                var ratio = animationLength / cooldownComponent.Value;
                animatorComponent.Value.SetFloat(_parametersMap.attackSpeed, ratio);
            }
        }
    }
}