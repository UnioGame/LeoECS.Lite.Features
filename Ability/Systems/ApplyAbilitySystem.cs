namespace Game.Ecs.Ability.Common.Systems
{
    using System;
    using Aspects;
    using Characteristics.Cooldown.Components;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Timer.Components;
    using Time.Service;
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
    public sealed class ApplyAbilitySystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private AbilityAspect _abilityAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<AbilityValidationSelfRequest>()
                .Inc<CooldownStateComponent>()
                .Inc<ActiveAbilityComponent>()
                .Exc<AbilityUsingComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var abilityCooldownComponent = ref _abilityAspect.CooldownState.Get(entity);
                abilityCooldownComponent.LastTime = GameTime.Time;
                
                _abilityAspect.AbilityUsing.Add(entity);
                _abilityAspect.UsingEvent.Add(entity);
            }
        }
    }
}