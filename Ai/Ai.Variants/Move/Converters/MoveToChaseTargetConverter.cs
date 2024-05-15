namespace Game.Ecs.GameAi.Move.Converters
{
    using System;
    using System.Threading;
    using AI.Converters;
    using AI.Data;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public class MoveToChaseTargetConverter : EcsComponentSubPlannerConverter, IMoveByConverter
    {
        [SerializeField]
        private int _priority;
        
        public override void Apply(EcsWorld world, int entity, ActionType actionId)
        {
            base.Apply(world, entity, actionId);
            ref var moveToDefaultComponent = ref world.AddComponent<MoveToChaseTargetComponent>(entity);
            moveToDefaultComponent.Priority = _priority;
        }
    }
}