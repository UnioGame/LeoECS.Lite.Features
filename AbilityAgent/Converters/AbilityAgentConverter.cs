namespace Game.Ecs.AbilityAgent.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Code.Configuration.Runtime.Ability;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AbilityAgentConverter : LeoEcsConverter
    {
        #region Inspector
        
        public List<AbilityCell> abilityForAgents = new List<AbilityCell>();
        public EcsEntityConverter entityConfiguration;

        #endregion
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var packedEntity = world.PackEntity(entity);
            ref var abilityAgentConfiguration = ref world.GetOrAddComponent<AbilityAgentConfigurationComponent>(entity);
            abilityAgentConfiguration.Value = entityConfiguration;
            ref var ownerTransformComponent = ref world.GetComponent<TransformComponent>(entity);
            ref var ownerGameObjectComponent = ref world.GetComponent<GameObjectComponent>(entity);
            
            foreach (var abilityId in abilityForAgents)
            {
                var entityAgent = world.NewEntity();
                ref var abilityAgentComponent = ref world.AddComponent<AbilityAgentComponent>(entityAgent);
                abilityAgentComponent.Value = abilityId;
                ref var ownerComponent = ref world.AddComponent<OwnerComponent>(entityAgent);
                ownerComponent.Value = packedEntity;
                
                //workaround for simultaneous work of ability agent and ability initiator
                ref var agentUnitOwnerComponent = ref world.AddComponent<AbilityAgentUnitOwnerComponent>(entityAgent);
                agentUnitOwnerComponent.Value = world.PackEntity(entity);
                
                ref var transformComponent = ref world.AddComponent<TransformComponent>(entityAgent);
                transformComponent.Value = ownerTransformComponent.Value;
                ref var gameObjectComponent = ref world.AddComponent<GameObjectComponent>(entityAgent);
                gameObjectComponent.Value = ownerGameObjectComponent.Value;
                
                foreach (var entityConverterConverter in entityConfiguration.converters)
                {
                    entityConverterConverter.converter.Apply(world, entityAgent);
                }
            }
        }
    }
}