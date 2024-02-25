namespace Game.Ecs.AbilityAgent.Systems
{
    using System;
    using Ability.Components;
    using Aspects;
    using Components;
    using GameLayers.Category.Components;
    using GameLayers.Layer.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Initialize ability agent
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class InitializeAbilityAgentSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private AbilityAgentAspect _abilityAgentAspect;
        private EcsFilter _abilityAgentFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityAgentFilter = _world
                .Filter<AbilityAgentComponent>()
                .Inc<CategoryIdComponent>()
                .Inc<LayerIdComponent>()
                .Exc<AbilityAgentReadyComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _abilityAgentFilter)
            {
                ref var agentUnitOwnerComponent = ref _abilityAgentAspect.AbilityAgentUnitOwnerComponent.Get(entity);
                if (!agentUnitOwnerComponent.Value.Unpack(_world, out var ownerEntity))
                    continue;
                if (!_abilityAgentAspect.EntityAvatarComponent.Has(ownerEntity))
                    continue;
                _abilityAgentAspect.EntityAvatarComponent.Copy(ownerEntity, entity);
                _abilityAgentAspect.EffectRootComponent.Copy(ownerEntity, entity);
                ref var abilityMapComponent = ref _abilityAgentAspect.AbilityMapComponent.Add(entity);
                ref var inHandLinkComponent = ref _abilityAgentAspect.AbilityInHandLinkComponent.Add(entity);
                ref var defaultSlotComponent = ref _abilityAgentAspect.DefaultSlotComponent.Add(entity);
                
                var abilityEntity = _world.NewEntity();
                ref var equipRequest = ref _abilityAgentAspect.EquipAbilityIdSelfRequest.Add(abilityEntity);
                ref var agentComponent = ref _abilityAgentAspect.AbilityAgentComponent.Get(entity);
                var abilityCell = agentComponent.Value;
                    
                equipRequest.AbilityId = abilityCell.AbilityId;
                equipRequest.AbilitySlot = abilityCell.SlotId;
                equipRequest.Owner = _world.PackEntity(entity);
                equipRequest.IsUserInput = false;
                equipRequest.IsDefault = abilityCell.IsDefault;
                    
                if (abilityCell.IsDefault)
                    defaultSlotComponent.Value = abilityCell.SlotId;
                    
                _abilityAgentAspect.AbilityAgentReadyComponent.Add(entity);
            }
        }
    }
}