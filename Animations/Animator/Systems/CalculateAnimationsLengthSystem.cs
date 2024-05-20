namespace Animations.Animatror.Systems
{
    using System;
    using System.Collections.Generic;
    using Animator.Data;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// Расчет длины анимаций и добавление мапы
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CalculateAnimationsLengthSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private AnimatorsMap _map;
        private List<KeyValuePair<AnimationClip, AnimationClip>> _overrides = new();
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AnimatorComponent>()
                .Exc<AnimationsLengthMapComponent>()
                .End();
            _map = _world.GetGlobal<AnimatorsMap>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var animatorComponent = ref _world.GetPool<AnimatorComponent>().Get(entity);
                
                var overrideController = animatorComponent.Value.runtimeAnimatorController as AnimatorOverrideController;
                if (overrideController != null) overrideController.GetOverrides(_overrides);
                
                ref var animationsLengthMap = ref _world.GetPool<AnimationsLengthMapComponent>().Add(entity);
                foreach (var overridesPair in _overrides)
                {
                    if(overridesPair.Value == null)
                        continue;
                    foreach (var basePair in _map.data)
                    {
                        if(overridesPair.Key.name != basePair.Value.clipName)
                            continue;
                        animationsLengthMap.value.Add(basePair.Key,overridesPair.Value.length);
                    }
                }
            }
        }
    }
}