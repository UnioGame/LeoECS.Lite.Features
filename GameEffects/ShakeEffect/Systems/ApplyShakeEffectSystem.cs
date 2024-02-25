namespace Game.Ecs.GameEffects.ShakeEffect.Systems
{
    using System;
    using Components;
    using DG.Tweening;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine;

    /// <summary>
    /// start shake effect
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ApplyShakeEffectSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _shakeFilter;

        private EcsPool<ShakeEffectDataComponent> _dataPool;
        private EcsPool<ShakeEffectTargetComponent> _targetPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _shakeFilter = _world
                .Filter<ShakeEffectDataComponent>()
                .Inc<ShakeEffectTargetComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var shakeEntity in _shakeFilter)
            {
                ref var dataComponent = ref _dataPool.Get(shakeEntity);
                ref var targetComponent = ref _targetPool.Get(shakeEntity);
                
                var transform = targetComponent.Value;
                
                transform.DOKill(true);
                
                var tween = transform.DOShakePosition(
                        dataComponent.Duration,
                        dataComponent.Strength,
                        dataComponent.Vibrato,
                        dataComponent.Random,
                        dataComponent.Snapping,
                        dataComponent.FadeOut);
                
                tween.SetAutoKill(true)
                    .OnKill(() => transform.localPosition = Vector3.zero)
                    .Play();
            }
        }
    }
}