namespace Game.Code.Ai.ActivateAbility
{
    using System;
    using Ecs.Ability.SubFeatures.Target.Tools;
    using Ecs.Ability.Tools;
    using Ecs.AI.Components;
    using Ecs.AI.Abstract;
    using Ecs.Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Show and Hides HealthBars based on UnderTheTargetComponent 
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    [Serializable]
    public sealed class ActivateAbilityActionSystem : IAiActionSystem,IEcsInitSystem
    {
        private AbilityTools _abilityTools;
        private AbilityTargetTools _targetTools;
        private EcsFilter _filter;
        private EcsWorld _world;
        
        private EcsPool<ActivateAbilityActionComponent> _activateAbilityPool;
        private EcsPool<OwnerComponent> _ownerPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            _targetTools = _world.GetGlobal<AbilityTargetTools>();
            
            _filter = _world
                .Filter<AiAgentComponent>()
                .Inc<ActivateAbilityActionComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var activateAbilityComponent = ref _activateAbilityPool.Get(entity);
                ref var target = ref activateAbilityComponent.Target;
                var slot = activateAbilityComponent.AbilityCellId;

                if(!activateAbilityComponent.Ability.Unpack(_world,out var abilityEntity))
                    continue;
                
                var cooldownPassed = _abilityTools.IsAbilityCooldownPassed(abilityEntity);
                //проверяем кулдаун абилки, если он не прошел - игнорируем
                if (!cooldownPassed) continue;
                
                _targetTools.ActivateAbilityForTarget(entity,target, slot);
            }
        }
    }
}