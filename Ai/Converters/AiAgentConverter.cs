namespace Game.Ecs.AI.Converters
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Components;
    using Configurations;
    using Leopotam.EcsLite;
    using UniGame.AddressableTools.Runtime;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniModules.UniGame.Core.Runtime.DataFlow.Extensions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using Data;
    using TargetSelection;
    using TargetSelection.Components;

    [Serializable]
    public class AiAgentConverter : LeoEcsConverter, ILeoEcsGizmosDrawer
    {
        public bool drawGizmos = false;
        
        public AssetReferenceT<AiAgentConfigurationAsset> configuration;
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ApplyAiDataAsync(target, world, entity).Forget();
        }

        private async UniTask ApplyAiDataAsync(GameObject target, EcsWorld world, int entity)
        {
            var lifeTime = target.GetAssetLifeTime();
            var aiData = await configuration
                .LoadAssetInstanceTaskAsync(lifeTime,true);
            
            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
            ApplyAiData(target, world, entity, aiData);
        }

        private void ApplyAiData(GameObject target, EcsWorld world, int entity, AiAgentConfigurationAsset aiData)
        {
            var aiConfiguration = aiData.agentConfiguration;
            
            ref var targetResultComponent = ref world.AddComponent<TargetsSelectionResultComponent>(entity);
            targetResultComponent.Values = new EcsPackedEntity[TargetSelectionData.MaxTargets];
            
            ref var aiAgent = ref world.AddComponent<AiAgentComponent>(entity);
            aiAgent.Configuration = aiConfiguration;
            aiAgent.PlannedActionsMask = 0;
            aiAgent.PlannerData = new Dictionary<ActionType, AiPlannerData>();

            /*foreach (var targetOverride in aiData.targetOverrideConfig.Converters)
            {
                targetOverride.Apply(world, entity);
            }*/
            
            foreach (var targetSelector in aiData.targetingConfig.targetSelectors)
            {
                targetSelector.Apply(world, entity);
            }
            
            aiData.prioritizerConfig.prioritizerConverter.Apply(world, entity);
            
            foreach (var planner in aiConfiguration.planners)
            {
                aiAgent.PlannerData.Add(planner.ActionId, new AiPlannerData());
                planner.Apply(target, world, entity);
            }

            foreach (var c in aiData.commonAiConverters)
            {
                c.commonAiConverters.Apply(world, entity);
            }

            if (aiData.isGroupAgent)
            {
                world.AddComponent<AiGroupAgentComponent>(entity);
            }
        }

        public void DrawGizmos(GameObject target)
        {
#if UNITY_EDITOR
            if(drawGizmos == false) return;
            var aiConfiguration = configuration.editorAsset;
            if (aiConfiguration == null) return;
            aiConfiguration.DrawGizmos(target);
#endif
        }
    }
}
