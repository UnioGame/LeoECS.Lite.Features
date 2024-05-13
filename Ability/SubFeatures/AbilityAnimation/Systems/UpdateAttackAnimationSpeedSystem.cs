namespace Ability.SubFeatures.AbilityAnimation.Systems
{
    using System;
    using System.Linq;
    using Components;
    using Game.Ecs.Ability.Aspects;
    using Game.Ecs.Ability.Common.Components;
    using Game.Ecs.Ability.SubFeatures.AbilityAnimation.Data;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
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

        public UpdateAttackAnimationSpeedSystem(AnimatorParametersMap parametersMap)
        {
            _parametersMap = parametersMap;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AnimatorComponent>()
                .Inc<AbilityInHandLinkComponent>()
                .End();
             _abilityFilter = _world.Filter<AbilityCooldownComponent>()
                .Inc<CooldownComponent>()
                .Inc<OwnerComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var abilityEntity in _abilityFilter)
            {
                ref var ownerComponent = ref _abilityAspect.Owner.Get(abilityEntity);
                if(!ownerComponent.Value.Unpack(_world, out var ownerEntity)) continue;

                ref var baseCooldownComponent = ref _abilityAspect.BaseCooldown.Get(abilityEntity);
                ref var cooldownComponent = ref _abilityAspect.Cooldown.Get(abilityEntity);
                
                //todo сейчас использовал старый кулдаун и базовый кулдаун. Не уверен что это будет работать.
                //todo если не будет работать, то нужно будет использовать мой новый AbilityCooldownValues
                // ref var abilityCooldownComponent = ref _abilityAspect.AbilityCooldownValues.Get(abilityEntity);
                ref var animatorComponent = ref _animatorPool.Get(ownerEntity);
                animatorComponent.Value.SetFloat(_parametersMap.attackSpeed,cooldownComponent.Value/baseCooldownComponent.Value);
            }
        }
    }
}