namespace Game.Ecs.Ability.Common.Systems
{
    using System;
    using Ability.Components;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Tools;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;

    /// <summary>
    /// initialize ui equip view with link to its data
    /// </summary>
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class DiscardLowPriorityAbilitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private AbilityTools _abilityTools;
        private EcsFilter _filter;
        private EcsWorld _world;
        private AbilityOwnerAspect _ownerAspect;
        private AbilityAspect _abilityAspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            
            _filter = _world
                .Filter<SetInHandAbilitySelfRequest>()
                .Inc<AbilityMapComponent>()
                .Inc<AbilityInHandLinkComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var abilityInHandLinkComponent = ref _ownerAspect.AbilityInHandLink.Get(entity);
                ref var request = ref _ownerAspect.SetInHandAbility.Get(entity);
                
                if(!request.Value.Unpack(_world,out var abilityEntity))
                    continue;

                ref var slotComponent = ref _ownerAspect.Slot.Get(abilityEntity);
                
                if(!abilityInHandLinkComponent.AbilityEntity.Unpack(_world,out var inHandEntity))
                    continue;

                var inHandSlot = _abilityTools.GetAbilitySlot(_world, entity, inHandEntity);
                var abilitySlot = _abilityTools.GetAbilitySlot(_world, entity, abilityEntity);
                var isDefaultInHand = _abilityAspect.Default.Has(inHandEntity);

                var isNotDefaultInSlotAbility = abilitySlot > inHandSlot && !isDefaultInHand;
                var isNotDefaultInHandAbility = !isDefaultInHand || inHandSlot==slotComponent.SlotType;
                
                if (!isNotDefaultInSlotAbility && isNotDefaultInHandAbility)
                    continue;
                
                _abilityAspect.CompleteAbility.GetOrAddComponent(inHandEntity);
            }
        }
    }
}