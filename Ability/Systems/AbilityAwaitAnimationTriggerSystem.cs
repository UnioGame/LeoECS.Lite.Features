namespace Ability.Systems
{
    using System;
    using System.Linq;
    using Components;
    using Game.Ecs.Ability.Common.Components;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Система ожидания триггера анимации. как только триггер срабатывает, система создает реквест на применение эффекта способности
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AbilityAwaitAnimationTriggerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<ApplyAbilityEffectsSelfRequest> _requestPool;
        private EcsPool<AbilityAwaitAnimationTriggerComponent> _awaitPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AbilityEvaluationComponent>()
                .Inc<AbilityUsingComponent>()
                .Inc<AbilityAwaitAnimationTriggerComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var abilityEntity in _filter)
            {
                _requestPool.Add(abilityEntity);
                _awaitPool.Del(abilityEntity);
            }
        }
    }
}