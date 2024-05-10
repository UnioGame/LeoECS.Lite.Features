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
    using Shared.Generated;
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

            ref var prioritizedTargetsComponent = ref world.AddComponent<PrioritizedTargetComponent>(entity);
            prioritizedTargetsComponent.Value = new Dictionary<int, EcsPackedEntity>();
            ref var targetResultComponent = ref world.AddComponent<TargetsSelectionResultComponent>(entity);
            targetResultComponent.Results = new Dictionary<int, SqrRangeTargetSelectionResult>();
            
            ref var aiAgent = ref world.AddComponent<AiAgentComponent>(entity);
            aiAgent.Configuration = aiConfiguration;
            aiAgent.PlannedActionsMask = 0;
            aiAgent.PlannerData = new Dictionary<ActionType, AiPlannerData>();
            
            foreach (var planner in aiConfiguration.planners)
            {
                aiAgent.PlannerData.Add(planner.ActionId, new AiPlannerData());
                planner.Apply(target, world, entity);
                prioritizedTargetsComponent.Value.Add((int)planner.ActionId, default);
                targetResultComponent.Results.Add((int)planner.ActionId, new SqrRangeTargetSelectionResult
                {
                    Values = new EcsPackedEntity[TargetSelectionData.MaxTargets]
                });
            }

            foreach (var c in aiData.commonAiConverters)
            {
                c.commonAiConverters.Apply(world, entity);
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
