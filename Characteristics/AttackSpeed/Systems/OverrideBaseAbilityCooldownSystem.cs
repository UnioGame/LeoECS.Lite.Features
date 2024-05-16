namespace Game.Ecs.Characteristics.AttackSpeed.Systems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Ability.Common.Components;
    using Ability.Components.Requests;
    using Base.Components;
    using Base.Components.Requests;
    using Components;
    using Cooldown.Components;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Переопределение базового времени восстановления способности
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class OverrideBaseAbilityCooldownSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<CreateCharacteristicRequest<AttackSpeedComponent>> _createPool;
        private EcsPool<BaseCooldownComponent> _baseCooldownPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<CreateCharacteristicRequest<AttackSpeedComponent>>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var attackCharacteristic in _filter)
            {
                Debug.Log("Attack characteristic was created and stored");
                ref var request = ref _createPool.Get(attackCharacteristic);
                if(!request.Owner.Unpack(_world, out var ownerEntity)) continue;
                
                ref var attackAbilityIdComponent = ref _world.GetOrAddComponent<AttackAbilityIdComponent>(ownerEntity);
                var slotId = attackAbilityIdComponent.Value;
                
                //todo найти нужную абилку, поменять у нее кулдаун
                ref var abilityBaseCooldownSelfRequest = ref _world.AddComponent<SetAbilityBaseCooldownSelfRequest>(ownerEntity);
                abilityBaseCooldownSelfRequest.AbilitySlot = slotId;
                abilityBaseCooldownSelfRequest.Cooldown = request.Value;
            }
        }
    }

    internal struct StoredData
    {
        public float BaseCooldown;
        public EcsPackedEntity Owner;
        
    }
}