namespace Game.Ecs.GameAi.Move.Converters
{
    using System;
    using AI.Converters;
    using AI.Data;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public class MoveToDefaultTargetConverter : EcsComponentSubPlannerConverter, IMoveByConverter
    {
        [SerializeField]
        private int _priority;
        
        public override void Apply(EcsWorld world, int entity, ActionType actionId)
        {
            ref var moveToDefaultComponent = ref world.AddComponent<MoveToDefaultTargetComponent>(entity);
            moveToDefaultComponent.Priority = _priority;
        }
    }
}