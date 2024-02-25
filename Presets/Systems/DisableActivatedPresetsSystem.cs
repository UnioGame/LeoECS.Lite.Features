namespace Game.Ecs.Presets.Systems
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif


    [Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class DisableActivatedPresetsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _sourceFilter;
        
        private EcsPool<ActivePresetSourceComponent> _activePool;
        private EcsPool<PresetActivatedComponent> _activatedPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _sourceFilter = _world
                .Filter<PresetComponent>()
                .Inc<ActivePresetSourceComponent>()
                .Inc<PresetActivatedComponent>()
                .End();

            _activePool = _world.GetPool<ActivePresetSourceComponent>();
            _activatedPool = _world.GetPool<PresetActivatedComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var sourceEntity in _sourceFilter)
            {
                _activePool.Del(sourceEntity);
                _activatedPool.Del(sourceEntity);
                    
                break;
            }
        }
    }
}