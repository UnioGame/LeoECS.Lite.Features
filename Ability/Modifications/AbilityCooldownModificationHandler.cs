namespace Game.Ecs.Ability.Modifications
{
    using System;
    using Characteristics.Base.Modification;
    using Characteristics.Cooldown.Components;
    using Common.Components;
    using Cooldown;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public sealed class AbilityCooldownModificationHandler : ModificationHandler
    {
        [SerializeField]
        public int _abilityId;
        
        public CooldownType cooldownType = CooldownType.Cooldown;

        public sealed override Modification Create()
        {
            var cooldownValue = value;
            if (cooldownType == CooldownType.Speed && !isPercent)
                cooldownValue = 1.0f / cooldownValue;
            
            var modificationValue = new Modification(cooldownValue, isPercent, allowedSummation,isMaxLimitModification);
            return modificationValue;
        }

        public override void AddModification(EcsWorld world,int source, int destinationEntity)
        {
            var abilityMapPool = world.GetPool<AbilityMapComponent>();
            if(!abilityMapPool.Has(destinationEntity))
                return;

            ref var abilityMap = ref abilityMapPool.Get(destinationEntity);
            if(_abilityId < 0 || _abilityId >= abilityMap.AbilityEntities.Count)
                return;
            
            var ability = abilityMap.AbilityEntities[_abilityId];
            if(!ability.Unpack(world, out var abilityEntity))
                return;
            
            var baseCooldownPool = world.GetPool<BaseCooldownComponent>();
            if(!baseCooldownPool.Has(abilityEntity))
                return;

            ref var baseCooldown = ref baseCooldownPool.Get(abilityEntity);
            baseCooldown.Modifications.AddModification(Modification);

            world.GetOrAddComponent<RecalculateCooldownSelfRequest>(abilityEntity);
        }

        public override void RemoveModification(EcsWorld world,int source, int destinationEntity)
        {
            var abilityMapPool = world.GetPool<AbilityMapComponent>();
            if(!abilityMapPool.Has(destinationEntity))
                return;

            ref var abilityMap = ref abilityMapPool.Get(destinationEntity);
            if(_abilityId < 0 || _abilityId >= abilityMap.AbilityEntities.Count)
                return;
            
            var ability = abilityMap.AbilityEntities[_abilityId];
            if(!ability.Unpack(world, out var abilityEntity))
                return;
            
            var baseCooldownPool = world.GetPool<BaseCooldownComponent>();
            if(!baseCooldownPool.Has(abilityEntity))
                return;

            ref var baseCooldown = ref baseCooldownPool.Get(abilityEntity);
            baseCooldown.Modifications.RemoveModification(Modification);
            
            var requestPool = world.GetPool<RecalculateCooldownSelfRequest>();
            if (!requestPool.Has(abilityEntity))
                requestPool.Add(abilityEntity);
        }
    }
}