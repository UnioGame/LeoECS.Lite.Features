namespace Game.Ecs.Characteristics.Dodge.Systems
{
    using System;
    using Base.Components;
    using Components;
    using Leopotam.EcsLite;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public sealed class RecalculateDodgeSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<CharacteristicComponent<DodgeComponent>> _characteristicPool;
        private EcsPool<DodgeComponent> _valuePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<CharacteristicChangedComponent<DodgeComponent>>()
                .Inc<CharacteristicComponent<DodgeComponent>>()
                .Inc<DodgeComponent>()
                .End();

            _characteristicPool = _world.GetPool<CharacteristicComponent<DodgeComponent>>();
            _valuePool = _world.GetPool<DodgeComponent>();
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