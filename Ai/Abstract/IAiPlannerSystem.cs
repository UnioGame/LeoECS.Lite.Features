namespace Game.Ecs.AI.Abstract
{
    using Configurations;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Data;
    using Shared.Generated;

    public interface IAiPlannerSystem : IAiPlannerSwitched
    {
        public UniTask Initialize(IEcsSystems ecsSystems, ActionType actionId);
       
        void ApplyPlanningResult(IEcsSystems systems, int entity, AiPlannerData data);
    }

    public interface IAiPlannerSwitched
    {
        void RemoveComponent(IEcsSystems systems, int entity);
    }
    
}