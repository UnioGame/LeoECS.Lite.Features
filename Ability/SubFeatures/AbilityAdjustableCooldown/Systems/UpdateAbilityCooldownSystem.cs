namespace Ability.SubFeatures.AbilityAdjustableCooldown.Systems
{
    using System;
    using Game.Ecs.Ability.Aspects;
    using Game.Ecs.Ability.Common.Components;
    using Game.Ecs.Ability.Tools;
    using Game.Ecs.Characteristics.AttackSpeed.Components;
    using Game.Ecs.Characteristics.Base.Components;
    using Game.Ecs.Cooldown;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Timer.Components;

    /// <summary>
    /// обновляем базовое значение кулдауна способности на значение скорости атаки
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class UpdateAbilityCooldownSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<CharacteristicComponent<AttackSpeedComponent>> _characteristicPool;
        private EcsPool<AttackSpeedComponent> _attackSpeed;
        private EcsPool<AttackSpeedCooldownTypeComponent> _cooldownType;
        private EcsPool<AttackAbilityIdComponent> _attackAbilityId;
        private AbilityTools _abilityTool;
        private AbilityAspect _abilityAspect;
        private EcsPool<AbilityIdComponent> _abilityFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            //todo разкоментировать компонент CharacteristicChangedComponent<AttackSpeedComponent>
            _filter = _world
                .Filter<CharacteristicComponent<AttackSpeedComponent>>()
                // .Inc<CharacteristicChangedComponent<AttackSpeedComponent>>()
                .Inc<AttackSpeedComponent>()
                .Inc<AttackSpeedCooldownTypeComponent>()
                .End();
            
            

            _characteristicPool = _world.GetPool<CharacteristicComponent<AttackSpeedComponent>>();
            _attackSpeed = _world.GetPool<AttackSpeedComponent>();
            _cooldownType = _world.GetPool<AttackSpeedCooldownTypeComponent>();
            _attackAbilityId = _world.GetPool<AttackAbilityIdComponent>();
            
            _abilityTool = _world.GetGlobal<AbilityTools>();
            _abilityFilter = _world.GetPool<AbilityIdComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var slotIdComponent = ref _attackAbilityId.Get(entity);
                ref var inHandAbility = ref _world.GetComponent<AbilityInHandLinkComponent>(entity);
                
                //вынужденная проверка в лоб, пока не почним AbilityMapComponent
                if(!inHandAbility.AbilityEntity.Unpack(_world, out var abilityEntity))
                    continue;
                ref var ablitySlotId = ref _abilityAspect.AbilitySlot.Get(abilityEntity);
                
                if(ablitySlotId.SlotType != slotIdComponent.Value) continue;
                
                //todo  не работает потому что не заполняется AbilityMapComponent
                // var abilityEntity = _abilitytool.GetAbilityBySlot(entity, slotIdComponent.Value);
                // if (abilityEntity < 0) continue;
                
                ref var attackSpeed = ref _characteristicPool.Get(entity);
                ref var cooldownTypeComponent = ref _cooldownType.Get(entity);
                
                ref var cooldownComponent = ref _abilityAspect.Cooldown.GetOrAddComponent(abilityEntity);
                
                cooldownComponent.Value = cooldownTypeComponent.Value switch
                {
                    CooldownType.Cooldown => attackSpeed.Value,
                    CooldownType.Speed => 1f / attackSpeed.Value,
                    _ => throw new ArgumentOutOfRangeException(nameof(cooldownTypeComponent.Value))
                };
            }
        }
    }
}