namespace Game.Ecs.AI.Systems
{
    using System;
    using Components;
    using Abstract;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using Data;

    [Serializable]
    public abstract class BasePlannerSystem<TComponent>: IAiPlannerSystem, IEcsRunSystem
        where TComponent : struct
    {
        protected ActionType _actionId;
        
        public async UniTask Initialize(IEcsSystems ecsSystems, ActionType actionId)
        {
            _actionId = actionId;
            await OnInitialize(ecsSystems);
            ecsSystems.Add(this);
        }

        public abstract void Run(IEcsSystems systems);
        
        public void ApplyPlanningResult(IEcsSystems systems, int entity, AiPlannerData data)
        {
            var world = systems.GetWorld();
            var aiAgentPool = world.GetPool<AiAgentComponent>();
            
            ref var aiAgentComponent = ref aiAgentPool.Get(entity);
            aiAgentComponent.PlannerData[_actionId] = data;
        }
        
        public void RemoveComponent(IEcsSystems systems, int entity)
        {
            systems.TryRemoveComponent<TComponent>(entity);
        }

        protected virtual UniTask OnInitialize(IEcsSystems systems) => UniTask.CompletedTask;

    }
}
