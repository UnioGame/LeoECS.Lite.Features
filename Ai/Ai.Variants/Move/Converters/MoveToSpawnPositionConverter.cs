namespace Game.Ecs.GameAi.Move.Converters
{
    using System;
    using AI.Converters;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public class MoveToSpawnPositionConverter : EcsComponentSubPlannerConverter, IMoveByConverter
    {
        [SerializeField]
        private int _priority;
        
        public sealed override void Apply(EcsWorld world, int entity)
        {
            ref var moveToSpawnPositionComponent = ref world.AddComponent<MoveToSpawnPositionComponent>(entity);
            moveToSpawnPositionComponent.Priority = _priority;
        }
    }
}