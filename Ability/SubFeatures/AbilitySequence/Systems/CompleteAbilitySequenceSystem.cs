namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Systems
{
    using System;
    using Ability.Aspects;
    using Aspects;
    using Common.Components;
    using Components;
    using AbilitySequence;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

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
    public class CompleteAbilitySequenceSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _sequenceFilter;
        
        private AbilitySequenceAspect _sequenceAspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _sequenceFilter = _world
                .Filter<CompleteAbilitySequenceSelfRequest>()
                .Inc<AbilitySequenceActiveComponent>()
                .Inc<AbilitySequenceComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var sequenceEntity in _sequenceFilter)
            {
                _sequenceAspect.Active.Del(sequenceEntity);
            }
        }
    }
}