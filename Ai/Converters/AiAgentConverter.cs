namespace Game.Ecs.AI.Converters
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using Components;
    using Configurations;
    using Leopotam.EcsLite;
    using Service;
    using Sirenix.OdinInspector;
    using UniGame.AddressableTools.Runtime;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniModules.UniGame.Core.Runtime.DataFlow.Extensions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Serialization;

    [Serializable]
    public class AiAgentConverter : LeoEcsConverter,ILeoEcsGizmosDrawer
    {
        public bool drawGizmos = false;
        
        public AssetReferenceT<AiAgentConfigurationAsset> configuration;
        
        [FormerlySerializedAs("_sensorRange")] 
        [SerializeField]
        public float sensorRange = 100f;

        [FormerlySerializedAs("_useForceControl")]
        [ShowIf(nameof(IsRuntime))]
        [BoxGroup("debug")]      
        [Tooltip("add AiAgentSelfControlComponent if checked")]
        [SerializeField]
        public bool useForceControl = false;

        [FormerlySerializedAs("_activeActions")]
        [ShowIf(nameof(IsRuntime))]
        [BoxGroup("debug")]
        [Tooltip("runtime inspector for selected ai actions")]
        [SerializeField]
        public bool[] activeActions;
        
        [FormerlySerializedAs("_plannerData")]
        [ShowIf(nameof(IsRuntime))]
        [BoxGroup("debug")]
        [Tooltip("runtime inspector for selected ai actions")]
        [SerializeField]
        public AiPlannerData[] plannerData;

        public bool IsRuntime => Application.isPlaying;
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var aiSensorAgent = ref world.GetOrAddComponent<AiSensorComponent>(entity);
            aiSensorAgent.Range = sensorRange;
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

        private void ApplyAiData(GameObject target,EcsWorld world,int entity,AiAgentConfigurationAsset aiData)
        {
            activeActions = new bool[aiData.ActionsCount];
            plannerData    = new AiPlannerData[aiData.ActionsCount];
            
            var availableActions    = new bool[aiData.ActionsCount];
            var aiConfiguration = aiData.agentConfiguration;

            foreach (var planner in aiConfiguration.planners)
                availableActions[planner.id] = true;

            foreach (var converter in aiConfiguration.planners)
                converter.Apply(target, world, entity);

            ref var aiAgent = ref world.AddComponent<AiAgentComponent>(entity);
            aiAgent.Configuration = aiConfiguration;
            aiAgent.PlannedActions = activeActions;
            aiAgent.PlannerData = plannerData;
            aiAgent.AvailableActions = availableActions;

            if (useForceControl)
                world.AddComponent<AiAgentSelfControlComponent>(entity);
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
