namespace Game.Ecs.Effects
{
    using System;
    using System.Collections.Generic;
    using Systems;
    using Components;
    using Cysharp.Threading.Tasks;
    using Data;
    using Feature;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Sirenix.OdinInspector;
    using UniGame.AddressableTools.Runtime;
    using UniGame.AddressableTools.Runtime.AssetReferencies;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif

    [Serializable]
    [CreateAssetMenu(menuName = "Game/Feature/Effects/Effects Feature",fileName = "Effects Feature")]
    public sealed class EffectsFeature : BaseLeoEcsFeature
    {
        [SerializeReference]
        [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        public List<EffectFeatureAsset> effectFeatures = new List<EffectFeatureAsset>();

        public AddressableValue<EffectsRootConfiguration> effectsRootValue;
        
        public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var worldLifeTime = world.GetWorldLifeTime();
            var configuration = await effectsRootValue.reference
                .LoadAssetInstanceTaskAsync(worldLifeTime,true);
            
            world.SetGlobal(configuration.data);
            
            //create global effect root entity data
            ecsSystems.Add(new CreateEffectGlobalRootsSystem());
            
            ecsSystems.Add(new UpdateEffectRootTargetsSystem());
            ecsSystems.Add(new ValidateEffectsListSystem());
            ecsSystems.Add(new ProcessEffectDurationSystem());
            
            ecsSystems.Add(new ValidateEffectSystem());
            ecsSystems.Add(new CreateEffectSystem());
            
            ecsSystems.DelHere<CreateEffectSelfRequest>();
            
            ecsSystems.Add(new DelayedEffectSystem());
            ecsSystems.Add(new ProcessEffectPeriodicitySystem());

            foreach (var feature in effectFeatures)
                await feature.InitializeFeatureAsync(ecsSystems);

            ecsSystems.DelHere<EffectAppliedSelfEvent>();
            ecsSystems.Add(new ProcessAppliedEffectsSystem());

            //====== select effect view parent ==========
            
            //select effect parent by avatar data
            ecsSystems.Add(new SelectAvatarParentTargetsSystem());
            //select effect parent by effect root id
            ecsSystems.Add(new SelectBakedParentByRootIdSystem());
            //select effect from destination entity at runtime if no backed found
            ecsSystems.Add(new SelectChildParentByRootIdSystem());
            //select global effect root entity by effect root id
            ecsSystems.Add(new SelectGlobalParentByRootIdSystem());
            
            //create effect view when effect applied
            ecsSystems.Add(new ShowEffectViewSystem());
            
            ecsSystems.Add(new ProcessEffectViewLifeTimeSystem());
            ecsSystems.Add(new ProcessEffectViewPrepareToDeathSystem());
            ecsSystems.Add(new ProcessEffectViewOwnerSystem());

            ecsSystems.Add(new DestroyEffectViewSystem());
            ecsSystems.Add(new DestroyEffectSystem());
            
            ecsSystems.DelHere<DestroyEffectSelfRequest>();
            ecsSystems.DelHere<RemoveEffectRequest>();
            ecsSystems.DelHere<ApplyEffectSelfRequest>();
        }

        [Button]
        public void FillEffects()
        {
#if UNITY_EDITOR
            var assets = AssetEditorTools.GetAssets<EffectFeatureAsset>();
            foreach (var effect in assets)
            {
                if(effectFeatures.Contains(effect))continue;
                effectFeatures.Add(effect);
            }
            this.MarkDirty();
#endif
        }
    }
}