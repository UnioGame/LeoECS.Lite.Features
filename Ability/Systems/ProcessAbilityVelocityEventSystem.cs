namespace Game.Ecs.Ability.Common.Systems
{
    using System;
    using Components;
    using Core.Death.Components;
    using Input.Components.Ability;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessAbilityVelocityEventSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<AbilityCellVelocityEvent> _velocityEventPool;
        private EcsPool<AbilityMapComponent> _abilityMapPool;
        private EcsPool<AbilityVelocityEvent> _abilityVelocityPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<AbilityCellVelocityEvent>()
                .Inc<AbilityMapComponent>()
                .Exc<DestroyComponent>()
                .End();
            
            _velocityEventPool = _world.GetPool<AbilityCellVelocityEvent>();
            _abilityMapPool = _world.GetPool<AbilityMapComponent>();
            _abilityVelocityPool = _world.GetPool<AbilityVelocityEvent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var velocity = ref _velocityEventPool.Get(entity);
                ref var map = ref _abilityMapPool.Get(entity);
                
                if(!map.AbilityEntities[velocity.AbilityCellId].Unpack(_world, out var abilityEntity))
                    continue;

                ref var abilityVelocity = ref _abilityVelocityPool.GetOrAddComponent(abilityEntity);
                abilityVelocity.Value = velocity.Value;
            }
        }
    }
}