namespace Game.Ecs.AbilityAgent.Aspects
{
    using System;
    using Ability.Common.Components;
    using Ability.Components;
    using AbilityInventory.Components;
    using Components;
    using Core.Components;
    using Effects.Components;
    using Gameplay.LevelProgress.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Components;

    /// <summary>
    /// Ability agent aspect
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AbilityAgentAspect : EcsAspect
    {
        public EcsPool<AbilityMapComponent> AbilityMapComponent;
        public EcsPool<AbilityAgentComponent> AbilityAgentComponent;
        public EcsPool<DefaultAbilityTargetSlotComponent> DefaultSlotComponent;
        public EcsPool<AbilityAgentReadyComponent> AbilityAgentReadyComponent;
        public EcsPool<AbilityInHandLinkComponent> AbilityInHandLinkComponent;
        public EcsPool<EntityAvatarComponent> EntityAvatarComponent;
        public EcsPool<EffectRootComponent> EffectRootComponent;
        public EcsPool<OwnerComponent> OwnerComponent;
        public EcsPool<AbilityAgentConfigurationComponent> AbilityAgentConfigurationComponent;
        public EcsPool<AbilityAgentUnitOwnerComponent> AbilityAgentUnitOwnerComponent;

        public EcsPool<EquipAbilityIdSelfRequest> EquipAbilityIdSelfRequest;
        public EcsPool<CreateAbilityAgentSelfRequest> CreateAbilityAgentSelfRequest;
    }
}