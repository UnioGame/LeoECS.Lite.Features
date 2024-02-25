namespace Game.Ecs.Characteristics.Block.Systems
{
    using System;
    using Components;
    using Game.Ecs.Characteristics.Base.Components;
    using Leopotam.EcsLite;
    using Unity.IL2CPP.CompilerServices;

    [Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class RecalculateBlockSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<CharacteristicComponent<BlockComponent>> _characteristicPool;
        private EcsPool<BlockComponent> _valuePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<CharacteristicChangedComponent<BlockComponent>>()
                .Inc<CharacteristicComponent<BlockComponent>>()
                .Inc<BlockComponent>()
                .End();

            _characteristicPool = _world.GetPool<CharacteristicComponent<BlockComponent>>();
            _valuePool = _world.GetPool<BlockComponent>();
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