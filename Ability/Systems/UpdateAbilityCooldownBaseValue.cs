namespace Game.Ecs.Ability.Systems
{
    using System;
    using Aspects;
    using Common.Components;
    using Components.Requests;
    using Leopotam.EcsLite;
    using Tools;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// set new value of cooldown of ability
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class UpdateAbilityCooldownBaseValue : IEcsInitSystem, IEcsRunSystem
    {
        private AbilityTools _abilityTools;
        private EcsWorld _world;
        private EcsFilter _filter;
        private AbilityAspect _abilityAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            
            _filter = _world
                .Filter<SetAbilityBaseCooldownSelfRequest>()
                .Inc<AbilityMapComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var requestComponent = ref _abilityAspect.SetBaseCooldown.Get(entity);
                var slot = requestComponent.AbilitySlot;
                var cooldown = requestComponent.Cooldown;
                
                var abilityEntity = _abilityTools.TryGetAbility( entity, slot);
                if(abilityEntity < 0) continue;

                ref var baseCooldownComponent = ref _abilityAspect.BaseCooldown.Get(abilityEntity);
                baseCooldownComponent.Value = cooldown;

                _abilityAspect.RecalculateCooldown.GetOrAddComponent(abilityEntity);
                _abilityAspect.SetBaseCooldown.Del(entity);
            }
        }
    }
}