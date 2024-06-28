namespace Game.Ecs.Characteristics.AttackSpeed.Systems
{
    using System;
    using Ability.Common.Components;
    using Components;
    using Game.Ecs.Characteristics.Base.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// update value of attack speed characteristic
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public sealed class UpdateAttackSpeedChangedSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<CharacteristicComponent<AttackSpeedComponent>> _characteristicPool;
        private EcsPool<AttackSpeedComponent> _attackSpeed;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<CharacteristicChangedComponent<AttackSpeedComponent>>()
                .Inc<CharacteristicComponent<AttackSpeedComponent>>()
                .Inc<AttackSpeedComponent>()
                .Inc<AttackSpeedCooldownTypeComponent>()
                .End();

            _characteristicPool = _world.GetPool<CharacteristicComponent<AttackSpeedComponent>>();
            _attackSpeed = _world.GetPool<AttackSpeedComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var characteristicComponent = ref _characteristicPool.Get(entity);
                ref var attackSpeedComponent = ref _attackSpeed.Get(entity);
                attackSpeedComponent.Value = characteristicComponent.Value;
            }
        }
    }
}