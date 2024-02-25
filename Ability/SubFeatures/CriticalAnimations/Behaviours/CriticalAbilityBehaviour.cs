namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations.Behaviours
{
    using System;
    using Components;
    using Core.Components;
    using Game.Code.Configuration.Runtime.Ability.Description;
    using Leopotam.EcsLite;
    using Tools;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// create animations options for critical strike
    /// </summary>
    [Serializable]
    public class CriticalAbilityBehaviour : IAbilityBehaviour
    {
        public AbilityId criticalAbilityId;

        public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
        {
            var tools = world.GetGlobal<AbilityTools>();
            ref var targetComponent = ref world.GetOrAddComponent<CriticalAbilityTargetComponent>(abilityEntity);
            targetComponent.Value = criticalAbilityId;
            
            ref var ownerComponent = ref world.GetComponent<OwnerComponent>(abilityEntity);
            if (!ownerComponent.Value.Unpack(world, out var ownerEntity))
            {
#if UNITY_EDITOR
                Debug.LogError($"CriticalAbilityBehaviour for {abilityEntity} owner entity {ownerEntity} not exists");
#endif
                return;
            }

            world.GetOrAddComponent<CriticalAbilityOwnerComponent>(ownerEntity);
            tools.EquipAbilityById(ref ownerComponent.Value, ref criticalAbilityId);
        }
    }
}