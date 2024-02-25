namespace Game.Ecs.Characteristics.CriticalChance.Systems
{
    using System;
    using Components;
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
    public sealed class UpdateCriticalChanceChangedSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<CharacteristicComponent<CriticalChanceComponent>> _characteristicPool;
        private EcsPool<CriticalChanceComponent> _valuePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<CharacteristicChangedComponent<CriticalChanceComponent>>()
                .Inc<CharacteristicComponent<CriticalChanceComponent>>()
                .Inc<CriticalChanceComponent>()
                .End();

            _characteristicPool = _world.GetPool<CharacteristicComponent<CriticalChanceComponent>>();
            _valuePool = _world.GetPool<CriticalChanceComponent>();
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