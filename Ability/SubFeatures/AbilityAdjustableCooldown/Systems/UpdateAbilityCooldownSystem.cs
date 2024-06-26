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
    using UniCore.Runtime.ProfilerTools;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Timer.Components;
    using UnityEngine;

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
        private AbilityAspect _abilityAspect;
        private AbilityOwnerAspect _abilityOwnerAspect;
        private EcsPool<AbilityIdComponent> _abilityFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            //обратить внимание что сисетма сработает только при изменении компонента AttackSpeedComponent, но не при его создании
            _filter = _world
                .Filter<CharacteristicComponent<AttackSpeedComponent>>()
                .Inc<CharacteristicChangedComponent<AttackSpeedComponent>>()
                .Inc<AttackSpeedComponent>()
                .Inc<AttackSpeedCooldownTypeComponent>()
                .End();

            _characteristicPool = _world.GetPool<CharacteristicComponent<AttackSpeedComponent>>();
            _attackSpeed = _world.GetPool<AttackSpeedComponent>();
            _cooldownType = _world.GetPool<AttackSpeedCooldownTypeComponent>();
            _attackAbilityId = _world.GetPool<AttackAbilityIdComponent>();
            
            _abilityFilter = _world.GetPool<AbilityIdComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var slotIdComponent = ref _attackAbilityId.Get(entity);
                ref var inHandAbility = ref _abilityOwnerAspect.AbilityInHandLink.Get(entity);
                
                //вынужденная проверка в лоб, пока не поймаем нормальное заполнение AbilityMapComponent
                if(!inHandAbility.AbilityEntity.Unpack(_world, out var abilityEntity))
                    continue;
                ref var ablitySlotId = ref _abilityAspect.AbilitySlot.Get(abilityEntity);
                
                if(ablitySlotId.SlotType != slotIdComponent.Value) continue;
                
                //todo  не работает потому что не заполняется AbilityMapComponent
                
                ref var attackSpeed = ref _characteristicPool.Get(entity);
                ref var cooldownTypeComponent = ref _cooldownType.Get(entity);
                
                ref var cooldownComponent = ref _abilityAspect.Cooldown.GetOrAddComponent(abilityEntity);
                
                cooldownComponent.Value = cooldownTypeComponent.Value switch
                {
                    CooldownType.Cooldown => attackSpeed.Value,
                    CooldownType.Speed => CalculateCooldown(attackSpeed.Value),
                    _ => throw new ArgumentOutOfRangeException(nameof(cooldownTypeComponent.Value))
                };
                
            }
        }

        private float CalculateCooldown(float attackSpeedValue)
        {
            if (!(attackSpeedValue <= 0)) return 1f / attackSpeedValue;
            GameLog.LogError("UpdateAbilityCooldownSystem: attack speed value is less or equal to zero");
            return 0;
        }
    }
}