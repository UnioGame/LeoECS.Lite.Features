namespace Game.Ecs.Ability.SubFeatures.AbilitySequence
{
    using System;
    using Aspects;
    using Components;
    using AbilitySequence;
    using Cysharp.Threading.Tasks;
    using Game.Ecs.Ability.Tools;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Systems;
    using Tools;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// add critical animations if critical hit exist
    /// </summary>
    [Serializable]
    [CreateAssetMenu(menuName = "Game/Feature/Ability/Ability Sequence SubFeature",fileName = "Ability Sequence SubFeature")]
    public class AbilitySequenceSubFeature : AbilitySubFeature
    {
        public override UniTask<IEcsSystems> OnStartSystems(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var abilitySequenceTools = new AbilitySequenceTools();
            
            //set global value
            world.SetGlobal(abilitySequenceTools);
            ecsSystems.Add(abilitySequenceTools);
            
            //create new ability sequence by request
            ecsSystems.Add(new CreateAbilitySequenceByIdSystem());
            ecsSystems.Add(new CreateAbilitySequenceByReferenceSystem());
            ecsSystems.Add(new CreateAbilitySequenceByRequestSystem());
            
            //if new ability started and this is not sequence ability - mark sequence as completed
            ecsSystems.Add(new CheckExecutionAbilitySequenceSystem());
            ecsSystems.Add(new CompleteAbilitySequenceSystem());
            
            //if ability in sequence not ready and equipped - mark as ready
            ecsSystems.Add(new CheckAbilitySequenceReadySystem());
            
            //activate sequence by name
            ecsSystems.Add(new ActivateAbilitySequenceByNameSystem());
            
            //activate sequence by self request. this request will restart sequence
            ecsSystems.Add(new ActivateAbilitySequenceSystem());
            
            //prepare ability execution
            ecsSystems.Add(new WaitToStartNextAbilitySystem());
            
            //select next ability in sequence and activate it on ActivateSequenceAbilitySelfRequest
            ecsSystems.Add(new ActivateNextAbilityInSequenceSystem());
            
            ecsSystems.DelHere<ActivateAbilitySequenceSelfRequest>();
            ecsSystems.DelHere<ActivateNextAbilityInSequenceSelfRequest>();
            ecsSystems.DelHere<CreateAbilitySequenceSelfRequest>();
            ecsSystems.DelHere<CreateAbilitySequenceByIdSelfRequest>();
            ecsSystems.DelHere<CreateAbilitySequenceReferenceSelfRequest>();
            ecsSystems.DelHere<CompleteAbilitySequenceSelfRequest>();
            
            return UniTask.FromResult(ecsSystems);
        }

    }
}