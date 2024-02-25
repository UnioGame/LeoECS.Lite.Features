namespace Game.Ecs.Characteristics.SplashDamage.Systems
{
    using Base.Components;
    using Components;
    using Components.Requests;
    using Leopotam.EcsLite;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

    /// <summary>
    /// Recalculates Splash Damage value.
    /// </summary>
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class RecalculateSplashDamageSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<CharacteristicComponent<SplashDamageComponent>> _characteristicPool;
        private EcsPool<SplashDamageComponent> _valuePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<CharacteristicChangedComponent<SplashDamageComponent>>()
                .Inc<CharacteristicComponent<SplashDamageComponent>>()
                .Inc<SplashDamageComponent>()
                .End();

            _characteristicPool = _world.GetPool<CharacteristicComponent<SplashDamageComponent>>();
            _valuePool = _world.GetPool<SplashDamageComponent>();
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