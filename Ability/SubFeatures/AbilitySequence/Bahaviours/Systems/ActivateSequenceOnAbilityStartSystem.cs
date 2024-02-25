namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Bahaviours.Systems
{
    using System;
    using System.Linq;
    using Ability.Aspects;
    using Ability.Components;
    using AbilitySequence.Aspects;
    using Aspects;
    using Common.Components;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// activate ability sequence on ability start
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ActivateSequenceOnAbilityStartSystem : IEcsInitSystem, IEcsRunSystem
    {
        private AbilityAspect _abilityAspect;
        private AbilitySequenceAspect _sequenceAspect;
        private AbilitySequenceOnStartAspect _activateAspect;
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _triggerFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<AbilityStartUsingSelfEvent>()
                .End();

            _triggerFilter = _world
                .Filter<ActivateSequenceTriggerComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                foreach (var triggerEntity in _triggerFilter)
                {
                    ref var triggerComponent = ref _activateAspect.ActivateTrigger.Get(triggerEntity);
                    if(entity != triggerComponent.Trigger)
                        continue;

                    var sequenceEntity = triggerComponent.Sequence;
                    //activate sequence
                    ref var activeComponent = ref _sequenceAspect
                        .Activate.GetOrAddComponent(sequenceEntity);
                }
            }
        }
    }
}