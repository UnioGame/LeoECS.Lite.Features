namespace Ability.SubFeatures.AbilityAnimation.Systems
{
    using System;
    using System.Linq;
    using Game.Ecs.Ability.Common.Components;
    using Game.Ecs.Ability.SubFeatures.AbilityAnimation.Aspects;
    using Game.Ecs.Ability.SubFeatures.AbilityAnimation.Components;
    using Game.Ecs.Animation.Components;
    using Game.Ecs.Characteristics.AttackSpeed.Components;
    using Game.Ecs.Characteristics.Base.Components;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;

    /// <summary>
    /// Ловим запрос на изменение скорости атаки, вешаем реквест на изменение скорости анимации
    /// который обработается в <seealso cref="UpdateAttackAnimationSpeedSystem"/>
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class GenerateAnimationAttackSpeedChangeRequest : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private AbilityAnimationAspect _animationAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AnimatorComponent>()
                .Exc<RecalculateAnimationAttackSpeedSelfRequest>()
                .Inc<CharacteristicChangedComponent<AttackSpeedComponent>>()
                .Inc<AbilityInHandLinkComponent>()
                .Inc<AttackAbilityIdComponent>()
                .Inc<AnimationsLengthMapComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var ownerEntity in _filter)
            {
                _animationAspect.RecalculateAttackSpeed.Add(ownerEntity);
            }
        }
    }
}