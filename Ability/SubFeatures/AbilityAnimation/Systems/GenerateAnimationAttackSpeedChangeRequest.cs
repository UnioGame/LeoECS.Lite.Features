namespace Ability.SubFeatures.AbilityAnimation.Systems
{
    using System;
    using Animations.Animatror.Components;
    using Game.Ecs.Ability.SubFeatures.AbilityAnimation.Aspects;
    using Game.Ecs.Ability.SubFeatures.AbilityAnimation.Components;
    using Game.Ecs.Characteristics.AttackSpeed.Components;
    using Game.Ecs.Characteristics.Base.Components;
    using Game.Ecs.Characteristics.Base.Components.Requests;
    using Leopotam.EcsLite;
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
        private EcsFilter _createFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<CharacteristicChangedComponent<AttackSpeedComponent>>()
                .Inc<AnimatorComponent>()
                .Inc<AnimationsLengthMapComponent>()
                .Exc<RecalculateAnimationAttackSpeedSelfRequest>()
                .End();

            _createFilter = _world.Filter<CreateCharacteristicRequest<AttackSpeedComponent>>()
                .Inc<AnimatorComponent>()
                .Inc<AnimationsLengthMapComponent>()
                .Exc<RecalculateAnimationAttackSpeedSelfRequest>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var ownerEntity in _filter)
            {
                _animationAspect.RecalculateAttackSpeed.Add(ownerEntity);
            }

            foreach (var ownerEntity in _createFilter)
            {
                _animationAspect.RecalculateAttackSpeed.Add(ownerEntity);
            }
        }
    }
}