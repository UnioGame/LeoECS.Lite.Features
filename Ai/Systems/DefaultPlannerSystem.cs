namespace Game.Ecs.AI.Systems
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using Data;

    [Serializable]
    public class DefaultPlannerSystem : BasePlannerSystem<AiDefaultActionComponent>,IEcsInitSystem
    {
        [InlineProperty]
        [HideLabel]
        [SerializeField]
        private AiPlannerData _plannerData;

        private EcsFilter _filter;
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world= systems.GetWorld();
            _filter = _world.Filter<AiAgentComponent>().End();
        }
        
        public override void Run(IEcsSystems systems)
        {
            var actionComponentPool = _world.GetPool<AiDefaultActionComponent>();
            
            foreach (var entity in _filter)
            {
                if(!IsPlannerEnabledForEntity(_world,entity))
                    continue;
                
                actionComponentPool.GetOrAddComponent<AiDefaultActionComponent>(entity);
                ApplyPlanningResult(systems,entity,_plannerData);
            }
        }
    }
}
