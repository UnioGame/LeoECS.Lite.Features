namespace Game.Ecs.Ability
{
    using System;
    using System.Collections.Generic;
    using Common.Components;
    using Common.Systems;
    using Components;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Sirenix.OdinInspector;
    using SubFeatures;
    using Systems;
    using Tools;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    
    [CreateAssetMenu(menuName = "Game/Feature/Ability/Ability Feature", 
        fileName = "Ability Feature")]
    [Serializable]
    public sealed class AbilityFeature : BaseLeoEcsFeature
    {
        [InlineEditor]
        public List<AbilitySubFeature> subFeatures = new List<AbilitySubFeature>();
        
        public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var abilityTools = new AbilityTools();

            world.SetGlobal(abilityTools);
            
            ecsSystems.DelHere<AbilityVelocityEvent>();
            
            ecsSystems.Add(abilityTools);
            ecsSystems.Add(new ProcessAbilityVelocityEventSystem());
            ecsSystems.Add(new ApplyAbilityRadiusSystem(abilityTools));

            foreach (var feature in subFeatures)
                await feature.OnInitializeSystems(ecsSystems);
            
            //setup ability in hand by slot
            ecsSystems.Add(new ProcessSetInHandAbilityBySlotRequestSystem(abilityTools));
            ecsSystems.DelHere<SetInHandAbilityBySlotSelfRequest>();
            
            //handle ApplyAbilityRequest and apply ability by slot
            ecsSystems.Add(new ProcessApplyAbilityBySlotRequestSystem());
            ecsSystems.DelHere<ApplyAbilityBySlotSelfRequest>();

            //activate ability by id with request ActivateAbilityByIdRequest, take it in hand and use
            ecsSystems.Add(new ActivateAbilityByIdSystem());
            ecsSystems.Add(new ActivateAbilitySystem());
            
            foreach (var feature in subFeatures)
                await feature.OnStartSystems(ecsSystems);
            
            ecsSystems.Add(new StartAbilityCooldownAbilitySystem());
            ecsSystems.Add(new ResetAbilityCooldownAbilitySystem());
            ecsSystems.Add(new CooldownRevokeAbilityRequestSystem());
            ecsSystems.Add(new UpdateAbilityCooldownBaseValue());
            ecsSystems.Add(new SetAbilityNotActiveWhenDeadSystem());
            //if non default slot ability in use, discard set in hand request
            ecsSystems.Add(new DiscardSetInHandWhileExecutingAbilitySystem());
            //if non default slot ability in use and try to run another slot ability -> complete active
            ecsSystems.Add(new DiscardLowPriorityAbilitySystem());
            ecsSystems.Add(new DiscardAbilityEffectMilestonesSystem());
            
            foreach (var feature in subFeatures)
                await feature.OnCompleteAbilitySystems(ecsSystems);

            //remove event after whole loop
            ecsSystems.DelHere<AbilityCompleteSelfEvent>();
            //mark ability as completed and fire AbilityCompleteSelfEvent
            ecsSystems.Add(new CompleteAbilitySystem());
            ecsSystems.DelHere<CompleteAbilitySelfRequest>();

            //set ability in hand by ability entity
            ecsSystems.Add(new SetInHandAbilityRequestSystem(abilityTools));
            ecsSystems.DelHere<SetInHandAbilitySelfRequest>();
                
            //add ability system to update in hand ability state
            foreach (var feature in subFeatures)
                await feature.OnAfterInHandSystems(ecsSystems);
            
            //additional actions before apply ability
            foreach (var feature in subFeatures)
                await feature.OnBeforeApplyAbility(ecsSystems);
            
            //apply ability by request, ability must be in hand and owned by entity
            ecsSystems.Add(new ApplyAbilityRequestSystem());
            ecsSystems.DelHere<ApplyAbilitySelfRequest>();

            foreach (var feature in subFeatures)
                await feature.OnRevokeSystems(ecsSystems);

            foreach (var feature in subFeatures)
                await feature.OnUtilitySystems(ecsSystems);
            
            //activate ability execution
            ecsSystems.DelHere<AbilityStartUsingSelfEvent>();
            ecsSystems.Add(new ApplyAbilitySystem());
            ecsSystems.DelHere<AbilityValidationSelfRequest>();

            //check is any ability activated and mark it with AbilityInProcessingComponent
            ecsSystems.Add(new UpdateAbilityProcessingStatusSystem());
            
            //include on activate systems
            foreach (var feature in subFeatures)
                await feature.OnActivateSystems(ecsSystems);
            
            ecsSystems.Add(new EvaluateAbilitySystem());
            
            foreach (var feature in subFeatures)
                await feature.OnEvaluateAbilitySystem(ecsSystems);

            ecsSystems.DelHere<ApplyAbilityEffectsSelfRequest>();
            ecsSystems.Add(new CreateApplyAbilityEffectsRequestSystem());
            ecsSystems.Add(new AbilityUnlockSystem());
            
            ecsSystems.Add(new ApplyPauseAbilityRequestSystem());
            ecsSystems.DelHere<PauseAbilityRequest>();
            ecsSystems.Add(new RemovePauseAbilityRequestSystem());
            
            ecsSystems.DelHere<RemovePauseAbilityRequest>();
            ecsSystems.DelHere<AbilityUnlockEvent>();
            
            foreach (var feature in subFeatures)
                await feature.OnPreparationApplyEffectsSystems(ecsSystems);
            
            foreach (var feature in subFeatures)
                await feature.OnApplyEffectsSystems(ecsSystems);

            //remove ability activation request
            ecsSystems.DelHere<ActivateAbilityByIdRequest>();
            ecsSystems.DelHere<ActivateAbilityRequest>();
            
            foreach (var feature in subFeatures)
                await feature.OnLastAbilitySystems(ecsSystems);
        }

        [Button]
        private void Fill()
        {
#if UNITY_EDITOR
            var features = AssetEditorTools.GetAssets<AbilitySubFeature>();
            subFeatures.Clear();
            subFeatures.AddRange(features);
            this.SaveAsset();
#endif
        }
    }
}