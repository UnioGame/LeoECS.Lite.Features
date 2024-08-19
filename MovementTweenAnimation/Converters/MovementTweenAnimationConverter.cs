namespace MovementTweenAnimation.Converters
{
    using System;
    using Components;
    using Game.Runtime.Tools.Tweens;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// Converting a GameObject into a MovementTweenAnimationComponent.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class MovementTweenAnimationConverter : GameObjectConverter
    {
        [SerializeField]
        private ScaleTween movementTween;
        protected override void OnApply(GameObject target, EcsWorld world, int entity)
        {
            ref var movementTweenAnimationComponent = ref world.AddComponent<MovementTweenAnimationComponent>(entity);
            movementTweenAnimationComponent.MoveTween = movementTween;
        }
    }
}