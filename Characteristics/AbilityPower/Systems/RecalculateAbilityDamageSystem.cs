namespace Game.Ecs.Characteristics.AbilityPower.Systems
{
    using Base.Components;
    using Components;
    using Leopotam.EcsLite;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

    /// <summary>
    /// Recalculates Ability Damage value.
    /// </summary>
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class RecalculateAbilityPowerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<CharacteristicComponent<AbilityPowerComponent>> _characteristicPool;
        private EcsPool<AbilityPowerComponent> _valuePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<CharacteristicChangedComponent<AbilityPowerComponent>>()
                .Inc<CharacteristicComponent<AbilityPowerComponent>>()
                .Inc<AbilityPowerComponent>()
                .End();

            _characteristicPool = _world.GetPool<CharacteristicComponent<AbilityPowerComponent>>();
            _valuePool = _world.GetPool<AbilityPowerComponent>();
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