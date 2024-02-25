namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations.Systems
{
    using System;
    using Ability.Components.Requests;
    using AbilitySequence.Tools;
    using Aspects;
    using Common.Components;
    using Components;
    using Core.Components;
    using Gameplay.CriticalAttackChance.Components;
    using Leopotam.EcsLite;
    using Target.Components;
    using Target.Tools;
    using Tools;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// if critical hit exist - add critical animations
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ActivateCriticalAbilitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private AbilityTools _abilityTools;
        private AbilityTargetTools _targetTools;
        private EcsWorld _world;
        private EcsFilter _filter;
        private CriticalAnimationsAspect _aspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            _targetTools = _world.GetGlobal<AbilityTargetTools>();
            
            _filter = _world
                .Filter<ApplyAbilitySelfRequest>()
                .Inc<CriticalAbilityOwnerComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _aspect.ApplyAbilitySelfRequest.Get(entity);
                if(!request.Value.Unpack(_world,out var abilityEntity))
                    continue;

                var hasAbilityTarget = _aspect.CriticalAbilityTarget.Has(abilityEntity);
                if(!hasAbilityTarget) continue;
                
                var hasCriticalAttackMarker = _aspect.CriticalAttackMarker.Has(entity);
                if(!hasCriticalAttackMarker) continue;
                
                ref var ownerComponent = ref _aspect.Owner.Get(abilityEntity);
                ref var abilityTargetComponent = ref _aspect.CriticalAbilityTarget.Get(abilityEntity);
                
                if(!_abilityTools.TryGetAbilityById(ref ownerComponent.Value,
                       abilityTargetComponent.Value,out var criticalAbilityEntity))
                    continue;
                
                //reset current auto cooldown
                //mark current auto as completed
                ref var completeRequest = ref _aspect.CompleteAbilitySelfRequest.GetOrAddComponent(abilityEntity);
                ref var restartAbilityCooldown = ref _aspect.RestartAbilityCooldownSelfRequest.GetOrAddComponent(abilityEntity);
                ref var targetsComponent = ref _aspect.AbilityTargets.GetOrAddComponent(abilityEntity);
                ref var resetAbilityCooldown = ref _aspect.ResetAbilityCooldownSelfRequest.GetOrAddComponent(criticalAbilityEntity);
                
                //activate critical ability with targets from current auto
                _targetTools.ActivateAbilityWithTargets(entity,criticalAbilityEntity,targetsComponent.Entities);
            }
        }
    }
}