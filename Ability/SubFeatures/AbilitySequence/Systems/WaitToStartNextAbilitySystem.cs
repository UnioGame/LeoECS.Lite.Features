namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Systems
{
    using System;
    using Ability.Components;
    using AbilityInventory.Components;
    using Aspects;
    using Common.Components;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Wait next queued ability for launch
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class WaitToStartNextAbilitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        
        private EcsFilter _abilitySequenceFilter;
        private EcsFilter _eventFilter;
        
        private AbilitySequenceAspect _aspect;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            //take sequence target no processing abilities
            _eventFilter = _world
                .Filter<AbilityCompleteSelfEvent>()
                .Inc<OwnerComponent>()
                .End();

            _abilitySequenceFilter = _world
                .Filter<AbilitySequenceReadyComponent>()
                .Inc<AbilitySequenceActiveComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var abilityEntity in _eventFilter)
            {
                foreach (var sequenceEntity in _abilitySequenceFilter)
                {
                    ref var activeComponent = ref _aspect.Active.Get(sequenceEntity);
                    if(activeComponent.Value != abilityEntity) continue;
                    _aspect.ActivateNextInSequence.GetOrAddComponent(sequenceEntity);
                    
                    break;
                }
            }
        }
    }
}