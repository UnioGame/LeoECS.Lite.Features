namespace Game.Ecs.Characteristics.Attack.Systems
{
    using System;
    using Game.Ecs.Characteristics.Attack.Components;
    using Game.Ecs.Characteristics.Base.Components;
    using Leopotam.EcsLite;

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
    public sealed class UpdateAttackDamageChangedSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<CharacteristicComponent<AttackDamageComponent>> _characteristicPool;
        private EcsPool<AttackDamageComponent> _valuePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<CharacteristicChangedComponent<AttackDamageComponent>>()
                .Inc<CharacteristicComponent<AttackDamageComponent>>()
                .Inc<AttackDamageComponent>()
                .End();

            _characteristicPool = _world.GetPool<CharacteristicComponent<AttackDamageComponent>>();
            _valuePool = _world.GetPool<AttackDamageComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var characteristicComponent = ref _characteristicPool.Get(entity);
                ref var valueComponent = ref _valuePool.Get(entity);
                valueComponent.Value = characteristicComponent.Value;
            }
        }
    }
}