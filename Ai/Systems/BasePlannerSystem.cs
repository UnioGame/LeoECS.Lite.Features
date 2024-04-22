namespace Game.Ecs.AI.Systems
{
    using System;
    using Components;
    using Abstract;
    using Cysharp.Threading.Tasks;
    using Service;
    using Leopotam.EcsLite;
    using Tools;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    public abstract class BasePlannerSystem<TComponent>: IAiPlannerSystem,IEcsRunSystem
        where TComponent : struct
    {
        protected int _id;

        public int Id => _id;
        
        public async UniTask Initialize(int id,IEcsSystems ecsSystems)
        {
            _id = id;
            await OnInitialize(id, ecsSystems);
            ecsSystems.Add(this);
        }

        public abstract void Run(IEcsSystems systems);

        public bool IsPlannerEnabledForEntity(EcsWorld world, int entity) => AiSystemsTools.IsPlannerEnabledForEntity(world, entity, _id);
        
        public void ApplyPlanningResult(IEcsSystems systems, int entity, AiPlannerData data)
        {
            var world = systems.GetWorld();
            var aiAgentPool = world.GetPool<AiAgentComponent>();
            
            ref var aiAgentComponent = ref aiAgentPool.Get(entity);
            
            var resultData = aiAgentComponent.PlannerData;
            resultData[_id] = data;
        }
        
        public void RemoveComponent(IEcsSystems systems, int entity)
        {
            systems.TryRemoveComponent<TComponent>(entity);
        }

        protected virtual UniTask OnInitialize(int id, IEcsSystems systems) => UniTask.CompletedTask;

    }
}
