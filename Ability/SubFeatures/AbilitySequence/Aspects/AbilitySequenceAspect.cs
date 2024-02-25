namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Aspects
{
    using System;
    using Components;
    using AbilitySequence;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class AbilitySequenceAspect : EcsAspect
    {
        public EcsWorld World;
        //sequence processing data and related ability entities
        public EcsPool<AbilitySequenceComponent> Sequence;
        //mark sequence as complete
        public EcsPool<AbilitySequenceFinishedComponent> Finished;
        public EcsPool<AbilitySequenceAwaitComponent> Await;
        public EcsPool<AbilitySequenceReadyComponent> Ready;
        //mark sequence as active
        public EcsPool<AbilitySequenceActiveComponent> Active;
        public EcsPool<AbilitySequenceLastComponent> Last;
        //mark ability as part of sequence
        public EcsPool<AbilitySequenceNodeComponent> Node;
        public EcsPool<OwnerComponent> Owner;
        public EcsPool<NameComponent> Name;
        
        //requests
        //create ability sequence by ability ids
        public EcsPool<CreateAbilitySequenceByIdSelfRequest> CreateById;
        public EcsPool<CreateAbilitySequenceReferenceSelfRequest> CreateByReference;
        //create ability sequence by exists abilities entity
        public EcsPool<CreateAbilitySequenceSelfRequest> Create;
        //activate ability sequence by name and owner
        public EcsPool<ActivateAbilitySequenceByNameRequest> ActivateByName;
        //activate next ability in sequence
        public EcsPool<ActivateNextAbilityInSequenceSelfRequest> ActivateNextInSequence;
        //activate ability sequence
        public EcsPool<ActivateAbilitySequenceSelfRequest> Activate;
        public EcsPool<CompleteAbilitySequenceSelfRequest> Complete;
    }
}