using System;
using Game.Code.Configuration.Runtime.Ability;
using Game.Code.Configuration.Runtime.Ability.Description;
using Game.Ecs.Core.Components;
using Leopotam.EcsLite;
using UniGame.LeoEcs.Converter.Runtime;
using UniGame.LeoEcs.Shared.Components;
using UniGame.LeoEcs.Shared.Extensions;
using UnityEngine;

namespace Game.Ecs.Ability.SubFeatures.AbilityInitiator.Behaviour
{
    using AbilityAgent.Components;

    [Serializable]
    public class AbilityInitiatorBehaviour : IAbilityBehaviour
    {
        public const string AbilityAgentName = "AbilityAgent";
        
        public AbilityCell targetAbility;
        public EcsEntityConverter entityConfiguration;
        public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
        {
            //get entity of unit with ability
            var ownerUnitComponent = world.GetComponent<OwnerComponent>(abilityEntity);
            var packedEntity = ownerUnitComponent.Value;
            if (!packedEntity.Unpack(world, out var unitEntity)) return;

            ref var abilityAgentConfiguration = ref world.GetOrAddComponent<AbilityAgentConfigurationComponent>(unitEntity);
            abilityAgentConfiguration.Value = entityConfiguration;
            ref var ownerTransformComponent = ref world.GetComponent<TransformComponent>(unitEntity);
            ref var ownerGameObjectComponent = ref world.GetComponent<GameObjectComponent>(unitEntity);
            
                var entityAgent = world.NewEntity();
                
                ref var agentUnitOwnerComponent = ref world.AddComponent<AbilityAgentUnitOwnerComponent>(entityAgent);
                agentUnitOwnerComponent.Value = world.PackEntity(unitEntity);
                
                ref var abilityAgentComponent = ref world.AddComponent<AbilityAgentComponent>(entityAgent);
                abilityAgentComponent.Value = targetAbility;
                
                ref var ownerComponent = ref world.AddComponent<OwnerComponent>(entityAgent);
                var abilityInitiatorPackedEntity = world.PackEntity(abilityEntity);
                ownerComponent.Value = abilityInitiatorPackedEntity;

                var abilityAgentGO = new GameObject(AbilityAgentName);
                var abilityAgentTransform = abilityAgentGO.transform;

                var position = ownerTransformComponent.Value.position;
                abilityAgentTransform.position = position;
                abilityAgentGO.transform.position = position;


                abilityAgentTransform.SetParent(ownerTransformComponent.Value);
                
                ref var transformComponent = ref world.AddComponent<TransformComponent>(entityAgent);
                transformComponent.Value = abilityAgentTransform;
                
                ref var gameObjectComponent = ref world.AddComponent<GameObjectComponent>(entityAgent);
                gameObjectComponent.Value = abilityAgentGO;
                
                foreach (var entityConverterConverter in entityConfiguration.converters)
                {
                    entityConverterConverter.converter.Apply(world, entityAgent);
                }
        }
    }
}