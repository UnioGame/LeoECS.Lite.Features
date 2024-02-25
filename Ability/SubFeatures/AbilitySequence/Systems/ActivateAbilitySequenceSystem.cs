namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Systems
{
    using System;
    using System.Linq;
    using Ability.Tools;
    using Aspects;
    using Components;
    using AbilitySequence;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// activate ability sequence when it ready and request exist
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ActivateAbilitySequenceSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _sequenceFilter;
        private EcsFilter _requestFilter;

        private AbilitySequenceAspect _aspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _requestFilter = _world
                .Filter<ActivateAbilitySequenceSelfRequest>()
                .Inc<AbilitySequenceReadyComponent>()
                .Inc<AbilitySequenceComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var sequenceEntity in _requestFilter)
            {
                //reset sequence data
                ref var sequenceComponent = ref _aspect.Sequence.Get(sequenceEntity);
                sequenceComponent.NextAbilityIndex = 0;
                sequenceComponent.Index = 0;
                sequenceComponent.ActiveAbility = -1;
                var abilities = sequenceComponent.Abilities;
                var count = abilities.Count;
                
                if(abilities.Count <= 0) continue;
                
                ref var activeComponent = ref _aspect.Active.GetOrAddComponent(sequenceEntity);
                ref var lastComponent = ref _aspect.Last.GetOrAddComponent(sequenceEntity);

                activeComponent.Value = abilities[0];
                lastComponent.Value = abilities[count-1];
                
                _aspect.ActivateNextInSequence.GetOrAddComponent(sequenceEntity);
            }
        }
    }
}