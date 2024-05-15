namespace Ai.Ai.Variants.Attack.Converters
{
    using System;
    using Components;
    using Game.Ecs.AI.Converters;
    using Game.Ecs.AI.Data;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public class AttackChaseTargetConverter : EcsComponentSubPlannerConverter, IAttackConverter
    {
        [SerializeField]
        private int _priority;
        
        public override void Apply(EcsWorld world, int entity, ActionType actionId)
        {
            base.Apply(world, entity, actionId);
            ref var attackChaseTargetComponent = ref world.AddComponent<AttackChaseTargetComponent>(entity);
            attackChaseTargetComponent.Priority = _priority;
        }
    }
}