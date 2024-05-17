namespace Game.Ecs.Ai.Targeting.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using TargetSelection.Aspects;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AttackEventTargetCountdownSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private TargetingAspect _targetingAspect;
        private TargetSelectionAspect _targetSelectionAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<AttackEventTargetComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            /*foreach (var attackEventTargetEntity in _filter)
            {
                ref var attackEventTargetComponent = ref _targetingAspect.AttackEventTarget.Get(attackEventTargetEntity);
                attackEventTargetComponent.Duration -= Time.deltaTime;

                if (attackEventTargetComponent.Duration <= 0f)
                {
                    _targetingAspect.AttackEventTarget.TryRemove(attackEventTargetEntity);
                }
            }*/
        }
    }
}