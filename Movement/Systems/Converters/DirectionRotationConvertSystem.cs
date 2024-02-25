namespace Game.Ecs.Movement.Systems.Converters
{
    using System;
    using Components;
    using Core.Components;
    using Input.Components.Direction;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine;

    /// <summary>
    /// Система отвечающая за конвертацию map space направления в квартенион направления.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class DirectionRotationConvertSystem : IEcsRunSystem,IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<DirectionInputEvent>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            var directionPool = _world.GetPool<DirectionInputEvent>();
            var quaternionPool = _world.GetPool<RotationComponent>();

            foreach (var entity in _filter)
            {
                ref var direction = ref directionPool.Get(entity);
                ref var quaternion = ref quaternionPool.Add(entity);
                
                quaternion.Value = Quaternion.LookRotation(direction.Value, Vector3.up);
            }
        }
    }
}